using Microsoft.AspNetCore.Mvc;
using InvoiceGenerator.BusinessLogic;
using InvoiceGenerator.ViewModels;
using System.Net;

namespace InvoiceGenerator.WebApi.Controllers;

[ApiController]
[Route("api/")]
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
    [HttpGet("[controller]/", Name = "GetClientList")]
    public async Task<ActionResult<IEnumerable<ClientViewModel>>> Get()
    {
        return new OkObjectResult((await _clientService.GetClients()).ToList());
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
    [HttpGet("[controller]/names", Name="GetClientNames")]
    public async Task<ActionResult<IEnumerable<ClientNameViewModel>>> GetNames()
    {
        return new OkObjectResult(await _clientService.GetClientNames());
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
    [HttpGet("[controller]/{id}", Name="GetClientById")]
    public async Task<IActionResult> GetClientById(Guid id)
    {
        var client = await _clientService.GetById(id);
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
    [HttpPost("[controller]/", Name="CreateNewClient")]
    public async Task<ActionResult> NewClient(ClientCreationModel viewModel)
    {
        var newClient = await _clientService.AddClient(viewModel);
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
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [HttpGet("[controller]/page", Name="GetPageOfClients")]
    public async Task<PagedResponse<ClientViewModel>> GetPage(int pageNumber, int pageSize = 10)
    {
       return await _clientService.GetPage(pageNumber, pageSize);
    }

    /// <summary>
    /// Used to delete a single Client from the system. THIS IS IRREVOCABLE
    /// </summary>
    /// <param name="id" example="3fa85f64-5717-4562-b3fc-2c963f66afa6"></param>
    /// <returns></returns>
    /// <response code="200">A client with the supplied ID was found and deleted</response>
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [HttpDelete("[controller]/", Name="DeleteClientById")]
    public async Task<ActionResult> DeleteById(Guid id)
    {
        await _clientService.DeleteById(id);
        return new OkResult();
    }
}
