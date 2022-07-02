using InvoiceGenerator.Domain.Models;
using InvoiceGenerator.ViewModels;

namespace InvoiceGenerator.BusinessLogic.Mappers;

public class ClientViewModelMapper : IMapper<ClientViewModel, Client>
{
    public Client Convert(ClientViewModel source) => new Client
    {
        ClientAddress = source.ClientAddress,
        ClientName = source.ClientName,
        ContactEmail = source.ContactEmail,
        ContactName = source.ContactName
    };

    public ClientViewModel Convert(Client destination) => new ClientViewModel
    {
        ClientId = destination.ClientId,
        ClientAddress = destination.ClientAddress,
        ClientName = destination.ClientName,
        ContactEmail = destination.ContactEmail,
        ContactName = destination.ContactName
    };
}
