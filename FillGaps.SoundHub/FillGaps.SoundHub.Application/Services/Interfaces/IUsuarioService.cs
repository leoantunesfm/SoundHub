using FillGaps.SoundHub.Application.DTOs.UserEngagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillGaps.SoundHub.Application.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task FavoritarMusicaAsync(Guid usuarioId, Guid musicaId);
        Task DesfavoritarMusicaAsync(Guid usuarioId, Guid musicaId);
        Task FavoritarArtistaAsync(Guid usuarioId, Guid artistaId);
        Task DesfavoritarArtistaAsync(Guid usuarioId, Guid artistaId);
        Task<FavoritosResponseDto> ObterFavoritosAsync(Guid usuarioId);
    }
}
