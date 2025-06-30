using FillGaps.SoundHub.Domain.UserEngagement;
using FillGaps.SoundHub.Domain.UserEngagement.Repositories;
using FillGaps.SoundHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillGaps.SoundHub.Infrastructure.Repositories.UserEngagement
{
    public class PlaylistRepository : IPlaylistRepository
    {
        private readonly SoundHubDbContext _context;

        public PlaylistRepository(SoundHubDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Playlist playlist)
        {
            await _context.Playlists.AddAsync(playlist);
        }

        public void Atualizar(Playlist playlist)
        {
            _context.Playlists.Update(playlist);
        }

        public void Remover(Playlist playlist)
        {
            _context.Playlists.Remove(playlist);
        }

        public async Task<Playlist?> ObterPorIdComMusicasAsync(Guid id)
        {
            return await _context.Playlists
                .Include(p => p.PlaylistMusicas)
                .ThenInclude(pm => pm.Musica)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Playlist?> ObterPorNomeAsync(string nome, Guid usuarioId)
        {
            return await _context.Playlists
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Nome == nome && p.UsuarioId == usuarioId);
        }

        public async Task<IEnumerable<Playlist>> ObterTodasPorUsuarioIdAsync(Guid usuarioId)
        {
            return await _context.Playlists
                .AsNoTracking()
                .Where(p => p.UsuarioId == usuarioId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Playlist>> PesquisarPorTermoAsync(string termo, Guid usuarioId)
        {
            return await _context.Playlists
                .AsNoTracking()
                .Where(p => p.UsuarioId == usuarioId && p.Nome.Contains(termo))
                .ToListAsync();
        }
    }
}
