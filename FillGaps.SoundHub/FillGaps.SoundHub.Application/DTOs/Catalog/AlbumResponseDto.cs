using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillGaps.SoundHub.Application.DTOs.Catalog
{
    public class AlbumResponseDto
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public int AnoLancamento { get; set; }
        public string? CapaUrl { get; set; }
        public Guid ArtistaId { get; set; }
    }
}
