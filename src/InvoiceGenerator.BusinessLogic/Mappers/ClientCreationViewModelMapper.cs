using InvoiceGenerator.Domain.Models;
using InvoiceGenerator.ViewModels;

namespace InvoiceGenerator.BusinessLogic.Mappers;

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