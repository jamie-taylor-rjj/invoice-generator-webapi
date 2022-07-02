using System.Diagnostics.CodeAnalysis;
using InvoiceGenerator.BusinessLogic;
using InvoiceGenerator.BusinessLogic.Mappers;
using InvoiceGenerator.Domain.Models;
using InvoiceGenerator.ViewModels;

namespace InvoiceGenerator.BusinessLogicTests.MapperTests;

[ExcludeFromCodeCoverage]
public class ClientNameViewModelMapperTests
{
    private readonly IMapper<ClientNameViewModel, Client> _mapper;

    public ClientNameViewModelMapperTests()
    {
        _mapper = new ClientNameViewModelMapper();
    }
    
    [Fact]
    public void Given_Client_Can_MapTo_ClientNameViewModel()
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
        Assert.IsAssignableFrom<ClientNameViewModel>(clientViewModel);
        Assert.Equal(client.ClientName, clientViewModel.ClientName);
        Assert.Equal(client.ClientId, clientViewModel.Id);
    }
    
    [Fact]
    public void Given_ClientViewModel_Cannot_MapTo_Client()
    {
        // Arrange
        var clientViewModel = new ClientNameViewModel
        {
            ClientName = Guid.NewGuid().ToString(),
            Id = Guid.NewGuid()
        };
        
        // Act
        var exception = Record.Exception(() => _mapper.Convert(clientViewModel));
        
        // Assert
        Assert.NotNull(exception);
        Assert.IsAssignableFrom<NotImplementedException>(exception);
    }
}