using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillGaps.SoundHub.Domain.Billing.Enums
{
    public enum StatusAssinatura
    {
        Ativa = 1,
        Inadimplente = 2,
        Cancelada = 3,
        Expirada = 4
    }
    public enum StatusTransacao
    {
        Aprovada = 1,
        Recusada = 2,
        AguardandoPagamento = 3
    }
}
