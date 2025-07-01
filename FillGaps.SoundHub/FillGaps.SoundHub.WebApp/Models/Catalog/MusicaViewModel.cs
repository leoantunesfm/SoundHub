using System.Text.Json.Serialization;

namespace FillGaps.SoundHub.WebApp.Models.Catalog
{
    public class MusicaViewModel
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("titulo")]
        public string Titulo { get; set; }

        [JsonPropertyName("duracaoSegundos")]
        public int DuracaoSegundos { get; set; }

        [JsonPropertyName("albumId")]
        public Guid AlbumId { get; set; }
    }
}
