using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MtgDb.Info;

namespace Models
{
    public class CardModel
    {
      protected Card BaseCard { get; set; }

      public string Name { get { return this.BaseCard.Name; } set { this.BaseCard.Name = value; } }
      public int ID { get { return this.BaseCard.Id; } set { this.BaseCard.Id = value; } }

      public string CardSetID { get { return this.BaseCard.CardSetId; } set { this.BaseCard.CardSetId = value; } }
      public string CardSetName { get { return this.BaseCard.CardSetName; } set { this.BaseCard.CardSetName = value; } }

      public int ConvertedManaCost { get { return this.BaseCard.ConvertedManaCost; } set { this.BaseCard.ConvertedManaCost = value; } }
      public string ManaCostSimple { get { return this.BaseCard.ManaCost; } set { this.BaseCard.ManaCost = value; } }


      public string ImageUrl { get { return this.BaseCard.CardImage; } }
      public string ImageUrlHiRes { get { return this.BaseCard.ImageHiRes; } }

      public CardModel()
      {
        this.BaseCard = new Card();
      }

      protected CardModel(Card baseCard)
      {
        this.BaseCard = baseCard;
      }
    }
}
