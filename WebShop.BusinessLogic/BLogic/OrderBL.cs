using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain.Order;
using WebShop.Domain.Product;

namespace WebShop.BusinessLogic.BLogic
{
    public class OrderBL : IOrder
    {
        public Order CreateNewOrder(List<ProductDTO> selectedProducts)
        {
            throw new NotImplementedException();
        }

        public void DeleteOrder(int id)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAllOrders(int userId)
        {
            throw new NotImplementedException();
        }

        public Order GetOrderById(int id)
        {
            throw new NotImplementedException();
        }

        public decimal GetOrderPrice(int id)
        {
            throw new NotImplementedException();
        }

        public void ModifyOrderStatus(int id)
        {
            throw new NotImplementedException();
        }

        public void SortOrdersByDate(List<Order> orders)
        {
            throw new NotImplementedException();
        }
    }
}
