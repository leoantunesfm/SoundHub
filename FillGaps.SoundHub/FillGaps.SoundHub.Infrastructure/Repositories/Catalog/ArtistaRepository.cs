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
    public class ArtistaRepository : IArtistaRepository
    {
        private readonly SoundHubDbContext _context;

        public ArtistaRepository(SoundHubDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Artista artista)
        {
            await _context.Artistas.AddAsync(artista);
        }

        public void Atualizar(Artista artista)
        {
            var artistaExistente = _context.Artistas.Local.FirstOrDefault(e => e.Id == artista.Id);
            if (artistaExistente != null)
            {
                _context.Entry(artistaExistente).State = EntityState.Detached;
            }
            _context.Artistas.Update(artista);
        }

        public async Task<Artista?> ObterPorIdAsync(Guid id)
        {
            return await _context.Artistas.FindAsync(id);
        }

        public async Task<Artista?> ObterPorIdComAlbunsAsync(Guid id)
        {
            return await _context.Artistas
                .Include(a => a.Albuns)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Artista?> ObterPorNomeAsync(string nome)
        {
            return await _context.Artistas
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Nome == nome);
        }

        public async Task<IEnumerable<Artista>> ObterTodosAsync()
        {
            return await _context.Artistas.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Artista>> PesquisarPorTermoAsync(string termo)
        {
            return await _context.Artistas
                .AsNoTracking()
                .Where(a => a.Nome.Contains(termo) || a.Descricao.Contains(termo))
                .ToListAsync();
        }
    }
}
