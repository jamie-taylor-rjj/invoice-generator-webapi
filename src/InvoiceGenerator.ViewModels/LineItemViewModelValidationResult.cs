
namespace InvoiceGenerator.ViewModels
{
    public class LineItemViewModelValidationResult
    {
        private static string NoError { get; } = "No error!";
        private static string NoCheckCostError { get; } = "No error!";
        private static string NoCheckQuantityError { get; } = "No error!";
        public string LineItemDescriptionValidationMessage { get; set; } = NoError;
        public string LineItemCostValidationMessage { get; set; } = NoError;
        public string LineItemQuantityValidationMessage { get; set; } = NoError;
        public string LineItemCheckCostValidationMessage { get; set; } = NoCheckCostError;
        public string LineItemCheckQuantityValidationMessage { get; set; } = NoCheckQuantityError;

        public bool LineItemDescriptionIsValid() =>
            LineItemDescriptionValidationMessage.Equals(NoError, StringComparison.InvariantCultureIgnoreCase);
        public bool LineItemCostIsValid() =>
            LineItemCostValidationMessage.Equals(NoError, StringComparison.InvariantCultureIgnoreCase);
        public bool LineItemQuantityIsValid() =>
            LineItemQuantityValidationMessage.Equals(NoError, StringComparison.InvariantCultureIgnoreCase);
        public bool LineItemCheckCostIsValid() =>
            LineItemCheckCostValidationMessage.Equals(NoCheckCostError, StringComparison.InvariantCultureIgnoreCase);
        public bool LineItemCheckQuantityIsValid() =>
            LineItemCheckQuantityValidationMessage.Equals(NoCheckQuantityError, StringComparison.InvariantCultureIgnoreCase);
        
        public bool IsValid() =>
            LineItemDescriptionIsValid() && LineItemCostIsValid() && LineItemQuantityIsValid();
        public bool IsValidCost() =>
            LineItemCheckCostIsValid();
        public bool IsValidQuantity() =>
            LineItemCheckQuantityIsValid();
    }
}
