namespace backend.Domain
{
  public class ChangeResultModel
  {
    public bool Success { get; set; }
    public int ChangeAmount { get; set; }
    public Dictionary<int, int> Breakdown { get; set; }
    public string Message { get; set; }
  }
}
