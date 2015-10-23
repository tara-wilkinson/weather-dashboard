using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherDashboard.Models
{
    public class WeatherReport
    {
        public Weather CurrentWeather { get; set; }

        public List<Forecast> FutureWeather { get; set; }

        public string Provider { get; set; }

        public WeatherReport()
        {

        }
    }
}