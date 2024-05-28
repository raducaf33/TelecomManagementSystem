using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomManagement.Data;
using TelecomManagement.Domain;
using TelecomManagement.Services;
using TelecomManagement.Services.Handler;

namespace TelecomManagement.Services.Handlers
{
    public class MenuHandler
    {
        private readonly RegistrationHandler _registrationHandler;
        private readonly PostLoginHandler _postLoginHandler;
        

        public MenuHandler(RegistrationHandler registrationHandler, PostLoginHandler postLoginHandler)
        {
            _registrationHandler = registrationHandler;
            _postLoginHandler = postLoginHandler;
        }

        public void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("Selectati o optiune:");
                Console.WriteLine("1. Inregistrare");
                Console.WriteLine("2. Logare");
                Console.WriteLine("3. Iesire");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        _registrationHandler.HandleRegistration();
                        break;
                    case "2":
                        int userId = _registrationHandler.HandleLogin();
                        if (userId != -1)
                        {
                            _postLoginHandler.ShowPostLoginMenu(userId);
                        }
                        break;
                    case "3":
                        Console.WriteLine("Bye!");
                        return;
                    default:
                        Console.WriteLine("Opțiune invalidă. Vă rugăm să încercați din nou.");
                        break;
                }
            }
        }
    }
}

