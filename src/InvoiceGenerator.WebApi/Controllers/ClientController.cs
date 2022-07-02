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
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /
    ///
    /// </remarks>
    /// <response code="200">a list of clients where found and retrieved as an array of ClientViewModel</response>
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [HttpGet("/", Name = "GetClientList")]
    public ActionResult<IEnumerable<ClientViewModel>> Get()
    {
        return new OkObjectResult(_clientService.GetClients().ToList());
    }

    /// <summary>
    /// Used to get an UNPAGED list of all client names (with their IDs) known to the system.
    /// </summary>
    /// <returns>
    /// An <see cref="IEnumerable{T}" /> where T is a <see cref="ClientNameViewModel"/>
    /// </returns>
        /// <remarks>
    /// Sample request:
    ///
    ///     GET /Names
    ///
    /// </remarks>
    /// <response code="200">a list of clients where found and retrieved as an array of ClientNameViewModel</response>
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [HttpGet("/Names", Name="GetClientNames")]
    public ActionResult<IEnumerable<ClientNameViewModel>> GetNames()
    {
        return new OkObjectResult(_clientService.GetClientNames());
    }

    /// <summary>
    /// Used to get a Book record by its Id
    /// </summary>
    /// <param name="id" example="3fa85f64-5717-4562-b3fc-2c963f66afa6">The Id of the client to get</param>
    /// <returns>A single ClientViewModel representing the client for the given ID</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /1
    ///
    /// </remarks>
    /// <response code="200">The requested client as found and returned</response>
    /// <response code="404">The requested client could not be found</response>
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [HttpGet("/{id}", Name="GetClientById")]
    public IActionResult GetClientById(Guid id)
    {
        var client = _clientService.GetById(id);
        if (client == null)
        {
            return new NotFoundResult();
        }
        return new OkObjectResult(client);
    }

    /// <summary>
    /// Used to create a new Client entry in the database
    /// </summary>
    /// <param name="viewModel">All the data required to create a new client</param>
    /// <returns>
    /// A new instance of the <see cref="ClientViewModel" /> with a reference to how to get the new data
    /// </returns>
    /// <response code="201">The new client record was created</response>
    /// <response code="500">The new client could not be created</response>
    [ProducesResponseType(typeof(ClientViewModel), (int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [HttpPost("/", Name="CreateNewClient")]
    public ActionResult NewClient(ClientCreationModel viewModel)
    {
        var newClient = _clientService.AddClient(viewModel);
        return CreatedAtAction(nameof(GetClientById), new { id = newClient.ClientId }, newClient);
    }

    /// <summary>
    /// Used to get a PAGED list of ClientViewModel instances, using the pageNumber and pageSize parameters
    /// as filters for the paged list
    /// </summary>
    /// <param name="pageNumber" example="1">The page number requested</param>
    /// <param name="pageSize" example="10">The number of items to return per page</param>
    /// <returns>
    /// A new instance of the <see cref="PagedResponse{T}"/> where T is a <see cref="ClientViewModel" />
    /// with the requested number of items (if available) and data about how many pages are available
    /// </returns>
    /// <response code="200">The pages list of ClientViewModels</response>
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [HttpPost("/Page", Name="GetPageOfClients")]
    public PagedResponse<ClientViewModel> GetPage(int pageNumber, int pageSize = 10)
    {
       return _clientService.GetPage(pageNumber, pageSize);
    }

}
