using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiDispatcher.App.Models
{
    public class Taxi
    {
        public TaxiDriver Driver { get; set; }
        public string Company { get; set; }
        public int Location { get; set; }
    }
}
