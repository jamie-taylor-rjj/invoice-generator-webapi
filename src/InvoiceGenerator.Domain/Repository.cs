using Microsoft.EntityFrameworkCore;

namespace InvoiceGenerator.Domain
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly InvoiceDbContext _dbContext;
        private DbSet<T> _entities;

        public Repository(InvoiceDbContext invoiceDbContext)
        {
            _dbContext = invoiceDbContext;
            _entities = _dbContext.Set<T>();
        }

        public int Add(T item)
        {
            _entities.Add(item);
            return _dbContext.SaveChanges();
        }

        public async Task<int> Delete(T item)
        {
            _entities.Remove(item);
            return await _dbContext.SaveChangesAsync();
        }

        public List<T> GetAll()
        {
            return _entities.ToList();
        }
    }
}
