using InvoiceGenerator.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoiceGenerator.BusinessLogic
{
    public interface IClientService
    {
        Task<List<ClientViewModel>> GetClients();
        Task<List<ClientNameViewModel>> GetClientNames();
        Task<ClientViewModel> AddClient(ClientCreationModel viewModel);
        Task<ClientViewModel?> GetById(Guid Id);
        Task<PagedResponse<ClientViewModel>> GetPage(int pageNumber, int pageSize = 10);
        Task<bool> DeleteById(Guid Id);
    }
}