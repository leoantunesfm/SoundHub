using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillGaps.SoundHub.Domain.Catalog
{
    public class Album
    {
        public Guid Id { get; private set; }
        public string Titulo { get; private set; }
        public int AnoLancamento { get; private set; }
        public string CapaUrl { get; private set; }
        public Guid ArtistaId { get; private set; }

        private readonly List<Musica> _musicas = new();
        public IReadOnlyCollection<Musica> Musicas => _musicas.AsReadOnly();

        private Album() { }

        public Album(string titulo, int anoLancamento)
        {
            Id = Guid.NewGuid();
            Titulo = titulo;
            AnoLancamento = anoLancamento;
        }

        public void AdicionarMusica(Musica musica) => _musicas.Add(musica);
    }
}
