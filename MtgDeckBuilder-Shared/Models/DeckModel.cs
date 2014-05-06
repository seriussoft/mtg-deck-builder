using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.QueryHelpers;

namespace Models
{
  public class DeckModel
  {
    public List<CardModel> Cards { get; private set; }

    public DeckModel(IEnumerable<CardModel> cards = null)
    {
      this.Cards = new List<CardModel>();

      AddCards(cards);
    }

    public CardModel AddCard(CardModel card)
    {
      this.Cards.Add(card);
      return card;
    }

    public IEnumerable<CardModel> AddCards(IEnumerable<CardModel> cards)
    {
      if (cards != null && cards.Any())
        this.Cards.AddRange(cards);

      return cards;
    }

    public CardModel RemoveCard(CardModel card)
    {
      if (this.Cards.Contains(card))
        this.Cards.Remove(card);

      return card;
    }

    public CardModel RemoveCard(string cardName)
    {
      var cardToRemove = this.Cards.FirstOrDefault(card => card.Name == cardName);
      if (cardToRemove != null)
      {
        this.Cards.Remove(cardToRemove);
        return cardToRemove;
      }

      return null;
    }

    public IEnumerable<CardModel> RemoveCards(params string[] cardNames)
    {
      var cardsToRemove = this.Cards.FindAll(c => cardNames.Contains(c.Name));
      if (cardsToRemove.Any())
      {
        var cardsRemoved = cardsToRemove.ToList();
        this.Cards.RemoveAll(c => cardNames.Contains(c.Name));
        return cardsRemoved;
      }

      return new List<CardModel>();
    }
  }
}
