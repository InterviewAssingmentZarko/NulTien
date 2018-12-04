using System;

namespace TaxiDispatcher.App.Intefaces
{
    public interface ICalculatorHelper
    {
        int CalculateDistance(int taxiLocation, int startLocation);
        int CalculatePrice(int startLocation, int endLocation, int priceMultiplier, int rideType, DateTime time);
    }
}
