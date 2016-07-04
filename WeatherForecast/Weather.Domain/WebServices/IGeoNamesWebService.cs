using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Domain.WebServices
{
    public interface IGeoNamesWebService
    {
        IEnumerable<Location> searchLocations(string location);
    }
}
