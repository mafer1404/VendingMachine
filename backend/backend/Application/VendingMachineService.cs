using backend.Domain;

public class VendingMachineService
{
  private readonly VendingMachine _vendingMachine;
  private readonly ChangeCalculator _changeCalculator;

  public VendingMachineService()
  {
    _vendingMachine = new VendingMachine();
    _changeCalculator = new ChangeCalculator();
  }

  public List<DrinkModel> GetAllDrinks() => _vendingMachine.GetAllDrinks();

  public bool MachineOutOfService()
  {
    bool noMoreAvailableCoins = _changeCalculator.MachineOutOfCoins();

    return noMoreAvailableCoins;
  }
  public ChangeResultModel ProcessPurchase(List<PurchaseItemModel> items
    , int amountPaid)
  {
    if (items == null || items.Count == 0)
      return new ChangeResultModel { Success = false, Message = "Error: No se" +
        " especificaron bebidas para comprar." };

    int totalCost = 0;

    foreach (var item in items)
    {
      if (string.IsNullOrEmpty(item.Name))
        return new ChangeResultModel { Success = false, Message = "Error: Nombre de" +
          " bebida inválido." };

      if (item.Quantity <= 0)
        return new ChangeResultModel { Success = false, Message = $"Error: Cantidad" +
          $" inválida para {item.Name}." };

      var drink = _vendingMachine.GetAllDrinks().FirstOrDefault(drink => drink.Name == item.Name);
      if (drink == null)
        return new ChangeResultModel { Success = false, Message = $"Error: Bebida" +
          $" {item.Name} no encontrada." };

      if (drink.Quantity < item.Quantity)
        return new ChangeResultModel { Success = false, Message = $"Error: No hay" +
          $" suficiente cantidad disponible de {item.Name}." };

      totalCost += drink.Price * item.Quantity;
    }

    var changeResult = _changeCalculator.Calculate(totalCost, amountPaid);
    if (!changeResult.Success)
      return changeResult;

    foreach (var item in items)
    {
      bool updated = _vendingMachine.BuyDrink(item.Name, item.Quantity);
      if (!updated)
        return new ChangeResultModel { Success = false, Message = $"Error al " +
          $"actualizar inventario para {item.Name}." };
    }
    return changeResult;
  }
}
