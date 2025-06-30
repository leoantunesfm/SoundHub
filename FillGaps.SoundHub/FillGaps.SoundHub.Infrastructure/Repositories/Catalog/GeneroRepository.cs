using FillGaps.SoundHub.Domain.Catalog;
using FillGaps.SoundHub.Domain.Catalog.Repositories;
using FillGaps.SoundHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillGaps.SoundHub.Infrastructure.Repositories.Catalog
{
    public class GeneroRepository : IGeneroRepository
    {
        private readonly SoundHubDbContext _context;

        public GeneroRepository(SoundHubDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Genero genero)
        {
            await _context.Generos.AddAsync(genero);
        }

        public void Atualizar(Genero genero)
        {
            _context.Generos.Update(genero);
        }

        public async Task<Genero?> ObterPorIdAsync(Guid id)
        {
            return await _context.Generos.FindAsync(id);
        }

        public async Task<Genero?> ObterPorNomeAsync(string nome)
        {
            return await _context.Generos
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Nome == nome);
        }

        public async Task<IEnumerable<Genero>> ObterTodosAsync()
        {
            return await _context.Generos.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Genero>> PesquisarPorTermoAsync(string termo)
        {
            return await _context.Generos
                .AsNoTracking()
                .Where(g => g.Nome.Contains(termo))
                .ToListAsync();
        }
    }
}
