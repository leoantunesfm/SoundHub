using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FillGaps.SoundHub.Domain.Catalog;

namespace FillGaps.SoundHub.Domain.UserEngagement
{
    public class PlaylistMusica
    {
        public Guid PlaylistId { get; private set; }
        public Guid MusicaId { get; private set; }
        public int Ordem { get; private set; }
        public DateTime DataAdicao { get; private set; }
        public virtual Playlist Playlist { get; private set; }
        public virtual Musica Musica { get; private set; }

        private PlaylistMusica() { }

        internal PlaylistMusica(Guid playlistId, Guid musicaId, int ordem)
        {
            PlaylistId = playlistId;
            MusicaId = musicaId;
            Ordem = ordem;
            DataAdicao = DateTime.Now;
        }

        internal void AtualizarOrdem(int novaOrdem)
        {
            Ordem = novaOrdem;
        }
    }
}
