// <copyright file="ClientService.cs" company="Transilvania University Of Brasov">
// Fintineru Raduca-Maria
// </copyright>
// <summary> Defines the Client Service class. </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomManagement.Domain;
using TelecomManagement.Data;
using TelecomManagement.Data.Interfaces;
using TelecomManagement.Services.Base;

namespace TelecomManagement.Services
{
    public class ClientService : ServiceBase<Client>
    {


        /// <summary>
        /// Initializes a new instance of the <see cref="StockService"/> class.
        /// </summary>
        /// <param name="stockRepository">The stock repository.</param>
        public ClientService(IRepositoryBase<Client> clientRepository) :
            base(clientRepository)
        {
        }


    }
}

