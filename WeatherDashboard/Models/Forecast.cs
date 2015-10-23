using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherDashboard.Models
{
    public class Forecast
    {
        public DateTime ForecastDate { get; set; }

        public string ForecastDateDisplay { get; set; }

        public Weather ForecastWeather { get; set; }

        public Forecast()
        {


        }
    }
}