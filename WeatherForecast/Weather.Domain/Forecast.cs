using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Domain
{
    public partial class Forecast
    {

        public Forecast()
        {
            // Empty!
        }
 
        public Forecast(DateTime date, int period, int tempC, int feelsLike, string weatherDesc, string icon, int sunshine)
        {
            Date = date;
            Period = period;
            TempC = tempC;
            FeelsLikeC = feelsLike;
            WeatherDesc = weatherDesc;
            WeatherIcon = icon;
            ChanceOfSunshine = sunshine;        
        }
     }
}
