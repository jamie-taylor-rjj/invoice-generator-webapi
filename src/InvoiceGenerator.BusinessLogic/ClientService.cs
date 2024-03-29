﻿using InvoiceGenerator.Domain;
using InvoiceGenerator.Domain.Models;
using InvoiceGenerator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceGenerator.BusinessLogic
{
    public class ClientService : IClientService
    {
        private readonly IRepository<Client> _clientRepository;
        private readonly IMapper<ClientViewModel, Client> _clientViewModelMapper;
        private readonly IMapper<ClientNameViewModel, Client> _clientNameViewModelMapper;
        private readonly IMapper<ClientCreationModel, Client> _clientCreationViewModelMapper;

        public ClientService(IRepository<Client> clientRepository,
                IMapper<ClientViewModel, Client> clientViewModelMapper,
                IMapper<ClientNameViewModel, Client> clientNameViewModelMapper,
                IMapper<ClientCreationModel, Client> clientCreationViewModelMapper)
        {
            _clientRepository = clientRepository;
            _clientViewModelMapper = clientViewModelMapper;
            _clientNameViewModelMapper = clientNameViewModelMapper;
            _clientCreationViewModelMapper = clientCreationViewModelMapper;
        }

        public async Task<List<ClientViewModel>> GetClients()
        {
            var clients = await _clientRepository.GetAll();
            return clients.Select(_clientViewModelMapper.Convert).ToList();
        }

        public async Task<List<ClientNameViewModel>> GetClientNames()
        {
            var clients = await _clientRepository.GetAll();
            return clients.Select(_clientNameViewModelMapper.Convert).ToList();
        }

        public async Task<ClientViewModel> AddClient(ClientCreationModel viewModel)
        {
            var client = _clientCreationViewModelMapper.Convert(viewModel);

            await _clientRepository.Add(client);
            return _clientViewModelMapper.Convert(client);
        }
        
        public async Task<ClientViewModel?> GetById(Guid id)
        {
            var client = (await _clientRepository.GetAll()).FirstOrDefault(c => Equals(c.ClientId, id));
            if (client == null)
            {
                return null;
            }
            
            return _clientViewModelMapper.Convert(client);
        }

        // TODO move this to the repository, as it is a data access thing
        public async Task<PagedResponse<ClientViewModel>> GetPage(int pageNumber, int pageSize = 10)
        {
            var pageNumberToUse = pageNumber < 1
                ? 1
                : pageNumber;
            
            var clients = (await _clientRepository.GetAll()).AsQueryable();

            var totalCount = clients.Count();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var page = clients
                .OrderBy(c => c.ClientId)
                .Skip((pageNumberToUse - 1) * pageSize)
                .Take(pageSize);

            return new PagedResponse<ClientViewModel>
            {
                Data = page.AsEnumerable().Select(_clientViewModelMapper.Convert).ToList(),
                PageNumber = pageNumber,
                PageSize = page.Count(),
                TotalPages = totalPages,
                TotalRecords = totalCount
            };
        }

        public async Task<bool> DeleteById(Guid id)
        {
            var client = (await _clientRepository.GetAll()).FirstOrDefault(c => Guid.Equals(c.ClientId, id));
            if (client == null) return false;
            await _clientRepository.Delete(client);
            return true;
        }
    }
}
