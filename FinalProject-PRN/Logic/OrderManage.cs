using FinalProject_PRN.Models;
namespace FinalProject_PRN.Logic
{
    public class OrderManage
    {
        public static void addOrder(Order o, List<CartItem> carts) {
            using (var context = new music_storeContext()) { 
                context.Orders.Add(o);
                context.SaveChanges();
                Order order = context.Orders.OrderByDescending(o => o.OrderId).FirstOrDefault();
                List<OrderDetail> od = new List<OrderDetail>();
                decimal total = 0;
                foreach (CartItem item in carts) {
                    item.OrderId = order.OrderId;
                    decimal quan = item.Quantity;
                    total += quan * item.UnitPrice;
                    od.Add(item);
                }
                o.Total = total;
                context.Orders.Update(o);
                context.SaveChanges();
                context.OrderDetails.AddRange(od);
                context.SaveChanges();
            }
        }
        public static List<Order> GetClientOrders(int id)
        {
            using (var context = new music_storeContext()) { 
                return context.Orders.Where(x => x.UserId == id).ToList();
            }
        }
        public static Order GetOrderById(int id)
        {
            using (var context = new music_storeContext())
            {
                return context.Orders.Where(x => x.OrderId == id).FirstOrDefault();
            }
        }
    }
}
