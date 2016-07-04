using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Domain
{
    public interface IWeatherService
    {
        void RefreshLocations(string location);
        void RefreshForecasts(string identifier);
        IEnumerable<Location> GetLocations();
        IEnumerable<Forecast> GetForecasts();
        Location GetLocationById(string identifier);
    }
}
