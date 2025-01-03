using Data.DTOs;
using Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Controllers;
public class ReportsController : Controller
{
    private readonly IReportRepository _reportRepository;
    public ReportsController(IReportRepository reportRepository)
    {
        _reportRepository = reportRepository;
    }
    public async Task<ActionResult> TopFiveSellingProducts(DateTime? sDate = null, DateTime? eDate = null)
    {
        try
        {
            DateTime startDate = sDate ?? DateTime.UtcNow.AddDays(-7);
            DateTime endDate = eDate ?? DateTime.UtcNow;
            var topFiveSellingProducts = await _reportRepository.GetTopNSellingProductsByDate(startDate, endDate);
            var vm = new TopNSoldProductsVm(startDate, endDate, topFiveSellingProducts);
            return View(vm);
        }
        catch (Exception ex)
        {
            return RedirectToAction("Index", "Home");
        }
    }
}