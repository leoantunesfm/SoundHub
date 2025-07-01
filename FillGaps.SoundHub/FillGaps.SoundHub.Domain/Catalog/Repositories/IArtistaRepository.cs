using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillGaps.SoundHub.Domain.Catalog.Repositories
{
    public interface IArtistaRepository
    {
        Task AdicionarAsync(Artista artista);
        void Atualizar(Artista artista);
        Task<Artista?> ObterPorIdAsync(Guid id);
        Task<Artista?> ObterPorIdComAlbunsAsync(Guid id);
        Task<Artista?> ObterPorNomeAsync(string nome);
        Task<IEnumerable<Artista>> ObterTodosAsync();
        Task<IEnumerable<Artista>> PesquisarPorTermoAsync(string termo);
        Task<Artista?> ObterPorNomeComGenerosAsync(string nome);
    }
}
