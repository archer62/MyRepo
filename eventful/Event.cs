using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eventful
{
    class Event
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("start_time")]
        public DateTime StartTime { get; set; }
        [JsonProperty("venue_name")]
        public string VenueName { get; set; }
        [JsonProperty("venue_address")]
        public string VenueAddress { get; set; }
        [JsonProperty("city_name")]
        public string City { get; set; }
    }
}
