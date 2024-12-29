using Data.Constants;
using Data.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace Data.Repositories;

[Authorize(Roles = nameof(Roles.Admin))]
public class ReportRepository : IReportRepository
{
    private readonly ApplicationDbContext _context;
    public ReportRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    //public async Task<IEnumerable<TopNSoldProductModel>> GetTopNSellingProductsByDate(DateTime startDate, DateTime endDate)
    //{
    //    var startDateParam = new SqlParameter("@startDate", startDate);
    //    var endDateParam = new SqlParameter("@endDate", endDate);
    //    // var topFiveSoldProducts = await _context.Database.FromSqlRaw<TopNSoldProductModel>("exec Usp_GetTopNSellingProductsByDate @startDate,@endDate", startDateParam, endDateParam).ToListAsync();
    //    // return topFiveSoldProducts;

    //    var result = await _context.Set<TopNSoldProductModel>()
    //    .FromSqlRaw("exec Usp_GetTopNSellingProductsByDate @startDate,@endDate", startDateParam, endDateParam)
    //    .ToListAsync();

    //    return result;

    //}

    public async Task<IEnumerable<dynamic>> GetTopNSellingProductsByDate(DateTime startDate, DateTime endDate)
    {
        var unitSold =
            from o in _context.Orders
            join od in _context.OrderDetails on o.Id equals od.OrderId
            where o.IsPaid == true && o.IsDeleted == false
                  && o.CreateDate >= startDate && o.CreateDate <= endDate
            group od by od.ProductId into g
            select new
            {
                ProductId = g.Key,
                TotalUnitSold = g.Sum(od => od.Quantity)
            };

        var topSellingProducts =
            (from us in unitSold
             join p in _context.Products on us.ProductId equals p.Id
             orderby us.TotalUnitSold descending
             select new
             {
                 p.Name,
                 us.TotalUnitSold
             })
            .Take(5);

        return topSellingProducts;
    }


}

public interface IReportRepository
{
    Task<IEnumerable<dynamic>> GetTopNSellingProductsByDate(DateTime startDate, DateTime endDate);
}