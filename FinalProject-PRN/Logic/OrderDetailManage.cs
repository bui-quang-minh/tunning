using FinalProject_PRN.Models;

namespace FinalProject_PRN.Logic
{
    public class OrderDetailManage
    {
        public static List<OrderDetail> GetClientOrderDetails(List<Order> list) {
            List <OrderDetail> orderDetails = new List <OrderDetail>();
            using (var context = new music_storeContext()) { 
                foreach (var item in list)
                {
                    orderDetails.AddRange(context.OrderDetails.Where(x => x.OrderId == item.OrderId).ToList());
                }
                return orderDetails;
            }
        }

        public static List<OrderDetail> GetClientOrderDetails(int oid)
        {

            using (var context = new music_storeContext())
            {
                return context.OrderDetails.Where(x => x.OrderId == oid).ToList();

            }
        }
    }
}
