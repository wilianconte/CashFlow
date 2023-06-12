using Newtonsoft.Json;

namespace CashFlow.Data.Model
{
    public class BaseEntity
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "partitionKey")]
        public string Partition { get; set; }

    }
}
