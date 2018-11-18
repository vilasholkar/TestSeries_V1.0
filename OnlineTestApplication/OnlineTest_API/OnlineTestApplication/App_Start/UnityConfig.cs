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

            container.RegisterType<IDQuiz, DQuiz>();
            container.RegisterType<IBQuiz, BQuiz>();

            container.RegisterType<IDEligibleStudent, DEligibleStudentcs>();
            container.RegisterType<IBEligibleStudent, BEligibleStudent>();

            container.RegisterType<IDResult, DResult>();
            container.RegisterType<IBResult, BResult>();

            container.RegisterType<IDAccount, DAccount>();
            container.RegisterType<IBAccount, BAccount>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}