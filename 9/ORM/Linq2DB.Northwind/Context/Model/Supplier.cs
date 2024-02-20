using LinqToDB.Mapping;

namespace Linq2DB.Northwind.Context.Model;

[Table("Suppliers")]
public class Supplier
{
    [Column("SupplierID"), Identity, PrimaryKey]
    public int Id { get; set; }
    [Column]
    public string CompanyName { get; set; }
    [Column]
    public string ContactName { get; set; }
    [Column]
    public string Address { get; set; }
    [Column]
    public string City { get; set; }
    [Column]
    public string Country { get; set; }
    [Column]
    public string Phone { get; set; }
    
    [Association(ThisKey = nameof(Id), OtherKey = nameof(Product.SupplierID))]
    public virtual ICollection<Product> Products { get; set; }
}