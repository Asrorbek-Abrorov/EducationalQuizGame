using Newtonsoft.Json;

namespace QuizApp;

public class QuizResponse
{
    [JsonProperty("response_code")]
    public int ResponseCode { get; set; }
    [JsonProperty("results")]
    public List<QuizQuestion> Results { get; set; }
}
