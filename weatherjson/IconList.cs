using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weatherjson
{
    public class IconList
    {
        [JsonProperty(PropertyName = "urlprefix")]
        public string UrlPrefix { get; set; }
        [JsonProperty(PropertyName = "icon")]
        public Icon[] Icons { get; set; }

        public class Icon
        {
            [JsonProperty(PropertyName = "day")]
            public string Day { get; set; }
            [JsonProperty(PropertyName = "night")]
            public string Night { get; set; }
            [JsonProperty(PropertyName = "shortText")]
            public string ShortText { get; set; }
            [JsonProperty(PropertyName = "phrases")]
            public object Phrases { get; set; }
        }

    }
}
