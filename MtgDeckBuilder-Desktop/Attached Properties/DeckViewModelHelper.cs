using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SeriusSoft.MtgDeckBuilder.ViewModels;
using SeriusSoft.MtgDeckBuilder.ViewModels.Test;

namespace SeriusSoft.MtgDeckBuilder_Desktop.Views
{
  public class DeckViewModelHelper : BaseUserControl
  {
    public static readonly DependencyProperty DeckViewModelProperty = 
      DependencyProperty.RegisterAttached
      (
        "TestViewModel", typeof(DeckViewModel), typeof(DeckViewModelHelper), 
        new FrameworkPropertyMetadata
        (
          null, 
          FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.AffectsRender, 
          DeckViewModelPropertyChanged
        )
      );

    private static void DeckViewModelPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
    {
      var editorView = obj as DeckEditorView;

      if (editorView != null)
      {
        editorView.TestViewModel = e.NewValue as DeckViewModel;
      }
    }

    public static void SetDeckViewModel(UIElement element, object viewModel)
    {
      element.SetValue(DeckViewModelProperty, viewModel);
    }

    public static DeckViewModel GetDeckViewModel(UIElement element)
    {
      return element.GetValue(DeckViewModelProperty) as DeckViewModel;
    }
  }
}
