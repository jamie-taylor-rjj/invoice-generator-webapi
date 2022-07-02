using InvoiceGenerator.Domain.Models;
using InvoiceGenerator.ViewModels;

namespace InvoiceGenerator.BusinessLogic;

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

public class ClientCreationViewModelMapper : IMapper<ClientCreationModel, Client>
{
    public Client Convert(ClientCreationModel source) => new Client
    {
        ClientAddress = source.ClientAddress,
        ClientName = source.ClientName,
        ContactEmail = source.ContactEmail,
        ContactName = source.ContactName
    };

    public ClientCreationModel Convert(Client destination)
    {
        throw new System.NotImplementedException();
    }
}