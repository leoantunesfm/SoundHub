using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FillGaps.SoundHub.Domain.Billing.Enums;
using FillGaps.SoundHub.Domain.Identity;

namespace FillGaps.SoundHub.Domain.Billing
{
    public class Assinatura
    {
        public Guid Id { get; private set; }
        public Guid UsuarioId { get; private set; }
        public Guid PlanoId { get; private set; }
        public StatusAssinatura Status { get; private set; }
        public DateTime DataInicio { get; private set; }
        public DateTime DataVigencia { get; private set; }
        public virtual Usuario Usuario { get; private set; }
        public virtual Plano Plano { get; private set; }

        private readonly List<Transacao> _transacoes = new();
        public IReadOnlyCollection<Transacao> Transacoes => _transacoes.AsReadOnly();

        private Assinatura() { }

        public Assinatura(Usuario usuario, Plano plano)
        {
            if (!plano.Ativo)
            {
                throw new InvalidOperationException("Não é possível assinar um plano inativo.");
            }

            Id = Guid.NewGuid();
            Usuario = usuario;
            UsuarioId = usuario.Id;
            Plano = plano;
            PlanoId = plano.Id;
            Status = StatusAssinatura.Ativa;
            DataInicio = DateTime.Now;
            DataVigencia = DateTime.Now.AddMonths(1);
        }

        public void Cancelar()
        {
            if (Status == StatusAssinatura.Cancelada)
            {
                throw new InvalidOperationException("Assinatura já está cancelada.");
            }
            Status = StatusAssinatura.Cancelada;
        }

        public void TornarInadimplente()
        {
            Status = StatusAssinatura.Inadimplente;
        }

        public void Renovar()
        {
            if (Status == StatusAssinatura.Cancelada || Status == StatusAssinatura.Expirada)
            {
                throw new InvalidOperationException("Não é possível renovar uma assinatura cancelada ou expirada.");
            }

            Status = StatusAssinatura.Ativa;

            DataVigencia = DataVigencia.AddMonths(1);
        }

        public void AdicionarTransacao(Transacao transacao)
        {
            _transacoes.Add(transacao);
        }
    }
}
