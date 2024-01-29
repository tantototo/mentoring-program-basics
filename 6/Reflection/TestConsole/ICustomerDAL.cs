using IOCContainer.Attributes;

namespace TestConsole;

public interface ICustomerDAL
{
}

[Export(typeof(ICustomerDAL))]
public class CustomerDAL : ICustomerDAL
{
}