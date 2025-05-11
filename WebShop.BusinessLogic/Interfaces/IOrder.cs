using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Enumerables;
using WebShop.Domain.Order;
using WebShop.Domain.Product;

namespace WebShop.BusinessLogic.Interfaces
{
    public interface IOrder
    {
        OrderActionResponse CreateNewOrder(OrderDTO order, int userId, List<int> products);

        List<OrderDTO> GetUserOrders(int userId);
        OrderGetAllResponse GetAllOrders(int page, int pageSize);

        OrderDTO GetOrderById(int id);

        OrderDTO UpdateOrderPrice(int orderId, decimal newPrice);

        void DeleteOrder(int id);

        OrderActionResponse ModifyOrderStatus(int id, OrderStatus newStatus);

        decimal GetOrderPrice(int id);

        List<OrderDTO> SortOrdersByDate(List<OrderDTO> orders);


    }
}
