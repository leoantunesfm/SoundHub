using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillGaps.SoundHub.Application.DTOs.Catalog
{
    public class CriarGeneroRequestDto
    {
        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [StringLength(255)]
        public string? Descricao { get; set; }
    }
}
