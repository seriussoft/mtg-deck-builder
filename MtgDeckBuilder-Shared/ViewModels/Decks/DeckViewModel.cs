using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using SeriusSoft.MtgDeckBuilder.Models;
using SeriusSoft.MtgDeckBuilder.Utilities;
using Utilities;

namespace SeriusSoft.MtgDeckBuilder.ViewModels
{
	public class DeckViewModel : BaseViewModel
	{
    #region Properties

    protected DeckModel _model;
    protected DeckModel Model
    {
      get { return this._model; }
      set
      {
        this._model = value;
        RaisePropertyChanged();
        UpdateCardCollectionsObject();
        RaiseAllBackedPropertiesChanged();
      }
    }

    private string _name;
    public string Name
    {
      get { return this._name; }
      set
      {
        this._name = value;
        RaisePropertyChanged();
      }
    }

    private string _id;
    public string ID
    {
      get { return this._id; }
      set
      {
        this._id = value;
        RaisePropertyChanged();
      }
    }

    public AsyncSynchronizedObservableCollection<CardInDeckViewModel, CardModel> Cards { get; private set; }

    private CardInDeckViewModel _currentlySelectedCard;
    public CardInDeckViewModel CurrentlySelectedCard
    {
      get { return this._currentlySelectedCard; }
      set
      {
        this._currentlySelectedCard = value;
        RaisePropertyChanged();
      }
    } 

    #endregion  Properties

    #region Commands

    private ICommand _addCardCommand;
    public ICommand AddCardCommand
    {
      get { return this._addCardCommand; }
      set
      {
        this._addCardCommand = value;
        RaisePropertyChanged();
      }
    }

    private ICommand _removeCardCommand;
    public ICommand RemoveCardCommand
    {
      get { return this._removeCardCommand; }
      set
      {
        this._removeCardCommand = value;
        RaisePropertyChanged();
      }
    }

    private ICommand _selectCardCommand;
    public ICommand SelectCardCommand
    {
      get { return this._selectCardCommand; }
      set
      {
        this._selectCardCommand = value;
        RaisePropertyChanged();
      }
    }

    private ICommand _unselectCardCommand;
    public ICommand UnselectCardCommand
    {
      get { return this._unselectCardCommand; }
      set
      {
        this._unselectCardCommand = value;
        RaisePropertyChanged();
      }
    }

    protected void HookupCommands()
    {
      this.AddCardCommand = new RelayCommand(AddCardCommand_Execute, AddCardCommand_CanExecute);
      this.RemoveCardCommand = new RelayCommand(RemoveCardCommand_Execute, RemoveCardCommand_CanExecute);
      this.SelectCardCommand = new RelayCommand(SelectCardCommand_Execute, SelectCardCommand_CanExecute);
      this.UnselectCardCommand = new RelayCommand(UnselectCardCommand_Execute, UnselectCardCommand_CanExecute);
    }

    private void AddCardCommand_Execute()
    {
      this.Model.AddCard(this.CurrentlySelectedCard.Model);
      this.CurrentlySelectedCard = null;
    }

    private bool AddCardCommand_CanExecute()
    {
      return this.Model != null;
    }

    private void RemoveCardCommand_Execute()
    {
      this.Model.RemoveCard(this.CurrentlySelectedCard.Model);
    }

    private bool RemoveCardCommand_CanExecute()
    {
      return this.Model != null && this.Model.Cards.Any() && this.CurrentlySelectedCard != null;
    }

    private void UnselectCardCommand_Execute()
    {
      this.CurrentlySelectedCard = null;
      //what to do with model?????
    }

    private bool UnselectCardCommand_CanExecute()
    {
      return this.CurrentlySelectedCard != null;
    }

    [Obsolete("Not implemented. May need to actually move the command object to the individual card view models and then hook this method up from there")]
    private void SelectCardCommand_Execute()
    {
      throw new NotImplementedException();
    }

    [Obsolete("Not implemented. May need to actually move the command object to the individual card view models and then hook this method up from there")]
    private bool SelectCardCommand_CanExecute()
    {
      throw new NotImplementedException();
    }

    #endregion  Commands

    public DeckViewModel()
      : this(new DeckModel())
    {
      
    }

    protected DeckViewModel(DeckModel deckModel)
    {
      this.Model = deckModel;
      HookupCommands();
    }

		protected override void RaiseAllBackedPropertiesChanged()
		{
      this.Name = this.Model.Name;
      this.ID = this.Model.ID;
		}

    protected void UpdateCardCollectionsObject()
    {
      if (this.Cards != null)
      {
        this.Cards.CollectionChanged -= Cards_CollectionChanged;
      }

      this.Cards = new AsyncSynchronizedObservableCollection<CardInDeckViewModel, CardModel>(this.Model.Cards, CreateDeckCardViewModelFromModel);
      this.Cards.CollectionChanged += Cards_CollectionChanged;
    }

    protected void Cards_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
      //is there anything we care about here???
    }

    protected CardInDeckViewModel CreateDeckCardViewModelFromModel(CardModel cardModel)
    {
      //if (this.Cards == null)
      //  return new CardInDeckViewModel(cardModel);

      //var cardFound = this.Cards.FirstOrDefault(c => c.Name == cardModel.Name);
      //if(cardFound == null)
        return new CardInDeckViewModel(cardModel);

      //cardFound.Quantity++;
      //return null;
    }
	}
}
