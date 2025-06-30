using FillGaps.SoundHub.Domain.Billing;
using FillGaps.SoundHub.Domain.Billing.Repositories;
using FillGaps.SoundHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillGaps.SoundHub.Infrastructure.Repositories.Billing
{
    public class AssinaturaRepository : IAssinaturaRepository
    {
        private readonly SoundHubDbContext _context;

        public AssinaturaRepository(SoundHubDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Assinatura assinatura)
        {
            await _context.Assinaturas.AddAsync(assinatura);
        }

        public void Atualizar(Assinatura assinatura)
        {
            _context.Assinaturas.Update(assinatura);
        }

        public async Task<Assinatura?> ObterPorUsuarioIdAsync(Guid usuarioId)
        {
            return await _context.Assinaturas
                .Include(a => a.Plano)
                .Include(a => a.Transacoes)
                .FirstOrDefaultAsync(a => a.UsuarioId == usuarioId);
        }
    }
}
