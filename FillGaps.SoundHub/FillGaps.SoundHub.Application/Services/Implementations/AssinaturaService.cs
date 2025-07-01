using FillGaps.SoundHub.Application.DTOs.Billing;
using FillGaps.SoundHub.Application.Services.Interfaces;
using FillGaps.SoundHub.Domain.Billing;
using FillGaps.SoundHub.Domain.Billing.Enums;
using FillGaps.SoundHub.Domain.Billing.Repositories;
using FillGaps.SoundHub.Domain.Identity;
using FillGaps.SoundHub.Domain.SharedKernel.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillGaps.SoundHub.Application.Services.Implementations
{
    public class AssinaturaService : IAssinaturaService
    {
        private readonly IAssinaturaRepository _assinaturaRepository;
        private readonly IPlanoRepository _planoRepository;
        private readonly UserManager<Usuario> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public AssinaturaService(
            IAssinaturaRepository assinaturaRepository,
            IPlanoRepository planoRepository,
            UserManager<Usuario> userManager,
            IUnitOfWork unitOfWork)
        {
            _assinaturaRepository = assinaturaRepository;
            _planoRepository = planoRepository;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<AssinaturaResponseDto> CriarAssinaturaAsync(CriarAssinaturaRequestDto dto, Guid usuarioId)
        {
            var usuario = await _userManager.FindByIdAsync(usuarioId.ToString());
            if (usuario == null) throw new Exception("Usuário não encontrado.");

            var assinaturaAtiva = await _assinaturaRepository.ObterPorUsuarioIdAsync(usuarioId);
            if (assinaturaAtiva != null && assinaturaAtiva.Status == StatusAssinatura.Ativa)
            {
                throw new Exception("Usuário já possui uma assinatura ativa.");
            }

            var plano = await _planoRepository.ObterPorIdAsync(dto.PlanoId);
            if (plano == null || !plano.Ativo)
            {
                throw new Exception("Plano não encontrado ou inativo.");
            }

            bool pagamentoAprovado = true;

            if (!pagamentoAprovado)
            {
                throw new Exception("Pagamento recusado.");
            }

            var novaAssinatura = new Assinatura(usuario, plano);
            var novaTransacao = new Transacao(novaAssinatura, plano.Preco, StatusTransacao.Aprovada, Guid.NewGuid().ToString());

            novaAssinatura.AdicionarTransacao(novaTransacao);

            await _assinaturaRepository.AdicionarAsync(novaAssinatura);
            await _unitOfWork.SalvarAlteracoesAsync();

            return new AssinaturaResponseDto
            {
                Id = novaAssinatura.Id,
                PlanoNome = plano.Nome,
                Status = novaAssinatura.Status.ToString(),
                DataInicio = novaAssinatura.DataInicio,
                DataVigencia = novaAssinatura.DataVigencia
            };
        }

        public async Task<AssinaturaResponseDto?> ObterAssinaturaPorUsuarioIdAsync(Guid usuarioId)
        {
            var assinatura = await _assinaturaRepository.ObterPorUsuarioIdAsync(usuarioId);
            if (assinatura == null) return null;

            return new AssinaturaResponseDto
            {
                Id = assinatura.Id,
                PlanoNome = assinatura.Plano.Nome,
                Status = assinatura.Status.ToString(),
                DataInicio = assinatura.DataInicio,
                DataVigencia = assinatura.DataVigencia
            };
        }
    }
}
