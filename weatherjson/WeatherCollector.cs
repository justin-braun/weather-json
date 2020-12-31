using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace weatherjson
{
    public static class WeatherCollector
    {
        public static string apiSampleUrl = "https://api.weather.gov/zones/forecast/MNZ069/forecast"

        private static string DownloadApiResult(string url)
        {
            using (var wc = new WebClient())
            {
                wc.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
                wc.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                return wc.DownloadString(url);
            }
        }

        public static ZoneForecast GetZonecast()
        {
            string apiResult = DownloadApiResult(apiSampleUrl);

            ZoneForecast zonecastJson = JsonConvert.DeserializeObject<ZoneForecast>(apiResult);

            return zonecastJson;
        }
    }
}
