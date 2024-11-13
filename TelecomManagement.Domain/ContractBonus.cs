// <copyright file="ContractBonus.cs" company="Transilvania University Of Brasov">
// Fintineru Raduca-Maria
// </copyright>
// <summary> Defines the ContractBonus class. </summary>

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelecomManagement.Domain
{   /// <summary>
    /// ContractBonus class.
    /// </summary>
    public class ContractBonus
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Contract Id.
        /// </summary>

        public int ContractId { get; set; }

        [ForeignKey("ContractId")]
        public Contract Contract { get; set; }

        /// <summary>
        /// Gets or sets the Bonus Id.
        /// </summary>

        public int BonusId { get; set; }

        [ForeignKey("BonusId")]
        public Bonus Bonus { get; set; }

        /// <summary>
        /// Gets or sets the Data Inchiere Adaugare Contract Bonus.
        /// </summary>

        public DateTime DataIncheiere { get; set; }

        /// <summary>
        /// Gets or sets the Data Expirare Contract Bonus
        /// </summary>

        public DateTime DataExpirare{ get; set; }

       

       
    }
}
