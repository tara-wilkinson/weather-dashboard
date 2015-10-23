using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using WeatherDashboard.Models;
using WeatherDashboard.DataAccess;

namespace WeatherDashboard.Repositories
{
    public class WundergroundWeatherRepository : IWeatherRepository
    {
        public WeatherReport GetWeatherReport()
        {
            var weatherDataAccess = new WundergroundWeatherDataAccess();
            var weatherDataCurrentJSON = weatherDataAccess.GetWeatherCurrentJSON(Location.GetDefaultLocation());
            var weatherDataCForecast10DayJSON = weatherDataAccess.GetWeatherForecast10DayJSON(Location.GetDefaultLocation());

            JObject currentWeatherJSON = JObject.Parse(weatherDataCurrentJSON["current_observation"].ToString());
            Weather currentWeather = WeatherDataMapper(currentWeatherJSON, "weather", "temp_f", "");

            JObject forecastWeatherJSON = JObject.Parse(weatherDataCForecast10DayJSON["forecast"]["simpleforecast"].ToString());
            List<Forecast> forecasts = ForecastsDataMapper(forecastWeatherJSON);

            WeatherReport LatestWeatherReport = new WeatherReport
            {
                CurrentWeather = currentWeather,
                FutureWeather = forecasts,
                Provider = "Wunderground"
            };


            return LatestWeatherReport;
        }

        public List<Forecast> ForecastsDataMapper(JObject jsonObject)
        {
            List<Forecast> forecasts = new List<Forecast>();

            JArray dataArray = new JArray(jsonObject["forecastday"].Children());

            foreach (var item in dataArray.Children())
            {
                JObject itemJSON = JObject.Parse(item.ToString());

                Forecast forecast = new Forecast();

                forecast.ForecastDate = FieldMapperDateTime(JObject.Parse(itemJSON["date"].ToString()), "epoch");
                forecast.ForecastDateDisplay = forecast.ForecastDate.ToShortDateString();
                forecast.ForecastWeather = WeatherDataMapper(itemJSON, "conditions", "high", "fahrenheit");

                forecasts.Add(forecast);
            }

            return forecasts;
        }

        public Weather WeatherDataMapper(JObject jsonObject, string conditionsDescriptionFieldName, string temperatureFarenheitFieldName1, string temperatureFarenheitFieldName2)
        {
            string conditionsDescription = FieldMapperString(jsonObject, conditionsDescriptionFieldName);
            double temperatureFarenheit = FieldMapperDouble(jsonObject, temperatureFarenheitFieldName1, temperatureFarenheitFieldName2);

            Weather weather = new Weather();
            weather.ConditionsDescription = conditionsDescription;
            weather.TemperatureFarenheit = temperatureFarenheit;

            return weather;
        }

        public string FieldMapperString(JObject jsonObject, string fieldName)
        {
            return jsonObject[fieldName].ToString();
        }

        public double FieldMapperDouble(JObject jsonObject, string fieldName1, string fieldName2)
        {
            if (!String.IsNullOrEmpty(fieldName2))
                return double.Parse(jsonObject[fieldName1][fieldName2].ToString());
            else
                return double.Parse(jsonObject[fieldName1].ToString());
        }


        public DateTime FieldMapperDateTime(JObject jsonObject, string fieldName)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(long.Parse(jsonObject[fieldName].ToString()));
        }

        public WeatherReport GetWeatherReportTest()
        {
            Weather currentWeather = new Weather { TemperatureFarenheit = 98.6, ConditionsDescription = "Hot and Sunny" };

            Forecast forecast1 = new Forecast
            {
                ForecastDate = DateTime.Parse("11-01-2015"),
                ForecastDateDisplay = (DateTime.Parse("11-01-2015")).ToShortDateString(),
                ForecastWeather = new Weather { TemperatureFarenheit = 88.6, ConditionsDescription = "Warm and Rainy" }
            };

            Forecast forecast2 = new Forecast
            {
                ForecastDate = DateTime.Parse("11-02-2015"),
                ForecastDateDisplay = (DateTime.Parse("11-02-2015")).ToShortDateString(),
                ForecastWeather = new Weather { TemperatureFarenheit = 78.6, ConditionsDescription = "Beautiful Day" }
            };

            Forecast forecast3 = new Forecast
            {
                ForecastDate = DateTime.Parse("11-03-2015"),
                ForecastDateDisplay = (DateTime.Parse("11-03-2015")).ToShortDateString(),
                ForecastWeather = new Weather { TemperatureFarenheit = 68.6, ConditionsDescription = "Cool and Breezy" }
            };

            List<Forecast> forecastList = new List<Forecast>();
            forecastList.Add(forecast1);
            forecastList.Add(forecast2);
            forecastList.Add(forecast3);

            WeatherReport LatestWeatherReport = new WeatherReport
            {
                CurrentWeather = currentWeather,
                FutureWeather = forecastList
            };

            return LatestWeatherReport;
        }
    }
}