using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Order;
using WebShop.Domain.Product;

namespace WebShop.BusinessLogic.Interfaces
{
    public interface IOrder
    {
        OrderActionResponse CreateNewOrder(OrderDTO order, int userId, List<int> products);

        List<OrderDTO> GetUserOrders(int userId);
        List<OrderDTO> GetAllOrders();

        OrderDTO GetOrderById(int id);

        void DeleteOrder(int id);

        void ModifyOrderStatus(int id);

        decimal GetOrderPrice(int id);

        void SortOrdersByDate(List<OrderDTO> orders);


    }
}
