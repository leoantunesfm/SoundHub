using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillGaps.SoundHub.Domain.Catalog.Repositories
{
    public interface IMusicaRepository
    {
        Task AdicionarAsync(Musica musica);
        void Atualizar(Musica musica);
        Task<Musica?> ObterPorIdAsync(Guid id);
        Task<Musica?> ObterPorTituloAsync(string titulo);
        Task<IEnumerable<Musica>> ObterTodosPorAlbumIdAsync(Guid albumId);
        Task<IEnumerable<Musica>> PesquisarAsync(string? termo, Guid? generoId);
        Task<Musica?> ObterPorTituloComGenerosAsync(string titulo);
        Task<IEnumerable<Musica>> ObterTodosAsync();
        Task<IEnumerable<Musica>> ObterTodosComDetalhesAsync();
    }
}
