// <copyright file="ContractBonusService.cs" company="Transilvania University Of Brasov">
// Fintineru Raduca-Maria
// </copyright>
// <summary> Defines the ContractBonus Service class. </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomManagement.Data;
using TelecomManagement.Domain;
using TelecomManagement.Services.Base;

namespace TelecomManagement.Services
{

    /// <summary>
    /// ContractBonusService Class which implements ServiceBase Class.
    /// </summary>
    public class ContractBonusService : ServiceBase<ContractBonus>
    {

        private readonly ContractBonusRepository _contractBonusRepository;
        /// <summary>
        /// Initializes a new instance of the ContractBonusService class
        /// </summary>
        public ContractBonusService(ContractBonusRepository contractBonusRepository) :
            base(contractBonusRepository)
        
        
        {
            _contractBonusRepository = contractBonusRepository;
        }

    


}
}
