using InvoiceGenerator.BusinessLogic;
using InvoiceGenerator.ViewModels;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet("/", Name = "GetClientList")]
    public IEnumerable<ClientViewModel> Get()
    {
        return _clientService.GetClients().ToList();
    }

    [HttpGet("/Names", Name="GetClientNames")]
    public IEnumerable<ClientNameViewModel> GetNames()
    {
        return _clientService.GetClientNames();
    }

    [HttpPost("/")]
    public int NewClient(ClientViewModel viewModel)
    {
        return _clientService.AddClient(viewModel);
    }
}
