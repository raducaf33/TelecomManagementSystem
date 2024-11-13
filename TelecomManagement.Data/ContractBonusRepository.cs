// <copyright file="ContractBonusRepository.cs" company="Transilvania University Of Brasov">
// Fintineru Raduca-Maria
// </copyright>
// <summary> Defines the ContractBonus Repository class. </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomManagement.Domain;

namespace TelecomManagement.Data
{
    /// <summary>
    /// ContractBonusRepository class.
    /// </summary>
    public class ContractBonusRepository : RepositoryBase<ContractBonus>
    {
        /// <summary>
        /// Initializes a new instance of the ContractBonusRepository class.
        /// </summary>
        public ContractBonusRepository(TelecomContext context) : base(context)
        {
        }
    }
}
