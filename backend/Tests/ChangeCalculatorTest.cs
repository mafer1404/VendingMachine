using backend.Domain;

namespace Tests
{
  [TestFixture]
  public class ChangeCalculatorTests
  {
    private ChangeCalculator _changeCalculator;

    [SetUp]
    public void Setup()
    {
      _changeCalculator = new ChangeCalculator();
    }

    [Test]
    public void MachineOutOfCoins_InitiallyFalse()
    {
      Assert.IsFalse(_changeCalculator.MachineOutOfCoins());
    }

    [Test]
    public void Calculate_AmountPaidLessThanCost_ReturnsError()
    {
      var result = _changeCalculator.Calculate(1000, 900);

      Assert.IsFalse(result.Success);
      Assert.That(result.Message, Does.Contain("Dinero insuficiente"));
    }

    [Test]
    public void Calculate_NotEnoughCoinsToGiveChange_ReturnsFailure()
    {

      var result = _changeCalculator.Calculate(1000, 1010);

      Assert.IsFalse(result.Success);
      Assert.That(result.Message, Does.Contain("no hay monedas suficientes"));
    }

    [Test]
    public void MachineOutOfCoins_WhenNoCoins_ShouldReturnTrue()
    {
      var field = typeof(ChangeCalculator).GetField("coinInventory", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
      var inventory = (Dictionary<int, int>)field.GetValue(_changeCalculator);
      foreach (var key in inventory.Keys.ToList())
        inventory[key] = 0;

      Assert.IsTrue(_changeCalculator.MachineOutOfCoins());
    }

    [Test]
    public void Calculate_ValidChange_ShouldReturnCorrectBreakdown()
    {
      var result = _changeCalculator.Calculate(1000, 1575);

      Assert.IsTrue(result.Success);
      Assert.AreEqual(575, result.ChangeAmount);

      Assert.That(result.Breakdown[500], Is.EqualTo(1));
      Assert.That(result.Breakdown[50], Is.EqualTo(1));
      Assert.That(result.Breakdown[25], Is.EqualTo(1));
    }

    [Test]
    public void Calculate_InsufficientCoinsForChange_ShouldFail()
    {
      var field = typeof(ChangeCalculator).GetField("coinInventory", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
      var inventory = (Dictionary<int, int>)field.GetValue(_changeCalculator);
      inventory[500] = 0;
      inventory[100] = 0;
      inventory[50] = 0;
      inventory[25] = 0;

      var result = _changeCalculator.Calculate(1000, 1500);

      Assert.IsFalse(result.Success);
      Assert.That(result.Message, Does.Contain("suficientes"));
    }
  }

}
