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

        private readonly BonusRepository _bonusRepository;
        private readonly ContractBonusRepository _contractBonusRepository;
        private readonly UserRepository _userRepository;
        private readonly ClientRepository _clientRepository;

        public ContractService(ContractRepository contractRepository, AbonamentRepository abonamentRepository, BonusRepository bonusRepository, ContractBonusRepository contractBonusRepository, UserRepository userRepository, ClientRepository clientRepository)
        {
            _contractRepository = contractRepository;
            _abonamentRepository = abonamentRepository;
            _bonusRepository = bonusRepository;
            _contractBonusRepository = contractBonusRepository;
            _userRepository = userRepository;
            _clientRepository = clientRepository;
        }

        public void AddBonusToContract(int contractId, int bonusId)
        {
            var contract = _contractRepository.GetById(contractId);
            if (contract != null && contract.DataExpirare > DateTime.Now)
            {
                var contractBonus = new ContractBonus
                {
                    ContractId = contractId,
                    BonusId = bonusId,
                    DataIncheiere = DateTime.Now,
                    DataExpirare = contract.DataExpirare
                };
                _contractBonusRepository.Insert(contractBonus);
                Console.WriteLine("Bonusul a fost adăugat cu succes.");
            }
            else
            {
                Console.WriteLine("Contractul nu este activ sau nu există.");
            }
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

        public List<Contract> GetContractsByClientId(int clientId)
        {
            return _contractRepository.GetContractsByClientId(clientId);
        }

        public List<Bonus> GetAllBonuses()
        {
            return _bonusRepository.GetAll();
        }

        public List<Contract> GetContractsByUserId(int userId)
        {
            // Căutăm clientul asociat utilizatorului
            var client = _clientRepository.GetClientByUserId(userId);
            if (client == null)
            {
                // Dacă nu găsim clientul, returnăm o listă goală de contracte
                return new List<Contract>();
            }

            // Dacă găsim clientul, căutăm contractele asociate clientului
            return _contractRepository.GetContractsByClientId(client.Id);
        }

        

        public int? GetClientByUserId(int userId)
        {
            var client = _clientRepository.GetClientByUserId(userId);
            return client?.Id;
        }






    }
   
}
