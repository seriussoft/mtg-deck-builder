using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.QueryHelpers
{
  public static class CardTypes
  {
    public static class BaseCardTypes
    {
      public const string Creature = "Creature";
      //public const string Aura = "Aura";
      //public const string Enchantment = "Enchantment";
      public const string Enchantment = "Enchant";
      public const string Land = "Land";
      public const string Artifact = "Artifact";
      public const string Instant = "Instant";
      public const string Interrupt = "Interrupt";
      public const string Sorcery = "Sorcery";
      public const string Summon = "Summon";
      public const string Planeswalker = "Planeswalker";
    }

    public static bool MatchesType(this string type, string searchType, QueryMatchMinimums queryMatchMinimum = QueryMatchMinimums.AcceptsPartialMatch)
    {
      switch (queryMatchMinimum)
      {
        case QueryMatchMinimums.MustBeFullMatch:
          return type == searchType;

        case QueryMatchMinimums.AcceptsPartialMatch:
        default:
          return type.Contains(searchType);
      }
    }

    public static List<string> AllCardTypes
    {
      get
      {
        return new List<string>()
          {
            "Artifact",
            "Artifact Creature",
            "Enchantment",
            "Creature",
            "Sorcery",
            "Instant",
            "Basic Land",
            "Land",
            "World Enchantment",
            "Legendary Creature",
            "Legendary Land",
            "Basic Snow Land",
            "Legendary Artifact",
            "Summon",
            "Interrupt",
            "Enchant Creature",
            "Enchant Player",
            "Scariest Creature You'll Ever See",
            "Legendary Artifact Creature",
            "Artifact Land",
            "Eaturecray",
            "Legendary Enchantment",
            "Snow Artifact",
            "Snow Land",
            "Snow Artifact Creature",
            "Snow Creature",
            "Snow Enchantment",
            "Legendary Snow Land",
            "Tribal Enchantment",
            "Enchantment Creature",
            "Land Creature",
            "Tribal Instant",
            "Tribal Sorcery",
            "Planeswalker",
            "Tribal Artifact",
            "Legendary Enchantment Creature",
            "Legendary Enchantment Artifact"
          };
      }
    }
  }
}
