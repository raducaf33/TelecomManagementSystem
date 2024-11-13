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
using TelecomManagement.Services.Base;

namespace TelecomManagement.Services
{

    /// <summary>
    /// ClientService Class which implements ServiceBase Class.
    /// </summary>
    public class ClientService : ServiceBase<Client>
    {


        /// <summary>
        /// Initializes a new instance of the ClientService class
        /// </summary>
        public ClientService(IRepositoryBase<Client> clientRepository) :
            base(clientRepository)
        {
        }


    }
}

