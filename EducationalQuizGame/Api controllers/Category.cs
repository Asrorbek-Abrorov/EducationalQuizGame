using Newtonsoft.Json;

namespace EducationalQuizGame.Uis
{
    public partial class TextQuizesUi
    {
        public class Category
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }
        }
    }
}