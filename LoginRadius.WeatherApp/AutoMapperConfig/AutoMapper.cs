using AutoMapper;
using LoginRadius.WeatherApp.DTO;
using LoginRadius.WeatherApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginRadius.WeatherApp
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<OpenWeatherResponse, WeatherDTO>()
                .ForMember(o => o.Temperature, ac => ac.MapFrom(o => o.main.temp + ConstValues.KELVIN))
                .ForMember(o => o.MaxTemperature, ac => ac.MapFrom(o => o.main.temp_max + ConstValues.KELVIN))
                .ForMember(o => o.MinTemperature, ac => ac.MapFrom(o => o.main.temp_min + ConstValues.KELVIN))
                .ForMember(o => o.FeelsLike, ac => ac.MapFrom(o => o.main.feels_like + ConstValues.KELVIN))
                .ForMember(o => o.Pressure , ac => ac.MapFrom(o => o.main.pressure))
                .ForMember(o => o.Humidity , ac => ac.MapFrom(o => o.main.humidity))
                .ForMember(o=>o.Summary , ac=>ac.MapFrom(o=>  o.weather.ToList()[0].description))
                .ForMember(o => o.Name, ac => ac.MapFrom(o => o.name));
        }
    }
}
