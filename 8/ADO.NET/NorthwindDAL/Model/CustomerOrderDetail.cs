namespace NorthwindDAL.Model;

public class CustomerOrderDetail
{
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal Sum { get; set; }
}