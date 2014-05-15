using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeriusSoft.MtgDeckBuilder.ViewModels.Test;

namespace ViewModelTests
{
  [TestClass]
  public class TestCardViewModelTests
  {
    [TestMethod]
    public void TestMethod1()
    {
      var test = new TestCardViewModel("Suntail Hawk", "W");

      Assert.AreEqual("Suntail Hawk", test.Name);
      Assert.IsNotNull(test.ManaCost);
      Assert.IsNotNull(test.ManaCost.ManaCost);
      Assert.AreEqual(1, test.ManaCost.ManaCost.Count);
      Assert.AreEqual("White", test.ManaCost.ManaCost[0].ManaColor);
      Assert.AreEqual(1, test.ManaCost.ManaCost[0].Count);
    }
  }
}
