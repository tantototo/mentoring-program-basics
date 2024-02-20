using LinqToDB.Mapping;

namespace Linq2DB.Northwind.Context.Model;

[Table("Region")]
public class Region
{
    [Column("RegionID"), Identity, PrimaryKey]
    public int Id { get; set; }
    
    [Column("RegionDescription")]
    public string Description { get; set; }
}