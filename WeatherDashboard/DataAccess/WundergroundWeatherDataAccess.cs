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
    public class WundergroundWeatherDataAccess
    {
        public WundergroundWeatherDataAccess()
        {


        }

        public JObject GetWeatherCurrentJSON(Location location)
        {
            WebClient weatherWebClient = new WebClient();
            string apiURL = String.Format("http://api.wunderground.com/api/8c1d3adae24c300f/conditions/q/{0}/{1}.json", location.State, location.City.Replace(" ", "_"));
            var weatherObject = weatherWebClient.DownloadString(apiURL);

            var weatherJSONObject = JObject.Parse(weatherObject);

            return weatherJSONObject;
        }

        public JObject GetWeatherForecast10DayJSON(Location location)
        {
            WebClient weatherWebClient = new WebClient();
            string apiURL = String.Format("http://api.wunderground.com/api/8c1d3adae24c300f/forecast10day/q/{0}/{1}.json", location.State, location.City.Replace(" ", "_"));
            var weatherObject = weatherWebClient.DownloadString(apiURL);

            var weatherJSONObject = JObject.Parse(weatherObject);

            return weatherJSONObject;
        }
    }


}