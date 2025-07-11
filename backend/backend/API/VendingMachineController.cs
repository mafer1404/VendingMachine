using backend.Domain;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class VendingMachineController : ControllerBase
  {
    private readonly VendingMachineService _vendingMachineService;

    public VendingMachineController(VendingMachineService vendingMachineService)
    {
      _vendingMachineService = vendingMachineService;
    }

    [HttpGet("Drinks")]
    public IActionResult GetDrinks()
    {
      var drinks = _vendingMachineService.GetAllDrinks();
      var noMoreAvailableCoins = _vendingMachineService.MachineOutOfService();

      return Ok(new
      {
        drinks,
        noMoreAvailableCoins
      });
    }

    [HttpPost("Buy")]
    public IActionResult BuyDrink([FromBody] PurchaseRequestModel request)
    {
      var result = _vendingMachineService.ProcessPurchase(request.Items
        , request.AmountPaid);

      if (!result.Success)
        return BadRequest(result.Message);

      return Ok(new
      {
        message = $"Compra realizada con éxito. Su vuelto es " +
        $"de {result.ChangeAmount} colones.",
        desglose = result.Breakdown.ToDictionary(
              kvp => $"₡{kvp.Key}",
              kvp => $"{kvp.Value} {(kvp.Value == 1 ? "moneda" : "monedas")}"
          )
      });
    }
  }

}
