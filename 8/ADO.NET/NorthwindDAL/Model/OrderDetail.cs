namespace NorthwindDAL.Model;

public class OrderDetail
{
    public int OrderID { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }
}