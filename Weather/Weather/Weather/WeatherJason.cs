using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Xamarin.Forms;
using Android.Graphics;

namespace Weather
{
    class WeatherJson
    {
        public Coord coord { get; set; }
        public Weather[] weather{ get; set; }
        public string Name { get; set; }
        public Temperature Main { get; set; }
        public int visibility { get; set; }
        public WindINF wind { get; set; }
        public CloudsINF clouds { get; set; }
        public SysINF sys { get; set; }
    }

    class Coord
    {
        public double lon { get; set; }
        public double lat { get; set; }

    }
    class Temperature
    {
        public double temp { get; set; }
        public double feels_like { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
    }

    class WindINF
    {
        public double speed { get; set; }
        public int deg { get; set; }
    }

    class SysINF
    {
        public int type { get; set; }
        public int id { get; set; }
        public double message { get; set; }
        public string country { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
    }

    class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    class CloudsINF
    {
        public int all { get; set; }
    }
}
