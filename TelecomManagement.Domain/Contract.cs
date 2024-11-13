// <copyright file="Contract.cs" company="Transilvania University Of Brasov">
// Fintineru Raduca-Maria
// </copyright>
// <summary> Defines the Contract class. </summary>

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelecomManagement.Domain
{
    /// <summary>
    /// Contract class.
    /// </summary>
    public class Contract
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the DataInchiere Contract.
        /// </summary>

        public DateTime DataIncheiere { get; set; }
        /// <summary>
        /// Gets or sets the Data Expirare Contract.
        /// </summary>
        public DateTime DataExpirare { get; set; }

        /// <summary>
        /// Gets or sets the Abonament Id.
        /// Foregin Key for Abonament
        /// </summary>
        public int AbonamentId { get; set; }

        [ForeignKey("AbonamentId")]
        public Abonament Abonament { get; set; }

        /// <summary>
        /// Gets or sets the Client Id.
        /// Foreign Key for Client
        /// </summary>
        public int ClientId { get; set; }

        [ForeignKey("ClientId")]
        public Client Client { get; set; }


        //public bool EsteExpirat => DataExpirare < DateTime.Today;

        

        

    }
}
