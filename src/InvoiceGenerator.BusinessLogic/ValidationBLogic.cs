using InvoiceGenerator.ViewModels;
using System;
using System.Net.Mail;

namespace InvoiceGenerator.BusinessLogic
{
    public class ValidationBLogic
    {
        public ClientViewModelValidationResult validateUserDetails(ClientViewModel viewModel) // Validate all user inputs on client details entry screen
        {
            var result = new ClientViewModelValidationResult();

            if (string.IsNullOrWhiteSpace(viewModel.ClientName))  // If client name is null or white space
            {
                result.ClientNameValidationMessage = "Client Name must not be empty!"; // Make error message
            }
            if (string.IsNullOrWhiteSpace(viewModel.ClientAddress))
            {
                result.ClientAddressValidationMessage = "Client Address must not be empty!";
            }
            if (string.IsNullOrWhiteSpace(viewModel.ContactName))
            {
                result.ContactNameValidationMessage = "Contact Name must not be empty!";
            }
            if (string.IsNullOrWhiteSpace(viewModel.ContactEmail))
            {
                result.ContactEmailValidationMessage = "Contact Email must not be empty!";
            }

            return result; // Return all the error messages
        }

        public ClientViewModelValidationResult validateEmailFormat(ClientViewModel viewModel)  // Validate contact email input to see if it is of the correct format
        {
            var result = new ClientViewModelValidationResult();

            try
            {
                MailAddress mailAddress = new MailAddress(viewModel.ContactEmail); // Validate email with an email checker
            }
            catch (FormatException) // Catch error so program does not crash
            {
                result.ContactEmailFormatValidationMessage = "Contact Email must be formatted correctly!"; // Make error message
            }

            return result; // Return error message
        }

        public LineItemViewModelValidationResult validateLineItemDetails(LineItemViewModel viewModel)
        {
            var result = new LineItemViewModelValidationResult();
            
            if (string.IsNullOrWhiteSpace(viewModel.Description)) // If the line item description is empty, throw error
            {
                result.LineItemDescriptionValidationMessage = "Line Item Description must not be empty!"; // Make error message
            }
            if (string.IsNullOrWhiteSpace(viewModel.CostPer))
            {
                result.LineItemCostValidationMessage = "Line Item Cost must not be empty!";
            }
            if (string.IsNullOrWhiteSpace(viewModel.Quantity))
            {
                result.LineItemQuantityValidationMessage = "Line Item Quantity must not be empty!";
            }

            return result;   // Return all the error messages
        }

        public LineItemViewModelValidationResult checkCost(LineItemViewModel viewModel)
        {
            double outcome;
            var result = new LineItemViewModelValidationResult();

            if (double.TryParse(viewModel.CostPer, out outcome))    // Try convert lineItemCost input to a double, if the parse was successful, do below...
            {
                result.LineItemCheckCostValidationMessage = "No error!"; // No error
            }
            else // If the parse wasn't successful, do below...
            {
                result.LineItemCheckCostValidationMessage = "Line Item Cost must be a decimal number!"; // Make error message
            }

            return result; // Return error message
        }

        public LineItemViewModelValidationResult checkQuantity(LineItemViewModel viewModel)
        {
            int outcome;
            var result = new LineItemViewModelValidationResult();

            if (int.TryParse(viewModel.Quantity, out outcome)) // Try convert lineItemQuantity input to an int, if the parse was successful, do below...
            {
                result.LineItemCheckQuantityValidationMessage = "No error!"; // No error
            }
            else // If the parse wasn't successful, do below...
            {
                result.LineItemCheckQuantityValidationMessage = "Line Item Quantity must be an integer!"; // Make error message
            }

            return result; // Return error message
        }

        public InvoiceViewModelValidationResult checkVAT(InvoiceViewModel viewModel)
        {
            int outcome;
            var result = new InvoiceViewModelValidationResult();

            if (int.TryParse(viewModel.VatRate, out outcome)) // Try convert VAT input to an int, if the parse was successful, do below...
            {
                result.VATValidationMessage = "No error!"; // No error
            }
            else // If the parse wasn't successful, do below...
            {
                result.VATValidationMessage = "VAT/Sales Tax must be an integer!"; // Make error message
            }

            return result; // Return error message
        }
    }
}
