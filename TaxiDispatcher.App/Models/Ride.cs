using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiDispatcher.App.Models
{
    public class Ride
    {
        public int Id { get; set; }
        public int StartLocation { get; set; }
        public int EndLocation { get; set; }
        public TaxiDriver Driver { get; set; }
        public int Price { get; set; }
    }
}
