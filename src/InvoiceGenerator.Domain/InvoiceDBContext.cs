using System.Diagnostics.CodeAnalysis;
using InvoiceGenerator.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceGenerator.Domain
{
    [ExcludeFromCodeCoverage]
    public class InvoiceDbContext : DbContext
    {
        public InvoiceDbContext(DbContextOptions<InvoiceDbContext> options) : base(options) { }
        public InvoiceDbContext() { }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public DbSet<Client> Clients => Set<Client>();

    }
}
