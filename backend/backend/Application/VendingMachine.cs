using backend.Domain;
namespace backend.Application
{
  public class VendingMachine
  {
    private List<DrinkModel> _drinks;

    public VendingMachine()
    {
      _drinks = new List<DrinkModel>
        {
            new DrinkModel { Name = "Sprite", Quantity = 15, Price = 975 },
            new DrinkModel { Name = "Fanta", Quantity = 10, Price = 950 },
            new DrinkModel { Name = "Pepsi", Quantity = 8, Price = 750 },
            new DrinkModel { Name = "Coca Cola", Quantity = 10, Price = 800 },
        };
    }

    public List<DrinkModel> GetAllDrinks() => _drinks;

  }
}
