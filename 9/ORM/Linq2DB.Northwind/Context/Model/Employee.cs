using LinqToDB.Mapping;

namespace Linq2DB.Northwind.Context.Model;

[Table("Employees")]
public class Employee
{
    [Column("EmployeeID"), Identity, PrimaryKey]
    public int Id { get; set; }
    [Column]
    public string LastName { get; set; }
    [Column]
    public string FirstName { get; set; }
    
    //[Association(ThisKey = nameof(Id), OtherKey = nameof(EmployeeTerritory.EmployeeID))] ?
    public ICollection<Territory> Territories { get; set; }
}