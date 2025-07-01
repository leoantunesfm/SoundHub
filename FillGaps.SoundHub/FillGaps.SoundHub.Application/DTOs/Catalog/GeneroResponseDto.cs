using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillGaps.SoundHub.Application.DTOs.Catalog
{
    public class GeneroResponseDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string? Descricao { get; set; }
    }
}
