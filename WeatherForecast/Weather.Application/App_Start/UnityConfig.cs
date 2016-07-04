using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using Weather.Domain.Repositories;
using Weather.Domain;
using Weather.Domain.WebServices;

namespace Weather.Application
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            container.RegisterType<IWeatherRepository, WeatherRepository>();
            container.RegisterType<IWeatherService, WeatherService>();
            container.RegisterType<IGeoNamesWebService, GeoNamesWebService>();
            container.RegisterType<IWorldWeatherWebService, WorldWeatherWebService>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}