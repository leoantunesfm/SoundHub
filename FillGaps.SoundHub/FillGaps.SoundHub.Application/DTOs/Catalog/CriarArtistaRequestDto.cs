using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillGaps.SoundHub.Application.DTOs.Catalog
{
    public class CriarArtistaRequestDto
    {
        [Required(ErrorMessage = "O nome do artista é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do artista deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [StringLength(250, ErrorMessage = "A descrição deve ter no máximo 250 caracteres.")]
        public string Descricao { get; set; }
    }
}
