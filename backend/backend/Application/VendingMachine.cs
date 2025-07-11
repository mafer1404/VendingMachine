using backend.Domain;

namespace backend.Domain
{
  public class VendingMachine : IVendingMachine
  {
    private List<DrinkModel> _drinks;

    public VendingMachine()
    {
      _drinks = new List<DrinkModel>
        {
            new DrinkModel { Name = "Sprite", Quantity = 15, Price = 975 },
            new DrinkModel { Name = "Fanta", Quantity = 10, Price = 950 },
            new DrinkModel { Name = "Pepsi", Quantity = 8, Price = 750 },
            new DrinkModel { Name = "Coca Cola", Quantity = 10, Price = 800 }
        };
    }

    public List<DrinkModel> GetAllDrinks() => _drinks;

    public bool BuyDrink(string name, int quantity)
    {
      if (quantity <= 0) return false;

      var drink = _drinks.FirstOrDefault(d => d.Name == name);
      if (drink == null || drink.Quantity < quantity) return false;

      drink.Quantity -= quantity;
      return true;
    }

  }
}
