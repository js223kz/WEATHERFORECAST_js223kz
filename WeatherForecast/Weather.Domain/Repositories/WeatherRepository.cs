using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Weather.Domain.DataModels;
using Weather.Domain.WebServices;

//class that handles all comunnication with database
namespace Weather.Domain.Repositories
{
    public class WeatherRepository : IWeatherRepository
    {
        private ForecastEntities _context = new ForecastEntities();
        private string path = Path.Combine(AppDomain.CurrentDomain.GetData("DataDirectory").ToString(), "LatestUpdates.xml");

        public IEnumerable<Location> GetLocations()
        {
           return _context.Locations.ToList();
        }

        private IQueryable<Location> QueryLocation()
        {
            return _context.Locations.AsQueryable();
        }

        public Location GetLocationById(string identifier)
        {
            return QueryLocation().SingleOrDefault(l => l.Identifier == identifier);
        }

        public void AddLocations(IEnumerable<Location> locations)
        {
            foreach (Location location in locations)
            {
                _context.Locations.Add(location);
            }          
        }

        public void DeleteAllLocations()
        {           
            var locations = _context.Locations.ToList();

            foreach (Location location in locations)
            {
                _context.Locations.Remove(location);
            }          
        }

        public void AddForecasts(IEnumerable<Forecast> forecasts)
        {           
            foreach (Forecast forecast in forecasts)
            {
                _context.Forecasts.Add(forecast);
            }           
        }

        public IEnumerable<Forecast> GetForecasts()
        {
         
            return _context.Forecasts.ToList();
        }

        public void DeleteAllForecasts()
        {
          
            var forecasts = _context.Forecasts.ToList();

            foreach (Forecast forecast in forecasts)
            {
                _context.Forecasts.Remove(forecast);
            }             
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        //Make sure no Db connections 
        //are open when not used
        #region IDisposable

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
