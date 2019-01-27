using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace weatherjson
{
    public class WeatherAlerts
    {
        private const string API_ALERT_URL = @"https://api.weather.gov/alerts/active/area/MN";

        public AlertList GetWeatherAlerts()
        {
            string contents = "";
            weatherjson.AlertList alerts = new AlertList();

            // Download CAP Format alert data
            try
            {
                using(var wc = new WebClient())
                {
                    wc.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                    contents = wc.DownloadString(API_ALERT_URL);
                    alerts = JsonConvert.DeserializeObject<AlertList>(contents);
                    alerts.features.OrderByDescending(x => x.properties.sent);
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return alerts;
        }
    }
}
