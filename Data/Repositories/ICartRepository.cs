using Data;
using Data.DTOs;

namespace Data.Repositories
{
    public interface ICartRepository
    {
        Task<int> AddItem(int productId, int qty, string user = null);
        Task<int> RemoveItem(int productId, string user);
        Task<ShoppingCart> GetUserCart(string user);
        Task<int> GetCartItemCount(string userId = "");
        Task<ShoppingCart> GetCart(string userId);
        Task<bool> DoCheckout(CheckoutModel model);
    }
}
