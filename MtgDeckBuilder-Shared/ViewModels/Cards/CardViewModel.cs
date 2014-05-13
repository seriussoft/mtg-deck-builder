using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeriusSoft.MtgDeckBuilder.Models;

namespace SeriusSoft.MtgDeckBuilder.ViewModels
{
	public class CardViewModel : BaseViewModel
	{
		private CardModel _model;
		protected internal CardModel Model
		{
			get { return this._model; }
			set
			{
				this._model = value;
				RaisePropertyChanged();
				RaiseAllBackedPropertiesChanged();
			}
		}

		protected ManaCostViewModel _manaCost;
		public ManaCostViewModel ManaCost
		{
			get { return this._manaCost; }
			protected set
			{
				this._manaCost = value;
				RaisePropertyChanged();
			}
		}

		protected string _imageUrlLowRes;
		public string ImageUrlLowRes
		{
			get { return this._imageUrlLowRes; }
			protected set
			{
				this._imageUrlLowRes = value;
				RaisePropertyChanged();
			}
		}

		protected string _imageUrlHiRes;
		public string ImageUrlHiRes
		{
			get { return this._imageUrlHiRes; }
			protected set
			{
				this._imageUrlHiRes = value;
				RaisePropertyChanged();
			}
		}

		protected string _name;
		public string Name
		{
			get { return this._name; }
			protected set 
			{ 
				this._name = value;
				RaisePropertyChanged();
			}
		}

		protected int _id;
		public int ID 
		{ 
			get { return this._id; }
			protected set
			{
				this._id = value;
				RaisePropertyChanged();
			}
		}

		protected string _type;
		public string Type
		{
			get { return this._type; }
			protected set
			{
				this._type = value;
				RaisePropertyChanged();
			}
		}

		protected string _subType;
		public string SubType
		{
			get { return this._subType; }
			protected set
			{
				this._subType = value;
				RaisePropertyChanged();
			}
		}

		protected string _cardSet;
		public string CardSet
		{
			get { return this._cardSet; }
			protected set
			{
				this._cardSet = value;
				RaisePropertyChanged();
			}
		}

		protected string _cardSetID;
		public string CardSetID
		{
			get { return this._cardSetID; }
			protected set
			{
				this._cardSetID = value;
				RaisePropertyChanged();
			}
		}

		protected override void RaiseAllBackedPropertiesChanged()
		{
			if(this.Model != null)
			{
				this.Name = this.Model.Name;
				this.ID = this.Model.ID;
				this.ImageUrlLowRes = this.Model.ImageUrl;
				this.ImageUrlHiRes = this.Model.ImageUrlHiRes;
				
				this.ManaCost = new ManaCostViewModel(this.Model.ManaCost); //this may cause a problem because i'm recreating this (so far as wpf and binding goes. if we see odd behavior, i can create a different way to copy the values over)
				this.Type = this.Model.Type;
				this.SubType = this.Model.SubType;
				this.CardSet = this.Model.CardSetName;
				this.CardSetID = this.Model.CardSetID;
			}
		}

		public CardViewModel() : this(new CardModel()) { }

		protected internal CardViewModel(CardModel model)
		{
			this.Model = model;
		}
	}
}
