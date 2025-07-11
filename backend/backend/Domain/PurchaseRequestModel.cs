namespace backend.Domain
{
  public class PurchaseItemModel
  {
    public string Name { get; set; }
    public int Quantity { get; set; }
  }

  public class PurchaseRequestModel
  {
    public List<PurchaseItemModel> Items { get; set; }
    public int AmountPaid { get; set; }

  }
}
