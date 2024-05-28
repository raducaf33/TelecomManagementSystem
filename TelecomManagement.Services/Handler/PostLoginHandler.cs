using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomManagement.Data;
using TelecomManagement.Domain;

namespace TelecomManagement.Services.Handler
{
    public class PostLoginHandler
    {
        private readonly ContractService _contractService;
        // Assumes you have a ContractService class
        private int _loggedInUserId;

        public PostLoginHandler(ContractService contractService)
        {
            _contractService = contractService;
        }

       

        public List<Contract> GetActiveContracts(int clientId)
        {
            var contracts = _contractService.GetContractsByClientId(clientId);
            return contracts.FindAll(c => c.DataIncheiere > DateTime.Now);
        }

        public List<Bonus> GetAllBonuses()
        {
            return _contractService.GetAllBonuses();
        }

        public void AddBonusToContract(int contractId, int bonusId)
        {
            _contractService.AddBonusToContract(contractId, bonusId);
        }

        public void ShowPostLoginMenu(int userId)
        {
            _loggedInUserId = userId;
            while (true)
            {
                Console.WriteLine("Selectati o optiune:");
                Console.WriteLine("1. Afisati abonamentele");
                Console.WriteLine("2. Adauga bonus");
                Console.WriteLine("3. Iesire");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowAbonamente();
                        break;
                    case "2":
                        AddBonus();
                        break;
                    case "3":
                        Console.WriteLine("Deconectare reusita.");
                        return;
                    default:
                        Console.WriteLine("Optiune invalida. Va rugam sa încercati din nou.");
                        break;
                }
            }
        }

        private void ShowAbonamente()
        {
            var abonamente = _contractService.GetAllAbonamente();

            if (!abonamente.Any())
            {
                Console.WriteLine("Nu exista abonamente disponibile.");
                return;
            }

            Console.WriteLine("Abonamente disponibile:");
            foreach (var abonament in abonamente)
            {
                Console.WriteLine($"- {abonament.Nume}");
            }

            Console.WriteLine("Introduceti numele abonamentului pentru a vedea detalii:");
            string abonamentName = Console.ReadLine();

            var abonamentDetails = _contractService.GetAbonamentDetails(abonamentName);

            if (abonamentDetails == null)
            {
                Console.WriteLine("Abonamentul specificat nu există.");
                return;
            }

            Console.WriteLine($"Detalii Abonament:\nNume: {abonamentDetails.Nume}\nPreț: {abonamentDetails.Pret}\nMinuteIncluse: {abonamentDetails.MinuteIncluse}\nSMSuriIncluse: {abonamentDetails.SMSuriIncluse}\n");

            Console.WriteLine("Doriți să semnați contractul pentru acest abonament? (da/nu)");
            string signChoice = Console.ReadLine();

            if (signChoice.Equals("da", StringComparison.OrdinalIgnoreCase))
            {
                _contractService.SignContract(_loggedInUserId, abonamentDetails.Id);
            }
        }

        private void AddBonus()
        {
            int? clientId = _contractService.GetClientByUserId(_loggedInUserId);
            if (clientId == null)
            {
                Console.WriteLine("Clientul nu a fost găsit.");
                return;
            }

            var activeContracts = _contractService.GetContractsByClientId(clientId.Value).FindAll(c => c.DataExpirare > DateTime.Now);

            if (!activeContracts.Any())
            {
                Console.WriteLine("Nu există contracte active pentru acest utilizator.");
                return;
            }

            Console.WriteLine("Selectează un contract:");
            for (int i = 0; i < activeContracts.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Contract ID: {activeContracts[i].Id}, Data Expirare: {activeContracts[i].DataExpirare}");
            }

            var contractChoice = int.Parse(Console.ReadLine()) - 1;
            var selectedContract = activeContracts[contractChoice];

            Console.WriteLine("Selectează un bonus:");
            var bonuses = _contractService.GetAllBonuses();
            for (int i = 0; i < bonuses.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {bonuses[i].Nume} - {bonuses[i].MinuteBonus} minute, {bonuses[i].SMSuriBonus} SMSuri, {bonuses[i].TraficDateBonus} MB");
            }

            var bonusChoice = int.Parse(Console.ReadLine()) - 1;
            var selectedBonus = bonuses[bonusChoice];

            _contractService.AddBonusToContract(selectedContract.Id, selectedBonus.Id);
        }
    }
}
 