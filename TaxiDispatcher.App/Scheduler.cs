using System;
using System.Collections.Generic;
using TaxiDispatcher.App.Models;

namespace TaxiDispatcher.App
{
    public class Scheduler
    {

        protected Taxi taxi1 = new Taxi { Driver = new TaxiDriver() { Id = 1, Name = "Predrag" }, Company = "Naxi", Location = 1};
        protected Taxi taxi2 = new Taxi { Driver = new TaxiDriver() { Id = 2, Name = "Nenad" }, Company = "Naxi", Location = 4 };
        protected Taxi taxi3 = new Taxi { Driver = new TaxiDriver() { Id = 3, Name = "Dragan" }, Company = "Alfa", Location = 6 };
        protected Taxi taxi4 = new Taxi { Driver = new TaxiDriver() { Id = 4, Name = "Goran" }, Company = "Gold", Location = 7 };

        public Ride OrderRide(int startLocation, int EndLocation, int rideType, DateTime time)
        {
            #region FindingTheBestVehicle 

            Taxi nearestTaxi = taxi1;
            int minimumDistanceFromCustomer = Math.Abs(taxi1.Location - startLocation);

            if (Math.Abs(taxi2.Location - startLocation) < minimumDistanceFromCustomer)
            {
                nearestTaxi = taxi2;
                minimumDistanceFromCustomer = Math.Abs(taxi2.Location - startLocation);
            }

            if (Math.Abs(taxi3.Location - startLocation) < minimumDistanceFromCustomer)
            {
                nearestTaxi = taxi3;
                minimumDistanceFromCustomer = Math.Abs(taxi3.Location - startLocation);
            }

            if (Math.Abs(taxi4.Location - startLocation) < minimumDistanceFromCustomer)
            {
                nearestTaxi = taxi4;
                minimumDistanceFromCustomer = Math.Abs(taxi4.Location - startLocation);
            }

            if (minimumDistanceFromCustomer > 15)
                throw new Exception("There are no available taxi vehicles!");

            #endregion

            #region CreatingRide

            Ride ride = new Ride();
            ride.Driver.Id = nearestTaxi.Driver.Id;
            ride.StartLocation = startLocation;
            ride.StartLocation = EndLocation;
            ride.Driver.Name = nearestTaxi.Driver.Name;

            #endregion

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

        public void AcceptRide(Ride ride)
        {
            InMemoryRideDataBase.SaveRide(ride);

            if (taxi1.Driver.Id == ride.Driver.Id)
            {
                taxi1.Location = ride.EndLocation;
            }

            if (taxi2.Driver.Id == ride.Driver.Id)
            {
                taxi2.Location = ride.EndLocation;
            }

            if (taxi3.Driver.Id == ride.Driver.Id)
            {
                taxi3.Location = ride.EndLocation;
            }

            if (taxi4.Driver.Id == ride.Driver.Id)
            {
                taxi4.Location = ride.EndLocation;
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
