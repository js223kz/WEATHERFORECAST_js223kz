using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using Weather.Domain;

namespace Weather.Application.ViewModels
{
    public class LocationViewModel
    {
        //validation of entered location name
        [Display(Prompt = "Enter location")]
        [Required(ErrorMessage = "You have to specify a location!")]
        [UIHint("Placeholder")]
        public string Name { get; set; }

        public string Identifier { get; set; }
        
        
        public IEnumerable<Forecast> Forecasts { get; set; }
        public Forecast forecast { get; set; }

        public Location location { get; set; }
        public IEnumerable<Location> Locations { get; set; }

        public IList<Day> GetDay()
        {
            List<Day> days = new List<Day>();
            days.Add(new Day(DateTime.Today, "Today"));
            days.Add(new Day(DateTime.Today.AddDays(1), "Tomorrow"));
            days.Add(new Day(DateTime.Today.AddDays(2), DateTime.Today.AddDays(2).ToString("ddd", CultureInfo.InvariantCulture)));
            days.Add(new Day(DateTime.Today.AddDays(3), DateTime.Today.AddDays(3).ToString("ddd", CultureInfo.InvariantCulture)));
            days.Add(new Day(DateTime.Today.AddDays(4), DateTime.Today.AddDays(4).ToString("ddd", CultureInfo.InvariantCulture)));

            return days;
        }

        //builds an integer to a correct formatted hourly string
        public string GetHour(int period)
        {
            string periodString = period.ToString();
            StringBuilder hour = new StringBuilder(periodString);

            if(hour.Length == 3){
                hour.Insert(0, "0");
            }
            hour.Insert(2, ":");

            return hour.ToString();
        } 
    }
}