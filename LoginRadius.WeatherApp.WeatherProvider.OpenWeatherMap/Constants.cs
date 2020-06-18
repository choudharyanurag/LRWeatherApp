using System;
using System.Collections.Generic;
using System.Text;

namespace LoginRadius.WeatherApp.WeatherProvider.OpenWeatherMap
{
    internal static class Constants
    {
        public readonly static string OWMURL = "http://api.openweathermap.org/data/2.5/weather?q={0}&APPID={1}";
    }
}
