using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common
{
    public interface IApplicationDbContext
    {
        DbSet<Documentation> Documentations { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
