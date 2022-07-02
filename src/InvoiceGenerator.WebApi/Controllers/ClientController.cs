using Microsoft.AspNetCore.Mvc;
using InvoiceGenerator.BusinessLogic;
using InvoiceGenerator.ViewModels;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace InvoiceGenerator.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;
    private readonly ILogger<ClientController> _logger;

    public ClientController(IClientService clientService, ILogger<ClientController> logger)
    {
        _clientService = clientService;
        _logger = logger;
    }

    /// <summary>
    /// Used to get an UNPAGED list of all clients in the system.
    /// </summary>
    /// <returns>
    /// An <see cref="IEnumerable{T}" /> where T is a <see cref="ClientViewModel"/>
    /// </returns>
    [SwaggerResponse((int)HttpStatusCode.OK, "The list of all Clients")]
    [HttpGet("/", Name = "GetClientList")]
    public IEnumerable<ClientViewModel> Get()
    {
        return _clientService.GetClients().ToList();
    }

    /// <summary>
    /// Used to get an UNPAGED list of all client names (with their IDs) known to the system.
    /// </summary>
    /// <returns>
    /// An <see cref="IEnumerable{T}" /> where T is a <see cref="ClientNameViewModel"/>
    /// </returns>
    [HttpGet("/Names", Name="GetClientNames")]
    [SwaggerResponse((int)HttpStatusCode.OK, "The list of all Client Name and Id pairs")]
    public IEnumerable<ClientNameViewModel> GetNames()
    {
        return _clientService.GetClientNames();
    }

    public int NewClient(ClientViewModel viewModel)
    {
        return _clientService.AddClient(viewModel);
    }

    [HttpPost("/Page", Name="GetPageOfClients")]
    public PagedResponse<ClientViewModel> GetPage(int pageNumber, int pageSize = 10)
    {
       return _clientService.GetPage(pageNumber, pageSize);
    }

}
