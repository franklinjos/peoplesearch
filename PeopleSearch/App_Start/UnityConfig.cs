using PeopleSearch.EntityFramework;
using PeopleSearch.Repository;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace PeopleSearch
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IPeopleContext, PeopleContext>();
            container.RegisterType<IPeopleRepository, PeopleRepository>();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}