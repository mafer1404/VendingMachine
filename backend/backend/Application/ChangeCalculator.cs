using backend.Domain;

public class ChangeCalculator : IChangeCalculator
{
  private Dictionary<int, int> coinInventory = new Dictionary<int, int>()
  {
    { 500, 20 },
    { 100, 30 },
    { 50, 50 },
    { 25, 25 }
  };
  public bool MachineOutOfCoins()
  {
    bool noMoreCoins =
        coinInventory[500] <= 0 &&
        coinInventory[100] <= 0 &&
        coinInventory[50] <= 0 &&
        coinInventory[25] <= 0;
    return noMoreCoins;
  }

  public ChangeResultModel Calculate(int totalCost, int amountPaid)
  {
    int changeToGive = amountPaid - totalCost;

    if (changeToGive < 0)
      return new ChangeResultModel { Success = false
        , Message = "Error: Dinero insuficiente para completar la compra." };

    var breakdown = new Dictionary<int, int>();
    int remaining = changeToGive;

    var coins = new List<int> { 500, 100, 50, 25 };

    foreach (var coin in coins)
    {
      int needed = remaining / coin;
      if (needed > 0)
      {
        int available = coinInventory.ContainsKey(coin)
          ? coinInventory[coin] : 0;
        int used = Math.Min(needed, available);
        if (used > 0)
        {
          breakdown[coin] = used;
          remaining -= used * coin;
        }
      }
    }

    if (remaining > 0)
    {
      return new ChangeResultModel
      {
        Success = false,
        Message = "Fallo al realizar la compra: no hay monedas " +
        "suficientes para el vuelto."
      };
    }

    foreach (var kvp in breakdown)
      coinInventory[kvp.Key] -= kvp.Value;

    return new ChangeResultModel
    {
      Success = true,
      ChangeAmount = changeToGive,
      Breakdown = breakdown
    };
  }
}
