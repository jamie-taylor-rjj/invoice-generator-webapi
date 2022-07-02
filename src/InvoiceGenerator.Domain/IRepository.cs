namespace InvoiceGenerator.Domain
{
    public interface IRepository<T> where T : class
    {
        Task<int> Add(T item);
        Task<List<T>> GetAll();
        Task<int> Delete(T item);
    }
}
