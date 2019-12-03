using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace TOM
{
    public class Assignment : CanvasItem
    {
        [JsonProperty(PropertyName = "due_at")]
        public DateTime Due_at { get; set; }

        [JsonProperty(PropertyName = "html_url")]
        public string Html_url { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}, Due at: {Due_at}";
        }

    }
}