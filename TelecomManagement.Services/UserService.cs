// <copyright file="UserService.cs" company="Transilvania University Of Brasov">
// Fintineru Raduca-Maria
// </copyright>
// <summary> Defines the User Service class. </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomManagement.Data;
using TelecomManagement.Domain;
using TelecomManagement.Services.Base;

namespace TelecomManagement.Services
{


    /// <summary>
    /// UserService Class which implements ServiceBase Class.
    /// </summary>
    public class UserService : ServiceBase<User>
    {


        /// <summary>
        /// Initializes a new instance of the UserService class
        /// </summary>
        public UserService(UserRepository userRepository) :
            base(userRepository)
        {
        }


    }
}
