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
using SeriusSoft.MtgDeckBuilder.ViewModels;
using SeriusSoft.MtgDeckBuilder.ViewModels.Test;

namespace SeriusSoft.MtgDeckBuilder_Desktop.Views
{
  public partial class DeckEditorView : BaseUserControl
  {
    private DeckViewModel _testViewModel;
    public DeckViewModel TestViewModel
    {
      get { return this._testViewModel; }
      set
      {
        this._testViewModel = value;
        RaisePropertyChanged();
      }
    }

    /// <summary>
    /// if you override this, make sure to call the base method because this will run InitializeComponent.
    /// </summary>
    protected override void InitializeUI()
    {
      InitializeComponent();
    }

    /// <summary>
    /// Override this method to setup your designmode view model as well as to hook it up to your DataContext. By default, this method does nothing.
    /// </summary>
    protected override void SetDataContextIfInDesignMode()
    {
      try
      {
        this.TestViewModel = new TestDeckViewModel() { Name = "Test 1", ID = "1" };
        //this.DataContext = this.TestViewModel;
      }
      catch (Exception e)
      {
        Debug.WriteLine(e.Message);
        Debug.Write(e.StackTrace);
        Debug.WriteLine(e.Source);
      }
      finally
      {
        //if (this.InDesignMode)
          this.DataContext = this.TestViewModel;
      }
    }
  }
}
