using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillGaps.SoundHub.Domain.Catalog.Repositories
{
    public interface IAlbumRepository
    {
        Task AdicionarAsync(Album album);
        void Atualizar(Album album);
        Task<Album?> ObterPorIdAsync(Guid id);
        Task<Album?> ObterPorTituloAsync(string titulo);
        Task<IEnumerable<Album>> ObterTodosPorArtistaIdAsync(Guid artistaId);
        Task<IEnumerable<Album>> PesquisarPorTermoAsync(string termo);
    }
}
