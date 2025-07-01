using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillGaps.SoundHub.Application.DTOs.Billing
{
    public class CriarPlanoRequestDto
    {
        [Required]
        public string Nome { get; set; }
        public string Descricao { get; set; }

        [Required]
        [Range(0.00, 1000)]
        public decimal Preco { get; set; }
    }
}
