using System;
using Xunit;
using TaxiDispatcher.App.Helpers;
using TaxiDispatcher.App;

namespace TaxiDispatcherTest
{
    public class CalculatorHelperTest
    {

        [Fact]
        public void CalculateDistance_PositiveNumbers_DistanceCalculated()
        {
            //Arrange
            var calculatorHelper = new CalculatorHelper();
            var taxiLocation = 2;
            var startLocation = 1;

            //Act
            var result = calculatorHelper.CalculateDistance(taxiLocation, startLocation);

            //Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void CalculateDistance_NegativeNumbers_DistanceCalculated()
        {
            //Arrange
            var calculatorHelper = new CalculatorHelper();
            var taxiLocation = -2;
            var startLocation = -1;

            //Act
            var result = calculatorHelper.CalculateDistance(taxiLocation, startLocation);

            //Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void CalculatePrice_CityDayRide_PriceCalculated()
        {
            //Arrange
            var calculatorHelper = new CalculatorHelper();
            var startLocation = 1;
            var endLocation = 5;
            var priceMultiplier = 10;
            var rideType = Constants.City;
            var time = new DateTime(2018, 1, 1, 10, 0, 0);


            //Act
            var result = calculatorHelper.CalculatePrice(startLocation, endLocation,priceMultiplier, rideType, time);

            //Assert
            Assert.Equal(40, result);
        }

        [Fact]
        public void CalculatePrice_CityNightRide_PriceCalculated()
        {
            //Arrange
            var calculatorHelper = new CalculatorHelper();
            var startLocation = 1;
            var endLocation = 5;
            var priceMultiplier = 10;
            var rideType = Constants.City;
            var time = new DateTime(2018, 1, 1, 23, 0, 0);


            //Act
            var result = calculatorHelper.CalculatePrice(startLocation, endLocation, priceMultiplier, rideType, time);

            //Assert
            Assert.Equal(80, result);
        }

        [Fact]
        public void CalculatePrice_InterCityNightRide_PriceCalculated()
        {
            //Arrange
            var calculatorHelper = new CalculatorHelper();
            var startLocation = 1;
            var endLocation = 5;
            var priceMultiplier = 10;
            var rideType = Constants.InterCity;
            var time = new DateTime(2018, 1, 1, 23, 0, 0);


            //Act
            var result = calculatorHelper.CalculatePrice(startLocation, endLocation, priceMultiplier, rideType, time);

            //Assert
            Assert.Equal(160, result);
        }

        [Fact]
        public void CalculatePrice_InterCityDayRide_PriceCalculated()
        {
            //Arrange
            var calculatorHelper = new CalculatorHelper();
            var startLocation = 1;
            var endLocation = 5;
            var priceMultiplier = 10;
            var rideType = Constants.InterCity;
            var time = new DateTime(2018, 1, 1, 10, 0, 0);


            //Act
            var result = calculatorHelper.CalculatePrice(startLocation, endLocation, priceMultiplier, rideType, time);

            //Assert
            Assert.Equal(80, result);
        }
    }
}
