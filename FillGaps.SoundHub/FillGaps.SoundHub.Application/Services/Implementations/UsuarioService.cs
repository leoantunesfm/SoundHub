using FillGaps.SoundHub.Application.DTOs.Catalog;
using FillGaps.SoundHub.Application.DTOs.UserEngagement;
using FillGaps.SoundHub.Application.Services.Interfaces;
using FillGaps.SoundHub.Domain.Catalog.Repositories;
using FillGaps.SoundHub.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillGaps.SoundHub.Application.Services.Implementations
{
    public class UsuarioService : IUsuarioService
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly IMusicaRepository _musicaRepository;
        private readonly IArtistaRepository _artistaRepository;

        public UsuarioService(UserManager<Usuario> userManager, IMusicaRepository musicaRepository, IArtistaRepository artistaRepository)
        {
            _userManager = userManager;
            _musicaRepository = musicaRepository;
            _artistaRepository = artistaRepository;
        }

        public async Task FavoritarMusicaAsync(Guid usuarioId, Guid musicaId)
        {
            var usuario = await _userManager.Users.Include(u => u.MusicasFavoritas)
                                                   .FirstOrDefaultAsync(u => u.Id == usuarioId);
            if (usuario == null) throw new Exception("Usuário não encontrado.");

            var musica = await _musicaRepository.ObterPorIdAsync(musicaId);
            if (musica == null) throw new Exception("Música não encontrada.");

            usuario.FavoritarMusica(musica);

            await _userManager.UpdateAsync(usuario);
        }

        public async Task DesfavoritarMusicaAsync(Guid usuarioId, Guid musicaId)
        {
            var usuario = await _userManager.Users.Include(u => u.MusicasFavoritas)
                                                   .FirstOrDefaultAsync(u => u.Id == usuarioId);
            if (usuario == null) throw new Exception("Usuário não encontrado.");

            var musica = usuario.MusicasFavoritas.FirstOrDefault(m => m.Id == musicaId);
            if (musica != null)
            {
                usuario.DesfavoritarMusica(musica);
                await _userManager.UpdateAsync(usuario);
            }
        }

        public async Task FavoritarArtistaAsync(Guid usuarioId, Guid artistaId)
        {
            var usuario = await _userManager.Users.Include(u => u.ArtistasFavoritos)
                                                   .FirstOrDefaultAsync(u => u.Id == usuarioId);
            if (usuario == null) throw new Exception("Usuário não encontrado.");

            var artista = await _artistaRepository.ObterPorIdAsync(artistaId);
            if (artista == null) throw new Exception("Artista não encontrado.");

            usuario.FavoritarArtista(artista);
            await _userManager.UpdateAsync(usuario);
        }

        public async Task DesfavoritarArtistaAsync(Guid usuarioId, Guid artistaId)
        {
            var usuario = await _userManager.Users.Include(u => u.ArtistasFavoritos)
                                                   .FirstOrDefaultAsync(u => u.Id == usuarioId);
            if (usuario == null) throw new Exception("Usuário não encontrado.");

            var artista = usuario.ArtistasFavoritos.FirstOrDefault(a => a.Id == artistaId);
            if (artista != null)
            {
                usuario.DesfavoritarArtista(artista);
                await _userManager.UpdateAsync(usuario);
            }
        }

        public async Task<FavoritosResponseDto> ObterFavoritosAsync(Guid usuarioId)
        {
            var usuario = await _userManager.Users
                .Include(u => u.MusicasFavoritas)
                .Include(u => u.ArtistasFavoritos)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == usuarioId);

            if (usuario == null) throw new Exception("Usuário não encontrado.");

            return new FavoritosResponseDto
            {
                MusicasFavoritas = usuario.MusicasFavoritas.Select(m => new MusicaResponseDto
                {
                    Id = m.Id,
                    Titulo = m.Titulo,
                    DuracaoSegundos = m.Duracao.Segundos,
                    AlbumId = m.AlbumId
                }).ToList(),
                ArtistasFavoritos = usuario.ArtistasFavoritos.Select(a => new ArtistaResponseDto
                {
                    Id = a.Id,
                    Nome = a.Nome,
                    Descricao = a.Descricao,
                    ImagemUrl = a.ImagemUrl
                }).ToList()
            };
        }
    }
}
