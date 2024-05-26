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
            // Creare utilizator
            var user = new User
            {
                Username = username,
                Password = password,
                LastLoggedIn = DateTime.Now
            };

            // Adăugare utilizator în repository
            _userRepository.Insert(user);

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
            _clientRepository.Insert(client);
        }

    }
}
