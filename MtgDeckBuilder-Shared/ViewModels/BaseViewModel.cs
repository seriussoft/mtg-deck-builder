using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SeriusSoft.MtgDeckBuilder.ViewModels
{
	public abstract class BaseViewModel : INotifyPropertyChanged, IBackedByModel
	{
		public virtual bool IsNew { get { return true; } }
		public virtual bool IsBacked { get; set; }

		protected void RaisePropertyChanged([CallerMemberName] string member = "")
		{
			var copy = PropertyChanged;
			if (copy != null)
			{
				var changedEvent = new PropertyChangedEventArgs(member);
				copy(this, changedEvent);
			}
		}

		protected abstract void RaiseAllBackedPropertiesChanged();

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion
	}
}
