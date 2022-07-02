using InvoiceGenerator.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoiceGenerator.BusinessLogic
{
    public interface IClientService
    {
        int AddClient(ClientViewModel viewModel);
        List<ClientNameViewModel> GetClientNames();
        List<ClientViewModel> GetClients();
        ClientViewModel GetById(Guid Id);
        PagedResponse<ClientViewModel> GetPage(int pageNumber, int pageSize = 10);
    }
}