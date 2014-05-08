using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriusSoft.MtgDeckBuilder.Models.QueryHelpers
{
    public enum QueryMatchMinimums
    {
      /// <summary>
      /// If you are looking for an enchantment, then the match will be true if the type contains "Enchant", "Enchantment", "Enchant Creature", "Enchantment Creature", etc.
      /// </summary>
      AcceptsPartialMatch = 0,

      /// <summary>
      /// If you are looking for an enchantment, then the match will only be true if the type is "Enchantment". So, if it is "Enchant Creature" or "Enchantment creature", then it will return false.
      /// </summary>
      MustBeFullMatch = 1,
    }
}
