using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weatherjson
{
    public class ZoneForecast
    {
        //public Context context { get; set; }
        //public Geometry geometry { get; set; }
        [JsonProperty(PropertyName = "updated")]
        public DateTime LastUpdated { get; set; }
        [JsonProperty(PropertyName = "periods")]
        public ForecastPeriod[] ForecastPeriods { get; set; }

        //public class Context
        //{
        //    public string wx { get; set; }
        //    public string geo { get; set; }
        //    public string unit { get; set; }
        //    public string vocab { get; set; }
        //}

        //public class Geometry
        //{
        //}

        public class ForecastPeriod
        {
            [JsonProperty(PropertyName = "number")]
            public int Number { get; set; }
            [JsonProperty(PropertyName = "name")]
            public string Name { get; set; }
            [JsonProperty(PropertyName = "detailedForecast")]
            public string DetailedForecast { get; set; }
        }

    }
}
