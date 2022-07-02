namespace InvoiceGenerator.Domain
{
    public interface IRepository<T> where T : class
    {
        int Add(T item);
        List<T> GetAll();
        Task<int> Delete(T item);
    }
}
