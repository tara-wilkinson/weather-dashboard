using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using WeatherDashboard.Models;

namespace WeatherDashboard.DataAccess
{
    public class ForecastIOWeatherDataAccess
    {
        public ForecastIOWeatherDataAccess()
        {


        }

        public JObject GetWeatherJSON(Location location)
        {
            WebClient weatherWebClient = new WebClient();
            string apiURL = String.Format("https://api.forecast.io/forecast/fb51689a534b69ac23eb524d69ed3c5e/{0}, {1}", location.Latitude, location.Longitude);
            var weatherObject = weatherWebClient.DownloadString(apiURL);

            var weatherJSONObject = JObject.Parse(weatherObject);

            return weatherJSONObject;
        }
    }
}