using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillGaps.SoundHub.Domain.SharedKernel.Repositories
{
    public interface IUnitOfWork
    {
        Task<bool> SalvarAlteracoesAsync();
    }
}
