using IOCContainer.Attributes;

namespace TestConsole;

[ImportConstructor]
public class CustomerBLL
{
    private readonly ICustomerDAL _dal;
    private readonly Logger _logger;
    
    public CustomerBLL(ICustomerDAL dal, Logger logger)
    {
        _dal = dal;
        _logger = logger;
    }
}