using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Weather.Domain
{
    
    public partial class Location
    {
        public Location() 
        { 
            //Empty
        }
        public Location(JToken LocationToken)   
        {
            Identifier = LocationToken.Value<string>("geonameId");
            Country = LocationToken.Value<string>("countryName");
            Name = LocationToken.Value<string>("name");
            Lat = LocationToken.Value<double>("lat");
            Lon = LocationToken.Value<double>("lng");
        }
    }
}
