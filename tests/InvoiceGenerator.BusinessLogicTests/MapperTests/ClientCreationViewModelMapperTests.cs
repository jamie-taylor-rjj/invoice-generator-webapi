using System.Diagnostics.CodeAnalysis;
using InvoiceGenerator.BusinessLogic;
using InvoiceGenerator.BusinessLogic.Mappers;
using InvoiceGenerator.Domain.Models;
using InvoiceGenerator.ViewModels;

namespace InvoiceGenerator.BusinessLogicTests.MapperTests;

[ExcludeFromCodeCoverage]
public class ClientCreationViewModelMapperTests
{
    private readonly IMapper<ClientCreationModel, Client> _mapper;

    public ClientCreationViewModelMapperTests()
    {
        _mapper = new ClientCreationViewModelMapper();
    }
    
    [Fact]
    public void Given_ClientCreationViewModel_Can_MapTo_Client()
    {
        // Arrange
        var clientCreationModel = new ClientCreationModel
        {
            ClientAddress = Guid.NewGuid().ToString(),
            ClientName = Guid.NewGuid().ToString(),
            ContactEmail = Guid.NewGuid().ToString(),
            ContactName = Guid.NewGuid().ToString(),
        };
        
        // Act
        var client = _mapper.Convert(clientCreationModel);
        
        // Assert
        Assert.NotNull(client);
        Assert.IsAssignableFrom<Client>(client);
        Assert.Equal(clientCreationModel.ClientAddress, client.ClientAddress);
        Assert.Equal(clientCreationModel.ClientName, client.ClientName);
        Assert.Equal(clientCreationModel.ContactEmail, client.ContactEmail);
        Assert.Equal(clientCreationModel.ContactName, client.ContactName);
    }
    
    [Fact]
    public void Given_Client_Cannot_MapTo_ClientCreationViewModel()
    {
        // Arrange
        var client = new Client
        {
            ClientAddress = Guid.NewGuid().ToString(),
            ClientName = Guid.NewGuid().ToString(),
            ContactEmail = Guid.NewGuid().ToString(),
            ContactName = Guid.NewGuid().ToString(),
            ClientId = Guid.NewGuid()
        };
        
        // Act
        var exception = Record.Exception(() => _mapper.Convert(client));
        
        // Assert
        Assert.NotNull(exception);
        Assert.IsAssignableFrom<NotImplementedException>(exception);
    }
}