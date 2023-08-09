using System.Diagnostics.CodeAnalysis;
using InvoiceGenerator.BusinessLogic;
using InvoiceGenerator.Domain;
using InvoiceGenerator.Domain.Models;
using InvoiceGenerator.ViewModels;
using NSubstitute;

namespace InvoiceGenerator.BusinessLogicTests.ServiceTests;

[ExcludeFromCodeCoverage]
public class ClientServiceTests
{
    private readonly Guid _clientId = Guid.NewGuid();
    private readonly string _clientName = Guid.NewGuid().ToString();
    private readonly string _clientAddress = Guid.NewGuid().ToString();
    private readonly string _contactName = Guid.NewGuid().ToString();
    private readonly string _contactEmail = Guid.NewGuid().ToString();

    private readonly IMapper<ClientViewModel, Client> _mockedClientViewModelMapper =
        Substitute.For<IMapper<ClientViewModel, Client>>();

    private readonly IMapper<ClientNameViewModel, Client> _mockedClientNameViewModelMapper =
        Substitute.For<IMapper<ClientNameViewModel, Client>>();

    private readonly IMapper<ClientCreationModel, Client> _mockedClientCreationModelMapper =
        Substitute.For<IMapper<ClientCreationModel, Client>>();

    private List<Client> GenerateRandomListOfClients(int length)
    {
        var retVal = new List<Client>();
        var count = 0;
        while (count < length)
        {
            retVal.Add(new Client
            {
                ClientId = Guid.NewGuid(),
                ClientName = Guid.NewGuid().ToString(),
                ClientAddress = Guid.NewGuid().ToString(),
                ContactName = Guid.NewGuid().ToString(),
                ContactEmail = Guid.NewGuid().ToString(),
            });
            count++;
        }

        return retVal;
    }

    [Fact]
    public async Task Given_A_Client_GetAll_Should_Return_At_Least_One_ClientViewModel()
    {
        // Arrange
        var client = new Client
        {
            ClientId = _clientId,
            ClientName = _clientName,
            ClientAddress = _clientAddress,
            ContactName = _contactName,
            ContactEmail = _contactEmail
        };
        var clientsForMock = new List<Client> { client };
        var mockedRepository = Substitute.For<IRepository<Client>>();
        mockedRepository.GetAll().Returns(clientsForMock);

        var expectedOutput = new ClientViewModel
        {
            ClientId = _clientId,
            ClientName = _clientName,
            ClientAddress = _clientAddress,
            ContactName = _contactName,
            ContactEmail = _contactEmail
        };

        _mockedClientViewModelMapper.Convert(client).ReturnsForAnyArgs(expectedOutput);

        var sut = new ClientService(mockedRepository,
            _mockedClientViewModelMapper,
            null!,
            null!);

        // Act
        var result = await sut.GetClients();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<List<ClientViewModel>>(result);

        Assert.Equal(_clientId, result.FirstOrDefault()?.ClientId);
        Assert.Equal(_clientName, result.FirstOrDefault()?.ClientName);
        Assert.Equal(_clientAddress, result.FirstOrDefault()?.ClientAddress);
        Assert.Equal(_contactName, result.FirstOrDefault()?.ContactName);
        Assert.Equal(_contactEmail, result.FirstOrDefault()?.ContactEmail);
    }

    [Fact]
    public async Task Check_If_Created_List_Of_Client_Names_Matches_DataBase_Client_Names()
    {
        // Arrange
        var client = new Client
        {
            ClientId = _clientId,
            ClientName = _clientName,
            ClientAddress = _clientAddress,
            ContactName = _contactName,
            ContactEmail = _contactEmail
        };

        var clientNamesForMock = new List<Client> { client };
        var mockedRepository = Substitute.For<IRepository<Client>>();
        mockedRepository.GetAll().Returns(clientNamesForMock);

        var expectedOutput = new ClientNameViewModel
        {
            Id = _clientId,
            ClientName = _clientName
        };

        _mockedClientNameViewModelMapper.Convert(client).ReturnsForAnyArgs(expectedOutput);

        var sut = new ClientService(mockedRepository,
            null!,
            _mockedClientNameViewModelMapper,
            null!);

        // Act
        var result = await sut.GetClientNames();
        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<List<ClientNameViewModel>>(result);

        Assert.Equal(_clientName, result.FirstOrDefault()?.ClientName);
        Assert.Equal(_clientId, result.FirstOrDefault()?.Id);
    }

    [Fact]
    public async Task Check_If_Client_Details_Are_Added_To_Database()
    {
        //Arrange
        const int expectedObjectsAltered = 1;
        var viewModel = new ClientCreationModel
        {
            ClientName = _clientName,
            ClientAddress = _clientAddress,
            ContactName = _contactName,
            ContactEmail = _contactEmail
        };
        var mockedRepository = Substitute.For<IRepository<Client>>();
        mockedRepository.Add(new Client()).ReturnsForAnyArgs(expectedObjectsAltered);

        var expectedDatabaseEntity = new Client
        {
            ClientId = _clientId,
            ClientName = _clientName,
            ClientAddress = _clientAddress,
            ContactName = _contactName,
            ContactEmail = _contactEmail
        };

        _mockedClientCreationModelMapper.Convert(viewModel).ReturnsForAnyArgs(expectedDatabaseEntity);

        var expectedOutput = new ClientViewModel
        {
            ClientId = _clientId,
            ClientName = _clientName,
            ClientAddress = _clientAddress,
            ContactName = _contactName,
            ContactEmail = _contactEmail
        };

        var mockedClientViewModelMapper = Substitute.For<IMapper<ClientViewModel, Client>>();
        mockedClientViewModelMapper.Convert(expectedDatabaseEntity).Returns(expectedOutput);

        var sut = new ClientService(mockedRepository,
            mockedClientViewModelMapper!,
            null!,
            _mockedClientCreationModelMapper);

        // Act
        var result = await sut.AddClient(viewModel);
        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<ClientViewModel>(result);

        Assert.Equal(_clientId, result.ClientId);
        Assert.Equal(_clientName, result.ClientName);
        Assert.Equal(_clientAddress, result.ClientAddress);
        Assert.Equal(_contactName, result.ContactName);
        Assert.Equal(_contactEmail, result.ContactEmail);
    }

    [Fact]
    public async Task Given_A_Valid_ClientId_GetById_Should_Return_The_Matching_ClientViewModel()
    {
        // Arrange
        var client = new Client
        {
            ClientId = _clientId,
            ClientName = _clientName,
            ClientAddress = _clientAddress,
            ContactName = _contactName,
            ContactEmail = _contactEmail
        };
        var clientsForMock = new List<Client> { client };
        var mockedRepository = Substitute.For<IRepository<Client>>();
        mockedRepository.GetAll().Returns(clientsForMock);

        var expectedOutput = new ClientViewModel
        {
            ClientId = _clientId,
            ClientName = _clientName,
            ClientAddress = _clientAddress,
            ContactName = _contactName,
            ContactEmail = _contactEmail
        };

        _mockedClientViewModelMapper.Convert(client).Returns(expectedOutput);

        var sut = new ClientService(mockedRepository,
            _mockedClientViewModelMapper,
            null!,
            null!);

        // Act
        var result = await sut.GetById(_clientId);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<ClientViewModel>(result);

        Assert.Equal(_clientId, result.ClientId);
        Assert.Equal(_clientName, result.ClientName);
        Assert.Equal(_clientAddress, result.ClientAddress);
        Assert.Equal(_contactName, result.ContactName);
        Assert.Equal(_contactEmail, result.ContactEmail);
    }

    [Fact]
    public async Task Given_An_Invalid_ClientId_GetById_Should_Return_Null()
    {
        // Arrange
        var client = new Client
        {
            ClientId = _clientId,
            ClientName = _clientName,
            ClientAddress = _clientAddress,
            ContactName = _contactName,
            ContactEmail = _contactEmail
        };
        var clientsForMock = new List<Client> { client };
        var mockedRepository = Substitute.For<IRepository<Client>>();
        mockedRepository.GetAll().Returns(clientsForMock);

        var sut = new ClientService(mockedRepository,
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
        // Arrange
        var client = new Client
        {
            ClientId = _clientId,
            ClientName = _clientName,
            ClientAddress = _clientAddress,
            ContactName = _contactName,
            ContactEmail = _contactEmail
        };
        var clientsForMock = new List<Client> { client };
        var mockedRepository = Substitute.For<IRepository<Client>>();
        mockedRepository.GetAll().Returns(clientsForMock);

        var sut = new ClientService(mockedRepository,
            null!,
            null!,
            null!);

        // Act
        var response = await sut.DeleteById(_clientId);

        // Assert
        Assert.True(response);
    }

    [Fact]
    public async Task Given_An_Invalid_ClientId_GetById_Should_Raise_ArgumentException()
    {
        // Arrange
        var client = new Client
        {
            ClientId = _clientId,
            ClientName = _clientName,
            ClientAddress = _clientAddress,
            ContactName = _contactName,
            ContactEmail = _contactEmail
        };
        var clientsForMock = new List<Client> { client };
        var mockedRepository = Substitute.For<IRepository<Client>>();
        mockedRepository.GetAll().Returns(clientsForMock);

        var sut = new ClientService(mockedRepository,
            null!,
            null!,
            null!);

        // Act
        var response = await sut.DeleteById(Guid.NewGuid());

        // Assert
        Assert.False(response);
    }

    [Fact]
    public async Task Given_Valid_Input_GetPage_Should_Return_PagedResult_Of_ClientViewModels()
    {
        // Arrange
        const int clientCount = 50;
        var clients = GenerateRandomListOfClients(clientCount);
        var mockedRepository = Substitute.For<IRepository<Client>>();
        mockedRepository.GetAll().Returns(clients);

        var expectedOutput = new ClientViewModel
        {
            ClientId = clients.First().ClientId,
            ClientName = clients.First().ClientName,
            ClientAddress = clients.First().ClientAddress,
            ContactName = clients.First().ContactName,
            ContactEmail = clients.First().ContactEmail
        };

        _mockedClientViewModelMapper.Convert(new Client()).ReturnsForAnyArgs(expectedOutput);

        var sut = new ClientService(mockedRepository,
            _mockedClientViewModelMapper,
            null!,
            null!);

        const int pageNumber = 1;
        const int pageSize = 10;

        // Act
        var result = await sut.GetPage(pageNumber);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<PagedResponse<ClientViewModel>>(result);

        Assert.Equal(pageNumber, result.PageNumber);
        Assert.Equal(pageSize, result.PageSize);
        Assert.Equal(clientCount / pageSize, result.TotalPages);
        Assert.Equal(clientCount, result.TotalRecords);

        Assert.NotNull(result.Data);
        Assert.IsAssignableFrom<List<ClientViewModel>>(result.Data);

        var first = result.Data.First();
        Assert.NotNull(first);
        Assert.Equal(expectedOutput.ClientId, first.ClientId);
        Assert.Equal(expectedOutput.ClientAddress, first.ClientAddress);
        Assert.Equal(expectedOutput.ClientName, first.ClientName);
        Assert.Equal(expectedOutput.ContactEmail, first.ContactEmail);
        Assert.Equal(expectedOutput.ContactName, first.ContactName);
    }
}