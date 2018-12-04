using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiDispatcher.App.Helpers
{
    public class CalculatorHelper
    {
        public int CalculateDistance(int taxiLocation, int startLocation)
        {
            return Math.Abs(taxiLocation - startLocation);
        }

        public int CalculatePrice(int startLocation, int endLocation, int PriceMultiplier)
        {
            return PriceMultiplier* Math.Abs(startLocation - endLocation);
        }
    }
}
