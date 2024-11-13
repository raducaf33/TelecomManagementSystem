// <copyright file="ContractRepository.cs" company="Transilvania University Of Brasov">
// Fintineru Raduca-Maria
// </copyright>
// <summary> Defines the Contract Repository class. </summary>

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TelecomManagement.Domain;
using System.Threading.Tasks;


namespace TelecomManagement.Data
{
    /// <summary>
    /// ContractRepository class.
    /// </summary>
    public class ContractRepository : RepositoryBase<Contract>

    {
        /// <summary>
        /// Initializes a new instance of the ContractRepository class.
        /// </summary>

        public ContractRepository(TelecomContext context) : base(context)
      {
          
        }

        

    }
}
