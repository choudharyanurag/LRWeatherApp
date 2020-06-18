using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using LoginRadius.WeatherApp.AppExtensions;
using LoginRadius.WeatherApp.Core;
using LoginRadius.WeatherApp.Models;
using LoginRadius.WeatherApp.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Logging;
using LoginRadius.WeatherApp.WeatherProvider.OpenWeatherMap;
using AutoMapper;
using System.Reflection;

namespace LoginRadius.WeatherApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHttpClient();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.Configure<OpenWeatherMapSettings>(Configuration.GetSection("OpenWeatherMapSettings"));

            services.AddScoped<IWeatherClient<string>, OpenWeatherHttpClient>(sp=> {
                var openWeartherHttpClient = sp.GetService<IHttpClientFactory>().CreateClient("OpenWeatherHttpClient");
                var typedClient =  sp.GetService<ITypedHttpClientFactory<OpenWeatherHttpClient>>();
                return typedClient.CreateClient(openWeartherHttpClient);
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Global Exception Handler
                app.UseExceptionHandler(builder=> {
                    builder.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        IExceptionHandlerFeature exceptionHandler = context.Features.Get<IExceptionHandlerFeature>();
                        if (exceptionHandler != null)
                        {
                            context.Response.AddErrorHeaders(exceptionHandler.Error.Message);
                            await context.Response.WriteAsync(exceptionHandler.Error.Message);
                        }
                    });
                });

                //app.UseHttpsRedirection();
            }

            app.UseRouting();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            //app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
