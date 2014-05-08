using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MtgDb.Info;
using DB = MtgDb.Info.Driver.Db;

namespace SeriusSoft.MtgDeckBuilder.Models
{
  public class CardsDB
  {
    const string DATABASE_URL = "https://api.mtgdb.info";
    protected DB Database { get; set; }

    public CardsDB()
    {
      this.Database = new DB(CardsDB.DATABASE_URL);
    }

    public async Task<CardModel> GetCardByName(string name)
    {
      var card = await this.Database.GetCardByNameAsync(name);
      return new CardModel(card);
    }

    public CardModel GetCardByID(int id)
    {
      var card = this.Database.GetCard(id);
      return new CardModel(card);
    }

    /// <summary>
    /// is a very slow running process (depending on internet connection, this may take a couple minutes to several minutes) [30mb + of data]
    /// </summary>
    /// <returns></returns>
    public async Task<List<CardModel>> GetAllCards()
    {
      var result = await Task.Run<List<CardModel>>
      (
        () =>
        {
          var cards = this.Database.GetCards();
          var cardModels = cards.Select(c => new CardModel(c));

          return cardModels.ToList();
        }
      );

      return result;
    }

    //public CardModel GetCardByName(string name)
    //{
      
    //}

    //public CardModel GetCardByID(int id)
    //{

    //}

    //public CardsDB SearchCards(string filterKey, string filterValue)
    //{

    //}
  }
}
