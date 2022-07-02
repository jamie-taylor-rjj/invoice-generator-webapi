using System.Diagnostics.CodeAnalysis;

namespace InvoiceGenerator.Domain.Models
{
    [ExcludeFromCodeCoverage]
    public class Client
    {
        public Guid ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientAddress { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        //public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
    }
}
