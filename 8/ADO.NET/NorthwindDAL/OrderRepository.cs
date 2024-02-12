using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using NorthwindDAL.Model;

namespace NorthwindDAL
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DbConnection _connection;

        public OrderRepository(string providerFactory, string connectionString)
        {
            DbProviderFactories.RegisterFactory("System.Data.SqlClient", SqlClientFactory.Instance);

            _connection = DbProviderFactories.GetFactory(providerFactory).CreateConnection();
            ArgumentNullException.ThrowIfNull(_connection);

            _connection.ConnectionString = connectionString;
            _connection.Open();
        }

        public IEnumerable<Order> GetOrders()
        {
            var orders = new List<Order>();
            using var reader = GetCommand("select * from dbo.Orders").ExecuteReader();
            while (reader.Read())
            {
                var order = new Order()
                {
                    Id = (int)reader["OrderID"],
                    Date = reader["OrderDate"] is DBNull ? null : (DateTime)reader["OrderDate"],
                    ShippedDate = reader["ShippedDate"] is DBNull ? null : (DateTime)reader["ShippedDate"],
                };
                order.Status = order.Date == null ? OrderStatus.New :
                    order.ShippedDate == null ? OrderStatus.InProgress : OrderStatus.Resolve;

                orders.Add(order);
            }
            return orders;
        }
        
        public IEnumerable<OrderDetail> GetOrderDetails(int orderId)
        {
            using var reader = GetCommand(
                "select od.OrderID, od.ProductID, od.UnitPrice, od.Quantity, p.ProductName " +
                "from dbo.[Order Details] od " +
                "inner join dbo.Products p on od.ProductID = p.ProductID " +
                $"where od.OrderID = {orderId}")
                .ExecuteReader();
            
            var listDetails = new List<OrderDetail>();
            while (reader.Read())
            {
                listDetails.Add(new OrderDetail()
                {
                    OrderID = orderId,
                    Product = new Product()
                    { Id = (int)reader["ProductID"], Name = (string)reader["ProductName"] },
                    Quantity = (short)reader["Quantity"]
                });
            }
            return listDetails;
        }
        
        public int CreateOrder(Order order)
        {
            var commandText = "insert into Orders(OrderDate, CustomerID) " +
                              $"values ('{order.Date ?? DateTime.Now}', '{order.CustomerId}')";
            
            return GetCommand(commandText).ExecuteNonQuery();
        }
        
        public void UpdateOrder(Order order)
        {
            if (order.Status != OrderStatus.New) return;
            
            var commandText = $"select OrderDate from dbo.Orders where OrderID = {order.Id}";
            using var reader = GetCommand(commandText).ExecuteReader();
            reader.Read();
            if (reader["OrderDate"] is not DBNull) return;

            commandText = $"update Orders set CustomerID='{order.CustomerId}' " +
                          $"where OrderID = {order.Id}";
            
            GetCommand(commandText).ExecuteNonQuery();
        }
        
        public void DeleteOrder(Order order)
        {
            if (order.Status == OrderStatus.Resolve) return;
            
            var commandText = $"select ShippedDate from dbo.Orders where OrderID = {order.Id}";
            using var reader = GetCommand(commandText).ExecuteReader();
            reader.Read();
            if (reader["ShippedDate"] is not DBNull) return;
            
            commandText = $"delete from Orders where OrderID = {order.Id}";
            GetCommand(commandText).ExecuteNonQuery();
        }

        public void ChangeStatus(int orderId, OrderStatus newStatus)
        {
            string commandText;
            switch (newStatus)
            {
                case OrderStatus.InProgress:
                    commandText = $"update Orders set OrderDate='{DateTime.Now}' ";
                    break;
                case OrderStatus.Resolve:
                    commandText = $"update Orders set ShippedDate='{DateTime.Now}' ";
                    break;
                case OrderStatus.New:
                default:
                    return;
            }
            commandText += $"where OrderID = {orderId}";
            GetCommand(commandText).ExecuteNonQuery();
        }
        
        public IEnumerable<OrderHistory> GetOrderHistory(string customerId)
        {
            var orders = new List<OrderHistory>();
            using var command = _connection.CreateCommand();
            command.CommandText = "CustOrderHist";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add( new SqlParameter()
            {
                ParameterName = "@CustomerID",
                Direction = ParameterDirection.Input,
                Value = customerId
            });
            
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                orders.Add(new OrderHistory()
                {
                    ProductName = (string)reader["ProductName"],
                    Total = (int)reader["Total"]
                });
            }
            return orders;
        }

        public IEnumerable<CustomerOrderDetail> GetCustomerOrderDetail(int orderId)
        {
            var details = new List<CustomerOrderDetail>();
            using var command = _connection.CreateCommand();
            command.CommandText = "CustOrdersDetail";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add( new SqlParameter()
            {
                ParameterName = "@OrderID",
                Direction = ParameterDirection.Input,
                Value = orderId
            });
            
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                details.Add(new CustomerOrderDetail()
                {
                    ProductName = (string)reader["ProductName"],
                    Price = (decimal)reader["UnitPrice"],
                    Quantity = (short)reader["Quantity"],
                    Sum = (decimal)reader["ExtendedPrice"]
                });
            }
            return details;
        }

        private DbCommand GetCommand (string commandText)
        {
            using var command = _connection.CreateCommand();
            command.CommandText = commandText;
            command.CommandType = CommandType.Text;
            return command;
        }

        ~OrderRepository()
        {
            _connection.Close();
        }
    }
}