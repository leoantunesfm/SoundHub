using FillGaps.SoundHub.Application.DTOs.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillGaps.SoundHub.Application.Services.Interfaces
{
    public interface IMusicaService
    {
        Task<MusicaResponseDto> CriarMusicaAsync(CriarMusicaRequestDto dto);
    }
}
