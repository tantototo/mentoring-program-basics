using MvcMusicStore.Infra;
using Ninject.Web.Mvc;
using Ninject;
using NLog;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MvcMusicStore
{
    public class MvcApplication : System.Web.HttpApplication
    {
        ILogger logger;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Counter.SetupCounters();

            logger = LogManager.GetCurrentClassLogger();
            var kernel = new StandardKernel();
            kernel.Bind<ILogger>().ToConstant(logger);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));

            logger.Info("Application Start");
        }

        protected void Application_Error()
        {
            logger.Error(Server.GetLastError().Message);
            Counter.Error.Increment();
        }

        protected void Application_End()
        {
            Counter.OutputSample(Counter.Error.NextSample());
            //Counter.DeleteCategories();

            logger.Info("Application End");
        }
    }
}
