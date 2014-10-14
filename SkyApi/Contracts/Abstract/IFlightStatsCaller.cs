using System;
using SkyApi.Database;

namespace SkyApi.Contracts.Abstract
{
    public interface IFlightStatsCaller
    {
        Flight LoadFlight(DateTime date, string flightName);
    }
}