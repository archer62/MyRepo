using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eventful
{
    class Response
    {
        [JsonProperty("events")]
        public Events Events { get; set; }
        [JsonProperty("total_items")]
        public int Items { get; set; }
        [JsonProperty("page_count")]
        public int PageCount { get; set; }
    }
}
