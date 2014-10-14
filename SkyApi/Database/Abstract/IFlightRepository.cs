using System;
using System.Linq;

namespace SkyApi.Database.Abstract
{
    public interface IFlightRepository
    {
        void Insert(Flight flight);
        Flight GetFlightByDateAndName(string flightName, DateTime date);
        void JoinFlight(int id, int userId);
        bool LeaveFlight(int id, int userId);
    }

    public class FlightRepository : IFlightRepository
    {
        public void Insert(Flight flight)
        {
            var db = new DatabaseDataContext(Constants.ConnectionString);
            db.Flights.InsertOnSubmit(flight);
            db.SubmitChanges();
        }

        public Flight GetFlightByDateAndName(string flightName, DateTime date)
        {
            var db = new DatabaseDataContext(Constants.ConnectionString);
            return db.Flights.SingleOrDefault(flight => flight.Name == flightName && flight.Date.DayOfYear == date.DayOfYear);
        }

        public void JoinFlight(int id, int userId)
        {
            var db = new DatabaseDataContext(Constants.ConnectionString);
            var flightMemver = new Flightmember
            {
                FlightId = id,
                UserId = userId
            };
            db.Flightmembers.InsertOnSubmit(flightMemver);
            db.SubmitChanges();
        }

        public bool LeaveFlight(int id, int userId)
        {
            var db = new DatabaseDataContext(Constants.ConnectionString);
            var flightMember = db.Flightmembers.SingleOrDefault(flightmember => flightmember.FlightId == id && flightmember.UserId == userId);
            if(flightMember== null)
                return false;
            
            db.Flightmembers.DeleteOnSubmit(flightMember);
            db.SubmitChanges();
            return true;
        }
    }
}