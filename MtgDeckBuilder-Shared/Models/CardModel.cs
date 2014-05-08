using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MtgDb.Info;

namespace SeriusSoft.MtgDeckBuilder.Models
{
    public class CardModel
    {
      protected Card _baseCard;
      protected Card BaseCard 
      {
        get { return this._baseCard; }
        set
        {
          this._baseCard = value;
          UpdateManaCost();
        }
      }

      public string Name { get { return this.BaseCard.Name; } set { this.BaseCard.Name = value; } }
      public int ID { get { return this.BaseCard.Id; } set { this.BaseCard.Id = value; } }

      public string CardSetID { get { return this.BaseCard.CardSetId; } set { this.BaseCard.CardSetId = value; } }
      public string CardSetName { get { return this.BaseCard.CardSetName; } set { this.BaseCard.CardSetName = value; } }

      //private ManaCostModel _manaCost;
      //public ManaCostModel ManaCost { get { return this._manaCost; } set { this._manaCost = value; } }
      public ManaCostModel ManaCost { get; set; }

      public int ConvertedManaCost { get { return this.BaseCard.ConvertedManaCost; } set { this.BaseCard.ConvertedManaCost = value; } }
      public string ManaCostSimple 
      { 
        get 
        { 
          return this.BaseCard.ManaCost; 
        } 
        set 
        { 
          this.BaseCard.ManaCost = value;
          UpdateManaCost();
        } 
      }

      private void UpdateManaCost()
      {
        try { this.ManaCost = new ManaCostModel(this.BaseCard.ManaCost); }
        catch { }
      }

      public string ImageUrl { get { return this.BaseCard.CardImage; } }
      public string ImageUrlHiRes { get { return this.BaseCard.ImageHiRes; } }

      public string Type { get { return this.BaseCard.Type; } set { this.BaseCard.Type = value; } }
      public string SubType { get { return this.BaseCard.SubType; } set { this.BaseCard.SubType = value; } }

      public CardModel()
      {
        this.BaseCard = new Card();
      }

      protected internal CardModel(Card baseCard)
      {
        this.BaseCard = baseCard;
      }
    }
}
