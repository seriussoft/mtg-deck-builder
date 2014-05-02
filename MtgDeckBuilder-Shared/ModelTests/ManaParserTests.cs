using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Models;
using MtgDb.Info;
using Newtonsoft.Json;
using Utils;

namespace ModelTests
{
  [TestClass]
  public class ManaCostParserTests
  {
    static readonly Assertion Assert = new Assertion();
    /// <summary>
    /// {2/W}{2/U}{2/B}{2/R}{2/G}
    /// </summary>
    public static string ComplicatedJsonResult
    {
      get
      {
        return "[{\"id\":159408,\"relatedCardId\":0,\"setNumber\":260,\"name\":\"Reaper King\",\"searchName\"" +
    ":\"reaperking\",\"description\":\"({Two or White} can be paid with any two mana or wi" +
    "th {White}. This card\'s converted mana cost is 10.)\\nOther Scarecrow creatures y" +
    "ou control get +1/+1.\\nWhenever another Scarecrow enters the battlefield under y" +
    "our control, destroy target permanent.\",\"flavor\":\" It\'s harvest time.\",\"colors\":" +
    "[\"blue\",\"red\",\"green\",\"black\",\"white\"],\"manaCost\":\"{2/W}{2/U}{2/B}{2/R}{2/G}\",\"c" +
    "onvertedManaCost\":10,\"cardSetName\":\"Shadowmoor\",\"type\":\"Legendary Artifact Creat" +
    "ure\",\"subType\":\"Scarecrow\",\"power\":6,\"toughness\":6,\"loyalty\":0,\"rarity\":\"Rare\",\"" +
    "artist\":\"Jim Murray\",\"cardSetId\":\"SHM\",\"token\":false,\"promo\":false,\"rulings\":[{\"" +
    "releasedAt\":\"2008-05-01\",\"rule\":\"If an effect reduces the cost to cast a spell b" +
    "y an amount of generic mana, it applies to a monocolored hybrid spell only if yo" +
    "u\'ve chosen a method of paying for it that includes generic mana.\"},{\"releasedAt" +
    "\":\"2008-05-01\",\"rule\":\"A card with a monocolored hybrid mana symbol in its mana " +
    "cost is each of the colors that appears in its mana cost, regardless of what man" +
    "a was spent to cast it. Thus, Reaper King is all colors even if you spend ten co" +
    "lorless mana to cast it.\"},{\"releasedAt\":\"2008-05-01\",\"rule\":\"A card with monoco" +
    "lored hybrid mana symbols in its mana cost has a converted mana cost equal to th" +
    "e highest possible cost it could be cast for. Its converted mana cost never chan" +
    "ges. Thus, Reaper King has a converted mana cost of 10, even if you spend {W}{U}" +
    "{B}{R}{G} to cast it.\"},{\"releasedAt\":\"2008-05-01\",\"rule\":\"If a cost includes mo" +
    "re than one monocolored hybrid mana symbol, you can choose a different way to pa" +
    "y for each symbol. For example, you can pay for Reaper King by spending one mana" +
    " of each color, {2} and one mana each of four different colors, {4} and one mana" +
    " each of three different colors, {6} and one mana each of two different colors, " +
    "{8} and one mana of any color, or {10}.\"}],\"formats\":[{\"name\":\"Modern\",\"legality" +
    "\":\"Legal\"},{\"name\":\"Lorwyn-Shadowmoor Block\",\"legality\":\"Legal\"},{\"name\":\"Legacy" +
    "\",\"legality\":\"Legal\"},{\"name\":\"Vintage\",\"legality\":\"Legal\"},{\"name\":\"Freeform\",\"" +
    "legality\":\"Legal\"},{\"name\":\"Prismatic\",\"legality\":\"Legal\"},{\"name\":\"Tribal Wars " +
    "Legacy\",\"legality\":\"Legal\"},{\"name\":\"Classic\",\"legality\":\"Legal\"},{\"name\":\"Singl" +
    "eton 100\",\"legality\":\"Legal\"},{\"name\":\"Commander\",\"legality\":\"Legal\"}],\"released" +
    "At\":\"2008-05-02\"}]";
      }
    }

    /// <summary>
    /// 3{B/G}{B/G}
    /// </summary>
    public static string ComplicatedJsonResult2
    {
      get
      {
        return @"[{""id"":290535,""relatedCardId"":0,""setNumber"":216,""name"":""Golgari Longlegs"",""searchName"":""golgarilonglegs"",""description"":"""",""flavor"":"" Despite its enormous stature, it can fold itself into a tunnel with startling quickness, vanishing back into the undercity."",""colors"":[""green"",""black""],""manaCost"":""3{B/G}{B/G}"",""convertedManaCost"":5,""cardSetName"":""Return to Ravnica"",""type"":""Creature"",""subType"":""Insect"",""power"":5,""toughness"":4,""loyalty"":0,""rarity"":""Common"",""artist"":""Volkan Baga"",""cardSetId"":""RTR"",""token"":false,""promo"":false,""rulings"":[],""formats"":[{""name"":""Standard"",""legality"":""Legal""},{""name"":""Modern"",""legality"":""Legal""},{""name"":""Extended"",""legality"":""Legal""},{""name"":""Return to Ravnica Block"",""legality"":""Legal""},{""name"":""Legacy"",""legality"":""Legal""},{""name"":""Vintage"",""legality"":""Legal""},{""name"":""Freeform"",""legality"":""Legal""},{""name"":""Prismatic"",""legality"":""Legal""},{""name"":""Tribal Wars Legacy"",""legality"":""Legal""},{""name"":""Tribal Wars Standard"",""legality"":""Legal""},{""name"":""Classic"",""legality"":""Legal""},{""name"":""Singleton 100"",""legality"":""Legal""},{""name"":""Commander"",""legality"":""Legal""}],""releasedAt"":""2012-10-05""}]";
      }
    }

    /// <summary>
    /// 1W
    /// </summary>
    public static string SimpleJsonResult
    {
      get
      {
        return @"[{""id"":72722,""relatedCardId"":0,""setNumber"":9,""name"":""Leonin Squire"",""searchName"":""leoninsquire"",""description"":""When Leonin Squire enters the battlefield, return target artifact card with converted mana cost 1 or less from your graveyard to your hand."",""flavor"":"" \""I may be kha, but without my soldiers, my people, I am nothing.\"" —Raksha Golden Cub"",""colors"":[""white""],""manaCost"":""1W"",""convertedManaCost"":2,""cardSetName"":""Fifth Dawn"",""type"":""Creature"",""subType"":""Cat Soldier"",""power"":2,""toughness"":2,""loyalty"":0,""rarity"":""Common"",""artist"":""Pete Venters"",""cardSetId"":""5DN"",""token"":false,""promo"":false,""rulings"":[],""formats"":[{""name"":""Modern"",""legality"":""Legal""},{""name"":""Mirrodin Block"",""legality"":""Legal""},{""name"":""Legacy"",""legality"":""Legal""},{""name"":""Vintage"",""legality"":""Legal""},{""name"":""Freeform"",""legality"":""Legal""},{""name"":""Prismatic"",""legality"":""Legal""},{""name"":""Tribal Wars Legacy"",""legality"":""Legal""},{""name"":""Classic"",""legality"":""Legal""},{""name"":""Singleton 100"",""legality"":""Legal""},{""name"":""Commander"",""legality"":""Legal""}],""releasedAt"":""2004-06-04""}]";
      }
    }

    public TCard GetCardValue<TCard>(string json)
    {
      return JsonConvert.DeserializeObject<TCard>(json);
    }

    [TestMethod]
    public void ManaCostSucceeds_SimpleTest()
    {
      var results = GetCardValue<List<Card>>(SimpleJsonResult);
      var result = results.FirstOrDefault();
      var model = new CardModel() { ID = result.Id, Name = result.Name, ManaCostSimple = result.ManaCost };

      var manaCost = model.ManaCost;

      Assert.IsNotNull(manaCost);
      Assert.IsNotNull(manaCost.Costs);
      Assert.CountIs(manaCost.Costs, 2);
      //CollectionAssertions.
    }
  }
}
