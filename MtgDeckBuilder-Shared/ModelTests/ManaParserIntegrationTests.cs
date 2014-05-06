using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Utils;

namespace ModelTests
{
  [TestClass]
  public class ManaParserIntegrationTests
  {
    public static Assertion Assert = new Assertion();

    [TestMethod]
    public void ManaParserTests_Success_GrabsLeoninSquire_ReturnsCorrectManaCostOf1W()
    {
      var db = new CardsDB();
      var testCardName = "Leonin Squire";
      var testCardNameUndercase = testCardName.ToLower();
      var expectedManaCost = "1W";
      var expectedComplexManaCost = new ManaCostModel(expectedManaCost);

      //var card = await db.GetCardByName(testCardNameUndercase);
      var result = db.GetCardByName(testCardNameUndercase);
      var card = result.Result;

      Assert.IsTrue(card.Type.Contains("Creature"));
      Assert.AreEqual(card.Name, testCardName);
      Assert.AreEqual(card.ManaCostSimple, expectedManaCost);
      Assert.IsNotNull(card.ManaCost);
      Assert.AreEqualCollections(card.ManaCost.Costs, expectedComplexManaCost.Costs);
    }
  }
}
