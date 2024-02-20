using LinqToDB.Mapping;

namespace Linq2DB.Northwind.Context.Model;

[Table("Orders")]
public class Order
{
    [Column("OrderID"), Identity, PrimaryKey]
    public int Id { get; set; }

    [Column]
    public int EmployeeID { get; set; }
    [Association(ThisKey = nameof(EmployeeID), OtherKey = nameof(Employee.Id))]
    public Employee Employee { get; set; }

    [Column]
    public DateTime? OrderDate { get; set; }
    [Column]
    public DateTime? ShippedDate { get; set; }
    
    [Column("ShipVia")]
    public int? ShipId { get; set; }
    [Association(ThisKey = nameof(ShipId), OtherKey = nameof(Shipper.Id))]
    public Shipper? Shipper { get; set; }
    
    [Association(ThisKey = nameof(Id), OtherKey = nameof(OrderDetail.OrderID))]
    public virtual ICollection<OrderDetail> Details { get; set; }
}