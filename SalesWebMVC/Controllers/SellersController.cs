using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModels;
using SalesWebMVC.Services;
using SalesWebMVC.Services.Exceptions;

namespace SalesWebMVC.Controllers
{
  using System.Diagnostics;

  public class SellersController : Controller
  {
    private readonly SellerService _sellerService;
    private readonly DepartmentService _departmentService;

    public SellersController(SellerService sellerService, DepartmentService departmentservice)
    {
      _sellerService = sellerService;
      _departmentService = departmentservice;
    }
    public IActionResult Index()
    {
      var list = _sellerService.FindAll();
      ViewData["Message"] = DateTime.Now;
      return View(list);
    }
    public IActionResult Create()
    {
      var departments = _departmentService.FindAll();
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
    public IActionResult Delete(int? id)
    {
      if (id == null)
      {
        return RedirectToAction(nameof(Error), new { message = "Id not provided"});
      }
      var obj = _sellerService.FindById(id.Value);
      if (obj == null)
      {
        return RedirectToAction(nameof(Error), new { message = "Id not found"});
      }
      return View(obj);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
      _sellerService.Remove(id);
      return RedirectToAction(nameof(Index));
    }

    public IActionResult Details(int? id)
    {
      if (id == null)
      {
        return RedirectToAction(nameof(Error), new { message = "Id not provided"});
      }
      var obj = _sellerService.FindById(id.Value);
      if (obj == null)
      {
        return RedirectToAction(nameof(Error), new { message = "Id not found"});
      }
      return View(obj);
    }

    public IActionResult Edit(int? Id)
    {
      if (Id == null)
      {
        return RedirectToAction(nameof(Error), new { message = "Id not provided"});
      }
      var obj = _sellerService.FindById(Id.Value);
      if (obj == null)
      {
        return RedirectToAction(nameof(Error), new { message = "Id not found"});
      }
      List<Department> departments = _departmentService.FindAll();
      SellerFormViewModel viewModel = new SellerFormViewModel() { Seller = obj, Departments = departments };
      return View(viewModel);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int Id, Seller seller)
    {
      if (Id != seller.Id)
      {
        return RedirectToAction(nameof(Error), new { message = "Id mismatch"});
      }
      try
      {
        _sellerService.Update(seller);
        return RedirectToAction(nameof(Index));
      }
      catch (ApplicationException e)
      {
        return RedirectToAction(nameof(Error), new { message = e.Message});
      }
    }
    public IActionResult Error(string message)
    {
      var errorViewModel = new ErrorViewModel()
      {
        Message = message,
        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
      };
      return View(errorViewModel);
    }
  }
}
