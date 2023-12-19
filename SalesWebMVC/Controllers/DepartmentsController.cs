using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;

namespace SalesWebMVC.Controllers
{
  public class DepartmentsController : Controller
  {
    public IActionResult Index()
    {
      List<Department> lista = new List<Department>();
      lista.Add(new Department { Id = 1, Name = "Eletronics" });
      lista.Add(new Department { Id = 2, Name = "Fashion" });

      return View(lista);
    }
  }
}
