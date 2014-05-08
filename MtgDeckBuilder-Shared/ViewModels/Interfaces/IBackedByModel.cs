using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriusSoft.MtgDeckBuilder.ViewModels
{
	public interface IBackedByModel
	{
		bool IsNew { get; }
		bool IsBacked { get; set; }
	}
}
