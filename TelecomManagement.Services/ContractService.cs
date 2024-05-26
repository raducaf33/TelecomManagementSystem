using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomManagement.Domain;
using TelecomManagement.Data;

namespace TelecomManagement.Services
{
    public class ContractService
    {
        private readonly ContractRepository _contractRepository;
        private readonly AbonamentRepository _abonamentRepository;

        public ContractService(ContractRepository contractRepository, AbonamentRepository abonamentRepository)
        {
            _contractRepository = contractRepository;
            _abonamentRepository = abonamentRepository;
        }

        public void SignContract(int clientId, int abonamentId)
        {
            var contract = new Contract
            {
                ClientId = clientId,
                AbonamentId = abonamentId,
                DataIncheiere = DateTime.Now,
                DataExpirare = DateTime.Now.AddMonths(24) // Example duration
            };

            _contractRepository.Insert(contract);
        }

        public List<Abonament> GetAllAbonamente()
        {
            return _abonamentRepository.GetAllAbonamente();
        }

        public Abonament GetAbonamentDetails(string abonamentName)
        {
            return _abonamentRepository.GetAllAbonamente()
                .FirstOrDefault(a => a.Nume.Equals(abonamentName, StringComparison.OrdinalIgnoreCase));
        }


    }
   
}
