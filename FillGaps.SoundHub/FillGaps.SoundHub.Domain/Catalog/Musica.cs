using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillGaps.SoundHub.Domain.Catalog
{
    public class Musica
    {
        public Guid Id { get; private set; }
        public string Titulo { get; private set; }
        public Duracao Duracao { get; private set; }

        public Guid AlbumId { get; private set; }

        public virtual ICollection<Genero> Generos { get; private set; } = new List<Genero>();

        private Musica() { }

        public Musica(string titulo, Duracao duracao, Guid albumId)
        {
            Id = Guid.NewGuid();
            Titulo = titulo;
            Duracao = duracao;
            AlbumId = albumId;
        }

        public void AdicionarGenero(Genero genero)
        {
            if (!Generos.Any(g => g.Id == genero.Id))
            {
                Generos.Add(genero);
            }
        }

        public void RemoverGenero(Genero genero)
        {
            var generoExistente = Generos.FirstOrDefault(g => g.Id == genero.Id);
            if (generoExistente != null)
            {
                Generos.Remove(generoExistente);
            }
        }
    }
}
