using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SeriusSoft.MtgDeckBuilder_Desktop.Views
{
  //public abstract class BaseObservableUserControl : UserControl, INotifyPropertyChanged
  //{
  //  public BaseObservableUserControl()
  //  {
  //    InitializeUI();
  //    InitializeData();
  //  }

  //  /// <summary>
  //  /// override this method to call your InitializeComponent generated method.
  //  /// </summary>
  //  protected virtual void InitializeUI()
  //  {
  //    //InitializeComponent();
  //  }

  //  /// <summary>
  //  /// if you override this, make sure to call the base method because this will run SetDataContextIfInDesignMode.
  //  /// </summary>
  //  protected virtual void InitializeData()
  //  {
  //    SetDataContextIfInDesignMode();
  //  }

  //  /// <summary>
  //  /// Override this method to setup your designmode view model as well as to hook it up to your DataContext
  //  /// </summary>
  //  protected virtual void SetDataContextIfInDesignMode()
  //  {
  //    /*****************
  //          * Example below:
  //          ****************/

  //    //try
  //    //{
  //    //  this.TestViewModel = new TestCardViewModel("Suntail Hawk", "W");
  //    //}
  //    //catch (Exception e)
  //    //{
  //    //  Debug.WriteLine(e.Message);
  //    //  Debug.Write(e.StackTrace);
  //    //  Debug.WriteLine(e.Source);
  //    //}
  //    //finally
  //    //{
  //    //  if (this.InDesignMode)
  //    //    this.DataContext = this.TestViewModel;
  //    //}
  //  }

  //  protected void RaisePropertyChanged([CallerMemberName] string member = "")
  //  {
  //    var copy = PropertyChanged;
  //    if (copy != null)
  //    {
  //      var changedEvent = new PropertyChangedEventArgs(member);
  //      copy(this, changedEvent);
  //    }
  //  }

  //  /// <summary>
  //  /// <para>Override this method to give your usercontrol an option for raising a changed event for all of your backing properties.</para>
  //  /// <para>You might use this in the case where if you update a model, it has cascading effects to other properties or viewmodels</para>
  //  /// </summary>
  //  protected virtual void RaiseAllBackedPropertiesChanged()
  //  {

  //  }

  //  #region INotifyPropertyChanged Members

  //  public event PropertyChangedEventHandler PropertyChanged;

  //  #endregion
  //}
}
