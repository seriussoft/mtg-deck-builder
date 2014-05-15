using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeriusSoft.MtgDeckBuilder.Models;
using SeriusSoft.MtgDeckBuilder.Models.QueryHelpers;

namespace SeriusSoft.MtgDeckBuilder.ViewModels.Test
{
  public class TestDeckViewModel : DeckViewModel
  {
    public TestDeckViewModel()
    {
      this.Model = new DeckModel();
      var cardsToCreate = CreateCardsForTestDeck();

      this.Model.AddCards(cardsToCreate);
    }

    private IEnumerable<CardModel> CreateCardsForTestDeck()
    {
      var cards = new List<CardModel>()
      {
        new CardModel() { Name = "Suntail Hawk", Type = CardTypes.BaseCardTypes.Creature, ManaCostSimple = "W", Quantity = 4 }, 
        //new CardModel() { Name = "Suntail Hawk", Type = CardTypes.BaseCardTypes.Creature, ManaCostSimple = "W" },
        //new CardModel() { Name = "Suntail Hawk", Type = CardTypes.BaseCardTypes.Creature, ManaCostSimple = "W" },
        //new CardModel() { Name = "Suntail Hawk", Type = CardTypes.BaseCardTypes.Creature, ManaCostSimple = "W" },

        new CardModel() { Name = "Sporemound", Type = CardTypes.BaseCardTypes.Creature, ManaCostSimple = "3GG" },

        new CardModel() { Name = "Pacifism", Type = CardTypes.BaseCardTypes.Enchantment, SubType = "Aura", ManaCostSimple = "1W", Quantity = 2 },
        //new CardModel() { Name = "Pacifism", Type = CardTypes.BaseCardTypes.Enchantment, SubType = "Aura", ManaCostSimple = "1W" },

        new CardModel() { Name = "Revoke Existence", Type = CardTypes.BaseCardTypes.Sorcery, ManaCostSimple = "1W", Quantity = 2 },
        //new CardModel() { Name = "Revoke Existence", Type = CardTypes.BaseCardTypes.Sorcery, ManaCostSimple = "1W" },

        new CardModel() { Name = "Sundering Growth", Type = CardTypes.BaseCardTypes.Instant, ManaCostSimple = "{G/W}{G/W}" },
        new CardModel() { Name = "Heroes' Reunion", Type = CardTypes.BaseCardTypes.Instant, ManaCostSimple = "GW" },

        new CardModel() { Name = "Fireshrieker", Type = CardTypes.BaseCardTypes.Artifact, ManaCostSimple = "3" },

        new CardModel() { Name = "Plains", Type = CardTypes.BaseCardTypes.Creature, ManaCostSimple = "0", Quantity = 4 },
        //new CardModel() { Name = "Plains", Type = CardTypes.BaseCardTypes.Creature, ManaCostSimple = "0" },
        //new CardModel() { Name = "Plains", Type = CardTypes.BaseCardTypes.Creature, ManaCostSimple = "0" },
        //new CardModel() { Name = "Plains", Type = CardTypes.BaseCardTypes.Creature, ManaCostSimple = "0" },

        new CardModel() { Name = "Forest", Type = CardTypes.BaseCardTypes.Creature, ManaCostSimple = "0", Quantity = 4 },
        //new CardModel() { Name = "Forest", Type = CardTypes.BaseCardTypes.Creature, ManaCostSimple = "0" },
        //new CardModel() { Name = "Forest", Type = CardTypes.BaseCardTypes.Creature, ManaCostSimple = "0" },
        //new CardModel() { Name = "Forest", Type = CardTypes.BaseCardTypes.Creature, ManaCostSimple = "0" },
      };

      return cards;
    }

  }
}
