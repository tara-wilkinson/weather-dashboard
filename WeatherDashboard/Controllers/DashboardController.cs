using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeatherDashboard.Models;
using WeatherDashboard.Repositories;

namespace WeatherDashboard.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }

        // GET: Dashboard
        public ActionResult Weather()
        {
            List<IWeatherRepository> weatherReportRepositories = new List<IWeatherRepository>();
            weatherReportRepositories.Add(new ForecastIOWeatherRepository());
            weatherReportRepositories.Add(new WundergroundWeatherRepository());

            List<WeatherReport> weatherReports = new List<WeatherReport>();

            foreach (IWeatherRepository weatherRepository in weatherReportRepositories)
            {
                var weatherReport = weatherRepository.GetWeatherReport();
                weatherReports.Add(weatherReport);
            }

            return Json(weatherReports, JsonRequestBehavior.AllowGet);
        }
    }
}