using InvoiceGenerator.Domain.Models;

namespace InvoiceGenerator.Domain
{
    public interface IRepository<T> where T : class
    {
        int Add(T item);
        List<T> GetAll();
    }
}
