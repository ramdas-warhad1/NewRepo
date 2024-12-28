

using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        private readonly ApplicationDbContext _db;

        public HomeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

       
        public async Task<IEnumerable<Product>> GetProducts(string sTerm = "", int productId = 0)
        {
            sTerm = sTerm.ToLower();
            IEnumerable<Product> products = await (from product in _db.Products
                        
                         join stock in _db.Stocks
                         on product.Id equals stock.ProductId
                         into Product_stocks
                         from productWithStock in Product_stocks.DefaultIfEmpty()
                         where string.IsNullOrWhiteSpace(sTerm) || (product != null && product.Name.ToLower().StartsWith(sTerm))
                         select new Product
                         {
                             Id = product.Id,
                            
                             Name = product.Name,
                             
                             Price = product.Price,
                            
                             Quantity=productWithStock==null? 0:productWithStock.Quantity
                         }
                         ).ToListAsync();
            if (productId > 0)
            {

                products = products.Where(a => a.Id == productId).ToList();
            }
            return products;

        }
    }
}
