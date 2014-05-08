using System;
using System.Collections.Generic;
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

namespace MtgDeckBuilder_Desktop
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : MetroWindow
  {
    public MainWindow()
    {
      InitializeComponent();
    }

		private void AddWindowBehaviors()
		{
			var windowBehavior = new BorderlessWindowBehavior() { EnableDWMDropShadow = true, AllowsTransparency = false, ResizeWithGrip = true };
			var behaviors = Interaction.GetBehaviors(this);
			behaviors.Add(windowBehavior);
		}
  }
}
