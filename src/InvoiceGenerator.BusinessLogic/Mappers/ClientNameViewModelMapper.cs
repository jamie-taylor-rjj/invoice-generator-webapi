using InvoiceGenerator.Domain.Models;
using InvoiceGenerator.ViewModels;

namespace InvoiceGenerator.BusinessLogic.Mappers;

public class ClientNameViewModelMapper : IMapper<ClientNameViewModel, Client>
{
    public Client Convert(ClientNameViewModel source)
    {
        throw new System.NotImplementedException();
    }

    public ClientNameViewModel Convert(Client destination) => new ClientNameViewModel
    {
        ClientName = destination.ClientName,
        Id = destination.ClientId
    };
}