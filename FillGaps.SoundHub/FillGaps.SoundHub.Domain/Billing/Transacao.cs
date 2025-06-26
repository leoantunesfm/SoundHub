using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FillGaps.SoundHub.Domain.Billing.Enums;
using FillGaps.SoundHub.Domain.SharedKernel.Events;

namespace FillGaps.SoundHub.Domain.Billing
{
    public class Transacao
    {
        public Guid Id { get; private set; }
        public Guid AssinaturaId { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime DataProcessamento { get; private set; }
        public StatusTransacao Status { get; private set; }
        public string IdTransacaoGateway { get; private set; }

        private readonly List<object> _domainEvents = new();
        public IReadOnlyCollection<object> DomainEvents => _domainEvents.AsReadOnly();

        private Transacao() { }

        public Transacao(Assinatura assinatura, decimal valor, StatusTransacao status, string idTransacaoGateway)
        {
            Id = Guid.NewGuid();
            AssinaturaId = assinatura.Id;
            Valor = valor;
            Status = status;
            IdTransacaoGateway = idTransacaoGateway;
            DataProcessamento = DateTime.UtcNow;

            if (status == StatusTransacao.Aprovada)
            {
                _domainEvents.Add(new TransacaoAutorizadaEvent(this.Id, assinatura.UsuarioId));
            }
        }
    }
}
