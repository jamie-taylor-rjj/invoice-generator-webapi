using InvoiceGenerator.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoiceGenerator.BusinessLogic
{
    public interface IClientService
    {
        int AddClient(ClientViewModel viewModel);
        List<ClientNameViewModel> GetClientNames();
        List<ClientViewModel> GetClients();
        PagedResponse<ClientViewModel> GetPage(int pageNumber, int pageSize = 10);
    }
}