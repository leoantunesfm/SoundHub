using System.Text.Json.Serialization;

namespace FillGaps.SoundHub.WebApp.Models.Billing
{
    public class AssinaturaViewModel
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("planoNome")]
        public string PlanoNome { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("dataVigencia")]
        public DateTime DataVigencia { get; set; }
    }
}
