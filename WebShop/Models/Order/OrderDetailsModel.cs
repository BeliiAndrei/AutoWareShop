using WebShop.Domain.User.Admin;

namespace WebShop.Models.Order
{
    public class OrderDetailsModel
    {
        public OrderModel orderModel {  get; set; }
        public UserInfo userInfo { get; set; }
        public DeliveryViewModel deliveryInfo { get; set; }
    }
}