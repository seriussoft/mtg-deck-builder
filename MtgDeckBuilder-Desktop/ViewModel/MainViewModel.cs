using GalaSoft.MvvmLight;
using SeriusSoft.MtgDeckBuilder.ViewModels;

namespace MtgDeckBuilder_Desktop.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : BaseViewModel
    {
      protected ColorTheme _colorTheme;
      public ColorTheme ColorTheme
      {
        get { return this._colorTheme; }
        set
        {
          this._colorTheme = value;
          RaisePropertyChanged();
          RaiseAllBackedPropertiesChanged();
        }
      }

      protected string _colorThemeName;
      public string ColorThemeName
      {
        get { return this._colorThemeName; }
        protected set
        {
          this._colorThemeName = value;
          RaisePropertyChanged();
        }
      }

      protected DeckViewModel _deckMode;
      public DeckViewModel DeckMode
      {
        get { return this._deckMode; }
        set
        {
          this._deckMode = value;
          this.RaisePropertyChanged();
        }
      }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
        }

        protected override void RaiseAllBackedPropertiesChanged()
        {
          this.ColorThemeName = GetColorThemeName(this.ColorTheme);
        }

        private string GetColorThemeName(ColorTheme colorTheme)
        {
          return colorTheme.ToString();

          //switch (colorTheme)
          //{
          //  case ViewModel.ColorTheme.Black:
          //    return "";

          //  case ViewModel.ColorTheme.Blue:
          //    return "";

          //  case ViewModel.ColorTheme.Green:
          //    return "";

          //  case ViewModel.ColorTheme.White:
          //    return "";

          //  case ViewModel.ColorTheme.Metal:
          //    return "";

          //  case ViewModel.ColorTheme.Red:
          //  default:
          //    return "";
          //}
        }
    }

    public enum ColorTheme
    {
      Red = 0,
      Black,
      Green,
      Blue,
      White,
      Metal
    }
}