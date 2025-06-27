using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillGaps.SoundHub.Domain.Billing.Repositories
{
    public interface IPlanoRepository
    {
        Task AdicionarAsync(Plano plano);
        void Atualizar(Plano plano);
        Task<Plano?> ObterPorIdAsync(Guid id);
        Task<Plano?> ObterPorNomeAsync(string nome);
        Task<IEnumerable<Plano>> ObterTodosAtivosAsync();
    }
}
