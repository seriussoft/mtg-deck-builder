using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MtgDeckBuilder_Desktop.ViewModel;
using SeriusSoft.MtgDeckBuilder.ViewModels;
using SeriusSoft.MtgDeckBuilder.ViewModels.Test;
using SeriusSoft.MtgDeckBuilder_Desktop.Views;

namespace SeriusSoft.MtgDeckBuilder_Desktop.Views
{
  /// <summary>
  /// Interaction logic for SimpleCardView.xaml
  /// </summary>
  public partial class SimpleCardView : BaseUserControl
  {
    private TestCardViewModel _testViewModel;
    public TestCardViewModel TestViewModel 
    {
      get { return this._testViewModel; }
      set
      {
        this._testViewModel = value;
        RaisePropertyChanged();
      }
    }

    protected override void InitializeData()
    {
      InitializeComponent();
      base.InitializeData();
    }

    protected override void SetDataContextIfInDesignMode()
    {
      try
      {
        this.TestViewModel = new TestCardViewModel("Suntail Hawk", "W");
      }
      catch (Exception e)
      {
        Debug.WriteLine(e.Message);
        Debug.Write(e.StackTrace);
        Debug.WriteLine(e.Source);
      }
      finally
      {
        if(this.InDesignMode)
          this.DataContext = this.TestViewModel;
      }
    }

    //protected void RaisePropertyChanged([CallerMemberName] string member = "")
    //{
    //  var copy = PropertyChanged;
    //  if (copy != null)
    //  {
    //    var changedEvent = new PropertyChangedEventArgs(member);
    //    copy(this, changedEvent);
    //  }
    //}

    //#region INotifyPropertyChanged Members

    //public event PropertyChangedEventHandler PropertyChanged;

    //#endregion
  }
}
