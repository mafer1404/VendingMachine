using Microsoft.AspNetCore.Mvc;
using backend.Domain;
using backend.Application;
namespace backend.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class VendingMachineController : ControllerBase
  {
    private readonly VendingMachine _vendingMachineService;

    public VendingMachineController(VendingMachine vendingMachineService)
    {
      _vendingMachineService = vendingMachineService;
    }

    [HttpGet("drinks")]
    public ActionResult<List<DrinkModel>> GetDrinks()
    {
      return Ok(_vendingMachineService.GetAllDrinks());
    }
  }
}
