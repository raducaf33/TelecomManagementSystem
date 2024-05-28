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
