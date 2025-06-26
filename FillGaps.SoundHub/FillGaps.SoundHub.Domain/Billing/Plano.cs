using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillGaps.SoundHub.Domain.Billing
{
    public class Plano
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public decimal Preco { get; private set; }
        public bool Ativo { get; private set; }

        private Plano() { }

        public Plano(string nome, string descricao, decimal preco)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
            Ativo = true;
        }

        public void AlterarPreco(decimal novoPreco)
        {
            if (novoPreco < 0)
            {
                throw new ArgumentException("O preço do plano não pode ser negativo.");
            }
            Preco = novoPreco;
        }

        public void Desativar()
        {
            Ativo = false;
        }

        public void Ativar()
        {
            Ativo = true;
        }
    }
}
