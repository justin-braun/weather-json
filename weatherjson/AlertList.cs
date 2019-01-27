using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weatherjson
{
    public class AlertList
    {

        public object[] context { get; set; }
        public string type { get; set; }
        public Feature[] features { get; set; }
        public string title { get; set; }
        public DateTime? updated { get; set; }


        public class Feature
        {
            public string id { get; set; }
            public string type { get; set; }
            public object geometry { get; set; }
            public Properties properties { get; set; }
        }

        public class Properties
        {
            public string id { get; set; }
            public string type { get; set; }
            public string areaDesc { get; set; }
            public Geocode geocode { get; set; }
            public string[] affectedZones { get; set; }
            public Reference[] references { get; set; }
            public DateTime? sent { get; set; }
            public DateTime? effective { get; set; }
            public DateTime? onset { get; set; }
            public DateTime? expires { get; set; }
            public DateTime? ends { get; set; }
            public string status { get; set; }
            public string messageType { get; set; }
            public string category { get; set; }
            public string severity { get; set; }
            public string certainty { get; set; }
            public string urgency { get; set; }
            [JsonProperty(PropertyName = "event")]
            public string Event { get; set; }
            public string sender { get; set; }
            public string senderName { get; set; }
            public string headline { get; set; }
            public string description { get; set; }
            public string instruction { get; set; }
            public string response { get; set; }
            public Parameters parameters { get; set; }
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

    }
}
