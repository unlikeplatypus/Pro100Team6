using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace TOM
{
    internal class Assignment : CanvasItem
    {
        [JsonProperty(PropertyName = "due_at")]
        public DateTime Due_at { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}, Due at: {Due_at}";
        }
    }
}