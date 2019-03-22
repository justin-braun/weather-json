using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weatherjson
{
    public class Forecaster
    {
       
        public List<ShortTermForecast> GetShortTermForecast(string IssuingOffice)
        {
            string text = Downloader.DownloadText(Downloader.DataType.ShortTermForecastIndex);
            ShortTermForecastIndex stfIndex = new ShortTermForecastIndex();
            stfIndex = JsonConvert.DeserializeObject<ShortTermForecastIndex>(text);
            stfIndex.ForecastIndex = stfIndex.ForecastIndex.Where(x => x.IssuingOffice.ToLower() == IssuingOffice.ToLower()).ToArray();

            List<ShortTermForecast> shortTermForecasts = new List<ShortTermForecast>();

            if (stfIndex.ForecastIndex.Count() > 0)
            {
                foreach(var forecast in stfIndex.ForecastIndex)
                {
                    ShortTermForecast stf = new ShortTermForecast();
                    string stringStf = Downloader.DownloadText(forecast.ForecastUrl);
                    stf = JsonConvert.DeserializeObject<ShortTermForecast>(stringStf);
                    shortTermForecasts.Add(stf);
                }
                
            }

            return shortTermForecasts;

        }

    }
}
