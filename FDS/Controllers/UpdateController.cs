using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FDS2.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FDS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UpdateController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private readonly IUpdate _updateService;

        public UpdateController(IUpdate updateService)
        {
            _updateService = updateService;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var update = _updateService.GetUpdate("F44A6650-7962-454A-971E-49B01F7D9E60");

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
