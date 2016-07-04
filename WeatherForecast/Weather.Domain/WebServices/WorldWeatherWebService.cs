using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Domain.WebServices
{
    //Web service that gets weather data from worl weather api
    public class WorldWeatherWebService : IWorldWeatherWebService
    {
        public IEnumerable<Forecast> searchLocationForecast(Location location)
        {
            string lat = location.Lat.ToString().Replace(",", ".");
            string lon = location.Lon.ToString().Replace(",", ".");
            string rawJson;

            //Should have saved my key in config file but couldn´t make it work
            var uri = String.Format("https://api.worldweatheronline.com/free/v2/weather.ashx?key=5acd3054141e31cc1ce50e9cfb57b&q={0},{1}&num_of_days=5&tp=3&format=json", lat, lon);

            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.ContentType = "application/json";

            using (var response = request.GetResponse())
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                rawJson = reader.ReadToEnd();
            }

            List<Forecast> forecasts = new List<Forecast>();

            dynamic jsonObj = JsonConvert.DeserializeObject(rawJson);
                
            var weatherData = jsonObj.data.weather;

            foreach (var obj in weatherData)
            {
               DateTime date = obj.date;
                foreach (var hour in obj.hourly)
                {
                    int period = hour.time;
                    int tempC = hour.tempC;
                    int feelsLike = hour.FeelsLikeC;
                    int chanceOfSunshine = hour.chanceofsunshine;
                    string weatherDesc = hour.weatherDesc[0].value;
                    string weatherIcon = hour.weatherIconUrl[0].value;
                    
                    forecasts.Add(new Forecast(date, period, tempC, feelsLike, weatherDesc, weatherIcon, chanceOfSunshine));
                }
            }
           return forecasts;
       }
    }
}
