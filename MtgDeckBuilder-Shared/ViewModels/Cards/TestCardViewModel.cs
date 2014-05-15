using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriusSoft.MtgDeckBuilder.ViewModels.Test
{
  public class TestCardViewModel : CardViewModel
  {
    public string FullName 
    { 
      get { return this.Name; } 
      set 
      { 
        this.Name = value;
        this.RaisePropertyChanged();
      } 
    }
    
    public string FullManaCost 
    { 
      get { return this.Model.ManaCostSimple; } 
      set 
      { 
        this.ManaCost = new ManaCostViewModel(value);
        this.RaisePropertyChanged();
      } 
    }

    public TestCardViewModel() : this(String.Empty, "0") { }
    //{
    //  this.ManaCost = new ManaCostViewModel("0");
    //  this.Name = "";
    //}

    public TestCardViewModel(string name, string manaCost) : base(new Models.CardModel() { Name = name, ManaCostSimple = manaCost }) { }
    //{
    //  this.Name = name;
    //  this.ManaCost = new ManaCostViewModel(manaCost);
    //}
  }
}
