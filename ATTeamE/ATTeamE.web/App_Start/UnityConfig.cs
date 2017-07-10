using ATTeamE.web.Interfaces;
using ATTeamE.web.Services.Watson;
using Microsoft.Practices.Unity;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using Unity.Mvc5;

namespace ATTeamE.web.App_Start
{
    public class UnityConfig
    {
        private static UnityContainer _container;
        public static UnityContainer GetContainer()
        {
            return _container;
        }

        public static void RegisterInterfaces(HttpConfiguration config)
        {
            UnityContainer container = new UnityContainer();


            container.RegisterType<IWatsonService, WatsonService>(new ContainerControlledLifetimeManager(), new InjectionConstructor("988558a8-ef47-4a26-8f2c-561581034c37", "RJQL403LfsXG"));

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            config.DependencyResolver = new UnityResolver(container);
            _container = container;
        }

    }
}