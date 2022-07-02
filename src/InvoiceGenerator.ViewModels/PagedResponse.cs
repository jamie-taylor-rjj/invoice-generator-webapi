namespace InvoiceGenerator.ViewModels;

public class PagedResponse<T> where T : IViewModel
{
    public List<T> Data { get; set; } = new List<T>();
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalRecords { get; set; }
}
