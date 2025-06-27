using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillGaps.SoundHub.Domain.Billing.Repositories
{
    public interface IAssinaturaRepository
    {
        Task AdicionarAsync(Assinatura assinatura);
        void Atualizar(Assinatura assinatura);
        Task<Assinatura?> ObterPorUsuarioIdAsync(Guid usuarioId);
    }
}
