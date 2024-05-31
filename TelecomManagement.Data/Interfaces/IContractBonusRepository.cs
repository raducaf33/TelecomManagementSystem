using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomManagement.Domain;

namespace TelecomManagement.Data.Interfaces
{
    public interface IContractBonusRepository : IRepositoryBase<ContractBonus>
    {
        List<Contract> GetContractsByClientId(int clientId);

    }
}
