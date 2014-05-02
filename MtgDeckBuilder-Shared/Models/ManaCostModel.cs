using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Models
{
  [Flags]
  public enum ManaColors
  {
    [Definition(ManaSymbols.Black)] Black,
    [Definition(ManaSymbols.Red)] Red,
    [Definition(ManaSymbols.Green)] Green,
    [Definition(ManaSymbols.White)] White,
    [Definition(ManaSymbols.Blue)] Blue,
    [Definition(ManaSymbols.Colorless)] Colorless
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

  public static class ManaColorTranslator
  {
    public static Dictionary<ManaColors, char> ManaColorsToDefinitions { get; private set; }
    public static Dictionary<char, ManaColors> DefinitionsToManaColors { get; private set; }

    public static ManaColors GetManaColor(this char definition)
    {
      if(DefinitionsToManaColors.ContainsKey(definition))
        return DefinitionsToManaColors[definition];

      if(definition.IsColorless())
        return ManaColors.Colorless;

      throw new ArgumentOutOfRangeException("The supplied definition has no matching mana color!");
    }

    public static char GetDefinition(this ManaColors manaColor)
    {
      if(ManaColorsToDefinitions.ContainsKey(manaColor))
        return ManaColorsToDefinitions[manaColor];

      throw new ArgumentOutOfRangeException("The supplied manacolors value does not match any known mana type");
    }

    static ManaColorTranslator()
    {
      ManaColorsToDefinitions = CreateManaColorsToDefinitions();
      DefinitionsToManaColors = CreateDefinitionsToManaColors();
    }

    /// <summary>
    /// Need to make this use reflection to populate in the future
    /// </summary>
    /// <returns></returns>
    static Dictionary<ManaColors, char> CreateManaColorsToDefinitions()
    {
      return new Dictionary<ManaColors, char>()
      {
        {ManaColors.Black, ManaSymbols.Black},
        {ManaColors.Red, ManaSymbols.Red},
        {ManaColors.Green, ManaSymbols.Green},
        {ManaColors.White, ManaSymbols.White},
        {ManaColors.Blue, ManaSymbols.Blue},
        {ManaColors.Colorless, ManaSymbols.Colorless},
      };
    }

    /// <summary>
    /// Need to make this use reflection to populate in the future
    /// </summary>
    /// <returns></returns>
    static Dictionary<char, ManaColors> CreateDefinitionsToManaColors()
    {
      return new Dictionary<char, ManaColors>()
      {
        {ManaSymbols.Black, ManaColors.Black},
        {ManaSymbols.Red, ManaColors.Red},
        {ManaSymbols.Green, ManaColors.Green},
        {ManaSymbols.White, ManaColors.White},
        {ManaSymbols.Blue, ManaColors.Blue},
        {ManaSymbols.Colorless, ManaColors.Colorless},
      };
    }

  }

  public class ManaCostModel
  {
    public Dictionary<ManaColors, int> Costs { get; private set; }

    public ManaCostModel(string manaCostSimple = null)
    {
      const string separator = "|";
      var manaOperations = ParseManaOperations(manaCostSimple);
      var orderedManaOperations = manaOperations.OrderBy(mo => mo).ToList();

      foreach (var manaOperation in orderedManaOperations)
      {
        if(manaOperation.Contains(separator))
        {
          var manaOperationPartLeft = manaOperation[0];
          var manaOperationPartRight = manaOperation[1];

          HandleManaColors(manaOperationPartLeft, manaOperationPartRight);
        }
        else
        {
          var manaOperationPart = manaOperation[0];
          HandleManaColor(manaOperationPart);
        }
      }
    }

    private void HandleManaColors(char manaOperationPartLeft, char manaOperationPartRight)
    {
      var manaColorLeft = manaOperationPartLeft.GetManaColor();
      var manaColorRight = manaOperationPartRight.GetManaColor();

      var comboColor = manaColorLeft | manaColorRight;

      if(this.Costs.ContainsKey(comboColor))
      {
        AddCostToTotal(comboColor, ManaSymbols.Close);
      }
      else
      {
        AppendCost(comboColor, ManaSymbols.Close);
      }
    }

    private void HandleManaColor(char manaOperationPart)
    {
      var manaColor = manaOperationPart.GetManaColor();

      if(this.Costs.ContainsKey(manaColor))
      {
        AddCostToTotal(manaColor, manaOperationPart);
      }
      else
      {
        AppendCost(manaColor, manaOperationPart);
      }
    }

    private void AddCostToTotal(ManaColors manaColor, char manaOperationPart)
    {
 	    switch(manaColor)
      {
        case ManaColors.Colorless:
          var colorlessCount = manaOperationPart.GetColorlessCount();
          this.Costs[manaColor] = this.Costs[manaColor] + colorlessCount;
          break;

        default:
          this.Costs[manaColor] = this.Costs[manaColor] + 1;
          break;
      }
    }

    private void AppendCost(ManaColors manaColor, char manaOperationPart)
    {
 	    switch(manaColor)
      {
        case ManaColors.Colorless:
          var colorlessCount = manaOperationPart.GetColorlessCount();
          this.Costs.Add(manaColor, colorlessCount);
          break;

        default:
          this.Costs.Add(manaColor, 1);
          break;
      }
    }

    private List<string> ParseManaOperations(string manaCostSimple)
    {
      var manaOperations = new List<string>();
      const char terminationLetter = '\0';

      //var isInGroup = false;  //indicates that we are inside a group ( {U/R} for example )

      for(int index=0; index < manaCostSimple.Length; index++)
      {
        var character = manaCostSimple[index];

        #region Colorless logic
        if (character.IsColorless())
        {
          var colorlessCount = character.GetColorlessCount();
          var nextDigit = manaCostSimple[index + 1];
          if (nextDigit.IsColorless())
          {

            var colorlessCount2 = nextDigit.GetColorlessCount();
            if (colorlessCount2 > 0)
              colorlessCount = colorlessCount * 10 + colorlessCount2;

            ++index;  //increment so that we don't count for the next integer in the for loop
          }

          manaOperations.Add(colorlessCount.ToString());
          continue;
        } 
        #endregion  Colorless logic

        #region Color logic

		    if (character.IsColor())
        {
          manaOperations.Add(character.ToString());
          continue;
        }
 
	      #endregion  Color logic

        #region Group Open logic

		if (character.IsOpen())
        {
          ++index; //skip to relevant items

          var lastColorlessCount = 0;
          var lastColor = terminationLetter;
          var lastLetterWasSeparator = false;

          while(!character.IsClose())
          {
            character = manaCostSimple[index];
            if(character.IsColorless())
            {
              lastColorlessCount = character.GetColorlessCount();

              //do this on each if so that we can skip extra checks
              character = manaCostSimple[++index];
              continue;
            }

            if(character.IsColor())
            {
              var currentColor = character;
              //lastColor = character;

              if(lastLetterWasSeparator)
              {
                manaOperations.Add(CreateManaOperation(lastColorlessCount, lastColor, currentColor));
                lastLetterWasSeparator = false;
                lastColor = terminationLetter;
                lastColorlessCount = 0;
              }

              //do this on each if so that we can skip extra checks
              character = manaCostSimple[++index];
              continue;
            }

            if(character.IsSeparator())
            {
              lastLetterWasSeparator = true;
              character = manaCostSimple[++index];
              continue;
            }
          } //end while not close

          continue;
        } //end if open 

	#endregion  Group Open logic

        
      } //end for loop

      return manaOperations;
    }

    private string CreateManaOperation(int lastColorlessCount,char lastColor,char currentColor)
    {
      const string formatter = "{0}|{1}";

 	    if(lastColorlessCount > 0)
        return String.Format(formatter, lastColorlessCount, currentColor);
      else
        return String.Format(formatter, lastColor, currentColor);
    }

    //private int GetDigitAmount(char character)
    //{
      
    //}

    //public void SetManaCost(string manaCostSimple)
    //{

    //}
  }
}
