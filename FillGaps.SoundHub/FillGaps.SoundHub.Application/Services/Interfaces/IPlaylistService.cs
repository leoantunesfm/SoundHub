using FillGaps.SoundHub.Application.DTOs.UserEngagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillGaps.SoundHub.Application.Services.Interfaces
{
    public interface IPlaylistService
    {
        Task<PlaylistResponseDto> CriarPlaylistAsync(CriarPlaylistRequestDto dto, Guid usuarioId);
        Task AdicionarMusicaAPlaylistAsync(AdicionarMusicaPlaylistRequestDto dto, Guid usuarioId);
    }
}
