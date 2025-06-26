using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FillGaps.SoundHub.Domain.Catalog;
using FillGaps.SoundHub.Domain.Identity;

namespace FillGaps.SoundHub.Domain.UserEngagement
{
    public class Playlist
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public Guid UsuarioId { get; private set; }
        public virtual Usuario Usuario { get; private set; }

        private readonly List<PlaylistMusica> _playlistMusicas = new();
        public IReadOnlyCollection<PlaylistMusica> PlaylistMusicas => _playlistMusicas.AsReadOnly();

        private Playlist() { }

        public Playlist(Usuario usuario, string nome, string descricao)
        {
            Id = Guid.NewGuid();
            Usuario = usuario;
            UsuarioId = usuario.Id;
            Nome = nome;
            Descricao = descricao;
        }

        public void AdicionarMusica(Musica musica)
        {
            if (_playlistMusicas.Any(m => m.MusicaId == musica.Id))
            {
                return;
            }

            var ordem = _playlistMusicas.Count > 0 ? _playlistMusicas.Max(m => m.Ordem) + 1 : 1;

            _playlistMusicas.Add(new PlaylistMusica(this.Id, musica.Id, ordem));
        }

        public void RemoverMusica(Guid musicaId)
        {
            var itemParaRemover = _playlistMusicas.FirstOrDefault(pm => pm.MusicaId == musicaId);
            if (itemParaRemover == null) return;

            int ordemRemovida = itemParaRemover.Ordem;
            _playlistMusicas.Remove(itemParaRemover);

            var itensParaReordenar = _playlistMusicas
                .Where(pm => pm.Ordem > ordemRemovida)
                .OrderBy(pm => pm.Ordem);

            foreach (var item in itensParaReordenar)
            {
                item.AtualizarOrdem(item.Ordem - 1);
            }
        }
    }
}
