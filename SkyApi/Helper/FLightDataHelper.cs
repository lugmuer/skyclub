using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyApi.Helper
{
    public static class FLightDataHelper
    {
        public static Tuple<string, int> ParseFlightName(string name)
        {
            
            var carrierName = string.Empty;
            int i = 0;
            while(!Char.IsNumber(name[i]) && i < name.Count())
            {
                carrierName += name[i];
                i++;
            }
            var numberString = name.Substring(carrierName.Count() - 0);
            int flightNumber = 0;
            int.TryParse(numberString, out flightNumber);

            return new Tuple<string, int>(carrierName, flightNumber);
        }
    }
}