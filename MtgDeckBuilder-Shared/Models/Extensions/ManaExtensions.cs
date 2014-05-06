using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Extensions
{
  public static class ManaExtensions
  {
    public static bool IsColor(this char letter)
    {
      return
           letter == ManaSymbols.Black
        || letter == ManaSymbols.Red
        || letter == ManaSymbols.Green
        || letter == ManaSymbols.White
        || letter == ManaSymbols.Blue;
    }

    public static bool IsColorless(this char letter)
    {
      return
        letter == ManaSymbols.SpecialColorless
        || (letter >= ManaSymbols.MinColorless && letter <= ManaSymbols.MaxColorless);
    }

    public static int GetColorlessCount(this char letter)
    {
      if (!letter.IsColorless())
        return 0;

      if (letter == ManaSymbols.SpecialColorless)
        return 0;

      return letter - ManaSymbols.MinColorless;
    }

    public static bool IsOpen(this char letter)
    {
      return letter == ManaSymbols.Open;
    }

    public static bool IsClose(this char letter)
    {
      return letter == ManaSymbols.Close;
    }

    public static bool IsSeparator(this char letter)
    {
      return letter == ManaSymbols.Separator;
    }
  }
}
