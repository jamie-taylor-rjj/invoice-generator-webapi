namespace InvoiceGenerator.ViewModels
{

    public class ClientViewModel : IViewModel
    {
        public Guid ClientId { get; set;}
        public string ClientName { get; set; } = string.Empty;
        public string ClientAddress { get; set; } = string.Empty;
        public string ContactName { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
    }
}
