using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModels;
using SalesWebMVC.Services;

namespace SalesWebMVC.Controllers
{
  public class SellersController : Controller
  {
    private readonly SellerService _sellerService;
    private readonly DepartmentService _departmentservice;

    public SellersController(SellerService sellerService, DepartmentService departmentservice)
    {
      _sellerService = sellerService;
      _departmentservice = departmentservice;
    }
    public IActionResult Index()
    {
      var list = _sellerService.FindAll();
      ViewData["Message"] = DateTime.Now;
      return View(list);
    }
    public IActionResult Create()
    {
      var departments = _departmentservice.FindAll();
      var viewModel = new SellerFormViewModel() 
      {
        Departments = departments
      };
      return View(viewModel);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Seller seller) 
    {
      _sellerService.Insert(seller);
      return RedirectToAction(nameof(Index));
    }
  }
}
