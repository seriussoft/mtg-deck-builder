using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeriusSoft.MtgDeckBuilder.Models;

namespace SeriusSoft.MtgDeckBuilder.ViewModels
{
	public class CardInDeckViewModel : CardViewModel
	{
		protected int _quantity;
		public int Quantity
		{
			get { return this._quantity; }
			set
			{
				this._quantity = value;
				RaisePropertyChanged();
			}
		}

		public CardInDeckViewModel() : this(new CardModel()) { }

		protected internal CardInDeckViewModel(CardModel model)
		{
			this.Model = model;
		}

		public CardInDeckViewModel(CardViewModel cardViewModel) : this(cardViewModel.Model) { }
	}
}
