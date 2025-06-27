using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillGaps.SoundHub.Domain.Catalog.Repositories
{
    public interface IGeneroRepository
    {
        Task AdicionarAsync(Genero genero);
        void Atualizar(Genero genero);
        Task<Genero?> ObterPorIdAsync(Guid id);
        Task<Genero?> ObterPorNomeAsync(string nome);
        Task<IEnumerable<Genero>> ObterTodosAsync();
        Task<IEnumerable<Genero>> PesquisarPorTermoAsync(string termo);
    }
}
