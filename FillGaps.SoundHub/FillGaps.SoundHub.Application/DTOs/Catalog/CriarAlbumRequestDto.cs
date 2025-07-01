using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillGaps.SoundHub.Application.DTOs.Catalog
{
    public class CriarAlbumRequestDto
    {
        [Required]
        [StringLength(150)]
        public string Titulo { get; set; }

        [Required]
        public int AnoLancamento { get; set; }

        public string? CapaUrl { get; set; }

        [Required]
        public Guid ArtistaId { get; set; }
    }
}
