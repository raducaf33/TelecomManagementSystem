// <copyright file="UserRepository.cs" company="Transilvania University Of Brasov">
// Fintineru Raduca-Maria
// </copyright>
// <summary> Defines the User Repository class. </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomManagement.Domain;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;

namespace TelecomManagement.Data
{
    /// <summary>
    /// UserRepository class.
    /// </summary>
    public class UserRepository : RepositoryBase<User>
    {
        /// <summary>
        /// Initializes a new instance of the UserRepository class.
        /// </summary>
        public UserRepository(TelecomContext context) : base(context)
        {
            
        }

        
    }
}
