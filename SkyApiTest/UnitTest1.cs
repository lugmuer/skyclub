using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkyApi;
using SkyApi.Contracts;
using SkyApi.Controllers;
using SkyApi.Database;
using SkyApi.Database.Abstract;

namespace SkyApiTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            /*
            var airPortRepository = new AirPortRepository();
            var flightStatsCallercs = new FlightStatsCallercs(airPortRepository);
            var flightsRepository = new FlightRepository();

            var controller = new FlightController(flightStatsCallercs, flightsRepository);
            controller.RegisterFlight(1, new DateTime(2014, 12, 28), "AB8589");*/

            var db = new DatabaseDataContext(Constants.ConnectionString);
            var flight = db.Users.First().Flightmembers.First().Flight;
            Assert.AreEqual(flight.Airport.FsCode, "ZRH");
            Assert.AreEqual(flight.Airport1.FsCode, "TXL");
        }
    }
}
