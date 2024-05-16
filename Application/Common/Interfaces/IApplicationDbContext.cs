using Domain.DocumentationAggregate;
using Domain.DocumentationTemplate;
using Domain.DocumentationTemplate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Documentation> Documentations { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
