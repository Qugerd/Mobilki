using System;
using System.Collections.Generic;
using System.Text;

namespace Weather
{
    class WeatherJasonOneCall
    {
        public Hourly[] hourly { get; set; }
        public Daily[] daily { get; set; }


    }
    class Hourly
    {
        public int dt { get; set; }
        public double temp { get; set; }
        public Weather[] weather { get; set; }
    }

    class Daily
    {
        public int dt { get; set; }
        public Temp temp { get; set; }

       public Weather[] weather { get; set; }
        
    }

    class Temp
    {
        public double day { get; set; }
        public double eve { get; set; }
    }

    class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }
}
