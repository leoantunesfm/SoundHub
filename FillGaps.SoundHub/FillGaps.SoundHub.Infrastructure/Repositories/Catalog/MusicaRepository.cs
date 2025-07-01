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
    public class MusicaRepository : IMusicaRepository
    {
        private readonly SoundHubDbContext _context;

        public MusicaRepository(SoundHubDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Musica musica)
        {
            await _context.Musicas.AddAsync(musica);
        }

        public void Atualizar(Musica musica)
        {
            _context.Musicas.Update(musica);
        }

        public async Task<Musica?> ObterPorIdAsync(Guid id)
        {
            return await _context.Musicas.FindAsync(id);
        }

        public async Task<Musica?> ObterPorTituloAsync(string titulo)
        {
            return await _context.Musicas
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Titulo == titulo);
        }

        public async Task<IEnumerable<Musica>> ObterTodosPorAlbumIdAsync(Guid albumId)
        {
            return await _context.Musicas
                .AsNoTracking()
                .Where(m => m.AlbumId == albumId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Musica>> PesquisarAsync(string? termo, Guid? generoId)
        {
            var query = _context.Musicas.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(termo))
            {
                query = query.Where(m => m.Titulo.Contains(termo));
            }

            if (generoId.HasValue)
            {
                query = query.Where(m => m.Generos.Any(g => g.Id == generoId.Value));
            }

            return await query.ToListAsync();
        }

        public async Task<Musica?> ObterPorTituloComGenerosAsync(string titulo)
        {
            return await _context.Musicas
                .Include(m => m.Generos)
                .FirstOrDefaultAsync(m => m.Titulo == titulo);
        }
        public async Task<IEnumerable<Musica>> ObterTodosAsync()
        {
            return await _context.Musicas.AsNoTracking().ToListAsync();
        }
    }
}
