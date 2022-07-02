using InvoiceGenerator.ViewModels;
using System.Collections.Generic;

namespace InvoiceGenerator.BusinessLogic
{
    public interface IClientService
    {
        int AddClient(ClientViewModel viewModel);
        List<ClientNameViewModel> GetClientNames();
        List<ClientViewModel> GetClients();
    }
}