using LinqToDB.Mapping;

namespace Linq2DB.Northwind.Context.Model;

[Table("Categories")]
public class Category
{
    [Column("CategoryID"), Identity, PrimaryKey]
    public int Id { get; set; }
    
    [Column("CategoryName")]
    public string Name { get; set; }
    
    [Column]
    public  string Description { get; set; }
    
    [Association(ThisKey = nameof(Id), OtherKey = nameof(Product.CategoryID))]
    public virtual ICollection<Product> Products { get; set; }
}