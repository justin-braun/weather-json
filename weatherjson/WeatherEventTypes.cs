using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weatherjson
{
    public static class WeatherEventTypes
    {
        public static string[] Events = {
            "Tornado Warning",
            "Severe Thunderstorm Warning",
            "Flood Warning",
            "Flash Flood Warning",
            "Blizzard Warning",
            "Winter Storm Warning",
            "Heat Warning",
            "Wind Chill Warning",
            "High Wind Warning",
            "Ice Storm Warning",
            "Tornado Watch",
            "Severe Thunderstorm Watch",
            "Flash Flood Watch",
            "Flood Watch",
            "Blizzard Watch",
            "Winter Storm Watch",
            "Wind Chill Watch",
            "High Wind Watch",
            "Fire Weather Watch",
            "Winter Weather Advisory",
            "Freezing Rain Advisory",
            "Wind Chill Advisory",
            "Flood Advisory",
            "Heat Advisory",
            "Dense Fog Advisory",
            "Wind Advisory",
            "Severe Weather Statement",
            "Special Weather Statement"
        };

        public static string[] MessageTypes =
        {
            "Cancel",
            "Alert",
            "Update"
        };

        public static List<string> GetEvents()
        {
            return Events.ToList();
        }
    }
}
