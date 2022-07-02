namespace InvoiceGenerator.ViewModels
{
    public class ClientCreationModel : IViewModel
    {
        public string ClientName { get; set; } = string.Empty;
        public string ClientAddress { get; set; } = string.Empty;
        public string ContactName { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
    }
}
