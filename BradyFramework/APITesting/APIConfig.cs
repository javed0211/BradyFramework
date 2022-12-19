using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradyFramework.APITesting
{
    public class Header
    {
        [JsonProperty("Content-Type")]
        public string ContentType { get; set; }
        public string Accept { get; set; }
    }

    public class APIConfig
    {
        public string BaseURL { get; set; }
        public Header Header { get; set; }
    }
}
