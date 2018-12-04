using System;
using TaxiDispatcher.App.Intefaces;

namespace TaxiDispatcher.App.Helpers
{
    public class CalculatorHelper : ICalculatorHelper
    {
        public int CalculateDistance(int taxiLocation, int startLocation)
        {
            return Math.Abs(taxiLocation - startLocation);
        }

        public int CalculatePrice(int startLocation, int endLocation, int priceMultiplier, int rideType, DateTime time)
        {
            var priceResult = priceMultiplier * Math.Abs(startLocation - endLocation);
            if (rideType == Constants.InterCity)
            {
                priceResult *= 2;
            }

            if (time.Hour < 6 || time.Hour > 22)
            {
                priceResult *= 2;
            }
            return priceResult;
        }
    }
}
