using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MtgDb.Info;
using DB = MtgDb.Info.Driver.Db;

namespace Models
{
  public class CardsDB
  {
    const string DATABASE_URL = "https://api.mtgdb.info";
    protected DB Database { get; set; }

    public CardsDB()
    {
      this.Database = new DB(CardsDB.DATABASE_URL);
    }

    public CardModel GetCardByName(string name)
    {
      
    }

    public CardModel GetCardByID(int id)
    {

    }

    public CardsDB SearchCards(string filterKey, string filterValue)
    {

    }
  }
}
