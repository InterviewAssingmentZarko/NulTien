using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiDispatcher.App.Models;

namespace TaxiDispatcher.App.Intefaces
{
    public interface IScheduler
    {
        Ride OrderRide(int startLocation, int endLocation, int rideType, DateTime time);
        Ride CreateRide(int startLocation, int endLocation, Taxi nearestTaxi);
        void AcceptRide(Ride ride);
        List<Ride> GetRideList(int driver_id);
    }
}
