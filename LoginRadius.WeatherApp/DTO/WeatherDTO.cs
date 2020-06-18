using System;

namespace LoginRadius.WeatherApp.DTO
{
    public class WeatherDTO
    {
        public string Name { get; set; }
        public double MaxTemperature { get; set; }
        public double MinTemperature { get; set; }
        public double Temperature { get; set; }

        public double FeelsLike{ get;set;}
        public int Pressure { get;set;}
        public int Humidity { get; set; }
        public string Summary { get; set; }
    }
}
