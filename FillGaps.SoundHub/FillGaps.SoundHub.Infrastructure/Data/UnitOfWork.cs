using FillGaps.SoundHub.Domain.SharedKernel.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillGaps.SoundHub.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SoundHubDbContext _context;

        public UnitOfWork(SoundHubDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SalvarAlteracoesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
