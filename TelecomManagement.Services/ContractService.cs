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
    public class ContractService : ServiceBase<Contract>
    {

        private readonly ContractRepository _contractRepository;
        /// <summary>
        /// Initializes a new instance of the <see cref="StockService"/> class.
        /// </summary>
        /// <param name="stockRepository">The stock repository.</param>
        public ContractService(ContractRepository contractRepository) :
            base(contractRepository)
        {
            _contractRepository = contractRepository;
        }
        public bool HasBonuses(int contractId)
        {
            // Verificăm dacă există bonusuri asociate contractului
            return _contractRepository.GetAll().Any(cb => cb.Id == contractId);
        }

        public bool EsteContractExpirat(DateTime dataIncheiere)
        {
            var dataExpirare = dataIncheiere.AddMonths(12);
            return dataExpirare < DateTime.Today;
        }

        

    }

}
