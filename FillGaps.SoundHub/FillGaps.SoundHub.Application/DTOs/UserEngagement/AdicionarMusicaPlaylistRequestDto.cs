using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillGaps.SoundHub.Application.DTOs.UserEngagement
{
    public class AdicionarMusicaPlaylistRequestDto
    {
        [Required]
        public Guid PlaylistId { get; set; }
        [Required]
        public Guid MusicaId { get; set; }
    }
}
