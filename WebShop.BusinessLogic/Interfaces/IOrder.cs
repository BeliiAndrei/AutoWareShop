using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Order;
using WebShop.Domain.Product;

namespace WebShop.BusinessLogic.Interfaces
{
    interface IOrder
    {
        Order CreateNewOrder(List <ProductDTO> selectedProducts);

        List<Order> GetAllOrders(int userId);

        Order GetOrderById(int id);

        void DeleteOrder(int id);

        void ModifyOrderStatus(int id);

        decimal GetOrderPrice(int id);

        void SortOrdersByDate(List<Order> orders);


    }
}
