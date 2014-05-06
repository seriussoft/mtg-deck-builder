using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Models.QueryHelpers;

namespace Models.Extensions
{
  public static class CardListExtensions
  {
    public static List<CardModel> Lands(this IEnumerable<CardModel> cards)
    {
      return cards.Where(c => c.Type.MatchesType(CardTypes.BaseCardTypes.Land)).ToList();
    }

    public static List<CardModel> Enchantments(this IEnumerable<CardModel> cards)
    {
      return cards.Where(c => c.Type.MatchesType(CardTypes.BaseCardTypes.Enchantment)).ToList();
    }

    public static List<CardModel> Artifacts(this IEnumerable<CardModel> cards)
    {
      return cards.Where(c => c.Type.MatchesType(CardTypes.BaseCardTypes.Artifact)).ToList();
    }

    public static List<CardModel> Creatures(this IEnumerable<CardModel> cards)
    {
      return cards.Where(c => c.Type.MatchesType(CardTypes.BaseCardTypes.Creature)).ToList();
    }

    public static List<CardModel> Summons(this IEnumerable<CardModel> cards)
    {
      return cards.Where(c => c.Type.MatchesType(CardTypes.BaseCardTypes.Summon)).ToList();
    }

    public static List<CardModel> Planeswalkers(this IEnumerable<CardModel> cards)
    {
      return cards.Where(c => c.Type.MatchesType(CardTypes.BaseCardTypes.Planeswalker)).ToList();
    }

    public static List<CardModel> Instants(this IEnumerable<CardModel> cards)
    {
      return cards.Where(c => c.Type.MatchesType(CardTypes.BaseCardTypes.Instant)).ToList();
    }

    public static List<CardModel> Interrupts(this IEnumerable<CardModel> cards)
    {
      return cards.Where(c => c.Type.MatchesType(CardTypes.BaseCardTypes.Interrupt)).ToList();
    }   

    public static List<CardModel> Sorceries(this IEnumerable<CardModel> cards)
    {
      return cards.Where(c => c.Type.MatchesType(CardTypes.BaseCardTypes.Sorcery)).ToList();
    }
  }
}
