using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillGaps.SoundHub.Domain.Catalog
{
    public class Artista
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public string ImagemUrl { get; private set; }
        private readonly List<Album> _albuns = new();
        public IReadOnlyCollection<Album> Albuns => _albuns.AsReadOnly();
        public virtual ICollection<Genero> Generos { get; private set; } = new List<Genero>();

        private Artista() { }

        public Artista(string nome, string descricao)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Descricao = descricao;
        }

        public void AdicionarAlbum(Album album)
        {
            if (_albuns.Any(a => a.Titulo == album.Titulo))
            {
                throw new InvalidOperationException("Este álbum já existe para este artista.");
            }
            _albuns.Add(album);
        }
    }
}
