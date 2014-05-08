using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeriusSoft.MtgDeckBuilder.Models;

namespace SeriusSoft.MtgDeckBuilder.ViewModels
{
	public class ManaColorCountViewModel : BaseViewModel
	{
		protected ManaColors _color;
		public ManaColors Color
		{
			get { return this._color; }
			set
			{
				this._color = value;
				RaisePropertyChanged();
				RaiseAllBackedPropertiesChanged();
			}
		}

		protected string _manaColor;
		public string ManaColor
		{ 
			get { return this._manaColor; }
			protected set
			{
				this._manaColor = value;
				RaisePropertyChanged();
			}
		}

		protected int _count;
		public int Count
		{
			get { return this._count; }
			set
			{
				this._count = value;
				RaisePropertyChanged();
			}
		}

		public ManaColorCountViewModel(ManaColors color = ManaColors.Colorless, int count = 0)
		{
			this.Count = count;
			this.Color = color;
		}

		protected override void RaiseAllBackedPropertiesChanged()
		{
			this.ManaColor = this.Color.ToString();
		}
	}
}
