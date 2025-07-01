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
    public class GeneroService : IGeneroService
    {
        private readonly IGeneroRepository _generoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GeneroService(IGeneroRepository generoRepository, IUnitOfWork unitOfWork)
        {
            _generoRepository = generoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<GeneroResponseDto> CriarGeneroAsync(CriarGeneroRequestDto dto)
        {
            var generoExistente = await _generoRepository.ObterPorNomeAsync(dto.Nome);
            if (generoExistente != null)
            {
                throw new Exception("Já existe um gênero com este nome.");
            }

            var novoGenero = new Genero(dto.Nome, dto.Descricao);

            await _generoRepository.AdicionarAsync(novoGenero);
            await _unitOfWork.SalvarAlteracoesAsync();

            return new GeneroResponseDto
            {
                Id = novoGenero.Id,
                Nome = novoGenero.Nome,
                Descricao = novoGenero.Descricao
            };
        }

        public async Task<IEnumerable<GeneroResponseDto>> ObterTodosGenerosAsync()
        {
            var generos = await _generoRepository.ObterTodosAsync();
            return generos.Select(g => new GeneroResponseDto
            {
                Id = g.Id,
                Nome = g.Nome,
                Descricao = g.Descricao
            });
        }
    }
}
