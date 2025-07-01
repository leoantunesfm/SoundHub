using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillGaps.SoundHub.Application.DTOs.Billing
{
    public class AssinaturaResponseDto
    {
        public Guid Id { get; set; }
        public string PlanoNome { get; set; }
        public string Status { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataVigencia { get; set; }
    }
}
