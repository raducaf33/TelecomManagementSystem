using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomManagement.Domain;
using TelecomManagement.Data;

namespace TelecomManagement.Services
{
    public class ClientService
    {
        private readonly ClientRepository _clientRepository;
        private readonly TelecomContext _telecomContext;

        public ClientService(ClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }





    }
}

