using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Domain.Repositories
{
    public interface IWeatherRepository : IDisposable
    {
        IEnumerable<Location> GetLocations();
        IEnumerable<Forecast> GetForecasts();
        Location GetLocationById(string identifier);
        void AddLocations(IEnumerable<Location> locations);
        void AddForecasts(IEnumerable<Forecast> forecasts);
        void DeleteAllLocations();
        void DeleteAllForecasts();
        void Save();

    }
}
