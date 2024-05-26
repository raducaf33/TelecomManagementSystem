using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelecomManagement.Domain
{
    public  class Abonament
    {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }

            public string Nume { get; set; }

            public decimal Pret { get; set; }

            public int MinuteIncluse { get; set; }

            public int SMSuriIncluse { get; set; }

            public decimal TraficDateInclus { get; set; }
        }
    
}
