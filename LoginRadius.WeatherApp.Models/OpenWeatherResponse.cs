using System;
using System.Collections.Generic;

namespace LoginRadius.WeatherApp.Models
{
    public class OpenWeatherResponse
    {
        public Coordinates coord { get; set; }
        public Main main { get; set; }

        public IEnumerable<Weather> weather { get; set; }

        public int timezone { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int cod { get; set; }
    }

    public class Coordinates
    {
        public float lon { get; set; }
        public float lat { get; set; }
    }

    public class Main
    {
        public float temp { get; set; }
        public float feels_like { get; set; }
        public float temp_min { get; set; }
        public float temp_max { get; set; }

        public int pressure { get; set; }
        public int humidity { get; set; }
    }

    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }

    }


    

}
