using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ScoreService.Contracts
{
    [DataContract]
    public class Score
    {
        [JsonProperty]
        public string User { get; set; }
        
        [JsonProperty(nameof(Score))]
        public int Value { get; set; }
    }
}