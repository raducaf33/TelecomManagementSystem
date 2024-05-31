using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelecomManagement.Domain
{
    public class Factura
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // ID-ul plății

        [Required(ErrorMessage = "ContractID-ul este obligatoriu.")]
        public int ContractId { get; set; } // ID-ul contractului asociat

        [ForeignKey("ContractId")]
        public Contract Contract { get; set; }

        [Required(ErrorMessage = "Suma plății este obligatorie.")]
        [Range(0, double.MaxValue, ErrorMessage = "Suma plății trebuie să fie mai mare sau egală cu 0.")]
        public decimal SumaTotalaPlata { get; set; } // Suma totală de plată

        [Required(ErrorMessage = "Data Emitere este obligatorie.")]
        public DateTime DataEmitere { get; set; } // Data efectuării plății

        [Required(ErrorMessage = "Data Scadenta este obligatorie.")]
        public DateTime DataScadenta{ get; set; } // Data efectuării plății
    }
}

