// <copyright file="Bonus.cs" company="Transilvania University Of Brasov">
// Fintineru Raduca-Maria
// </copyright>
// <summary> Defines the Bonus class. </summary>

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
    /// Bonus class.
    /// </summary>
    public class Bonus
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Nume.
        /// </summary>

        public string Nume { get; set; }

        /// <summary>
        /// Gets or sets the MinuteBonus.
        /// </summary>

        public int? MinuteBonus { get; set; }

        /// <summary>
        /// Gets or sets the SMSuriBonus.
        /// </summary>

        public int? SMSuriBonus { get; set; }

        /// <summary>
        /// Gets or sets the TraficDateBonus.
        /// </summary>

        public decimal? TraficDateBonus { get; set; }
    }
}
