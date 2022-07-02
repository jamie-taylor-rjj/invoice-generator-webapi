
namespace InvoiceGenerator.ViewModels
{
    public class InvoiceViewModelValidationResult
    {
        private static string NoCheckVATError { get; } = "No error!";
        public string VATValidationMessage { get; set; } = NoCheckVATError;

        public bool VATIsValid() =>
            VATValidationMessage.Equals(NoCheckVATError, StringComparison.InvariantCultureIgnoreCase);

        public bool IsValidVAT() =>
            VATIsValid();
    }
}
