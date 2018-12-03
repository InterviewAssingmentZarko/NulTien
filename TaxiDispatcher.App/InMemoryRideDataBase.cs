using System.Collections.Generic;
using TaxiDispatcher.App.Models;

namespace TaxiDispatcher.App
{
    public static class InMemoryRideDataBase
    {
        public static List<Ride> Rides = new List<Ride>();

        public static void SaveRide(Ride ride)
        {
            int maxId = Rides.Count == 0 ? 0 : Rides[0].Id;
            foreach (Ride r in Rides)
            {
                if (r.Id > maxId)
                    maxId = r.Id;
            }

            ride.Id = maxId + 1;
            Rides.Add(ride);
        }

        public static Ride GetRide(int id)
        {
            Ride ride = Rides[0];
            bool found = ride.Id == id;
            int current = 1;
            while (!found)
            {
                ride = Rides[current];
                found = ride.Id == id;
                current += 1;
            }

            return ride;
        }

        public static List<int> GetRideIds()
        {
            List<int> ids = new List<int>();
            foreach (Ride ride in Rides)
            {
                ids.Add(ride.Id);
            }

            return ids;
        }
    }
}
