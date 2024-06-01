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
{
    public class ContractBonus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ContractId { get; set; }

        public int BonusId { get; set; }

        public DateTime DataIncheiere { get; set; }

        public DateTime DataExpirare{ get; set; }

        [ForeignKey("ContractId")]
        public Contract Contract { get; set; }

        [ForeignKey("BonusId")]
        public Bonus Bonus { get; set; }
    }
}
