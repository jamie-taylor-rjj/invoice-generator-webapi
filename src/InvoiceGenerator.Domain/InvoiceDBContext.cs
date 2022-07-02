using InvoiceGenerator.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceGenerator.Domain
{

    public class InvoiceDbContext : DbContext
    {
        public InvoiceDbContext(DbContextOptions<InvoiceDbContext> options) : base(options) { }
        public InvoiceDbContext() { }

        public override int SaveChanges(bool acceptAllChangesOnSuccess = true)
        {
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public DbSet<Client> Clients => Set<Client>();

    }
}
