using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using InvoiceGenerator.ViewModels;
using InvoiceGenerator.WebApi.IntegrationTests.Fixtures;
using InvoiceGenerator.WebApi.IntegrationTests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit.Abstractions;

namespace InvoiceGenerator.WebApi.IntegrationTests.Controllers;

[ExcludeFromCodeCoverage]
public class ClientControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Startup> _factory;

    // public ClientControllerTests()
    // {
    //     // var appFactory = new WebApplicationFactory<Startup>();
    //     // _client = appFactory.CreateClient();
    //     _client = CreateClient(new WebApplicationFactoryClientOptions
    //     {
    //         AllowAutoRedirect = false
    //     });
    // }

    public ClientControllerTests(CustomWebApplicationFactory<Startup> factory)
    {
        _factory = factory;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    [Fact]
    public async Task GetClientList_Returns_A_List_Of_ClientViewModel()
    {
        // Arrange
        
        // Act
        var response = await _client.GetAsync("api/client/");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadFromJsonAsync<List<ClientViewModel>>();
        Assert.NotNull(content);
        Assert.NotEmpty(content);
        Assert.IsAssignableFrom<List<ClientViewModel>>(content);
    }
    
    [Fact]
    public async Task GetClientList_Returns_A_List_Of_ClientNameViewModel()
    {
        // Arrange
        
        // Act
        var response = await _client.GetAsync("api/client/names");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadFromJsonAsync<List<ClientNameViewModel>>();
        Assert.NotNull(content);
        Assert.NotEmpty(content);
        Assert.IsAssignableFrom<List<ClientNameViewModel>>(content);
    }
    
    // NOTE THIS IS A VERY FRAGILE TEST
    [Fact]
    public async Task When_Valid_Id_Given_To_GetById_Returns_A_ClientViewModel()
    {
        // Arrange
        var clientId = Utilities.KnownGoodClientId;
        
        // Act
        var response = await _client.GetAsync($"api/client/{clientId}");
        
        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadFromJsonAsync<ClientViewModel>();
        
        Assert.NotNull(content);
        Assert.Equal(content.ClientId, clientId);
    }
    
    [Fact]
    public async Task When_Invalid_Id_Given_To_GetById_Returns_A_NotFound()
    {
        // Arrange
        var clientId = Guid.NewGuid();
        
        // Act
        var response = await _client.GetAsync($"api/client/{clientId}");

        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Given_Valid_Input_CreateNew_Creates_A_New_Record_And_Returns_A_ClientViewModel()
    {
        // Arrange
        var client = new ClientCreationModel
        {
            ClientAddress = "DELETE_ME",
            ClientName = "DELETE_ME",
            ContactEmail = "DELETE_ME",
            ContactName = "DELETE_ME"
        };

        var json = JsonConvert.SerializeObject(client);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/api/client/", data);
        
        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadFromJsonAsync<ClientViewModel>();
        
        Assert.NotNull(content);
        Assert.Equal(client.ClientAddress, content.ClientAddress);
        Assert.Equal(client.ClientName, content.ClientName);
        Assert.Equal(client.ContactEmail, content.ContactEmail);
        Assert.Equal(client.ContactName, content.ContactName);
    }

    [Fact]
    public async Task GetPage_Returns_PagedList_Of_ClientViewModel()
    {
        // Arrange
        const int pageNumber = 2;
        const int pageSize = 20;
        
        // Assert
        var response = await _client.GetAsync($"/api/client/page?pageNumber={pageNumber}&pageSize={pageSize}");
        
        // // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadFromJsonAsync<PagedResponse<ClientViewModel>>();
        
        Assert.NotNull(content);
        Assert.Equal(pageNumber, content.PageNumber);
    }
    
    [Fact]
    public async Task Given_Valid_Input_Delete_Returns_OK()
    {
        // Arrange
        var client = new ClientCreationModel
        {
            ClientAddress = "DELETE_ME",
            ClientName = "DELETE_ME",
            ContactEmail = "DELETE_ME",
            ContactName = "DELETE_ME"
        };

        var json = JsonConvert.SerializeObject(client);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        var createResponse = await _client.PostAsync("/api/client/", data);
        createResponse.EnsureSuccessStatusCode();
        var createContent = await createResponse.Content.ReadFromJsonAsync<ClientViewModel>();

        var clientId = createContent.ClientId;
        
        // Act
        var deleteResponse = await _client.DeleteAsync($"/api/client?id={clientId}");
        
        // Assert
        deleteResponse.EnsureSuccessStatusCode();
    }
    
    [Fact]
    public async Task Given_Invalid_Input_Delete_Does_Not_Return_Ok()
    {
        // Arrange
        var clientId = Guid.NewGuid();
        
        // Act
        var deleteResponse = await _client.DeleteAsync($"/api/client?id={clientId}");
        
        // Assert
        Assert.NotNull(deleteResponse);
        Assert.Equal(HttpStatusCode.InternalServerError, deleteResponse.StatusCode);
    }
}