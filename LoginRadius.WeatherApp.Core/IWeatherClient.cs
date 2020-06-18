using System.Collections.Generic;
using System.Threading.Tasks;


namespace LoginRadius.WeatherApp.Core
{
    public interface IWeatherClient<T>
    {
        public Task<T> GetWeatherAsync(Dictionary<string,string> query);
    }
}