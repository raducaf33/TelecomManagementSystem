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
    public class ContractBonusRepository : RepositoryBase<ContractBonus>
    {
        public ContractBonusRepository(TelecomContext context) : base(context)
        {
        }
    }
}
