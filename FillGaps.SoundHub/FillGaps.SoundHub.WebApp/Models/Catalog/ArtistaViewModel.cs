using System.Text.Json.Serialization;

namespace FillGaps.SoundHub.WebApp.Models.Catalog
{
    public class ArtistaViewModel
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        [JsonPropertyName("descricao")]
        public string Descricao { get; set; }

        [JsonPropertyName("imagemUrl")]
        public string? ImagemUrl { get; set; }
    }
}
