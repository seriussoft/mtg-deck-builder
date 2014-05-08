using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriusSoft.MtgDeckBuilder.Models
{
  [Flags]
  public enum ManaColors
  {
    [Definition(ManaSymbols.Black)]
    Black = 0x1,
    
    [Definition(ManaSymbols.Red)]
    Red = 0x1 << 1,
    
    [Definition(ManaSymbols.Green)]
    Green = 0x1 << 2,
    
    [Definition(ManaSymbols.White)]
    White = 0x1 << 3,
    
    [Definition(ManaSymbols.Blue)]
    Blue = 0x1 << 4,
    
    [Definition(ManaSymbols.Colorless)]
    Colorless = 0x1 << 5
  }

  public static class ManaSymbols
  {
    public const char Black = 'B';
    public const char Red = 'R';
    public const char Green = 'G';
    public const char White = 'W';
    public const char Blue = 'U';
    public const char Colorless = '?';

    public const char MinColorless = '0';
    public const char MaxColorless = '9';
    public const char SpecialColorless = 'X';

    public const char Open = '{';
    public const char Close = '}';
    public const char Separator = '/';
  }


}
