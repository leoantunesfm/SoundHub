using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillGaps.SoundHub.Domain.Catalog
{
    public class Genero
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string? Descricao { get; private set; } // Opcional

        public virtual ICollection<Musica> Musicas { get; private set; } = new List<Musica>();
        public virtual ICollection<Artista> Artistas { get; private set; } = new List<Artista>();

        private Genero() { }

        public Genero(string nome, string? descricao = null)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Descricao = descricao;
        }

        public void Update(string nome, string? descricao)
        {
            Nome = nome;
            Descricao = descricao;
        }
    }
}
