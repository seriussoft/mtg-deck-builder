using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using SeriusSoft.MtgDeckBuilder.Models;
using MtgDb.Info;
using Newtonsoft.Json;
using Utils;
using ModelTests.ModelMocks;

namespace ModelTests
{
  [TestClass]
  public class ManaCostParserTests
  {
    static readonly Assertion Assert = new Assertion();

    [TestMethod]
    public void ManaCostSucceeds_SimpleTest_NotNull()
    {
			var results = ManaCostModelMocks.GetCardValue<List<Card>>(ManaCostModelMocks.SimpleJsonResult);
      var result = results.FirstOrDefault();
      var model = new CardModel() { ID = result.Id, Name = result.Name, ManaCostSimple = result.ManaCost };

      var manaCost = model.ManaCost;
			var expectationCost = new ManaCostModel();
			expectationCost.Costs.Add(ManaColors.Colorless, 1);
			expectationCost.Costs.Add(ManaColors.White, 1);

      Assert.IsNotNull(manaCost);
      Assert.IsNotNull(manaCost.Costs);
    }

		[TestMethod]
		public void ManaCostSucceeds_SimpleTest_CountMatches()
		{
			var results = ManaCostModelMocks.GetCardValue<List<Card>>(ManaCostModelMocks.SimpleJsonResult);
			var result = results.FirstOrDefault();
			var model = new CardModel() { ID = result.Id, Name = result.Name, ManaCostSimple = result.ManaCost };

			var manaCost = model.ManaCost;
			var expectationCost = new ManaCostModel();
			expectationCost.Costs.Add(ManaColors.Colorless, 1);
			expectationCost.Costs.Add(ManaColors.White, 1);

			Assert.CountIs(manaCost.Costs, 2);
		}

		[TestMethod]
		public void ManaCostSucceeds_SimpleTest_ItemsInListMatch()
		{
			var results = ManaCostModelMocks.GetCardValue<List<Card>>(ManaCostModelMocks.SimpleJsonResult);
			var result = results.FirstOrDefault();
			var model = new CardModel() { ID = result.Id, Name = result.Name, ManaCostSimple = result.ManaCost };

			var manaCost = model.ManaCost;
			var expectationCost = new ManaCostModel();
			expectationCost.Costs.Add(ManaColors.Colorless, 1);
			expectationCost.Costs.Add(ManaColors.White, 1);

			Assert.AreEqualCollections(manaCost.Costs, expectationCost.Costs, message: "The mana costs are not the same in the same order");
		}

		[TestMethod]
		public void ManaCostSucceeds_ComplexTest1_NotNull()
		{
			var results = ManaCostModelMocks.GetCardValue<List<Card>>(ManaCostModelMocks.ComplicatedJsonResult);
			var result = results.FirstOrDefault();
			var model = new CardModel() { ID = result.Id, Name = result.Name, ManaCostSimple = result.ManaCost };

			var manaCost = model.ManaCost;

			Assert.IsNotNull(manaCost);
			Assert.IsNotNull(manaCost.Costs);
		}

		[TestMethod]
		public void ManaCostSucceeds_ComplexText1_CountMatches()
		{
			var results = ManaCostModelMocks.GetCardValue<List<Card>>(ManaCostModelMocks.ComplicatedJsonResult);
			var result = results.FirstOrDefault();
			var model = new CardModel() { ID = result.Id, Name = result.Name, ManaCostSimple = result.ManaCost };

			var manaCost = model.ManaCost;

			var expectationCost = ManaCostModelMocks.ComplicatedResult;

			Assert.CountIs(manaCost.Costs, expectationCost.Costs.Count);
		}

		[TestMethod]
		public void ManaCostSucceeds_ComplextTest1_ItemsInListMatch()
		{
			var results = ManaCostModelMocks.GetCardValue<List<Card>>(ManaCostModelMocks.ComplicatedJsonResult);
			var result = results.FirstOrDefault();
			var model = new CardModel() { ID = result.Id, Name = result.Name, ManaCostSimple = result.ManaCost };

			var manaCost = model.ManaCost;

			var expectationCost = ManaCostModelMocks.ComplicatedResult;

			Assert.AreEqualCollections(manaCost.Costs, expectationCost.Costs, message: "The mana costs are not the same in the same order");
		}

		[TestMethod]
		public void ManaCostSucceeds_ComplexTest2_NotNull()
		{
			var results = ManaCostModelMocks.GetCardValue<List<Card>>(ManaCostModelMocks.ComplicatedJsonResult2);
			var result = results.FirstOrDefault();
			var model = new CardModel() { ID = result.Id, Name = result.Name, ManaCostSimple = result.ManaCost };

			var manaCost = model.ManaCost;

			Assert.IsNotNull(manaCost);
			Assert.IsNotNull(manaCost.Costs);
		}

		[TestMethod]
		public void ManaCostSucceeds_ComplexText2_CountMatches()
		{
			var results = ManaCostModelMocks.GetCardValue<List<Card>>(ManaCostModelMocks.ComplicatedJsonResult2);
			var result = results.FirstOrDefault();
			var model = new CardModel() { ID = result.Id, Name = result.Name, ManaCostSimple = result.ManaCost };

			var manaCost = model.ManaCost;

			var expectationCost = ManaCostModelMocks.ComplicatedResult2;

			Assert.CountIs(manaCost.Costs, expectationCost.Costs.Count);
		}

		[TestMethod]
		public void ManaCostSucceeds_ComplextTest2_ItemsInListMatch()
		{
			var results = ManaCostModelMocks.GetCardValue<List<Card>>(ManaCostModelMocks.ComplicatedJsonResult2);
			var result = results.FirstOrDefault();
			var model = new CardModel() { ID = result.Id, Name = result.Name, ManaCostSimple = result.ManaCost };

			var manaCost = model.ManaCost;

			var expectationCost = ManaCostModelMocks.ComplicatedResult2;

			Assert.AreEqualCollections(manaCost.Costs, expectationCost.Costs, message: "The mana costs are not the same in the same order");
		}
  }
}
