namespace TaxiDispatcher.App.Models
{
    public class Taxi
    {
        public TaxiDriver Driver { get; set; }
        public string Company { get; set; }
        public int Location { get; set; }
    }
}
