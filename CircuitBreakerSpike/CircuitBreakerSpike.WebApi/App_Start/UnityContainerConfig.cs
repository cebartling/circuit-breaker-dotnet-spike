using System.Web.Http;
using CircuitBreakerSpike.WebApi.Config;
using Microsoft.Practices.Unity;

namespace CircuitBreakerSpike.WebApi
{
    public static class UnityContainerConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();
            Services.Config.UnityConfig.RegisterComponents(container);
            config.DependencyResolver = new UnityResolver(container);
        }
    }
}