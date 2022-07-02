using Microsoft.EntityFrameworkCore;

namespace InvoiceGenerator.Domain
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly InvoiceDbContext dbContext;
        private DbSet<T> entities;

        public Repository(InvoiceDbContext invoiceDbContext)
        {
            dbContext = invoiceDbContext;
            entities = dbContext.Set<T>();
        }

        public int Add(T item)
        {
            entities.Add(item);
            return dbContext.SaveChanges();
        }

        public async Task<int> Delete(T item)
        {
            entities.Remove(item);
            return await dbContext.SaveChangesAsync();
        }

        public List<T> GetAll()
        {
            return entities.ToList();
        }
    }
}
