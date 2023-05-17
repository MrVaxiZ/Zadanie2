using System.Text.Json.Serialization;

namespace MilitariaPytanieRekrutacyjne2
{
    /// <summary>
    /// Klasa Area reprezentuje dane pobierane z API
    /// </summary>
    public class Area
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nazwa")]
        public string Name { get; set; }

        [JsonPropertyName("id-nadrzedny-element")]
        public int ParentId { get; set; }

        [JsonPropertyName("nazwa-poziom")]
        public string LevelName { get; set; }
    }
}