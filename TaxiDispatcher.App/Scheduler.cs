using System;
using System.Collections.Generic;
using TaxiDispatcher.App.Helpers;
using TaxiDispatcher.App.Models;

namespace TaxiDispatcher.App
{
    public class Scheduler
    {
        protected List<Taxi> TaxisList { get; set; }
        protected Dictionary<string, int> PriceMultipliers { get; set; }

        public Scheduler()
        {
            TaxisList = new List<Taxi>
            {
                new Taxi { Driver = new TaxiDriver() { Id = 1, Name = "Predrag" }, Company = "Naxi", Location = 1 },
                new Taxi { Driver = new TaxiDriver() { Id = 2, Name = "Nenad" }, Company = "Naxi", Location = 4 },
                new Taxi { Driver = new TaxiDriver() { Id = 3, Name = "Dragan" }, Company = "Alfa", Location = 6 },
                new Taxi { Driver = new TaxiDriver() { Id = 4, Name = "Goran" }, Company = "Gold", Location = 7 }
            };

            PriceMultipliers = new Dictionary<string, int>
            {
                { "Naxi", 10 },
                { "Alfa", 15 },
                { "Gold", 13 }
             };
        }

        public Ride OrderRide(int startLocation, int endLocation, int rideType, DateTime time)
        {
            var calculatorHelper = new CalculatorHelper();
            var nearestTaxi = new Taxi();
            int minimumDistanceFromCustomer = Constants.MaximumDistanceFromCustomer;

            foreach (Taxi taxi in TaxisList)
            {
                var taxiAndCustomerDistance = calculatorHelper.CalculateDistance(taxi.Location, startLocation);
                if (taxiAndCustomerDistance < minimumDistanceFromCustomer)
                {
                    nearestTaxi = taxi;
                    minimumDistanceFromCustomer = taxiAndCustomerDistance;
                }
            }

            if (minimumDistanceFromCustomer >= Constants.MaximumDistanceFromCustomer)
            {
                throw new Exception("There are no available taxi vehicles!");
            }

            var ride = CreateRide(startLocation, endLocation, nearestTaxi);
            var companyNotExists = true;
            foreach (var kvp in PriceMultipliers)
            {
                if (nearestTaxi.Company.Equals(kvp.Key))
                {
                    ride.Price = calculatorHelper.CalculatePrice(startLocation, endLocation, kvp.Value, rideType, time);
                    companyNotExists = false;
                }
            }

            if (companyNotExists)
            {
                throw new Exception("Ilegal company");
            }

            Console.WriteLine("Ride ordered, price: " + ride.Price.ToString());
            return ride;
        }

        public Ride CreateRide(int startLocation, int endLocation, Taxi nearestTaxi)
        {
            Ride ride = new Ride()
            {
                Driver = new TaxiDriver()
            };
            ride.Driver.Id = nearestTaxi.Driver.Id;
            ride.StartLocation = startLocation;
            ride.StartLocation = endLocation;
            ride.Driver.Name = nearestTaxi.Driver.Name;
            return ride;
        }

        public void AcceptRide(Ride ride)
        {
            InMemoryRideDataBase.SaveRide(ride);
            foreach (Taxi taxi in TaxisList)
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
