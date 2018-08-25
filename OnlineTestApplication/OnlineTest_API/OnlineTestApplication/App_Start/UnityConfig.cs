using System.Web.Http;
using Unity;
using Unity.WebApi;
using DataAccessLayer;
using BusinessAccessLayer;

namespace OnlineTestApplication
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IDStudent, DStudent>();
            container.RegisterType<IBStudent, BStudent>();

            container.RegisterType<IDTestType, DTestType>();
            container.RegisterType<IBTestType, BTestType>();

            container.RegisterType<IDTestSeries, DTestSeries>();
            container.RegisterType<IBTestSeries, BTestSeries>();

            container.RegisterType<IDMaster, DMaster>();
            container.RegisterType<IBMaster, BMaster>();

            container.RegisterType<IDOnlineTest, DOnlineTest>();
            container.RegisterType<IBOnlineTest, BOnlineTest>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}