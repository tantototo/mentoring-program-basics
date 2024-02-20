using LinqToDB.Mapping;

namespace Linq2DB.Northwind.Context.Model;

[Table("EmployeeTerritories")]
public class EmployeeTerritory
{
    [Column]
    public int EmployeeID { get; set; }
    [Association(ThisKey = nameof(EmployeeID), OtherKey = nameof(Employee.Id))]
    public Employee Employee { get; set; }

    [Column]
    public string TerritoryID { get; set; }
    [Association(ThisKey = nameof(TerritoryID), OtherKey = nameof(Territory.Id))]
    public Territory Territory { get; set; }
}