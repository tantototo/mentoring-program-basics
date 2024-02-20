using LinqToDB.Mapping;

namespace Linq2DB.Northwind.Context.Model;

[Table("Order Details")]
public class OrderDetail
{
    [Column]
    public int OrderID { get; set; }
    [Association(ThisKey = nameof(OrderID), OtherKey = nameof(Order.Id))]
    public Order Order { get; set; }
    
    [Column]
    public int ProductID { get; set; }
    [Association(ThisKey = nameof(ProductID), OtherKey = nameof(Product.Id))]
    public Product Product { get; set; }
    
    [Column]
    public decimal UnitPrice { get; set; }
    [Column]
    public int Quantity { get; set; }
    [Column]
    public float Discount { get; set; }
}