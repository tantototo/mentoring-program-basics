using System.Linq.Expressions;
using LinqToDB;
using LinqToDB.Mapping;

namespace Linq2DB.Northwind.Context.Model;

[Table("Products")]
public class Product
{
    [Column("ProductID"), Identity, PrimaryKey]
    public int Id { get; set; }
    
    [Column("ProductName")]
    public string Name { get; set; }
    
    //[Association(QueryExpressionMethod = nameof(CategoryImpl))]
    [Association(ThisKey = nameof(CategoryID), OtherKey = nameof(Category.Id))]
    public Category Category { get; set; }
    [Column]
    public int CategoryID { get; set; }
    
    [Association(ThisKey = nameof(SupplierID), OtherKey = nameof(Supplier.Id))]
    public Supplier Supplier { get; set; }
    [Column]
    public int SupplierID { get; set; }
    
    //func
    private static Expression<Func<Product, IDataContext, IQueryable<Category>>> CategoryImpl()   
    {
        return (m, db) => db.GetTable<Category>()
            .Where(c => c.Id == Sql.Property<int>(m, nameof(CategoryID)));
    }
    
    private static Expression<Func<Product, IDataContext, IQueryable<Supplier>>> SupplierImpl()   
    {
        return (m, db) => db.GetTable<Supplier>()
            .Where(c => c.Id == Sql.Property<int>(m, nameof(SupplierID)));
    }
}