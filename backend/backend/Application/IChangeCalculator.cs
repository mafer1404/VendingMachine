using backend.Domain;
public interface IChangeCalculator
{
  bool MachineOutOfCoins();
  ChangeResultModel Calculate(int totalCost, int amountPaid);
}
