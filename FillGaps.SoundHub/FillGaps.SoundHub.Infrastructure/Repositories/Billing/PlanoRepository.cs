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
    public class PlanoRepository : IPlanoRepository
    {
        private readonly SoundHubDbContext _context;

        public PlanoRepository(SoundHubDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Plano plano)
        {
            await _context.Planos.AddAsync(plano);
        }

        public void Atualizar(Plano plano)
        {
            _context.Planos.Update(plano);
        }

        public async Task<Plano?> ObterPorIdAsync(Guid id)
        {
            return await _context.Planos.FindAsync(id);
        }

        public async Task<Plano?> ObterPorNomeAsync(string nome)
        {
            return await _context.Planos
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Nome == nome);
        }

        public async Task<IEnumerable<Plano>> ObterTodosAtivosAsync()
        {
            return await _context.Planos
                .AsNoTracking()
                .Where(p => p.Ativo)
                .ToListAsync();
        }
    }
}
