using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Domain
{
    public class Day
    {
        public DateTime Date { get; set; }
        public string DayName { get; set; }

         public Day(DateTime date, string day)
        {
            Date = date;
            DayName = day;     
        }
    }
}
