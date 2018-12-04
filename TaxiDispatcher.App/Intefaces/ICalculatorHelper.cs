using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiDispatcher.App.Intefaces
{
    public interface ICalculatorHelper
    {
        int CalculateDistance(int taxiLocation, int startLocation);
        int CalculatePrice(int startLocation, int endLocation, int PriceMultiplier, int rideType, DateTime time);
    }
}
