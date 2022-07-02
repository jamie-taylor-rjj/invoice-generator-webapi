using System.Diagnostics.CodeAnalysis;
using InvoiceGenerator.BusinessLogic;
using InvoiceGenerator.Domain;
using InvoiceGenerator.Domain.Models;
using InvoiceGenerator.ViewModels;
using Moq;

namespace InvoiceGenerator.BusinessLogicTests.ServiceTests;

[ExcludeFromCodeCoverage]
public class ClientServiceTests
{
    [Fact]
    public async Task Given_A_Client_GetAll_Should_Return_At_Least_One_ClientViewModel()
    {
        var clientId = Guid.NewGuid();
        var clientName = Guid.NewGuid().ToString();
        var clientAddress = Guid.NewGuid().ToString();
        var contactName = Guid.NewGuid().ToString();
        var contactEmail = Guid.NewGuid().ToString();
        
        // Arrange
        var client = new Client
        {
            ClientId = clientId,
            ClientName = clientName,
            ClientAddress = clientAddress,
            ContactName = contactName,
            ContactEmail = contactEmail
        };
        var clientsForMock = new List<Client> { client };
        var mockedRepository = new Mock<IRepository<Client>>();
        mockedRepository.Setup(x => x.GetAll()).ReturnsAsync(clientsForMock);

        var expectedOutput = new ClientViewModel
        {
            ClientId = clientId,
            ClientName = clientName,
            ClientAddress = clientAddress,
            ContactName = contactName,
            ContactEmail = contactEmail
        };

        var mockedClientViewModelMapper = new Mock<IMapper<ClientViewModel, Client>>();
        mockedClientViewModelMapper.Setup(x => x.Convert(client)).Returns(expectedOutput);

        var sut = new ClientService(mockedRepository.Object,
            mockedClientViewModelMapper.Object,
            null!,
            null!);
        
        // Act
        var result = await sut.GetClients();
        
        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<List<ClientViewModel>>(result);
        
        Assert.Equal(clientId, result.FirstOrDefault()?.ClientId);
        Assert.Equal(clientName, result.FirstOrDefault()?.ClientName);
        Assert.Equal(clientAddress, result.FirstOrDefault()?.ClientAddress);
        Assert.Equal(contactName, result.FirstOrDefault()?.ContactName);
        Assert.Equal(contactEmail, result.FirstOrDefault()?.ContactEmail);
    }

    [Fact]
    public async Task Check_If_Created_List_Of_Client_Names_Matches_DataBase_Client_Names()
    {
        //Arrange
        var clientId = Guid.NewGuid();
        var clientName = Guid.NewGuid().ToString();
        var clientAddress = Guid.NewGuid().ToString();
        var contactName = Guid.NewGuid().ToString();
        var contactEmail = Guid.NewGuid().ToString();
        
        // Arrange
        var client = new Client
        {
            ClientId = clientId,
            ClientName = clientName,
            ClientAddress = clientAddress,
            ContactName = contactName,
            ContactEmail = contactEmail
        };
        
        var clientNamesForMock = new List<Client> { client };
        var mockedRepository = new Mock<IRepository<Client>>();
        mockedRepository.Setup(x => x.GetAll()).ReturnsAsync(clientNamesForMock);
        
        var expectedOutput = new ClientNameViewModel
        {
            Id = clientId,
            ClientName = clientName
        };
    
        var mockedClientNameViewModelMapper = new Mock<IMapper<ClientNameViewModel, Client>>();
        mockedClientNameViewModelMapper.Setup(x => x.Convert(client)).Returns(expectedOutput);

        var sut = new ClientService(mockedRepository.Object,
            null!,
            mockedClientNameViewModelMapper.Object,
            null!);
        
        // Act
        var result = await sut.GetClientNames();
        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<List<ClientNameViewModel>>(result);
        
        Assert.Equal(clientName, result.FirstOrDefault()?.ClientName);
        Assert.Equal(clientId, result.FirstOrDefault()?.Id);
    }

    [Fact]
    public async Task Check_If_Client_Details_Are_Added_To_Database()
    {
        //Arrange
        var expectedObjectsAltered = 1;
        var clientId = Guid.NewGuid();
        var clientName = Guid.NewGuid().ToString();
        var clientAddress = Guid.NewGuid().ToString();
        var contactName = Guid.NewGuid().ToString();
        var contactEmail = Guid.NewGuid().ToString();
        
        var viewModel = new ClientCreationModel
        {
            ClientName = clientName,
            ClientAddress = clientAddress,
            ContactName = contactName,
            ContactEmail = contactEmail
        };
        var mockedRepository = new Mock<IRepository<Client>>();
        mockedRepository.Setup(x => x.Add(It.IsAny<Client>())).ReturnsAsync(expectedObjectsAltered);
        
        var expectedDatabaseEntity = new Client
        {
            ClientId = clientId,
            ClientName = clientName,
            ClientAddress = clientAddress,
            ContactName = contactName,
            ContactEmail = contactEmail
        };
        
        var mockedClientCreationModelMapper = new Mock<IMapper<ClientCreationModel, Client>>();
        mockedClientCreationModelMapper.Setup(x => x.Convert(viewModel)).Returns(expectedDatabaseEntity);
        
        var expectedOutput = new ClientViewModel
        {
            ClientId = clientId,
            ClientName = clientName,
            ClientAddress = clientAddress,
            ContactName = contactName,
            ContactEmail = contactEmail
        };

        var mockedClientViewModelMapper = new Mock<IMapper<ClientViewModel, Client>>();
        mockedClientViewModelMapper.Setup(x => x.Convert(expectedDatabaseEntity)).Returns(expectedOutput);

        var sut = new ClientService(mockedRepository.Object,
            mockedClientViewModelMapper.Object!,
            null!,
            mockedClientCreationModelMapper.Object);
        
        // Act
        var result = await sut.AddClient(viewModel);
        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<ClientViewModel>(result);
        
        Assert.Equal(clientId, result.ClientId);
        Assert.Equal(clientName, result.ClientName);
        Assert.Equal(clientAddress, result.ClientAddress);
        Assert.Equal(contactName, result.ContactName);
        Assert.Equal(contactEmail, result.ContactEmail);
    }
    
    [Fact]
    public async Task Given_A_Valid_ClientId_GetById_Should_Return_The_Matching_ClientViewModel()
    {
        var clientId = Guid.NewGuid();
        var clientName = Guid.NewGuid().ToString();
        var clientAddress = Guid.NewGuid().ToString();
        var contactName = Guid.NewGuid().ToString();
        var contactEmail = Guid.NewGuid().ToString();
        
        // Arrange
        var client = new Client
        {
            ClientId = clientId,
            ClientName = clientName,
            ClientAddress = clientAddress,
            ContactName = contactName,
            ContactEmail = contactEmail
        };
        var clientsForMock = new List<Client> { client };
        var mockedRepository = new Mock<IRepository<Client>>();
        mockedRepository.Setup(x => x.GetAll()).ReturnsAsync(clientsForMock);

        var expectedOutput = new ClientViewModel
        {
            ClientId = clientId,
            ClientName = clientName,
            ClientAddress = clientAddress,
            ContactName = contactName,
            ContactEmail = contactEmail
        };

        var mockedClientViewModelMapper = new Mock<IMapper<ClientViewModel, Client>>();
        mockedClientViewModelMapper.Setup(x => x.Convert(client)).Returns(expectedOutput);

        var sut = new ClientService(mockedRepository.Object,
            mockedClientViewModelMapper.Object,
            null!,
            null!);
        
        // Act
        var result = await sut.GetById(clientId);
        
        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<ClientViewModel>(result);
        
        Assert.Equal(clientId, result.ClientId);
        Assert.Equal(clientName, result.ClientName);
        Assert.Equal(clientAddress, result.ClientAddress);
        Assert.Equal(contactName, result.ContactName);
        Assert.Equal(contactEmail, result.ContactEmail);
    }
    
    [Fact]
    public async Task Given_An_Invalid_ClientId_GetById_Should_Return_Null()
    {
        var clientId = Guid.NewGuid();
        var clientName = Guid.NewGuid().ToString();
        var clientAddress = Guid.NewGuid().ToString();
        var contactName = Guid.NewGuid().ToString();
        var contactEmail = Guid.NewGuid().ToString();
        
        // Arrange
        var client = new Client
        {
            ClientId = clientId,
            ClientName = clientName,
            ClientAddress = clientAddress,
            ContactName = contactName,
            ContactEmail = contactEmail
        };
        var clientsForMock = new List<Client> { client };
        var mockedRepository = new Mock<IRepository<Client>>();
        mockedRepository.Setup(x => x.GetAll()).ReturnsAsync(clientsForMock);

        var sut = new ClientService(mockedRepository.Object,
            null!,
            null!,
            null!);
        
        // Act
        var result = await sut.GetById(Guid.NewGuid());
        
        // Assert
        Assert.Null(result);
    }
    
    [Fact]
    public async Task Given_A_Valid_ClientId_Delete_Should_Not_Raise_ArgumentException()
    {
        var clientId = Guid.NewGuid();
        var clientName = Guid.NewGuid().ToString();
        var clientAddress = Guid.NewGuid().ToString();
        var contactName = Guid.NewGuid().ToString();
        var contactEmail = Guid.NewGuid().ToString();
        
        // Arrange
        var client = new Client
        {
            ClientId = clientId,
            ClientName = clientName,
            ClientAddress = clientAddress,
            ContactName = contactName,
            ContactEmail = contactEmail
        };
        var clientsForMock = new List<Client> { client };
        var mockedRepository = new Mock<IRepository<Client>>();
        mockedRepository.Setup(x => x.GetAll()).ReturnsAsync(clientsForMock);

        var sut = new ClientService(mockedRepository.Object,
            null!,
            null!,
            null!);
        
        // Act
        await sut.DeleteById(clientId);
        
        // Assert
        // Nothing to assert, as an exception would be raised by Service Delete method
    }
    
    [Fact]
    public async Task Given_An_Invalid_ClientId_GetById_Should_Raise_ArgumentException()
    {
        var clientId = Guid.NewGuid();
        var clientName = Guid.NewGuid().ToString();
        var clientAddress = Guid.NewGuid().ToString();
        var contactName = Guid.NewGuid().ToString();
        var contactEmail = Guid.NewGuid().ToString();
        
        // Arrange
        var client = new Client
        {
            ClientId = clientId,
            ClientName = clientName,
            ClientAddress = clientAddress,
            ContactName = contactName,
            ContactEmail = contactEmail
        };
        var clientsForMock = new List<Client> { client };
        var mockedRepository = new Mock<IRepository<Client>>();
        mockedRepository.Setup(x => x.GetAll()).ReturnsAsync(clientsForMock);

        var sut = new ClientService(mockedRepository.Object,
            null!,
            null!,
            null!);
        
        // Act
        var exception = await Record.ExceptionAsync(() => sut.DeleteById(Guid.NewGuid()));
        
        // Assert
        Assert.NotNull(exception);
        Assert.IsAssignableFrom<ArgumentException>(exception);
    }
}