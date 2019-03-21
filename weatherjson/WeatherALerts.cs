using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace weatherjson
{
    public class WeatherAlerts
    {
        private const string API_ALERT_URL = @"https://api.weather.gov/alerts/active/area/MN";
        private const string API_ALL_ALERT_URL = @"https://api.weather.gov/alerts?area=MN";

        public enum AlertFilterType
        {
            Alerts,
            Statements,
            All
        }

        public enum AlertSortOrder
        {
            Ascending,
            Descending
        }
        public AlertSourceList GetWeatherAlertsForCounty(DateTime sinceDate, string countyName)
        {
            AlertSourceList alerts = GetWeatherAlerts(sinceDate, AlertFilterType.All, AlertSortOrder.Descending);

            //if (alerts.AlertItems.Count() == 0)
            //    return alerts;

            // More than 0 alerts available

            alerts.AlertItems = alerts.AlertItems.Where(x => x.AlertInfo.AreaDescRaw.ToLower().Contains(countyName.ToLower())).ToArray();

            return alerts;


        }
        public AlertSourceList GetWeatherAlerts(DateTime sinceDate, AlertFilterType alertType, AlertSortOrder sortOrder)
        {
            string alertContents = "";
            string alertCancelContents = "";

            weatherjson.AlertSourceList alertActiveSourceList = new AlertSourceList();
            weatherjson.AlertSourceList alertCancelSourceList = new AlertSourceList();
            weatherjson.AlertSourceList alertList = new AlertSourceList();
            try
            {
                // Download JSON Format alert data from API - ACTIVE MN ALERTS ONLY
                using (var wc = new WebClient())
                {
                    wc.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
                    wc.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                    alertContents = wc.DownloadString(API_ALERT_URL);
                }

                // Download JSON Format alert data from API - MN CANCELLATIONS ONLY
                using (var wc = new WebClient())
                {
                    wc.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
                    wc.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                    alertCancelContents = wc.DownloadString(API_ALL_ALERT_URL);
                }

                // Deserialize objects
                alertActiveSourceList = JsonConvert.DeserializeObject<AlertSourceList>(alertContents);
                alertCancelSourceList = JsonConvert.DeserializeObject<AlertSourceList>(alertCancelContents);

                // Filter based on sinceDate and don't include expired alerts
                alertActiveSourceList.AlertItems = alertActiveSourceList.AlertItems.Where(x => x.AlertInfo.SentTime > sinceDate && x.AlertInfo.EndsTime >= DateTime.Now).ToArray();
                alertCancelSourceList.AlertItems = alertCancelSourceList.AlertItems.Where(x => x.AlertInfo.SentTime > sinceDate && x.AlertInfo.EndsTime >= DateTime.Now).ToArray();

                // Merge both datasets together into a new list
                alertList.SourceLastUpdated = alertActiveSourceList.SourceLastUpdated;
                alertList.AlertItems = alertActiveSourceList.AlertItems.Concat(alertCancelSourceList.AlertItems).ToArray();

                // Add county details
                foreach (var alert in alertList.AlertItems)
                {
                    
                    List<string> counties = alert.AlertInfo.AreaDescRaw.Split(';').ToList();
                    string[] geocodes = alert.AlertInfo.Geocodes.UGC;

                    var combinedList = counties.Zip(geocodes, (c, s) => new { CountyName = c.Trim(), StateAbbrev = s.Substring(0, 2) }).OrderBy(x => x.StateAbbrev).ToList();

                    List<weatherjson.AlertSourceList.Counties> countiesList = new List<AlertSourceList.Counties>();

                    foreach (var county in combinedList)
                    {
                        weatherjson.AlertSourceList.Counties cd = new AlertSourceList.Counties();
                        cd.CountyName = county.CountyName;
                        cd.StateAbbrev = county.StateAbbrev;
                        countiesList.Add(cd);
                    }

                    // Sort counties list
                    alert.AlertInfo.Counties = countiesList.OrderBy(x => x.StateAbbrev).ThenBy(x => x.CountyName).ToArray();
                }

                // Filter for alert type
                switch (alertType)
                {
                    case AlertFilterType.Alerts:
                        alertList.AlertItems = alertList.AlertItems.Where(x => x.AlertInfo.EventName.ToLower().Contains("warning") || x.AlertInfo.EventName.ToLower().Contains("watch") || x.AlertInfo.EventName.ToLower().Contains("advisory") || x.AlertInfo.EventName.ToLower().Contains("alert") || x.AlertInfo.EventName.ToLower().Contains("emergency")).ToArray();
                        break;
                    case AlertFilterType.Statements:
                        alertList.AlertItems = alertList.AlertItems.Where(x => x.AlertInfo.EventName.ToLower().Contains("statement")).ToArray();
                        break;
                }

                // Sort results
                switch (sortOrder)
                {
                    case AlertSortOrder.Ascending:
                        alertList.AlertItems = alertList.AlertItems.OrderBy(x => Array.IndexOf(WeatherEventTypes.MessageTypes, x.AlertInfo.MessageType)).ThenBy(x => Array.IndexOf(WeatherEventTypes.Events, x.AlertInfo.EventName)).ThenBy(x => x.AlertInfo.SentTime).ToArray();
                        break;
                    case AlertSortOrder.Descending:
                        alertList.AlertItems = alertActiveSourceList.AlertItems.OrderBy(x => Array.IndexOf(WeatherEventTypes.MessageTypes, x.AlertInfo.MessageType)).ThenBy(x => Array.IndexOf(WeatherEventTypes.Events, x.AlertInfo.EventName)).ThenByDescending(x => x.AlertInfo.SentTime).ToArray();
                        break;
                }
            }
            catch
            {
                throw;
            }

            // Return final object
            return alertList;
        }
    }
}
