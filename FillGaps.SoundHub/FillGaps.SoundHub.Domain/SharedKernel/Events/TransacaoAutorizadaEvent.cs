using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillGaps.SoundHub.Domain.SharedKernel.Events
{
    public class TransacaoAutorizadaEvent
    {
        public Guid TransacaoId { get; }
        public Guid UsuarioId { get; }

        public TransacaoAutorizadaEvent(Guid transacaoId, Guid usuarioId)
        {
            TransacaoId = transacaoId;
            UsuarioId = usuarioId;
        }
    }
}
