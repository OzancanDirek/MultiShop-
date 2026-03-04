namespace MultiShop.RapidApiWebUI.Models
{
    public class WeatherViewModel
    {
        public Location location { get; set; }
        public Current current { get; set; }

        public class Location
        {
            public string name { get; set; } 
            public string country { get; set; }
        }

        public class Current
        {
            public float temp_c { get; set; }
            public int humidity { get; set; }  
            public float wind_kph { get; set; }
            public Condition condition { get; set; }
        }

        public class Condition
        {
            public string text { get; set; }
        }
    }
}