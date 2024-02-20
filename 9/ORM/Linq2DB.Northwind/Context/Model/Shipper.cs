using LinqToDB.Mapping;

namespace Linq2DB.Northwind.Context.Model;

[Table("Shippers")]
public class Shipper
{
    [Column("ShipperID"), Identity, PrimaryKey]
    public int Id { get; set; }
    [Column]
    public string CompanyName { get; set; }
    [Column]
    public string Phone { get; set; }
}