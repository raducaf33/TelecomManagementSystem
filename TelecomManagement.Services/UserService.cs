using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomManagement.Data;
using TelecomManagement.Domain;

namespace TelecomManagement.Services
{
    public  class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User Login(string username, string password)
        {
            // Găsirea utilizatorului în baza de date
            var user = _userRepository.GetRecords().FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                user.LastLoggedIn = DateTime.Now;
                _userRepository.Update(user);
            }

            return user;
        }
    }
}
