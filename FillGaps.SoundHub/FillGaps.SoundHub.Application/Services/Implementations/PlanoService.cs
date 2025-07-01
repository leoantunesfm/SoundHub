using FillGaps.SoundHub.Application.DTOs.Billing;
using FillGaps.SoundHub.Application.Services.Interfaces;
using FillGaps.SoundHub.Domain.Billing;
using FillGaps.SoundHub.Domain.Billing.Repositories;
using FillGaps.SoundHub.Domain.SharedKernel.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillGaps.SoundHub.Application.Services.Implementations
{
    public class PlanoService : IPlanoService
    {
        private readonly IPlanoRepository _planoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PlanoService(IPlanoRepository planoRepository, IUnitOfWork unitOfWork)
        {
            _planoRepository = planoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PlanoResponseDto> CriarPlanoAsync(CriarPlanoRequestDto dto)
        {
            var planoExistente = await _planoRepository.ObterPorNomeAsync(dto.Nome);
            if (planoExistente != null)
            {
                throw new Exception("Já existe um plano com este nome.");
            }

            var novoPlano = new Plano(dto.Nome, dto.Descricao, dto.Preco);

            await _planoRepository.AdicionarAsync(novoPlano);
            await _unitOfWork.SalvarAlteracoesAsync();

            return new PlanoResponseDto
            {
                Id = novoPlano.Id,
                Nome = novoPlano.Nome,
                Descricao = novoPlano.Descricao,
                Preco = novoPlano.Preco,
                Ativo = novoPlano.Ativo
            };
        }

        public async Task<IEnumerable<PlanoResponseDto>> ObterPlanosAtivosAsync()
        {
            var planos = await _planoRepository.ObterTodosAtivosAsync();
            return planos.Select(p => new PlanoResponseDto
            {
                Id = p.Id,
                Nome = p.Nome,
                Descricao = p.Descricao,
                Preco = p.Preco,
                Ativo = p.Ativo
            });
        }
    }
}
