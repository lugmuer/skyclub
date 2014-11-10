using System.Linq;

namespace SkyApi.Database.Abstract
{
    public interface IAirPortRepository
    {
        int GetAirPortIdByName(string departureAirportFsCode);
        void Insert(Airport airport);
    }

    public class AirPortRepository : IAirPortRepository
    {
        public int GetAirPortIdByName(string departureAirportFsCode)
        {
            var db = new DatabaseDataContext(Constants.ConnectionString);
            var airport = db.Airports.SingleOrDefault(ap => ap.FsCode == departureAirportFsCode);
            if (airport == null)
                return -1;
            return airport.Id;
        }

        public void Insert(Airport airport)
        {
            var db = new DatabaseDataContext(Constants.ConnectionString);
            db.Airports.InsertOnSubmit(airport);
            db.SubmitChanges();
        }
    }
}