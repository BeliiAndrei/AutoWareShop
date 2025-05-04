using WebShop.Models.User;

namespace WebShop.Models.Order
{
    public class OrderDetailsModel
    {
        public OrderModel orderModel {  get; set; }
        public UserInfoModel userInfo { get; set; }
        public DeliveryViewModel deliveryInfo { get; set; }
    }
}