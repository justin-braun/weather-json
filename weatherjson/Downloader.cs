using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace weatherjson
{
    public static class Downloader
    {
        public enum DataType
        {
            ShortTermForecastIndex,
            ZoneForecast,
            ActiveAdvisories,
            AllAdvisories
        }

        private const string API_ACTIVE_ADVISORIES_URL = @"https://api.weather.gov/alerts/active/area/MN";
        private const string API_ALL_ADVISORIES_URL = @"https://api.weather.gov/alerts?area=MN";
        private const string API_STF_INDEX_URL = @"https://api.weather.gov/products/types/NOW";
        private const string API_ZONECAST_URL = @"";

        public static string GetApiUrl(DataType dataType)
        {
            switch (dataType)
            {
                case DataType.ActiveAdvisories:
                    return API_ACTIVE_ADVISORIES_URL;
                case DataType.AllAdvisories:
                    return API_ALL_ADVISORIES_URL;
                case DataType.ShortTermForecastIndex:
                    return API_STF_INDEX_URL;
                case DataType.ZoneForecast:
                    return API_ZONECAST_URL;
            }

            return null;
        }

        public static string DownloadText(DataType dataType)
        {
            string text = "";

            using (var wc = new WebClient())
            {
                wc.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
                wc.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                text = wc.DownloadString(GetApiUrl(dataType));
            }

            return text;
        }

        public static string DownloadText(string url)
        {
            string text = "";

            using (var wc = new WebClient())
            {
                wc.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
                wc.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                text = wc.DownloadString(url);
            }

            return text;
        }
    }
}
