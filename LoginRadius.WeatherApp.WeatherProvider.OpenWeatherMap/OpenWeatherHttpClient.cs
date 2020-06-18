using LoginRadius.WeatherApp.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace LoginRadius.WeatherApp.WeatherProvider.OpenWeatherMap
{
    public class OpenWeatherHttpClient : IWeatherClient<string>
    {
        private HttpClient _httpClient;
        private ILogger<OpenWeatherHttpClient> _logger;
        public OpenWeatherHttpClient(HttpClient client, ILogger<OpenWeatherHttpClient> logger)
        {
            _httpClient = client;
            _logger = logger;
        }

        public async Task<string> GetWeatherAsync(Dictionary<string, string> query)
        {
            // We know that we have just two parameters in the query for OpenWeatherMap weather provider. 
            // Using this query to get the 
            // first query parameter
            // Second is API Key

            string location = query["Location"];
            string apiKey = query["APIKEY"];
            _logger.LogInformation($"Calling Weather Info {location}");

            string strUri = string.Format(Constants.OWMURL, location, apiKey);
            String stringResp = await _httpClient.GetStringAsync(strUri);
            return stringResp;
        }
    }
}
