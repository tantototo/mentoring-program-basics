using System.Reflection;
using IOCContainer;
using TestConsole;

var container = new Container(); 
container.AddType(typeof(CustomerBLL));
container.AddType(typeof(Logger)); 
container.AddType(typeof(ICustomerDAL), typeof(CustomerDAL));

var containerWithAssembly = new Container();
containerWithAssembly.AddAssembly(Assembly.GetExecutingAssembly());

var bll = (CustomerBLL)container.Create(typeof(CustomerBLL))!; 
var bLL = container.Create<CustomerBLL>();

var dall = (CustomerDAL)containerWithAssembly.Create(typeof(CustomerDAL))!; 
var daLL = containerWithAssembly.Create<CustomerDAL>();
