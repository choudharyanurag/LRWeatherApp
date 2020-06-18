using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using LoginRadius.WeatherApp.Core;
using LoginRadius.WeatherApp.DTO;
using LoginRadius.WeatherApp.Models;
using LoginRadius.WeatherApp.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LoginRadius.WeatherApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherNowController : ControllerBase
    {

        private readonly ILogger<WeatherNowController> _logger;
        private IWeatherClient<string> _weatherClient;
        private IConfiguration _configuration;
        OpenWeatherMapSettings _openWeatherSettings;
        IMapper _mapper;
        public WeatherNowController( IWeatherClient<string> weatherClient, 
                                            ILogger<WeatherNowController> logger, 
                                            IConfiguration config,
                                            IOptions<OpenWeatherMapSettings> openWeatherSettingsAccessor,
                                            IMapper mapper)
        {
            _weatherClient = weatherClient;
            _logger = logger;
            _configuration = config;
            _openWeatherSettings = openWeatherSettingsAccessor.Value;
            _mapper = mapper;
        }

        [HttpGet("{loc}")]
        public async Task<IActionResult> Get(string loc)
        {
            Dictionary<string, string> query = new Dictionary<string, string>();
            query.Add("Location", loc);
            query.Add("APIKEY", _openWeatherSettings.APIKEY);

            string respAsString =  await _weatherClient.GetWeatherAsync(query);
            var obj = JsonSerializer.Deserialize<OpenWeatherResponse>(respAsString);
            WeatherDTO cleanWeatherDesiredObject = _mapper.Map<OpenWeatherResponse, WeatherDTO>(obj);
            return Ok(cleanWeatherDesiredObject);
        }
    }
}
