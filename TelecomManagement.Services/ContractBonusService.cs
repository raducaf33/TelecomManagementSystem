using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomManagement.Data;
using TelecomManagement.Data.Interfaces;
using TelecomManagement.Domain;
using TelecomManagement.Services.Base;

namespace TelecomManagement.Services
{
    public class ContractBonusService : ServiceBase<ContractBonus>
    {

        private readonly ContractBonusRepository _contractBonusRepository;
        /// <summary>
        /// Initializes a new instance of the <see cref="StockService"/> class.
        /// </summary>
        /// <param name="stockRepository">The stock repository.</param>
        public ContractBonusService(ContractBonusRepository contractBonusRepository) :
            base(contractBonusRepository)
        
        
        {
            _contractBonusRepository = contractBonusRepository;
        }

    


}
}
