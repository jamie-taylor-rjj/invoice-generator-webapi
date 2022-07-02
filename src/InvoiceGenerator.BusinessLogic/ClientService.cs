using InvoiceGenerator.Domain;
using InvoiceGenerator.Domain.Models;
using InvoiceGenerator.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace InvoiceGenerator.BusinessLogic
{
    public class ClientService : IClientService
    {
        private readonly IRepository<Client> _clientRepository;
        private readonly IMapper<ClientViewModel, Client> _clientViewModelMapper;
        private readonly IMapper<ClientNameViewModel, Client> _clientNameViewModelMapper;

        public ClientService(IRepository<Client> clientRepository,
                IMapper<ClientViewModel, Client> clientViewModelMapper,
                IMapper<ClientNameViewModel, Client> clientNameViewModelMapper)
        {
            _clientRepository = clientRepository;
            _clientViewModelMapper = clientViewModelMapper;
            _clientNameViewModelMapper = clientNameViewModelMapper;
        }

        public List<ClientViewModel> GetClients()
        {
            var clients = _clientRepository.GetAll();
            return clients.Select(_clientViewModelMapper.Convert).ToList();
        }

        public List<ClientNameViewModel> GetClientNames()
        {
            var clients = _clientRepository.GetAll();
            return clients.Select(_clientNameViewModelMapper.Convert).ToList();;
        }

        public int AddClient(ClientViewModel viewModel)
        {

            var client = _clientViewModelMapper.Convert(viewModel);

            return _clientRepository.Add(client);
        }
    }
}
