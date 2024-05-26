using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TelecomManagement.Data;
using TelecomManagement.Domain;
using TelecomManagement.Services;

namespace TelecomManagement.Services.Handler
{
    public class RegistrationHandler
    {
        private readonly RegistrationService _registrationService;
        private readonly UserService _userService;
        
        public RegistrationHandler(RegistrationService registrationService, UserService userService)
        {
            _registrationService = registrationService;
            _userService = userService;
        }

        public bool ValidatePassword(string password)
        {
            // Verifică dacă parola are cel puțin 6 caractere
            if (password.Length < 6)
            {
                return false;
            }

            // Verifică dacă parola conține cel puțin un număr
            if (!Regex.IsMatch(password, @"\d"))
            {
                return false;
            }

            // Verifică dacă parola conține cel puțin un simbol
            if (!Regex.IsMatch(password, @"[^\w\d]"))
            {
                return false;
            }

            // Verifică dacă parola conține cel puțin o literă majusculă
            if (!Regex.IsMatch(password, @"[A-Z]"))
            {
                return false;
            }

            return true;
        }


        public void HandleRegistration()
        {
            
            Console.WriteLine("Introduceti un nume de utilizator:");
            string username = Console.ReadLine();

            string password;
            do
            {
                Console.WriteLine("Introduceti o parola (minim 6 caractere, 1 numar, 1 simbol, 1 litera mare):");
                password = Console.ReadLine();
            } while (!ValidatePassword(password));

            Console.WriteLine("Introduceti numele:");
            string nume = Console.ReadLine();

            Console.WriteLine("Introduceti prenumele:");
            string prenume = Console.ReadLine();

            Console.WriteLine("Introduceti emailul:");
            string email = Console.ReadLine();

            Console.WriteLine("Introduceti telefonul:");
            string telefon = Console.ReadLine();

            Console.WriteLine("Introduceti CNP:");
            string cnp = Console.ReadLine();

            _registrationService.Register(username, password, nume, prenume, email, telefon, cnp);
            Console.WriteLine("Inregistrare reusita.");
        }

        public int HandleLogin()
        {
            Console.WriteLine("Introduceti numele de utilizator:");
            string username = Console.ReadLine();

            Console.WriteLine("Introduceti parola:");
            string password = Console.ReadLine();

            var user = _userService.Login(username, password);
            if (user != null)
            {
                Console.WriteLine("Autentificare reusita.");
                return user.Id;
            }
            else
            {
                Console.WriteLine("Autentificare esuata. Verificati numele de utilizator si parola.");
                return -1;
            }
        }

    }
}
