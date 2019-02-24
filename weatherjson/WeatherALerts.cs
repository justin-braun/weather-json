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
        //private const string API_ALERT_URL = @"https://api.weather.gov/alerts/active/area/MN";
        private const string API_ALERT_URL = @"https://api.weather.gov/alerts?area=MN";

               

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

        public enum AlertStatus
        {
            New,
            Update,
            Cancel,
            All
        }

        public AlertSourceList GetWeatherAlerts(DateTime sinceDate, AlertFilterType alertType, AlertSortOrder sortOrder)
        {
            string contents = "";
            weatherjson.AlertSourceList asl = new AlertSourceList();

            try
            {
                // Download JSON Format alert data from API
                using (var wc = new WebClient())
                {
                    wc.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                    contents = wc.DownloadString(API_ALERT_URL);
                }

                // Deserialize object
                asl = JsonConvert.DeserializeObject<AlertSourceList>(contents);

                // Get collection of alerts to build county details
                AlertSourceList.Feature[] alerts = asl.AlertItems;

                foreach(var alert in alerts)
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

                    //alert.AlertInfo.CountyDetails = countiesList.ToArray();
                    alert.AlertInfo.Counties = countiesList.OrderBy(x => x.StateAbbrev).ThenBy(x => x.CountyName).ToArray();
                }

                // Merge county details back into dataset
                asl.AlertItems = alerts;

                // Filter based on sinceDate provided
                AlertSourceList.Feature[] filteredDateAlertItems = asl.AlertItems.Where(x => x.AlertInfo.SentTime.Value > sinceDate).ToArray();
                asl.AlertItems = filteredDateAlertItems;

                // Filter on alertType provided
                AlertSourceList.Feature[] filteredTypeAlertItems;

                switch (alertType)
                {
                    case AlertFilterType.Alerts:
                        filteredTypeAlertItems = asl.AlertItems.Where(x => x.AlertInfo.EventName.ToLower().Contains("warning") || x.AlertInfo.EventName.ToLower().Contains("watch") || x.AlertInfo.EventName.ToLower().Contains("advisory") || x.AlertInfo.EventName.ToLower().Contains("alert") || x.AlertInfo.EventName.ToLower().Contains("emergency")).ToArray();
                        asl.AlertItems = filteredTypeAlertItems;
                        break;
                    case AlertFilterType.Statements:
                        filteredTypeAlertItems = asl.AlertItems.Where(x => x.AlertInfo.EventName.ToLower().Contains("statement")).ToArray();
                        asl.AlertItems = filteredTypeAlertItems;
                        break;
                }

                // Sort by Alert Type
                //string[] customAlertOrder = { "Emergency", "Alert", "Warning", "Watch", "Advisory" };
                //
                //// Sort so most recent is top of list
                AlertSourceList.Feature[] SortAlertItems;

                switch (sortOrder)
                {
                    case AlertSortOrder.Ascending:
                        // SortAlertItems = asl.AlertItems.OrderBy(x => x.AlertInfo.EventName).ThenBy(x => x.AlertInfo.SentTime).ToArray();
                        SortAlertItems = asl.AlertItems.OrderBy(x => Array.IndexOf(WeatherEventTypes.Events, x.AlertInfo.EventName)).ThenBy(x => x.AlertInfo.SentTime).ToArray();
                        asl.AlertItems = SortAlertItems;
                        break;
                    case AlertSortOrder.Descending:
                        SortAlertItems = asl.AlertItems.OrderBy(x => Array.IndexOf(WeatherEventTypes.Events, x.AlertInfo.EventName)).ThenByDescending(x => x.AlertInfo.SentTime).ToArray();
                        asl.AlertItems = SortAlertItems;
                        break;
                }



                //AlertSourceList.Feature[] finalSortItems;
            }
            catch
            {
                throw;
            }

            return asl;
        }
    }
}
