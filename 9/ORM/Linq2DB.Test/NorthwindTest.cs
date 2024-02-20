using Linq2DB.Northwind.Context;
using Linq2DB.Northwind.Context.Model;
using LinqToDB;

namespace Linq2DB.Test;

public class NorthwindTest
{
    [Fact]
    public void GetProducts_Success()
    {
        // Arrange
        using var db = new NorthwindContext();

        // Act
        //var product = db.Products.Include(p => p.Category).ToList(); // ?
        var products = db.Products
            .LoadWith(x => x.Category)
            .LoadWith(x => x.Supplier)
            .ToList();

        // Assert
        Assert.True(products.Count != 0);
    }
    
    [Fact]
    public void GetEmployeeRegion_Success()
    {
        // Arrange
        using var db = new NorthwindContext();

        // Act ?
        var employees = (from emp in db.Employees
                join eter in db.EmployeeTerritories on emp.Id equals eter.EmployeeID 
                join ter in db.Territories on eter.TerritoryID equals ter.Id
                join reg in db.Regions on ter.RegionID equals reg.Id
                select new { emp, reg })
            .Distinct();

        // Assert
        Assert.True(employees.Any());
    }
    
    [Fact]
    public void GetRegionStatistic_Success()
    {
        // Arrange
        using var db = new NorthwindContext();

        // Act ?
        var regions = (from emp in db.Employees
                // join eter in db.EmployeeTerritories on emp.Id equals eter.EmployeeID
                // join ter in db.Territories on eter.TerritoryID equals ter.Id
                // join reg in db.Regions on ter.RegionID equals reg.Id
                from eter in db.EmployeeTerritories.Where(et => et.EmployeeID == emp.Id)
                from ter in db.Territories.Where(t => t.Id == eter.TerritoryID)
                from reg in db.Regions.Where(r => r.Id == ter.RegionID)
                select new { emp, reg })
            .Distinct().GroupBy(r => r.reg)
            .Select(r => new
            {
                Region = r.Key,
                Count = r.Count()
            });

        // Assert
        Assert.True(regions.Any());
    }
    
    [Fact]
    public void GetEmployeeShippers_Success()
    {
        // Arrange
        using var db = new NorthwindContext();

        // Act
        var employees = db.Orders
            .LoadWith(x => x.Employee)
            .LoadWith(x => x.Shipper)
            .Select(e => new { e.Employee, e.Shipper })
            .Distinct() //.ToList()
            .GroupBy(e => e.Employee)
            .Select(e => new
            {
                Employee = e.Key,
                Count = e.Count(),
                // Shippers = e.Select(s => s.Shipper) ?
            });
        
        // Assert
        Assert.True(employees.Any());
    }
    
    [Fact]
    public void CreateEmployeeWithTerritory_()
    {
        // Arrange
        using var db = new NorthwindContext();
        var employee = new Employee()
        {
            FirstName = "Tom",
            LastName = "Mass"
        };
        var territory = new Territory()
        {
            // Id = "1541", ?
            Description = "Oregon",
            RegionID = 2
        };

        // Act ?
        db.Insert(territory);
        var employeeId = db.Insert(employee);
        db.Insert(new EmployeeTerritory()
        {
            EmployeeID = employeeId,
            TerritoryID = territory.Id
        });
    }
    
    [Fact]
    public void ChangeProductCategory_Success()
    {
        // Arrange
        using var db = new NorthwindContext();
        var oldCategory = 4;
        var newCategory = 5;

        // Act
        db.Products
            .Where(p => p.CategoryID == oldCategory)
            .Set(p => p.CategoryID, newCategory)
            .Update();
    }
    
    [Fact]
    public void CreateProducts_()
    {
        // Arrange
        using var db = new NorthwindContext();
        var category = new Category() { Name = "Spices" };
        var supp = new Supplier() { CompanyName = "SuperSpices" };
        var products = new List<Product>()
        {
            new()
            {
                Name = "Paper",
                Category = category,
                Supplier = supp
                    
            },
            new()
            {
                Name = "Salt",
                Category = category,
                Supplier = supp,
            },
            new()
            {
                Name = "Cocktail umbrellas",
                Category = new Category(){ Name = "Other"},
                Supplier = new Supplier() { ContactName = "Vik", CompanyName = "Mai tai roa ae"},
            },
            new()
            {
                Name = "Balsamic",
                Category = new Category(){ Name = "Condiments"},
                Supplier = new Supplier() { CompanyName = "Exotic Liquids"},
            },
        };

        // Act 
        // db.Insert(products); ?
        foreach (var product in products)
        {
            var oldProduct = db.Products.FirstOrDefault(c => c.Name == product.Name);
            if (oldProduct is not null)
                continue;
            
            var oldCategory = db.Categories.FirstOrDefault(c => c.Name == product.Category.Name);
            var oldSupplier = db.Suppliers.FirstOrDefault(c => c.CompanyName == product.Supplier.CompanyName);
            
            // ?
            var catId = oldCategory?.Id ?? db.Insert(product.Category);
            var supId = oldSupplier?.Id ?? db.Insert(product.Supplier);
            
            db.Insert(new Product()
            {
                Name = product.Name,
                CategoryID = catId,
                SupplierID = supId
            });
        }
    }
    
    [Fact]
    public void ChangeProductInOrder_Success()
    {
        // Arrange
        using var db = new NorthwindContext();
        var productId = 1;
        var analogId = 24;

        // Act (order 11070)
        db.OrderDetails
            .LoadWith(o => o.Order)
            .Where(p => p.Order.ShippedDate == null && p.ProductID == productId)
            .Set(p => p.ProductID, analogId)
            .Update();
    }
}