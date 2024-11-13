// <copyright file="ClientRepository.cs" company="Transilvania University Of Brasov">
// Fintineru Raduca-Maria
// </copyright>
// <summary> Defines the Client Repository class. </summary>

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomManagement.Domain;

using TelecomManagement.Data;
namespace TelecomManagement.Data
{
    /// <summary>
    /// ClientRepository class.
    /// </summary>
    public class ClientRepository : RepositoryBase<Client>
    {
        /// <summary>
        /// Initializes a new instance of the ClientRepository class.
        /// </summary>

        public ClientRepository(TelecomContext context) : base(context)
        {

        }

    }
}



