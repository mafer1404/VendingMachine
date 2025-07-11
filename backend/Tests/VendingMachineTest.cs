
using backend.Domain;

namespace Tests
{
  [TestFixture]
  public class VendingMachineTests
  {
    private VendingMachine _vendingMachine;

    [SetUp]
    public void Setup()
    {
      _vendingMachine = new VendingMachine();
    }

    [Test]
    public void GetAllDrinks_ShouldReturnInitialDrinks()
    {
      var drinks = _vendingMachine.GetAllDrinks();

      Assert.That(drinks, Has.Count.EqualTo(4));
      Assert.That(drinks.Any(d => d.Name == "Sprite" && d.Quantity == 15 && d.Price == 975));
      Assert.That(drinks.Any(d => d.Name == "Fanta" && d.Quantity == 10 && d.Price == 950));
      Assert.That(drinks.Any(d => d.Name == "Pepsi" && d.Quantity == 8 && d.Price == 750));
      Assert.That(drinks.Any(d => d.Name == "Coca Cola" && d.Quantity == 10 && d.Price == 800));
    }

    [Test]
    public void InitialDrinks_ShouldHaveCorrectStockAndPrice()
    {
      var drinks = _vendingMachine.GetAllDrinks();
      Assert.That(drinks.Count, Is.EqualTo(4));

      var cocaCola = drinks.FirstOrDefault(d => d.Name == "Coca Cola");
      Assert.NotNull(cocaCola);
      Assert.AreEqual(10, cocaCola.Quantity);
      Assert.AreEqual(800, cocaCola.Price);

      var pepsi = drinks.FirstOrDefault(d => d.Name == "Pepsi");
      Assert.NotNull(pepsi);
      Assert.AreEqual(8, pepsi.Quantity);
      Assert.AreEqual(750, pepsi.Price);

      var fanta = drinks.FirstOrDefault(d => d.Name == "Fanta");
      Assert.NotNull(fanta);
      Assert.AreEqual(10, fanta.Quantity);
      Assert.AreEqual(950, fanta.Price);

      var sprite = drinks.FirstOrDefault(d => d.Name == "Sprite");
      Assert.NotNull(sprite);
      Assert.AreEqual(15, sprite.Quantity);
      Assert.AreEqual(975, sprite.Price);
    }

    [Test]
    public void BuyDrink_ValidDrinkAndQuantity_ShouldReduceQuantityAndReturnTrue()
    {
      bool result = _vendingMachine.BuyDrink("Pepsi", 3);

      var pepsi = _vendingMachine.GetAllDrinks().First(d => d.Name == "Pepsi");

      Assert.IsTrue(result);
      Assert.AreEqual(5, pepsi.Quantity);
    }

    [Test]
    public void BuyDrink_ExactQuantity_ShouldReduceToZeroAndReturnTrue()
    {
      bool result = _vendingMachine.BuyDrink("Fanta", 10);

      var fanta = _vendingMachine.GetAllDrinks().First(d => d.Name == "Fanta");

      Assert.IsTrue(result);
      Assert.AreEqual(0, fanta.Quantity);
    }

    [Test]
    public void BuyDrink_InsufficientStock_ShouldReturnFalseAndNotChangeQuantity()
    {
      var before = _vendingMachine.GetAllDrinks().First(d => d.Name == "Sprite").Quantity;

      bool result = _vendingMachine.BuyDrink("Sprite", before + 1);

      var after = _vendingMachine.GetAllDrinks().First(d => d.Name == "Sprite").Quantity;

      Assert.IsFalse(result);
      Assert.AreEqual(before, after);
    }

    [Test]
    public void BuyDrink_InvalidDrinkName_ShouldReturnFalse()
    {
      bool result = _vendingMachine.BuyDrink("Red Bull", 1);

      Assert.IsFalse(result);
    }

    [Test]
    public void BuyDrink_NegativeQuantity_ShouldReturnFalse()
    {
      bool result = _vendingMachine.BuyDrink("Fanta", -5);

      Assert.IsFalse(result);
    }

    [Test]
    public void BuyDrink_ZeroQuantity_ShouldReturnFalse()
    {
      bool result = _vendingMachine.BuyDrink("Coca Cola", 0);

      Assert.IsFalse(result);
    }
  }
}
