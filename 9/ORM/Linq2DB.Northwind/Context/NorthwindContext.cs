using Linq2DB.Northwind.Context.Model;
using LinqToDB;
using LinqToDB.Data;
using Microsoft.Extensions.Configuration;

namespace Linq2DB.Northwind.Context;

public class NorthwindContext : DataConnection
{
    public NorthwindContext() : //base(OnCreating()) { } ?
        base("System.Data.SqlClient", 
        "data source=Tkacheva;initial catalog=Northwind;integrated security=True;" +
        "multipleactiveresultsets=True;App=EntityFramework") { }

    public ITable<Category> Categories => this.GetTable<Category>();
    public ITable<Product> Products => this.GetTable<Product>();
    public ITable<Supplier> Suppliers => this.GetTable<Supplier>();
    public ITable<Employee> Employees => this.GetTable<Employee>();
    public ITable<EmployeeTerritory> EmployeeTerritories => this.GetTable<EmployeeTerritory>();
    public ITable<Territory> Territories => this.GetTable<Territory>();
    public ITable<Region> Regions => this.GetTable<Region>();
    public ITable<Order> Orders => this.GetTable<Order>();
    public ITable<Shipper> Shippers => this.GetTable<Shipper>();
    public ITable<OrderDetail> OrderDetails => this.GetTable<OrderDetail>();

    private static IConfigurationSection OnCreating()
    {
        var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
        var config = builder.Build();
        return config.GetSection("NorthwindContext");
    }
}