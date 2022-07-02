
namespace InvoiceGenerator.ViewModels
{
    public class ClientViewModelValidationResult
    {
        private static string NoError { get; } = "No error!";
        private static string NoEmailFormatError { get; } = "No error!";
        public string ClientNameValidationMessage { get; set; } = NoError;
        public string ClientAddressValidationMessage { get; set; } = NoError;
        public string ContactNameValidationMessage { get; set; } = NoError;
        public string ContactEmailValidationMessage { get; set; } = NoError;
        public string ContactEmailFormatValidationMessage { get; set; } = NoEmailFormatError;

        public bool ClientNameIsValid() =>
            ClientNameValidationMessage.Equals(NoError, StringComparison.InvariantCultureIgnoreCase);

        public bool ClientAddressIsValid() =>
             ClientAddressValidationMessage.Equals(NoError, StringComparison.InvariantCultureIgnoreCase);

        public bool ContactNameIsValid() =>
            ContactNameValidationMessage.Equals(NoError, StringComparison.InvariantCultureIgnoreCase);

        public bool ContactEmailIsValid() =>
            ContactEmailValidationMessage.Equals(NoError, StringComparison.InvariantCultureIgnoreCase);

        public bool ContactEmailFormatIsValid() =>
            ContactEmailFormatValidationMessage.Equals(NoEmailFormatError, StringComparison.InvariantCultureIgnoreCase);

        public bool IsValid() =>
            ClientNameIsValid() && ClientAddressIsValid() && ContactNameIsValid() && ContactEmailIsValid();

        public bool IsValidEmail() =>
            ContactEmailFormatIsValid();
    }
}