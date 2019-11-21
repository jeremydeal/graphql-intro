using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BabysFirstGraphQLService.GraphTypes;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BabysFirstGraphQLService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastGraphController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly WeatherSchema _weatherSchema;

        public WeatherForecastGraphController(
            ILogger<WeatherForecastController> logger,
            WeatherSchema weatherSchema
        )
        {
            _logger = logger;
            _weatherSchema = weatherSchema;
        }

        [HttpGet]
        public string Get([FromQuery]string query = null)
        {
            var json = _weatherSchema.Execute(_ =>
            {
                // default query, for easy testing
                _.Query = query ?? @"{ weather { date temperatureC temperatureF } }";
            });

            return json;
        }

        [HttpPost]
        public string Post([FromBody]string query)
        {
            var json = _weatherSchema.Execute(_ =>
            {
                // default query, for easy testing
                _.Query = query;
            });

            return json;
        }
    }
}
