using FillGaps.SoundHub.Application.DTOs.Catalog;
using FillGaps.SoundHub.Application.Services.Interfaces;
using FillGaps.SoundHub.Domain.Catalog;
using FillGaps.SoundHub.Domain.Catalog.Repositories;
using FillGaps.SoundHub.Domain.SharedKernel.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillGaps.SoundHub.Application.Services.Implementations
{
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IArtistaRepository _artistaRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AlbumService(IAlbumRepository albumRepository, IArtistaRepository artistaRepository, IUnitOfWork unitOfWork)
        {
            _albumRepository = albumRepository;
            _artistaRepository = artistaRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<AlbumResponseDto> CriarAlbumAsync(CriarAlbumRequestDto dto)
        {
            var artista = await _artistaRepository.ObterPorIdAsync(dto.ArtistaId);
            if (artista == null)
            {
                throw new Exception("Artista não encontrado.");
            }

            var novoAlbum = new Album(dto.Titulo, dto.AnoLancamento);

            artista.AdicionarAlbum(novoAlbum);
            _artistaRepository.Atualizar(artista);

            await _unitOfWork.SalvarAlteracoesAsync();

            return new AlbumResponseDto
            {
                Id = novoAlbum.Id,
                Titulo = novoAlbum.Titulo,
                AnoLancamento = novoAlbum.AnoLancamento,
                CapaUrl = novoAlbum.CapaUrl,
                ArtistaId = artista.Id
            };
        }

        public async Task<IEnumerable<AlbumResponseDto>> ObterAlbunsPorArtistaAsync(Guid artistaId)
        {
            var albuns = await _albumRepository.ObterTodosPorArtistaIdAsync(artistaId);
            return albuns.Select(album => new AlbumResponseDto
            {
                Id = album.Id,
                Titulo = album.Titulo,
                AnoLancamento = album.AnoLancamento,
                CapaUrl = album.CapaUrl,
                ArtistaId = album.ArtistaId
            });
        }
    }
}
