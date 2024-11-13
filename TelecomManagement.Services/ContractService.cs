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
    /// ContractService Class which implements ServiceBase Class.
    /// </summary>
    public class ContractService : ServiceBase<Contract>
    {

        private readonly ContractRepository _contractRepository;
        /// <summary>
        /// Initializes a new instance of the ContractService class
        /// </summary>
        public ContractService(ContractRepository contractRepository) :
            base(contractRepository)
        {
            _contractRepository = contractRepository;
        }

        /// <summary>
        /// Creates a bool function to check if the contract is expired
        /// </summary>
        public bool EsteContractExpirat(DateTime dataIncheiere)
        {
            var dataExpirare = dataIncheiere.AddMonths(12);
            return dataExpirare < DateTime.Today;
        }

        

    }

}
