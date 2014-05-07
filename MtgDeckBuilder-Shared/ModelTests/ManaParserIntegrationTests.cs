using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Utils;

namespace ModelTests
{
  [TestClass]
  public class ManaParserIntegrationTests
  {
    public static Assertion Assert = new Assertion();

    [TestMethod]
    public void ManaParserTests_Success_GrabsLeoninSquire_ReturnsCorrectManaCostOf1W()
    {
      var db = new CardsDB();
      var testCardName = "Leonin Squire";
      var testCardNameUndercase = testCardName.ToLower();
      var expectedManaCost = "1W";
      var expectedComplexManaCost = new ManaCostModel(expectedManaCost);

      //var card = await db.GetCardByName(testCardNameUndercase);
      var result = db.GetCardByName(testCardNameUndercase);
      var card = result.Result;

      Assert.IsTrue(card.Type.Contains("Creature"));
      Assert.AreEqual(card.Name, testCardName);
      Assert.AreEqual(card.ManaCostSimple, expectedManaCost);
      Assert.IsNotNull(card.ManaCost);
      Assert.AreEqualCollections(card.ManaCost.Costs, expectedComplexManaCost.Costs);
    }

		[TestMethod]
		public void ManaParserTests_Success_4LeoninSquire_ReturnsCorrectTotalConvertedCostAndAverageConvertedCost()
		{
			var db = new CardsDB();
			var testCardName = "Leonin Squire";
			var testCardNameUndercase = testCardName.ToLower();

			var result = db.GetCardByName(testCardNameUndercase);
			var card = result.Result;
			var deck = new DeckModel(new[] { card, card, card, card });

			Assert.AreEqual(8, deck.TotalConvertedManaCost);
			Assert.AreEqual(2, deck.AverageConvertedManaCost);
		}

		[TestMethod]
		public void ManaParserTests_Success_4LeoninSquire_ReturnsCorrectTotalActualCost()
		{
			var db = new CardsDB();
			var testCardName = "Leonin Squire";
			var testCardNameUndercase = testCardName.ToLower();

			var result = db.GetCardByName(testCardNameUndercase);
			var card = result.Result;
			var deck = new DeckModel(new[] { card, card, card, card });

			var totalCosts = deck.TotalCosts;

			//Makes the assumption that we just want the total number of each monocolor used, each hybrid color used, and each colorless used
			Assert.AreEqual(2, totalCosts.Count);
			Assert.AreEqual(4, totalCosts[ManaColors.Colorless]);
			Assert.AreEqual(4, totalCosts[ManaColors.White]);
		}

		[TestMethod]
		public void ManaParserTests_Success_4LeoninSquire_ReturnsCorrectTotalGroupedCost()
		{
			var db = new CardsDB();
			var testCardName = "Leonin Squire";
			var testCardNameUndercase = testCardName.ToLower();
			var expectedCost = "1W";
			var convertedExpectedCost = new ManaCostModel(expectedCost);

			var result = db.GetCardByName(testCardNameUndercase);
			var card = result.Result;
			var deck = new DeckModel(new[] { card, card, card, card });

			var totalCosts = deck.TotalCostsByGroup;

			//Makes the assumption that we just want the total number of each monocolor used, each hybrid color used, and each colorless used
			Assert.AreEqual(1, totalCosts.Count);
			Assert.IsNotNull(totalCosts.Keys.FirstOrDefault());
			Assert.AreEqual(2, totalCosts.Keys.FirstOrDefault().Costs.Count);
			Assert.AreEqualCollections(convertedExpectedCost.Costs, totalCosts.Keys.FirstOrDefault().Costs);
			Assert.AreEqual(4, totalCosts.Values.FirstOrDefault());
		}
  }
}
