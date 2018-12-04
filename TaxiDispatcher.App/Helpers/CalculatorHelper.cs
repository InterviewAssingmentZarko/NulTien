using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiDispatcher.App.Intefaces;

namespace TaxiDispatcher.App.Helpers
{
    public class CalculatorHelper : ICalculatorHelper
    {
        public int CalculateDistance(int taxiLocation, int startLocation)
        {
            return Math.Abs(taxiLocation - startLocation);
        }

        public int CalculatePrice(int startLocation, int endLocation, int PriceMultiplier, int rideType, DateTime time)
        {
            var priceResult = PriceMultiplier * Math.Abs(startLocation - endLocation);
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
