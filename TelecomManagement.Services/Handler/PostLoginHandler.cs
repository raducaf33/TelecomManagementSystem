using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelecomManagement.Services.Handler
{
    public class PostLoginHandler
    {
        private readonly ContractService _contractService; // Assumes you have a ContractService class

        public PostLoginHandler(ContractService contractService)
        {
            _contractService = contractService;
        }

        private int _loggedInUserId;

        

        public void ShowPostLoginMenu(int userId)
        {
            _loggedInUserId = userId;
            while (true)
            {
                Console.WriteLine("Selectati o optiune:");
                Console.WriteLine("1. Afisati abonamentele");
                Console.WriteLine("2. Iesire");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowAbonamente();
                        break;
                    case "2":
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
    }
}
 