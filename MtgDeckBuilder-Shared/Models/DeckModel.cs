﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.QueryHelpers;

namespace Models
{
  public class DeckModel
	{
		#region Properties

		public List<CardModel> Cards { get; private set; }

		public int TotalConvertedManaCost { get { return this.Cards.Sum(c => c.ConvertedManaCost); } }
		public double AverageConvertedManaCost { get { return this.Cards.Average(c => c.ConvertedManaCost); } }
		public IDictionary<ManaColors, int> TotalCosts
		{
			get
			{
				var costs = this.Cards.SelectMany(c => c.ManaCost.Costs.ToList()).Select(kvp => new { Cost = kvp.Key, Total = kvp.Value });
				var costTotals =
					costs
					.GroupBy(c => c.Cost)
					.Select
					(
						grp => new KeyValuePair<ManaColors, int>
						(
							grp.Key, grp.Sum(g => g.Total)
						)
					).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

				// want it to be obvious that this is a readonly collection because at some point, 
				// this will need to be offloaded due to api calls being so inefficient
				return new ReadOnlyDictionary<ManaColors,int>(costTotals);
			}
		}

		public IDictionary<ManaCostModel, int> TotalCostsByGroup
		{
			get 
			{ 
				var costs = this.Cards
					.Select(c => c.ManaCostSimple)
					.GroupBy(mc => mc)
					.Select(grp => new KeyValuePair<ManaCostModel, int>(new ManaCostModel(grp.Key), grp.Count()))
					.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

				return new ReadOnlyDictionary<ManaCostModel,int>(costs);
			}
		}

		#endregion	Properties

		public DeckModel(IEnumerable<CardModel> cards = null)
    {
      this.Cards = new List<CardModel>();

      AddCards(cards);
    }

		#region Card Add/Remove Methods

		public CardModel AddCard(CardModel card)
		{
			this.Cards.Add(card);
			return card;
		}

		public IEnumerable<CardModel> AddCards(IEnumerable<CardModel> cards)
		{
			if (cards != null && cards.Any())
				this.Cards.AddRange(cards);

			return cards;
		}

		public CardModel RemoveCard(CardModel card)
		{
			if (this.Cards.Contains(card))
				this.Cards.Remove(card);

			return card;
		}

		public CardModel RemoveCard(string cardName)
		{
			var cardToRemove = this.Cards.FirstOrDefault(card => card.Name == cardName);
			if (cardToRemove != null)
			{
				this.Cards.Remove(cardToRemove);
				return cardToRemove;
			}

			return null;
		}

		public IEnumerable<CardModel> RemoveCards(params string[] cardNames)
		{
			var cardsToRemove = this.Cards.FindAll(c => cardNames.Contains(c.Name));
			if (cardsToRemove.Any())
			{
				var cardsRemoved = cardsToRemove.ToList();
				this.Cards.RemoveAll(c => cardNames.Contains(c.Name));
				return cardsRemoved;
			}

			return new List<CardModel>();
		} 
		
		#endregion	Card Add/Remove Methods


  }
}
