using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Threading.Tasks;
//using System.Web.Configuration;
using Weather.Domain.DataModels;
using System.Runtime.Serialization.Json;

namespace Weather.Domain.WebServices
{
    public class GeoNamesWebService : IGeoNamesWebService
    {
        //Webservice that gets geographical data
        public IEnumerable<Location> searchLocations(string location) 
        {
            string rawJson;
            
            //can't use this WHY?
            //string userName = WebConfigurationManager.AppSettings["geonameskey"];
            var uri = String.Format("http://api.geonames.org/searchJSON?name={0}&maxRows=50&username=joszep", location);

           var request = (HttpWebRequest) WebRequest.Create(uri);

           using(var response = request.GetResponse())
           using (var reader = new StreamReader(response.GetResponseStream()))
           {
                rawJson = reader.ReadToEnd();
           }

           var start = rawJson.IndexOf("[");
           var length = rawJson.Length;
           var content = rawJson.Substring(start, length - start - 1);

           return JArray.Parse(content).Select(f => new Location(f)).ToList();
        }
    }
}

