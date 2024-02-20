using LinqToDB.Mapping;

namespace Linq2DB.Northwind.Context.Model;

[Table("Territories")]
public class Territory
{
    [Column("TerritoryID"), Identity, PrimaryKey]
    public string Id { get; set; }
    
    [Column("TerritoryDescription")]
    public string Description { get; set; }
    
    [Column]
    public int RegionID { get; set; }
    [Association(ThisKey = nameof(RegionID), OtherKey = nameof(Region.Id))]
    public virtual Region Region { get; set; }

    //[Association(ThisKey = nameof(Id), OtherKey = nameof(EmployeeTerritory.TerritoryID))] ?
    public ICollection<Employee> Employees { get; set; }
}