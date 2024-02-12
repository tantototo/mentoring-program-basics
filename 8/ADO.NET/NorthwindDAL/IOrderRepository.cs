using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthwindDAL.Model;

namespace NorthwindDAL
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetOrders();
        IEnumerable<OrderDetail> GetOrderDetails(int orderId);
        int CreateOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(Order order);
        void ChangeStatus(int orderId, OrderStatus newStatus);
        IEnumerable<OrderHistory> GetOrderHistory(string customerId);
        IEnumerable<CustomerOrderDetail> GetCustomerOrderDetail(int orderId);
    }
}
