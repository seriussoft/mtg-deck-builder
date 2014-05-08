using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Behaviours;
using MahApps.Metro.Controls;
using MtgDeckBuilder_Desktop.ViewModel;

namespace MtgDeckBuilder_Desktop
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : MetroWindow
  {
    public bool IsInDesignMode
    {
      get { return DesignerProperties.GetIsInDesignMode(this); }
    }

    public MainWindow()
    {
      InitializeComponent();

      if (this.IsInDesignMode)
      {
        this.DataContext = new MainViewModel() { ColorTheme = ColorTheme.Red };
      }
      else
      {
        this.DataContext = new MainViewModel();
      }
    }

		private void AddWindowBehaviors()
		{
			var windowBehavior = new BorderlessWindowBehavior() { EnableDWMDropShadow = true, AllowsTransparency = false, ResizeWithGrip = true };
			var behaviors = Interaction.GetBehaviors(this);
			behaviors.Add(windowBehavior);
		}
  }
}
