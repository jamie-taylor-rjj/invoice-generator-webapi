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
            return clients.Select(_clientNameViewModelMapper.Convert).ToList();
        }

        public int AddClient(ClientViewModel viewModel)
        {

            var client = _clientViewModelMapper.Convert(viewModel);

            return _clientRepository.Add(client);
        }

        // TODO move this to the repository, as it is a data access thing
        public PagedResponse<ClientViewModel> GetPage(int pageNumber, int pageSize = 10)
        {
            var pageNumberToUse = pageNumber < 1
                ? 1
                : pageNumber;
            
            var clients = _clientRepository.GetAll().AsQueryable();

            var totalCount = clients.Count();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var page = clients
                .OrderBy(c => c.ClientId)
                .Skip((pageNumberToUse - 1) * pageSize)
                .Take(pageSize);

            return new PagedResponse<ClientViewModel>
            {
                Data = page.Select(_clientViewModelMapper.Convert).ToList(),
                PageNumber = pageNumber,
                PageSize = page.Count(),
                TotalPages = totalPages,
                TotalRecords = totalCount
            };
        }

        public ClientViewModel GetById(Guid Id)
        {
            var client = _clientRepository.GetAll().FirstOrDefault(c => Guid.Equals(c.ClientId, Id));
            if (client == null)
            {
                throw new ArgumentException($"Could not find Client with provided Id {Id}");
            }
            
            return _clientViewModelMapper.Convert(client);
        }
    }
}
