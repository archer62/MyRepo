using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eventful
{
    class Events
    {
        [JsonProperty("event")]
        public List<Event> Event { get; set; }
    }
}
