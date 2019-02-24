using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weatherjson
{
    public class AlertSourceList
    {
        [JsonProperty(PropertyName = "context")]
        public object[] SourceContext { get; set; }
        [JsonProperty(PropertyName = "type")]
        public string SourceType { get; set; }
        [JsonProperty(PropertyName = "features")]
        public Feature[] AlertItems { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string SourceTitle { get; set; }
        [JsonProperty(PropertyName = "updated")]
        public DateTime? SourceLastUpdated { get; set; }


        public class Feature
        {
            [JsonProperty(PropertyName = "id")]
            public string AlertIdUrl { get; set; }
            [JsonProperty(PropertyName = "type")]
            public string AlertType { get; set; }
            [JsonProperty(PropertyName = "geometry")]
            public object Geometry { get; set; }
            [JsonProperty(PropertyName = "properties")]
            public Properties AlertInfo { get; set; }
        }

        public class Properties
        {
            [JsonProperty(PropertyName = "id")]
            public string AlertID { get; set; }
            [JsonProperty(PropertyName = "type")]
            public string AlertType { get; set; }
            [JsonProperty(PropertyName = "areaDesc")]
            public string AreaDescRaw { get; set; }
            [JsonProperty(PropertyName = "geocode")]
            public Geocode Geocodes { get; set; }
            [JsonProperty(PropertyName = "affectedZones")]
            public string[] AffectedZonesUrls { get; set; }
            [JsonProperty(PropertyName = "references")]
            public Reference[] References { get; set; }
            [JsonProperty(PropertyName = "sent")]
            public DateTime? SentTime { get; set; }
            [JsonProperty(PropertyName = "effective")]
            public DateTime? EffectiveTime { get; set; }
            [JsonProperty(PropertyName = "onset")]
            public DateTime? OnsetTime { get; set; }
            [JsonProperty(PropertyName = "expires")]
            public DateTime? ExpirationTime { get; set; }
            [JsonProperty(PropertyName = "ends")]
            public DateTime? EndsTime { get; set; }
            [JsonProperty(PropertyName = "status")]
            public string Status { get; set; }
            [JsonProperty(PropertyName = "messageType")]
            public string MessageType { get; set; }
            [JsonProperty(PropertyName = "category")]
            public string Category { get; set; }
            [JsonProperty(PropertyName = "severity")]
            public string Severity { get; set; }
            [JsonProperty(PropertyName = "certainty")]
            public string Certainty { get; set; }
            [JsonProperty(PropertyName = "urgency")]
            public string Urgency { get; set; }
            [JsonProperty(PropertyName = "event")]
            public string EventName { get; set; }
            [JsonProperty(PropertyName = "sender")]
            public string SenderEmail { get; set; }
            [JsonProperty(PropertyName = "senderName")]
            public string SenderName { get; set; }
            [JsonProperty(PropertyName = "headline")]
            public string Headline { get; set; }
            [JsonProperty(PropertyName = "description")]
            public string Description { get; set; }
            [JsonProperty(PropertyName = "instruction")]
            public string Instruction { get; set; }
            [JsonProperty(PropertyName = "response")]
            public string Response { get; set; }
            [JsonProperty(PropertyName = "parameters")]
            public Parameters Parameters { get; set; }
            [JsonProperty(PropertyName = "replacedBy")]
            public string ReplacedBy { get; set; }
            [JsonProperty(PropertyName = "replacedAt")]
            public string ReplacedAt { get; set; }
            public Counties[] Counties { get; set; }
        }

        public class Geocode
        {
            public string[] UGC { get; set; }
            public string[] SAME { get; set; }
        }

        public class Parameters
        {
            public string[] NWSheadline { get; set; }
            public string[] EASORG { get; set; }
            public string[] PIL { get; set; }
            public string[] BLOCKCHANNEL { get; set; }
            public string[] HazardType { get; set; }
            public string[] VTEC { get; set; }
            public DateTime[] eventEndingTime { get; set; }
        }

        public class Reference
        {
            public string id { get; set; }
            public string identifier { get; set; }
            public string sender { get; set; }
            public DateTime? sent { get; set; }
        }

        public class Counties
        {
            public string CountyName { get; set; }
            public string StateAbbrev { get; set; }
        }
    }
}
