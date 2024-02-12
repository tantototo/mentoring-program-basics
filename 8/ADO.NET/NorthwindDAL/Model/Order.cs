namespace NorthwindDAL.Model
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? ShippedDate { get; set; }
        public OrderStatus Status { get; set; }
        public string? CustomerId { get; set; }

        public IEnumerable<OrderDetail> Details {get; set;}
    }
}