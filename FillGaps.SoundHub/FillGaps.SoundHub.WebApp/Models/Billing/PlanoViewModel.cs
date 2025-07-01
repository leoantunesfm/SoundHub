using System.Text.Json.Serialization;

namespace FillGaps.SoundHub.WebApp.Models.Billing
{
    public class PlanoViewModel
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        [JsonPropertyName("descricao")]
        public string Descricao { get; set; }

        [JsonPropertyName("preco")]
        public decimal Preco { get; set; }
    }
}
