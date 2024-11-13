// <copyright file="User.cs" company="Transilvania University Of Brasov">
// Fintineru Raduca-Maria
// </copyright>
// <summary> Defines the User class. </summary>

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelecomManagement.Domain
{
    /// <summary>
    /// Plata class.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the Username.
        /// </summary>
        public String Username { get; set; }

        /// <summary>
        /// Gets or sets the Password.
        /// </summary>
        public String Password { get; set; }

        /// <summary>
        /// Gets or sets the LastLoggedIn Information of the User.
        /// </summary>
        public DateTime LastLoggedIn { get; set; }
        
      


    }
}
