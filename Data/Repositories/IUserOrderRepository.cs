using Data.DTOs;

namespace Data.Repositories;

public interface IUserOrderRepository
{
    Task<IEnumerable<Order>> UserOrders(bool getAll=false, string userId=null);
    Task ChangeOrderStatus(UpdateOrderStatusModel data);
    Task TogglePaymentStatus(int orderId);
    Task<Order?> GetOrderById(int id);
    Task<IEnumerable<OrderStatus>> GetOrderStatuses();

}