using Moq;
using backend.Domain;

namespace Tests
{
  [TestFixture]
  public class VendingMachineServiceTests
  {
    private Mock<IVendingMachine> _vendingMachineMock;
    private Mock<IChangeCalculator> _changeCalculatorMock;
    private VendingMachineService _service;

    [SetUp]
    public void Setup()
    {
      _vendingMachineMock = new Mock<IVendingMachine>();
      _changeCalculatorMock = new Mock<IChangeCalculator>();
      _service = new VendingMachineService(_vendingMachineMock.Object, _changeCalculatorMock.Object);
    }

    [Test]
    public void GetAllDrinks_ShouldReturnDrinksFromVendingMachine()
    {
      var drinks = new List<DrinkModel> { new DrinkModel { Name = "Test", Quantity = 1, Price = 100 } };
      _vendingMachineMock.Setup(vm => vm.GetAllDrinks()).Returns(drinks);

      var result = _service.GetAllDrinks();

      Assert.AreEqual(drinks, result);
    }

    [Test]
    public void MachineOutOfService_ShouldReturnTrueWhenChangeCalculatorReturnsTrue()
    {
      _changeCalculatorMock.Setup(cc => cc.MachineOutOfCoins()).Returns(true);

      Assert.IsTrue(_service.MachineOutOfService());
    }


    [Test]
    public void ProcessPurchase_NoItems_ReturnsError()
    {
      var result = _service.ProcessPurchase(null, 1000);

      Assert.IsFalse(result.Success);
      Assert.That(result.Message, Does.Contain("No se especificaron bebidas"));
    }

    [Test]
    public void ProcessPurchase_ItemNameEmpty_ReturnsError()
    {
      var items = new List<PurchaseItemModel>
        {
            new PurchaseItemModel { Name = "", Quantity = 1 }
        };

      var result = _service.ProcessPurchase(items, 1000);

      Assert.IsFalse(result.Success);
      Assert.That(result.Message, Does.Contain("Nombre de bebida inválido"));
    }

    [Test]
    public void ProcessPurchase_QuantityZeroOrNegative_ReturnsError()
    {
      var items = new List<PurchaseItemModel>
        {
            new PurchaseItemModel { Name = "Pepsi", Quantity = 0 }
        };

      var result = _service.ProcessPurchase(items, 1000);

      Assert.IsFalse(result.Success);
      Assert.That(result.Message, Does.Contain("Cantidad inválida"));
    }

    [Test]
    public void ProcessPurchase_DrinkNotFound_ReturnsError()
    {
      _vendingMachineMock.Setup(vm => vm.GetAllDrinks()).Returns(new List<DrinkModel>());

      var items = new List<PurchaseItemModel>
        {
            new PurchaseItemModel { Name = "Pepsi", Quantity = 1 }
        };

      var result = _service.ProcessPurchase(items, 1000);

      Assert.IsFalse(result.Success);
      Assert.That(result.Message, Does.Contain("no encontrada"));
    }

    [Test]
    public void ProcessPurchase_NotEnoughQuantity_ReturnsError()
    {
      var drinks = new List<DrinkModel>
        {
            new DrinkModel { Name = "Pepsi", Quantity = 1, Price = 750 }
        };
      _vendingMachineMock.Setup(vm => vm.GetAllDrinks()).Returns(drinks);

      var items = new List<PurchaseItemModel>
        {
            new PurchaseItemModel { Name = "Pepsi", Quantity = 2 }
        };

      var result = _service.ProcessPurchase(items, 2000);

      Assert.IsFalse(result.Success);
      Assert.That(result.Message, Does.Contain("suficiente cantidad"));
    }

    [Test]
    public void ProcessPurchase_ChangeCalculatorFails_ReturnsFailure()
    {
      var drinks = new List<DrinkModel>
        {
            new DrinkModel { Name = "Pepsi", Quantity = 10, Price = 750 }
        };
      _vendingMachineMock.Setup(vm => vm.GetAllDrinks()).Returns(drinks);

      var items = new List<PurchaseItemModel>
        {
            new PurchaseItemModel { Name = "Pepsi", Quantity = 2 }
        };

      _changeCalculatorMock.Setup(cc => cc.Calculate(It.IsAny<int>(), It.IsAny<int>()))
          .Returns(new ChangeResultModel
          {
            Success = false,
            Message = "No hay monedas suficientes para el vuelto."
          });

      var result = _service.ProcessPurchase(items, 1000);

      Assert.IsFalse(result.Success);
      Assert.That(result.Message, Does.Contain("monedas suficientes"));
    }

    [Test]
    public void ProcessPurchase_SuccessfulPurchase_UpdatesInventoryAndReturnsSuccess()
    {
      var drinks = new List<DrinkModel>
        {
            new DrinkModel { Name = "Pepsi", Quantity = 10, Price = 750 }
        };
      _vendingMachineMock.Setup(vm => vm.GetAllDrinks()).Returns(drinks);

      var items = new List<PurchaseItemModel>
        {
            new PurchaseItemModel { Name = "Pepsi", Quantity = 2 }
        };

      _changeCalculatorMock.Setup(cc => cc.Calculate(1500, 2000))
          .Returns(new ChangeResultModel
          {
            Success = true,
            ChangeAmount = 500,
            Breakdown = new Dictionary<int, int> { { 500, 1 } }
          });

      _vendingMachineMock.Setup(vm => vm.BuyDrink("Pepsi", 2)).Returns(true);

      var result = _service.ProcessPurchase(items, 2000);

      Assert.IsTrue(result.Success);
      Assert.AreEqual(500, result.ChangeAmount);
      Assert.IsTrue(result.Breakdown.ContainsKey(500));

      _vendingMachineMock.Verify(vm => vm.BuyDrink("Pepsi", 2), Times.Once);
    }
  }
}
