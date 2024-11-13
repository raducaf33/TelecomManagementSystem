// <copyright file="RegistrationService.cs" company="Transilvania University Of Brasov">
// Fintineru Raduca-Maria
// </copyright>
// <summary> Defines the User Registration Service class. </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomManagement.Data;
using TelecomManagement.Domain;

namespace TelecomManagement.Services
{

    /// <summary>
    /// ResgitrationService Class.
    /// </summary>
    public class RegistrationService
    {
        private readonly UserRepository _userRepository;
        private readonly ClientRepository _clientRepository;

        /// <summary>
        /// Initializes a new instance of the RegistrationService class
        /// </summary>
        public RegistrationService(UserRepository userRepository, ClientRepository clientRepository)
        {
            _userRepository = userRepository;
            _clientRepository = clientRepository;
        }
        /// <summary>
        /// Checks for the Registration process
        /// </summary>
        public void Register(string username, string password, string Nume, string Prenume, string Email, string Telefon, string CNP)
        {
            /// <summary>
            /// Nume should start with Uppsercase
            /// </summary>
            if (char.IsLower(Nume[0]))
            {
                throw new ArgumentException("Numele trebuie să înceapă cu literă mare.");
            }

            /// <summary>
            /// Prenume should start with Uppsercase
            /// </summary>
            if (char.IsLower(Prenume[0]))
            {
                throw new ArgumentException("Prenumele trebuie să înceapă cu literă mare.");
            }

            /// <summary>
            /// Email should contain @ and end with a valid email domain.
            /// </summary>
            if (!Email.Contains("@") || (!Email.EndsWith("gmail.com") && !Email.EndsWith("yahoo.com") && !Email.EndsWith("example.com")))
            {
                throw new ArgumentException("Email-ul trebuie să fie valid și să conțină un domeniu acceptat.");
            }

            /// <summary>
            /// Phone number should be 10 characters and start with 0
            /// </summary>
            if (Telefon.Length != 10 || !Telefon.StartsWith("0"))
            {
                throw new ArgumentException("Numărul de telefon trebuie să aibă 10 caractere și să înceapă cu 0.");
            }

            /// <summary>
            /// CNP should be 13 characters and only digits
            /// </summary>
            if (CNP.Length != 13 || !CNP.All(char.IsDigit))
            {
                throw new ArgumentException("CNP-ul trebuie să aibă 13 cifre.");
            }

            /// <summary>
            /// Create the user
            /// </summary>
            var user = new User
            {
                Username = username,
                Password = password,
                LastLoggedIn = DateTime.Now
            };

            /// <summary>
            /// Add the user to repository
            /// </summary>
            _userRepository.Create(user);

            /// <summary>
            /// Create client associated to user
            /// </summary>
            var client = new Client
            {
                Nume = Nume,
                Prenume = Prenume,
                Email = Email,
                Telefon = Telefon,
                CNP = CNP,
                UserId = user.Id 
            };

            /// <summary>
            /// Add client to repository
            /// </summary>
            _clientRepository.Create(client);
        }

    }
}
