using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weatherjson
{
    public class ShortTermForecast
    {
        [JsonProperty(PropertyName = "@id")]
        public string ForecastUrl { get; set; }
        [JsonProperty(PropertyName = "id")]
        public string ForecastId { get; set; }
        [JsonProperty(PropertyName = "wmoCollectiveId")]
        public string WmoCollectiveId { get; set; }
        [JsonProperty(PropertyName = "issuingOffice")]
        public string IssuingOffice { get; set; }
        [JsonProperty(PropertyName = "issuanceTime")]
        public DateTime IssuanceTime { get; set; }
        [JsonProperty(PropertyName = "productCode")]
        public string ProductCode { get; set; }
        [JsonProperty(PropertyName = "productName")]
        public string ProductName { get; set; }
        [JsonProperty(PropertyName = "productText")]
        public string ForecastText { get; set; }
    }
}
