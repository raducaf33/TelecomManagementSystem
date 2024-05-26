using TelecomManagement.Data;
using TelecomManagement.Domain;
using TelecomManagement.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using TelecomManagement.Services.Handler;
using TelecomManagement.Services.Handlers;

namespace TelecomManagementSystem
{

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                using (var context = new TelecomContext())
                {
                   
                    // Initialize repositories
                     var userRepository = new UserRepository(context);
                     var clientRepository = new ClientRepository(context);
                     var contractRepository = new ContractRepository(context);
                     var abonamentRepository = new AbonamentRepository(context);

                     // Initialize services
                     var registrationService = new RegistrationService(userRepository, clientRepository);
                     var userService = new UserService(userRepository);
                     var contractService = new ContractService(contractRepository, abonamentRepository);

                     // Initialize handlers
                     var registrationHandler = new RegistrationHandler(registrationService, userService);
                     var postLoginHandler = new PostLoginHandler(contractService);
                     var menuHandler = new MenuHandler(registrationHandler, postLoginHandler);

                     // Show main menu
                     menuHandler.ShowMenu();


                   /*// Creare și inserare abonament 1
                    var abonament1 = new Abonament
                    {
                        Nume = "Abonament Standard",
                        Pret = 50.0m,
                        MinuteIncluse = 300,
                        SMSuriIncluse = 100,
                        TraficDateInclus = 10.0m
                    };
                    
                    abonamentRepository.Insert(abonament1);

                    // Creare și inserare abonament 2
                    var abonament2 = new Abonament
                    {
                        Nume = "Abonament Premium",
                        Pret = 100.0m,
                        MinuteIncluse = 600,
                        SMSuriIncluse = 200,
                        TraficDateInclus = 20.0m
                    };
                    abonamentRepository.Insert(abonament2);

                    // Creare și inserare abonament 3
                    var abonament3 = new Abonament
                    {
                        Nume = "Abonament Super Premium",
                        Pret = 150.0m,
                        MinuteIncluse = 1000,
                        SMSuriIncluse = 300,
                        TraficDateInclus = 30.0m
                    };
                    abonamentRepository.Insert(abonament3);

                    Console.WriteLine("Cele trei abonamente au fost adăugate cu succes în baza de date.");*/
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("A apărut o eroare .");
                Console.WriteLine($"Detalii eroare: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Eroare internă: {ex.InnerException.Message}");
                }
            }
        }
    }


}


