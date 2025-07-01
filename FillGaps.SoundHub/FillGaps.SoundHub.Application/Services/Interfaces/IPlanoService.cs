using FillGaps.SoundHub.Application.DTOs.Billing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillGaps.SoundHub.Application.Services.Interfaces
{
    public interface IPlanoService
    {
        Task<PlanoResponseDto> CriarPlanoAsync(CriarPlanoRequestDto dto);
        Task<IEnumerable<PlanoResponseDto>> ObterPlanosAtivosAsync();
    }
}
