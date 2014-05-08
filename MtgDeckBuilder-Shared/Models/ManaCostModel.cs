using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

using SeriusSoft.MtgDeckBuilder.Models.Extensions;
using SeriusSoft.MtgDeckBuilder.Models.Translators;

namespace SeriusSoft.MtgDeckBuilder.Models
{
  public class ManaCostModel
  {
    public Dictionary<ManaColors, int> Costs { get; private set; }

    public ManaCostModel(string manaCostSimple = null)
    {
      this.Costs = new Dictionary<ManaColors, int>();
			SetManaCostFromString(manaCostSimple);
    }

    public bool SetManaCostFromString(string manaCostSimple)
    {
      return ManaCostTranslator.TrySetManaCostFromString(this, manaCostSimple);
    }
		
  //  public void SetManaCostFromString(string manaCostSimple)
  //  {
  //    const string separator = "|";

  //    if (String.IsNullOrEmpty(manaCostSimple))
  //      return;

  //    var manaOperations = ParseManaOperations(manaCostSimple);
  //    //var orderedManaOperations = manaOperations.OrderBy(mo => mo).ToList();

  //    //foreach (var manaOperation in orderedManaOperations)
  //    foreach (var manaOperation in manaOperations)
  //    {
  //      if (manaOperation.Contains(separator))
  //      {
  //        var manaOperationPartLeft = manaOperation[0];
  //        var manaOperationPartRight = manaOperation[2];

  //        HandleManaColors(manaOperationPartLeft, manaOperationPartRight);
  //      }
  //      else
  //      {
  //        var manaOperationPart = manaOperation[0];
  //        HandleManaColor(manaOperationPart);
  //      }
  //    }
  //  }

  //  private void HandleManaColors(char manaOperationPartLeft, char manaOperationPartRight)
  //  {
  //    var manaColorLeft = manaOperationPartLeft.GetManaColor();
  //    var manaColorRight = manaOperationPartRight.GetManaColor();

  //    var comboColor = manaColorLeft | manaColorRight;

  //    if(this.Costs.ContainsKey(comboColor))
  //    {
  //      AddCostToTotal(comboColor, ManaSymbols.Close);
  //    }
  //    else
  //    {
  //      AppendCost(comboColor, ManaSymbols.Close);
  //    }
  //  }

  //  private void HandleManaColor(char manaOperationPart)
  //  {
  //    var manaColor = manaOperationPart.GetManaColor();

  //    if(this.Costs.ContainsKey(manaColor))
  //    {
  //      AddCostToTotal(manaColor, manaOperationPart);
  //    }
  //    else
  //    {
  //      AppendCost(manaColor, manaOperationPart);
  //    }
  //  }

  //  private void AddCostToTotal(ManaColors manaColor, char manaOperationPart)
  //  {
  //    switch(manaColor)
  //    {
  //      case ManaColors.Colorless:
  //        var colorlessCount = manaOperationPart.GetColorlessCount();
  //        this.Costs[manaColor] = this.Costs[manaColor] + colorlessCount;
  //        break;

  //      default:
  //        this.Costs[manaColor] = this.Costs[manaColor] + 1;
  //        break;
  //    }
  //  }

  //  private void AppendCost(ManaColors manaColor, char manaOperationPart)
  //  {
  //    switch(manaColor)
  //    {
  //      case ManaColors.Colorless:
  //        var colorlessCount = manaOperationPart.GetColorlessCount();
  //        this.Costs.Add(manaColor, colorlessCount);
  //        break;

  //      default:
  //        this.Costs.Add(manaColor, 1);
  //        break;
  //    }
  //  }

  //  private List<string> ParseManaOperations(string manaCostSimple)
  //  {
  //    const char terminationLetter = '\0';

  //    var manaOperations = new List<string>();
  //    if(String.IsNullOrEmpty(manaCostSimple))
  //    {
  //      return manaOperations;
  //    }

  //    //var isInGroup = false;  //indicates that we are inside a group ( {U/R} for example )

  //    for(int index=0; index < manaCostSimple.Length; index++)
  //    {
  //      var character = manaCostSimple[index];

  //      #region Colorless logic
  //      if (character.IsColorless())
  //      {
  //        var colorlessCount = character.GetColorlessCount();
  //        var nextDigit = manaCostSimple[index + 1];
  //        if (nextDigit.IsColorless())
  //        {

  //          var colorlessCount2 = nextDigit.GetColorlessCount();
  //          if (colorlessCount2 > 0)
  //            colorlessCount = colorlessCount * 10 + colorlessCount2;

  //          ++index;  //increment so that we don't count for the next integer in the for loop
  //        }

  //        manaOperations.Add(colorlessCount.ToString());
  //        continue;
  //      } 
  //      #endregion  Colorless logic

  //      #region Color logic

  //      if (character.IsColor())
  //      {
  //        manaOperations.Add(character.ToString());
  //        continue;
  //      }
 
  //      #endregion  Color logic

  //      #region Group Open logic

  //      if (character.IsOpen())
  //      {
  //        ++index; //skip to relevant items

  //        var lastColorlessCount = 0;
  //        var lastColor = terminationLetter;
  //        var lastLetterWasSeparator = false;

  //        while(!character.IsClose())
  //        {
  //          character = manaCostSimple[index];
  //          if(character.IsColorless())
  //          {
  //            lastColorlessCount = character.GetColorlessCount();

  //            //do this on each if so that we can skip extra checks
  //            character = manaCostSimple[++index];
  //            continue;
  //          }

  //          if(character.IsColor())
  //          {
  //            var currentColor = character;
  //            //lastColor = character;

  //            if(lastLetterWasSeparator)
  //            {
  //              manaOperations.Add(CreateManaOperation(lastColorlessCount, lastColor, currentColor));
  //              lastLetterWasSeparator = false;
  //              lastColor = terminationLetter;
  //              lastColorlessCount = 0;
  //            }
  //            else
  //            {
  //              lastColor = currentColor;
  //            }

  //            //do this on each if so that we can skip extra checks
  //            character = manaCostSimple[++index];
  //            continue;
  //          }

  //          if(character.IsSeparator())
  //          {
  //            lastLetterWasSeparator = true;
  //            character = manaCostSimple[++index];
  //            continue;
  //          }
  //        } //end while not close

  //        continue;
  //      } //end if open 

  //#endregion  Group Open logic

        
  //    } //end for loop

  //    return manaOperations;
  //  }

  //  private string CreateManaOperation(int lastColorlessCount,char lastColor,char currentColor)
  //  {
  //    const string formatter = "{0}|{1}";

  //    if(lastColorlessCount > 0)
  //      return String.Format(formatter, lastColorlessCount, currentColor);
  //    else
  //      return String.Format(formatter, lastColor, currentColor);
  //  }

    //private int GetDigitAmount(char character)
    //{
      
    //}

    //public void SetManaCost(string manaCostSimple)
    //{

    //}
  }
}
