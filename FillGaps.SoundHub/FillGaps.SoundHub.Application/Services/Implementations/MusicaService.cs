using FillGaps.SoundHub.Application.DTOs.Catalog;
using FillGaps.SoundHub.Application.Services.Interfaces;
using FillGaps.SoundHub.Domain.Catalog;
using FillGaps.SoundHub.Domain.Catalog.Repositories;
using FillGaps.SoundHub.Domain.SharedKernel.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillGaps.SoundHub.Application.Services.Implementations
{
    public class MusicaService : IMusicaService
    {
        private readonly IMusicaRepository _musicaRepository;
        private readonly IAlbumRepository _albumRepository;
        private readonly IGeneroRepository _generoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MusicaService(IMusicaRepository musicaRepository, IAlbumRepository albumRepository, IGeneroRepository generoRepository, IUnitOfWork unitOfWork)
        {
            _musicaRepository = musicaRepository;
            _albumRepository = albumRepository;
            _generoRepository = generoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<MusicaResponseDto> CriarMusicaAsync(CriarMusicaRequestDto dto)
        {
            var album = await _albumRepository.ObterPorIdAsync(dto.AlbumId);
            if (album == null)
            {
                throw new Exception("Álbum não encontrado.");
            }

            var novaMusica = new Musica(dto.Titulo, new Duracao(dto.DuracaoSegundos), dto.AlbumId);

            await _musicaRepository.AdicionarAsync(novaMusica);

            foreach (var generoId in dto.GenerosId)
            {
                var genero = await _generoRepository.ObterPorIdAsync(generoId);
                if (genero != null)
                {
                    novaMusica.AdicionarGenero(genero);
                }
            }

            await _unitOfWork.SalvarAlteracoesAsync();

            return new MusicaResponseDto
            {
                Id = novaMusica.Id,
                Titulo = novaMusica.Titulo,
                DuracaoSegundos = novaMusica.Duracao.Segundos,
                AlbumId = album.Id
            };
        }
    }
}

