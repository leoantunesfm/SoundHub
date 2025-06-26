using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillGaps.SoundHub.Domain.Catalog
{
    public class Duracao
    {
        public int Segundos { get; private set; }

        public Duracao(int segundos)
        {
            if (segundos < 0)
                throw new ArgumentException("Duração não pode ser negativa.");
            Segundos = segundos;
        }

        public override string ToString() => $"{Segundos / 60:00}:{Segundos % 60:00}";
    }
}
