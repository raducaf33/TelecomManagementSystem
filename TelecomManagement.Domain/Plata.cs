// <copyright file="Plata.cs" company="Transilvania University Of Brasov">
// Fintineru Raduca-Maria
// </copyright>
// <summary> Defines the Plata class. </summary>

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
    public class Plata
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id{ get; set; } // ID-ul plății
        /// <summary>
        /// Gets or sets the Contract Id.
        /// </summary>
        [Required(ErrorMessage = "ContractID-ul este obligatoriu.")]
        public int ContractId { get; set; } // ID-ul contractului asociat

        [ForeignKey("ContractId")]
        public Contract Contract { get; set; }

        /// <summary>
        /// Gets or sets the state of EstePlatita.
        /// </summary>

        [Required(ErrorMessage = "Starea plății este obligatorie.")]
        public bool EstePlatita { get; set; } // Starea plății

        /// <summary>
        /// Gets or sets the Suma platii.
        /// </summary>

        [Required(ErrorMessage = "Suma plății este obligatorie.")]
        [Range(0, double.MaxValue, ErrorMessage = "Suma plății trebuie să fie mai mare sau egală cu 0.")]
        public decimal SumaPlata { get; set; } // Suma totală de plată

        /// <summary>
        /// Gets or sets the Data Plata.
        /// </summary>

        [Required(ErrorMessage = "Data plății este obligatorie.")]
        public DateTime DataPlata { get; set; } // Data efectuării plății
    }
}
