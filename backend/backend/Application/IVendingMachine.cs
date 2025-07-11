using backend.Domain;
public interface IVendingMachine
{
  List<DrinkModel> GetAllDrinks();
  bool BuyDrink(string name, int quantity);
}
