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
using TelecomManagement.Data.Interfaces;

namespace TelecomManagement.Data
{
    public class ContractRepository : RepositoryBase<Contract>

    {
    

        public ContractRepository(TelecomContext context) : base(context)
      {
          
        }

        

    }
}
