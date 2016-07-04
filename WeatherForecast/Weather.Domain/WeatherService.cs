using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Domain.Repositories;
using Weather.Domain.WebServices;

namespace Weather.Domain
{
    public class WeatherService : IWeatherService
    {
        private readonly IWeatherRepository _weatherRepository;
        private readonly IGeoNamesWebService _geoNamesWebService;
        private readonly IWorldWeatherWebService _worldWeatherWebService;

        public WeatherService(IWeatherRepository repository, IGeoNamesWebService locationService, IWorldWeatherWebService weatherService)
        {
            _weatherRepository = repository;
            _geoNamesWebService = locationService;
            _worldWeatherWebService = weatherService;
        }

        //get locations from database
        public IEnumerable<Location> GetLocations()
        {
            return _weatherRepository.GetLocations();
        }

        public Location GetLocationById(string identifier)
        {
           return _weatherRepository.GetLocationById(identifier);     
        }

        //refresh locations from webservice, delete old ones from database and save new to database
        public void RefreshLocations(string location)
        {          
            IEnumerable<Location> locations = _geoNamesWebService.searchLocations(location);

            _weatherRepository.DeleteAllLocations();
            _weatherRepository.AddLocations(locations);
            _weatherRepository.Save();                   
        }

        public IEnumerable<Forecast> GetForecasts()
        {          
            return _weatherRepository.GetForecasts();                    
        }
       
        //refresh forecasts from web service, delete old ones from database and save new to database
        public void RefreshForecasts(string identifier)
        {
            Location location = GetLocationById(identifier);
                
            IEnumerable<Forecast> forecasts = _worldWeatherWebService.searchLocationForecast(location);
            _weatherRepository.DeleteAllForecasts();
            _weatherRepository.AddForecasts(forecasts);
            _weatherRepository.Save();            
        }
    }
}
