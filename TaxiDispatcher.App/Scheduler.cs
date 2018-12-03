using System;
using System.Collections.Generic;
using TaxiDispatcher.App.Models;

namespace TaxiDispatcher.App
{
    public class Scheduler
    {
        protected List<Taxi> TaxisList { get; set; }

        public Scheduler()
        {
            TaxisList = new List<Taxi>
            {
                new Taxi { Driver = new TaxiDriver() { Id = 1, Name = "Predrag" }, Company = "Naxi", Location = 1},
                new Taxi { Driver = new TaxiDriver() { Id = 2, Name = "Nenad" }, Company = "Naxi", Location = 4 },
                new Taxi { Driver = new TaxiDriver() { Id = 3, Name = "Dragan" }, Company = "Alfa", Location = 6 },
                new Taxi { Driver = new TaxiDriver() { Id = 4, Name = "Goran" }, Company = "Gold", Location = 7 }
            };
        }

        public Ride OrderRide(int startLocation, int EndLocation, int rideType, DateTime time)
        {

            var nearestTaxi = new Taxi();
            int minimumDistanceFromCustomer = Constants.MaximumDistanceFromCustomer;

            foreach (Taxi taxi in TaxisList)
            {
                if (Math.Abs(taxi.Location - startLocation) < minimumDistanceFromCustomer)
                {
                    nearestTaxi = taxi;
                    minimumDistanceFromCustomer = Math.Abs(taxi.Location - startLocation);
                }
            }

            if (minimumDistanceFromCustomer > Constants.MaximumDistanceFromCustomer)
            {
                throw new Exception("There are no available taxi vehicles!");
            }

            Ride ride = CreateRide(startLocation, EndLocation, nearestTaxi);


            #region CalculatingPrice

            switch (nearestTaxi.Company)
            {
                case "Naxi":
                    {
                        ride.Price = 10 * Math.Abs(startLocation - EndLocation);
                        break;
                    }
                case "Alfa":
                    {
                        ride.Price = 15 * Math.Abs(startLocation - EndLocation);
                        break;
                    }
                case "Gold":
                    {
                        ride.Price = 13 * Math.Abs(startLocation - EndLocation);
                        break;
                    }
                default:
                    {
                        throw new Exception("Ilegal company");
                    }
            }

            if (rideType == Constants.InterCity)
            {
                ride.Price *= 2;
            }

            if (time.Hour < 6 || time.Hour > 22)
            {
                ride.Price *= 2;
            }

            #endregion

            Console.WriteLine("Ride ordered, price: " + ride.Price.ToString());
            return ride;
        }

        private static Ride CreateRide(int startLocation, int EndLocation, Taxi nearestTaxi)
        {
            Ride ride = new Ride();
            ride.Driver.Id = nearestTaxi.Driver.Id;
            ride.StartLocation = startLocation;
            ride.StartLocation = EndLocation;
            ride.Driver.Name = nearestTaxi.Driver.Name;
            return ride;
        }

        public void AcceptRide(Ride ride)
        {
            InMemoryRideDataBase.SaveRide(ride);
            foreach(Taxi taxi in TaxisList)
            {
                if (taxi.Driver.Id == ride.Driver.Id)
                {
                    taxi.Location = ride.EndLocation;
                }
            }

            Console.WriteLine("Ride accepted, waiting for driver: " + ride.Driver.Name);
        }

        public List<Ride> GetRideList(int driver_id)
        {
            List<Ride> rides = new List<Ride>();
            List<int> ids = InMemoryRideDataBase.GetRideIds();
            foreach (int id in ids)
            {
                Ride ride = InMemoryRideDataBase.GetRide(id);
                if (ride.Driver.Id == driver_id)
                    rides.Add(ride);
            }

            return rides;
        }

  

    }
}
