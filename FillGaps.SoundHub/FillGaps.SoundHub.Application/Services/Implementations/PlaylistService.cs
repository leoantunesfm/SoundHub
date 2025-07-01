using FillGaps.SoundHub.Application.DTOs.UserEngagement;
using FillGaps.SoundHub.Application.Services.Interfaces;
using FillGaps.SoundHub.Domain.Catalog.Repositories;
using FillGaps.SoundHub.Domain.Identity;
using FillGaps.SoundHub.Domain.SharedKernel.Repositories;
using FillGaps.SoundHub.Domain.UserEngagement;
using FillGaps.SoundHub.Domain.UserEngagement.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillGaps.SoundHub.Application.Services.Implementations
{
    public class PlaylistService : IPlaylistService
    {
        private readonly IPlaylistRepository _playlistRepository;
        private readonly IMusicaRepository _musicaRepository;
        private readonly UserManager<Usuario> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public PlaylistService(IPlaylistRepository playlistRepository, IMusicaRepository musicaRepository, UserManager<Usuario> userManager, IUnitOfWork unitOfWork)
        {
            _playlistRepository = playlistRepository;
            _musicaRepository = musicaRepository;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<PlaylistResponseDto> CriarPlaylistAsync(CriarPlaylistRequestDto dto, Guid usuarioId)
        {
            var usuario = await _userManager.FindByIdAsync(usuarioId.ToString());
            if (usuario == null) throw new Exception("Usuário não encontrado.");

            var playlist = new Playlist(usuario, dto.Nome, dto.Descricao ?? string.Empty);

            await _playlistRepository.AdicionarAsync(playlist);
            await _unitOfWork.SalvarAlteracoesAsync();

            return new PlaylistResponseDto { Id = playlist.Id, Nome = playlist.Nome, Descricao = playlist.Descricao };
        }

        public async Task AdicionarMusicaAPlaylistAsync(AdicionarMusicaPlaylistRequestDto dto, Guid usuarioId)
        {
            var playlist = await _playlistRepository.ObterPorIdComMusicasAsync(dto.PlaylistId);
            if (playlist == null || playlist.UsuarioId != usuarioId)
            {
                throw new Exception("Playlist não encontrada ou não pertence ao usuário.");
            }

            var musica = await _musicaRepository.ObterPorIdAsync(dto.MusicaId);
            if (musica == null)
            {
                throw new Exception("Música não encontrada.");
            }

            playlist.AdicionarMusica(musica);
            _playlistRepository.Atualizar(playlist);
            await _unitOfWork.SalvarAlteracoesAsync();
        }
    }
}
