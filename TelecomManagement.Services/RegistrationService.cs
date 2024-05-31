using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomManagement.Data;
using TelecomManagement.Domain;

namespace TelecomManagement.Services
{
    public  class RegistrationService
    {
        private readonly UserRepository _userRepository;
        private readonly ClientRepository _clientRepository;

        public RegistrationService(UserRepository userRepository, ClientRepository clientRepository)
        {
            _userRepository = userRepository;
            _clientRepository = clientRepository;
        }

        public void Register(string username, string password, string Nume, string Prenume, string Email, string Telefon, string CNP)
        {
            // Validarea numelui
            if (char.IsLower(Nume[0]))
            {
                throw new ArgumentException("Numele trebuie să înceapă cu literă mare.");
            }

            // Validarea prenumelui
            if (char.IsLower(Prenume[0]))
            {
                throw new ArgumentException("Prenumele trebuie să înceapă cu literă mare.");
            }

            // Validarea email-ului
            if (!Email.Contains("@") || (!Email.EndsWith("gmail.com") && !Email.EndsWith("yahoo.com") && !Email.EndsWith("example.com")))
            {
                throw new ArgumentException("Email-ul trebuie să fie valid și să conțină un domeniu acceptat.");
            }

            // Validarea numărului de telefon
            if (Telefon.Length != 10 || !Telefon.StartsWith("0"))
            {
                throw new ArgumentException("Numărul de telefon trebuie să aibă 10 caractere și să înceapă cu 0.");
            }

            // Validarea CNP-ului
            if (CNP.Length != 13 || !CNP.All(char.IsDigit))
            {
                throw new ArgumentException("CNP-ul trebuie să aibă 13 cifre.");
            }

            // Creare utilizator
            var user = new User
            {
                Username = username,
                Password = password,
                LastLoggedIn = DateTime.Now
            };

            // Adăugare utilizator în repository
            _userRepository.Create(user);

            // Creare client asociat utilizatorului
            var client = new Client
            {
                Nume = Nume,
                Prenume = Prenume,
                Email = Email,
                Telefon = Telefon,
                CNP = CNP,
                UserId = user.Id // Asigurarea relației între utilizator și client
            };

            // Adăugare client în repository
            _clientRepository.Create(client);
        }

    }
}
