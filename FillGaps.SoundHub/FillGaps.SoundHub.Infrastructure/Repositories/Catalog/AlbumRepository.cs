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
    public class AlbumRepository : IAlbumRepository
    {
        private readonly SoundHubDbContext _context;

        public AlbumRepository(SoundHubDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Album album)
        {
            await _context.Albuns.AddAsync(album);
        }

        public void Atualizar(Album album)
        {
            _context.Albuns.Update(album);
        }

        public async Task<Album?> ObterPorIdAsync(Guid id)
        {
            return await _context.Albuns.FindAsync(id);
        }

        public async Task<Album?> ObterPorTituloAsync(string titulo)
        {
            return await _context.Albuns
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Titulo == titulo);
        }

        public async Task<IEnumerable<Album>> ObterTodosPorArtistaIdAsync(Guid artistaId)
        {
            return await _context.Albuns
                .AsNoTracking()
                .Where(a => a.ArtistaId == artistaId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Album>> PesquisarPorTermoAsync(string termo)
        {
            return await _context.Albuns
                .AsNoTracking()
                .Where(a => a.Titulo.Contains(termo))
                .ToListAsync();
        }
    }
}
