using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
      this.AddCardCommand = new RelayCommand<CardInDeckViewModel>(AddCardCommand_Execute, AddCardCommand_CanExecute);
      this.RemoveCardCommand = new RelayCommand<CardInDeckViewModel>(RemoveCardCommand_Execute, RemoveCardCommand_CanExecute);
      this.SelectCardCommand = new RelayCommand<CardInDeckViewModel>(SelectCardCommand_Execute, SelectCardCommand_CanExecute);
      this.UnselectCardCommand = new RelayCommand(UnselectCardCommand_Execute, UnselectCardCommand_CanExecute);
    }

    private void AddCardCommand_Execute(CardInDeckViewModel cardInDeck)
    {
      //this.CurrentlySelectedCard.Model
      this.Model.AddCard(cardInDeck.Model);
      this.CurrentlySelectedCard = null;
    }

    private bool AddCardCommand_CanExecute(CardInDeckViewModel cardInDeck)
    {
      return this.Model != null && cardInDeck != null;
    }

    private void RemoveCardCommand_Execute(CardInDeckViewModel cardInDeck)
    {
      //this.CurrentlySelectedCar.dModel
      this.Model.RemoveCard(cardInDeck.Model);
    }

    private bool RemoveCardCommand_CanExecute(CardInDeckViewModel cardInDeck)
    {
      return this.Model != null && this.Model.Cards.Any() && cardInDeck != null;
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

    private void SelectCardCommand_Execute(CardInDeckViewModel cardInDeck)
    {
      this.CurrentlySelectedCard = cardInDeck;
    }

    private bool SelectCardCommand_CanExecute(CardInDeckViewModel cardInDeck)
    {
      return cardInDeck != null;
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

    protected void Cards_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      switch (e.Action)
      {
        case NotifyCollectionChangedAction.Add:
          if (e.NewItems == null)
            return;

          var itemsAdded = e.NewItems.Cast<CardInDeckViewModel>().ToList();
          foreach (var item in itemsAdded)
          {
            //HookupCommandsToCard(item);
          }
          break;

        case NotifyCollectionChangedAction.Remove:
        case NotifyCollectionChangedAction.Replace:
          var itemsRemoved = e.OldItems.Cast<CardInDeckViewModel>().ToList();
          foreach (var item in itemsRemoved)
          {
            //UnhookCommandsToCard(item);
          }
          break;

        case NotifyCollectionChangedAction.Reset:
          var itemsReset = e.OldItems.Cast<CardInDeckViewModel>().ToList();
          foreach (var item in itemsReset)
          {
            //UnhookCommandsToCard(item);
          }
          break;

        default:
          //do nothing for now. but, if we need to cover the other actions, we can here
          break;
      }
    }

    [Obsolete("Not yet implemented")]
    protected void HookupCommandsToCard(CardInDeckViewModel cardInDeck)
    {
      //hookup the commands for select and remove
      throw new NotImplementedException("HookupCommandsToCard is not yet implemented");
    }

    [Obsolete("Not yet implemented")]
    protected void UnhookCommandsToCard(CardInDeckViewModel cardInDeck)
    {
      //unhook the commands for select and remove
      throw new NotImplementedException("UnhookCommandsToCard is not yet implemented");
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
