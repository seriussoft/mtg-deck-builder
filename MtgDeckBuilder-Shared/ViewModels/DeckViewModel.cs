using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeriusSoft.MtgDeckBuilder.Models;

namespace SeriusSoft.MtgDeckBuilder.ViewModels
{
	public class DeckViewModel : BaseViewModel
	{
		protected DeckModel _model;
		protected DeckModel Model
		{
			get { return this._model; }
			set
			{
				this._model = value;
				RaisePropertyChanged();
				RaiseAllBackedPropertiesChanged();
			}
		}

		protected override void RaiseAllBackedPropertiesChanged()
		{
			//this.Model.Cards
		}
	}
}
