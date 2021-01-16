using System;
using System.Collections.Generic;
using System.Text;

namespace Weather
{
    class WeatherJasonOneCall
    {
        public Daily[] daily { get; set; }


    }

    class Daily
    {
        public int dt { get; set; }
        public Temp temp { get; set; }
        
    }

    class Temp
    {
        public double day { get; set; }
        public double eve { get; set; }
    }
}
