using backend.Domain;

namespace backend.Application
{
  public interface IVendingMachineService
  {
    List<DrinkModel> GetAllDrinks();
    bool MachineOutOfService();
    ChangeResultModel ProcessPurchase(List<PurchaseItemModel> items, int amountPaid);
  }

}
