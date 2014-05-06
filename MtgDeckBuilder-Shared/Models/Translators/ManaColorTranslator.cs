using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Models.Extensions;

namespace Models.Translators
{
  public static class ManaColorTranslator
  {
    public static Dictionary<ManaColors, char> ManaColorsToDefinitions { get; private set; }
    public static Dictionary<char, ManaColors> DefinitionsToManaColors { get; private set; }

    public static ManaColors GetManaColor(this char definition)
    {
      if (DefinitionsToManaColors.ContainsKey(definition))
        return DefinitionsToManaColors[definition];

      if (definition.IsColorless())
        return ManaColors.Colorless;

      throw new ArgumentOutOfRangeException(String.Format("The supplied definition, '{0}', has no matching mana color!", definition));
    }

    public static char GetDefinition(this ManaColors manaColor)
    {
      if (ManaColorsToDefinitions.ContainsKey(manaColor))
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
}
