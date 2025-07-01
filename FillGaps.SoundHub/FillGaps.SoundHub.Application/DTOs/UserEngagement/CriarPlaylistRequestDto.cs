using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillGaps.SoundHub.Application.DTOs.UserEngagement
{
    public class CriarPlaylistRequestDto
    {
        [Required]
        public string Nome { get; set; }
        public string? Descricao { get; set; }
    }
}
