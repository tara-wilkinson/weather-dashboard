using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherDashboard.Models
{
    public class Location
    {

        public Location(double latitude, double longitude, string state, string city, string zip)
        {
            Latitude = latitude;
            Longitude = longitude;
            State = state;
            City = city;
            Zip = zip;
        }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Zip { get; set; }

        public static Location GetDefaultLocation()
        {
            return new Location(39.84668, -75.711667, "PA", "Kennett Square", "19348");
        }
    }
}