using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelecomManagement.Domain
{
    public class Contract
    {

        // Proprietăți
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
      
        public DateTime DataIncheiere { get; set; }
        public DateTime DataExpirare { get; set; }
        public int AbonamentId { get; set; }
        public int ClientId { get; set; }


      

        public bool EsteExpirat => DataExpirare < DateTime.Today;

        // Foreign key for Abonament

        [ForeignKey("ClientId")]
        public Client Client { get; set; }
        

        [ForeignKey("AbonamentId")]
        public Abonament Abonament { get; set; }

        

    }
}
