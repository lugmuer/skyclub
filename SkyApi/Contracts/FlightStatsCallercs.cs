using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Web;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using SkyApi.Contracts.Abstract;
using SkyApi.Controllers;
using SkyApi.Database;
using SkyApi.Database.Abstract;
using SkyApi.Helper;

namespace SkyApi.Contracts
{
    public class FlightStatsCallercs : IFlightStatsCaller
    {
        private const string webapiUrl = "https://api.flightstats.com/flex/schedules/rest/v1/json/";

        private IAirPortRepository airPortRepository;

        public FlightStatsCallercs(IAirPortRepository airPortRepository)
        {
            this.airPortRepository = airPortRepository;
        }

        public Flight LoadFlight(DateTime date, string name)
        {
            var flightName = FLightDataHelper.ParseFlightName(name);

            var parameter= "flight/" + flightName.Item1 + "/" + flightName.Item2 + "/departing/" + date.Year + "/" + date.Month + "/" + date.Day;
            var jsonString = this.callapi(webapiUrl, parameter);
            var jsonParser = new JavaScriptSerializer();
            var rootObject = jsonParser.Deserialize<RootObject>(jsonString);

            var departureAirportFsCode = rootObject.scheduledFlights[0].departureAirportFsCode;
            var arrivalAirportFsCode = rootObject.scheduledFlights[0].arrivalAirportFsCode;

            var sourceId = this.airPortRepository.GetAirPortIdByName(departureAirportFsCode);
            var targetId = this.airPortRepository.GetAirPortIdByName(arrivalAirportFsCode);

            
            this.LazyLoadAirports(sourceId, rootObject, departureAirportFsCode, targetId, arrivalAirportFsCode);

            return new Flight
            {
                Name = name,
                Date = date,
                SourceId =
                    sourceId,
                TargetId =
                    targetId
            };
        }

        private string callapi(string uristart, string urlParameters)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(uristart);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            urlParameters = urlParameters + "?appId=" + Constants.FlightStatsAppId + "&appKey=" + Constants.FlightStatsAppKey;

            // List data response.
            HttpResponseMessage response = client.GetAsync(urlParameters).Result;

            var task = response.Content.ReadAsStringAsync();
            task.Wait();
            return task.Result;
        }

        private void LazyLoadAirports(int sourceId, RootObject rootObject, string departureAirportFsCode, int targetId,
            string arrivalAirportFsCode)
        {
            if (sourceId < 0)
            {
                var airport = rootObject.appendix.airports.SingleOrDefault(ap => ap.fs == departureAirportFsCode);
                var airportDb = new Database.Airport
                {
                    Name = airport.name,
                    FsCode = airport.fs
                };
                this.airPortRepository.Insert(airportDb);
            }
            if (targetId < 0)
            {
                var airport = rootObject.appendix.airports.SingleOrDefault(ap => ap.fs == arrivalAirportFsCode);
                var airportDb = new Database.Airport
                {
                    Name = airport.name,
                    FsCode = airport.fs
                };
                this.airPortRepository.Insert(airportDb);
            }
        }
    }

    public class Carrier
{
    public string requestedCode { get; set; }
    public string fsCode { get; set; }
}

public class CodeType
{
}

public class FlightNumber
{
    public string requested { get; set; }
    public string interpreted { get; set; }
}

public class Date
{
    public string year { get; set; }
    public string month { get; set; }
    public string day { get; set; }
    public string interpreted { get; set; }
}

public class Request
{
    public Carrier carrier { get; set; }
    public CodeType codeType { get; set; }
    public FlightNumber flightNumber { get; set; }
    public bool departing { get; set; }
    public Date date { get; set; }
    public string url { get; set; }
}

public class Codeshare
{
    public string carrierFsCode { get; set; }
    public string flightNumber { get; set; }
    public string serviceType { get; set; }
    public List<string> serviceClasses { get; set; }
    public List<object> trafficRestrictions { get; set; }
    public int referenceCode { get; set; }
}

public class ScheduledFlight
{
    public string carrierFsCode { get; set; }
    public string flightNumber { get; set; }
    public string departureAirportFsCode { get; set; }
    public string arrivalAirportFsCode { get; set; }
    public int stops { get; set; }
    public string departureTerminal { get; set; }
    public string arrivalTerminal { get; set; }
    public string departureTime { get; set; }
    public string arrivalTime { get; set; }
    public string flightEquipmentIataCode { get; set; }
    public bool isCodeshare { get; set; }
    public bool isWetlease { get; set; }
    public string serviceType { get; set; }
    public List<string> serviceClasses { get; set; }
    public List<object> trafficRestrictions { get; set; }
    public List<Codeshare> codeshares { get; set; }
    public string referenceCode { get; set; }
}

public class Airline
{
    public string fs { get; set; }
    public string iata { get; set; }
    public string icao { get; set; }
    public string name { get; set; }
    public string phoneNumber { get; set; }
    public bool active { get; set; }
}

public class Airport
{
    public string fs { get; set; }
    public string iata { get; set; }
    public string icao { get; set; }
    public string faa { get; set; }
    public string name { get; set; }
    public string street1 { get; set; }
    public string city { get; set; }
    public string cityCode { get; set; }
    public string stateCode { get; set; }
    public string postalCode { get; set; }
    public string countryCode { get; set; }
    public string countryName { get; set; }
    public string regionName { get; set; }
    public string timeZoneRegionName { get; set; }
    public string weatherZone { get; set; }
    public string localTime { get; set; }
    public float utcOffsetHours { get; set; }
    public double latitude { get; set; }
    public double longitude { get; set; }
    public int elevationFeet { get; set; }
    public int classification { get; set; }
    public bool active { get; set; }
}

public class Equipment
{
    public string iata { get; set; }
    public string name { get; set; }
    public bool turboProp { get; set; }
    public bool jet { get; set; }
    public bool widebody { get; set; }
    public bool regional { get; set; }
}

public class Appendix
{
    public List<Airline> airlines { get; set; }
    public List<Airport> airports { get; set; }
    public List<Equipment> equipments { get; set; }
}

public class RootObject
{
    public Request request { get; set; }
    public List<ScheduledFlight> scheduledFlights { get; set; }
    public Appendix appendix { get; set; }
}

}