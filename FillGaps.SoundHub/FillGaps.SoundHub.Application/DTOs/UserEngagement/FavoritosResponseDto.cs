using FillGaps.SoundHub.Application.DTOs.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillGaps.SoundHub.Application.DTOs.UserEngagement
{
    public class FavoritosResponseDto
    {
        public List<MusicaResponseDto> MusicasFavoritas { get; set; } = new();
        public List<ArtistaResponseDto> ArtistasFavoritos { get; set; } = new();
    }
}
