// <copyright file="Factura.cs" company="Transilvania University Of Brasov">
// Fintineru Raduca-Maria
// </copyright>
// <summary> Defines the Factura class. </summary>

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
    /// Factura class.
    /// </summary>
    public class Factura
    {    /// <summary>
         /// Gets or sets the identifier.
         /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // ID-ul plății
        /// <summary>
        /// Gets or sets the Contract ID.
        /// </summary>
        [Required(ErrorMessage = "ContractID-ul este obligatoriu.")]
        public int ContractId { get; set; } // ID-ul contractului asociat


        [ForeignKey("ContractId")]
        public Contract Contract { get; set; }

        /// <summary>
        /// Gets or sets the Suma Totala de Plata.
        /// </summary>

        [Required(ErrorMessage = "Suma plății este obligatorie.")]
        [Range(0, double.MaxValue, ErrorMessage = "Suma plății trebuie să fie mai mare sau egală cu 0.")]
        public decimal SumaTotalaPlata { get; set; } // Suma totală de plată

        /// <summary>
        /// Gets or sets the Data Emitere Contract.
        /// </summary>

        [Required(ErrorMessage = "Data Emitere este obligatorie.")]
        public DateTime DataEmitere { get; set; } // Data efectuării plății

        /// <summary>
        /// Gets or sets the Data Scadenta.
        /// </summary>

        [Required(ErrorMessage = "Data Scadenta este obligatorie.")]
        public DateTime DataScadenta{ get; set; } // Data efectuării plății
    }
}

