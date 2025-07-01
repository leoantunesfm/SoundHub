using FillGaps.SoundHub.WebApp.Models.Catalog;
using System.Text.Json.Serialization;

namespace FillGaps.SoundHub.WebApp.Models.UserEngagement
{
    public class FavoritosViewModel
    {
        [JsonPropertyName("musicasFavoritas")]
        public List<MusicaViewModel> MusicasFavoritas { get; set; } = new();

        [JsonPropertyName("artistasFavoritos")]
        public List<ArtistaViewModel> ArtistasFavoritos { get; set; } = new();
    }
}
