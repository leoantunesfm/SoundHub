using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillGaps.SoundHub.Application.DTOs.Catalog
{
    public class CriarMusicaRequestDto
    {
        [Required(ErrorMessage = "O nome da música é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome da música deve ter no máximo 100 caracteres.")]
        public string Titulo { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int DuracaoSegundos { get; set; }

        [Required]
        public Guid AlbumId { get; set; }

        public List<Guid> GenerosId { get; set; } = new();
    }
}
