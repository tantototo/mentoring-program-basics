using NorthwindDAL.Model;

namespace NorthwindDAL.Tests;

public class OrderTest
{
    private readonly OrderRepository _repository = new ("System.Data.SqlClient", 
        "data source=ServerName;initial catalog=Northwind;integrated security=True;" +
        "multipleactiveresultsets=True;App=EntityFramework");
    
    [Fact]
    public void GetOrders_Success()
    {
        // Act 
        var result = _repository.GetOrders();
        
        // Assert
        Assert.True(result.Any());
    }
    
    [Fact]
    public void GetOrderDetail_Success()
    {
        // Act 
        var result = _repository.GetOrderDetails(10248);
        
        // Assert
        Assert.True(result.Any());
    }
    
    [Fact]
    public void CreateOrder_Success()
    {
        // Arrange
        var order = new Order()
        {
            Date = DateTime.Now,
            CustomerId = "LILAS"
        };
        
        // Act 
        var result = _repository.CreateOrder(order);
        
        // Assert
        Assert.NotEqual(0, result);
    }
    
    [Fact]
    public void UpdateOrder_Success()
    {
        // Arrange
        var order = new Order()
        {
            Id = 11077,
            CustomerId = "LILAS",
            Status = OrderStatus.New
        };
        
        // Act 
        _repository.UpdateOrder(order);
    }
    
    [Fact]
    public void DeleteOrder_Success()
    {
        // Arrange
        var order = new Order()
        {
            Id = 11079,
            Status = OrderStatus.New
        };
        
        // Act 
        _repository.DeleteOrder(order);
    }
    
    [Fact]
    public void ChangeStatus_Success()
    {
        // Act 
        _repository.ChangeStatus(11079, OrderStatus.InProgress);
    }
    
    [Fact]
    public void GetOrderHistory_Success()
    {
        // Act 
        var result = _repository.GetOrderHistory("LILAS");
        
        // Assert
        Assert.True(result.Any());
    }
    
    [Fact]
    public void GetCustomerOrderDetail_Success()
    {
        // Act 
        var result = _repository.GetCustomerOrderDetail(10248);
        
        // Assert
        Assert.True(result.Any());
    }
}