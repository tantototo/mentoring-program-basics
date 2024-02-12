using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace NorthwindDAL.NetFramework
{
    public class OrderRepository
    {
        private readonly NorthwindEntities _db = new NorthwindEntities();

        public OrderRepository()
        {
            // FixEfProviderServicesProblem
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
            // or
            //var type = typeof(System.Data.Entity.SqlServer.SqlProviderServices);
        }

        public IEnumerable<Orders> GetOrders()
        {
            return _db.Orders.ToList();
        }

        public IEnumerable<Order_Details> GetOrderDetails(int orderId)
        {
            return _db.Order_Details
                .Include(nameof(Orders))
                .Include(nameof(Products))
                .Where(d => d.OrderID == orderId).ToList();
        }

        public int AddOrder(Orders order)
        {
            var newOrder = _db.Orders.Add(order);
            _db.SaveChanges();
            return newOrder.OrderID;
        }
    }
}