using FillGaps.SoundHub.Application.DTOs.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillGaps.SoundHub.Application.Services.Interfaces
{
    public interface IArtistaService
    {
        Task<ArtistaResponseDto> CriarArtistaAsync(CriarArtistaRequestDto dto);
        Task<ArtistaResponseDto?> ObterArtistaPorIdAsync(Guid id);
        Task<IEnumerable<ArtistaResponseDto>> ObterTodosArtistasAsync();
        Task<IEnumerable<ArtistaResponseDto>> PesquisarArtistasPorTermoAsync(string termo);
    }
}
