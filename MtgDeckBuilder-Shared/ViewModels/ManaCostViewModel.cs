using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeriusSoft.MtgDeckBuilder.Models;

namespace SeriusSoft.MtgDeckBuilder.ViewModels
{
	public class ManaCostViewModel : BaseViewModel
	{
		protected ManaCostModel _model;
		protected internal ManaCostModel Model
		{
			get { return this._model; }
			set
			{
				this._model = value;
				RaisePropertyChanged();
				RaiseAllBackedPropertiesChanged();
			}
		}

		protected ObservableCollection<ManaColorCountViewModel> _manaCost;
		public ObservableCollection<ManaColorCountViewModel> ManaCost
		{
			get { return this._manaCost; }
			protected set
			{
				this._manaCost = value;
				RaisePropertyChanged();
			}
		}

		protected override void RaiseAllBackedPropertiesChanged()
		{
			this.ManaCost.Clear();
			var newCostCounts = this.Model.Costs.Select(c => new ManaColorCountViewModel(color:c.Key, count:c.Value)).ToList();
			foreach (var costCount in newCostCounts)
			{
				this.ManaCost.Add(costCount);
			}
		}

		public ManaCostViewModel(string manaCost = null) : this(new ManaCostModel(manaCost)) { }

		protected internal ManaCostViewModel(ManaCostModel model) : this()
		{
			this.ManaCost = new ObservableCollection<ManaColorCountViewModel>();
			this.Model = model;
		}
	}
}
