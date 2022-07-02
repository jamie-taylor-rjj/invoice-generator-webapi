using System.Diagnostics.CodeAnalysis;
using InvoiceGenerator.BusinessLogic;
using InvoiceGenerator.BusinessLogic.Mappers;
using InvoiceGenerator.Domain.Models;
using InvoiceGenerator.ViewModels;

namespace InvoiceGenerator.BusinessLogicTests.MapperTests;

[ExcludeFromCodeCoverage]
public class ClientViewModelMapperTests
{
    private readonly IMapper<ClientViewModel, Client> _mapper;

    public ClientViewModelMapperTests()
    {
        _mapper = new ClientViewModelMapper();
    }
    
    [Fact]
    public void Given_Client_Can_MapTo_ClientViewModel()
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
        var clientViewModel = _mapper.Convert(client);
        
        // Assert
        Assert.NotNull(clientViewModel);
        Assert.IsAssignableFrom<ClientViewModel>(clientViewModel);
        Assert.Equal(client.ClientAddress, clientViewModel.ClientAddress);
        Assert.Equal(client.ClientName, clientViewModel.ClientName);
        Assert.Equal(client.ContactEmail, clientViewModel.ContactEmail);
        Assert.Equal(client.ContactName, clientViewModel.ContactName);
        Assert.Equal(client.ClientId, clientViewModel.ClientId);
    }
    
    [Fact]
    public void Given_ClientViewModel_Can_MapTo_Client()
    {
        // Arrange
        var clientViewModel = new ClientViewModel
        {
            ClientAddress = Guid.NewGuid().ToString(),
            ClientName = Guid.NewGuid().ToString(),
            ContactEmail = Guid.NewGuid().ToString(),
            ContactName = Guid.NewGuid().ToString(),
            ClientId = Guid.NewGuid()
        };
        
        // Act
        var client = _mapper.Convert(clientViewModel);
        
        // Assert
        Assert.NotNull(client);
        Assert.IsAssignableFrom<Client>(client);
        Assert.Equal(clientViewModel.ClientAddress, client.ClientAddress);
        Assert.Equal(clientViewModel.ClientName, client.ClientName);
        Assert.Equal(clientViewModel.ContactEmail, client.ContactEmail);
        Assert.Equal(clientViewModel.ContactName, client.ContactName);
    }
}