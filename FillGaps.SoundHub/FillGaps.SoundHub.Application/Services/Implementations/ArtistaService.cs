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
    public class ArtistaService : IArtistaService
    {
        private readonly IArtistaRepository _artistaRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ArtistaService(IArtistaRepository artistaRepository, IUnitOfWork unitOfWork)
        {
            _artistaRepository = artistaRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ArtistaResponseDto> CriarArtistaAsync(CriarArtistaRequestDto dto)
        {
            var artistaExistente = await _artistaRepository.ObterPorNomeAsync(dto.Nome);
            if (artistaExistente != null)
            {
                throw new Exception("Já existe um artista cadastrado com este nome.");
            }

            var novoArtista = new Artista(dto.Nome, dto.Descricao);

            await _artistaRepository.AdicionarAsync(novoArtista);

            await _unitOfWork.SalvarAlteracoesAsync();

            return new ArtistaResponseDto
            {
                Id = novoArtista.Id,
                Nome = novoArtista.Nome,
                Descricao = novoArtista.Descricao,
                ImagemUrl = novoArtista.ImagemUrl
            };
        }

        public async Task<ArtistaResponseDto?> ObterArtistaPorIdAsync(Guid id)
        {
            var artista = await _artistaRepository.ObterPorIdAsync(id);
            if (artista == null) return null;

            return new ArtistaResponseDto
            {
                Id = artista.Id,
                Nome = artista.Nome,
                Descricao = artista.Descricao,
                ImagemUrl = artista.ImagemUrl
            };
        }

        public async Task<IEnumerable<ArtistaResponseDto>> ObterTodosArtistasAsync()
        {
            var artistas = await _artistaRepository.ObterTodosAsync();

            var artistasDto = new List<ArtistaResponseDto>();
            foreach (var artista in artistas)
            {
                artistasDto.Add(new ArtistaResponseDto
                {
                    Id = artista.Id,
                    Nome = artista.Nome,
                    Descricao = artista.Descricao,
                    ImagemUrl = artista.ImagemUrl
                });
            }
            return artistasDto;
        }

        public async Task<IEnumerable<ArtistaResponseDto>> PesquisarArtistasPorTermoAsync(string termo)
        {
            var artistas = await _artistaRepository.PesquisarPorTermoAsync(termo);

            var artistasDto = new List<ArtistaResponseDto>();
            foreach (var artista in artistas)
            {
                artistasDto.Add(new ArtistaResponseDto
                {
                    Id = artista.Id,
                    Nome = artista.Nome,
                    Descricao = artista.Descricao,
                    ImagemUrl = artista.ImagemUrl
                });
            }
            return artistasDto;
        }
    }
}
