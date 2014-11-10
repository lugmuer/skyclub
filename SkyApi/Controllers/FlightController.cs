using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SkyApi.Contracts;
using SkyApi.Contracts.Abstract;
using SkyApi.Database;
using SkyApi.Database.Abstract;

namespace SkyApi.Controllers
{
    public class FlightController : ApiController
    {
        private IFlightStatsCaller flightStatsCaller;

        private IFlightRepository flightsRepository;

        public FlightController(IFlightStatsCaller flightStatsCaller, IFlightRepository flightsRepository)
        {
            this.flightStatsCaller = flightStatsCaller;
            this.flightsRepository = flightsRepository;
        }

        [HttpPut]
        public bool RegisterFlight(int UserId, DateTime date, string flightName)
        {
            var flight = this.flightsRepository.GetFlightByDateAndName(flightName, date);
            if (flight == null)
            {
                flight = this.flightStatsCaller.LoadFlight(date, flightName);
                if (flight == null)
                {
                    return false; // flight does not exist
                }

                this.flightsRepository.Insert(flight);
            }

            this.flightsRepository.JoinFlight(flight.Id, UserId);
            return true;
        }
    }
}
