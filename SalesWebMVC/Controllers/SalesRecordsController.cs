using Microsoft.AspNetCore.Mvc;

namespace SalesWebMVC.Controllers;

using Services;

public class SalesRecordsController : Controller
  {
    private readonly SalesRecordService _salesRecordService;

    public SalesRecordsController(SalesRecordService salesRecordService)
    {
      _salesRecordService = salesRecordService;
    }

    public IActionResult Index()
    {
      return View();
    }
    public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
    {
      if (!minDate.HasValue)
      {
        minDate = new DateTime(2018,9,1);
      }
      if (!maxDate.HasValue)
      {
        minDate = new DateTime(2018,9,30);
      }
      ViewData["minDate"] = minDate.Value.ToString("dd/MM/yyyy");
      ViewData["maxDate"] = maxDate.Value.ToString("dd/MM/yyyy");
      var result = await _salesRecordService.FindByDateAsync(minDate, maxDate);
      return View(result);
    }
    public IActionResult GroupingSearch()
    {
      return View();
    }
  }
