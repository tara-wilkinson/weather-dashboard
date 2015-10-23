using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherDashboard.Models
{
    public class Weather
    {
        public double TemperatureFarenheit { get; set; }

        public string ConditionsDescription { get; set; }

        public Weather()
        {

        }
    }
}