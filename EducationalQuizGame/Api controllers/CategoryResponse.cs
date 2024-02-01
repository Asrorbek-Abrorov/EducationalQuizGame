using Newtonsoft.Json;

namespace EducationalQuizGame.Uis
{
    public partial class TextQuizesUi
    {
        public class CategoryResponse
        {
            [JsonProperty("trivia_categories")]
            public List<Category> Categories { get; set; }
        }
    }
}