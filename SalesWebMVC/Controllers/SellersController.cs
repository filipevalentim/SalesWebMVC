using Microsoft.AspNetCore.Mvc;

namespace SalesWebMVC.Controllers
{
  public class SellersController : Controller
  {

    public IActionResult Index()
    {
      ViewData["Message"] = DateTime.Now;
      return View();
    }
  }
}
