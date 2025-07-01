using FillGaps.SoundHub.Application.DTOs.Billing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillGaps.SoundHub.Application.Services.Interfaces
{
    public interface IAssinaturaService
    {
        Task<AssinaturaResponseDto> CriarAssinaturaAsync(CriarAssinaturaRequestDto dto, Guid usuarioId);
        Task<AssinaturaResponseDto?> ObterAssinaturaPorUsuarioIdAsync(Guid usuarioId);
    }
}
