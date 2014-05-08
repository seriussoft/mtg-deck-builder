using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MtgDb.Info;
using Newtonsoft.Json;

namespace SeriusSoft.MtgDeckBuilder.Models
{
  /// <summary>
  /// Note that async methods use async http client access, but the json implementation is still synchronous and therefore blocking. This should be fine for client based applications, but is not guaranteed to scale performably in a large environment with heavy use.
  /// </summary>
  public static class MtgDbExtensions
  {
    public enum SingletonRules
    {
      First,
      Latest
    }

    public enum SearchTypes
    {
      StartsWith,
      Contains,
      EndsWith,
      Equals
    }

    public static async Task<Card> GetCardByNameAsync(this MtgDb.Info.Driver.Db database, string name, SingletonRules singletonRule = SingletonRules.First)
    {
      using (var client = new HttpClient())
      {
        var url = String.Format("{0}/cards/{1}", database.ApiUrl, name);
        var json = await client.GetStringAsync(url);
        var cards = JsonConvert.DeserializeObject<List<Card>>(json);

        return cards.GetCardFromCollection(singletonRule);
      }
    }

    public static async Task<CardSet> GetCardSetByIDAsync(this MtgDb.Info.Driver.Db database, string id)
    {
      var sets = await database.GetCardSetsAsync(id);
      return sets.FirstOrDefault();
    }

    public static async Task<CardSet> GetCardSetByNameAsync(this MtgDb.Info.Driver.Db database, string name, SearchTypes searchTypes = SearchTypes.Contains)
    {
      var cardSets = await database.GetCardSetsAsync();
      return cardSets.FirstOrDefault(cs => SetMatches(cs, name, searchTypes));
    }

    private static bool SetMatches(CardSet set, string name, SearchTypes searchTypes, bool caseSensitive = false)
    {
      var setName = caseSensitive
        ? set.Name
        : set.Name.ToLower();

      var promptName = caseSensitive
        ? name
        : name.ToLower();

      switch (searchTypes)
      {
        case SearchTypes.StartsWith:
          return setName.StartsWith(promptName);

        case SearchTypes.EndsWith:
          return setName.EndsWith(promptName);

        case SearchTypes.Equals:
          return setName == promptName;

        case SearchTypes.Contains:
        default:
          return setName.Contains(promptName);
      }
    }

    public static async Task<List<CardSet>> GetCardSetsAsync(this MtgDb.Info.Driver.Db database, string id = null)
    {
      const string formatter = "{0}/sets/";
      const string idFormatter = formatter + "{1}";

      var searchByID = String.IsNullOrEmpty(id);

      using (var client = new HttpClient())
      {
        var url = searchByID
          ? String.Format(idFormatter, database.ApiUrl, id)
          : String.Format(formatter, database.ApiUrl);
            
        var json = await client.GetStringAsync(url);

        return JsonConvert.DeserializeObject<List<CardSet>>(json);
      }
    }

    public static Card GetCardFromCollection(this IEnumerable<Card> cards, SingletonRules singletonRule)
    {
      switch(singletonRule)
      {
        case SingletonRules.Latest:
          return cards.LastOrDefault();

        case SingletonRules.First:
        default:
          return cards.FirstOrDefault();
      }
    }
  }
}
