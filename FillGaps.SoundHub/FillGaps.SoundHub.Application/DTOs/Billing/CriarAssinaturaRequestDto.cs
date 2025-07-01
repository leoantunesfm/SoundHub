using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillGaps.SoundHub.Application.DTOs.Billing
{
    public class CriarAssinaturaRequestDto
    {
        [Required(ErrorMessage = "O ID do plano é obrigatório.")]
        public Guid PlanoId { get; set; }
    }
}
