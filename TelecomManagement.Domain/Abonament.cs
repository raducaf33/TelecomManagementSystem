// <copyright file="Abonament.cs" company="Transilvania University Of Brasov">
// Fintineru Raduca-Maria
// </copyright>
// <summary> Defines the Abonament class. </summary>

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
    /// Abonament class.
    /// </summary>
    public class Abonament
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        [Required(ErrorMessage = "Numele abonamentului este obligatoriu.")]
        public string Nume { get; set; }
        /// <summary>
        /// Gets or sets the Pret.
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "Pretul  trebuie să fie mai mari sau egale cu 0.")]

        public decimal Pret { get; set; }
        /// <summary>
        /// Gets or sets the Minute.
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "Minutele incluse trebuie să fie mai mari sau egale cu 0.")]

        public int MinuteIncluse { get; set; }
        /// <summary>
        /// Gets or sets the SMS-uri.
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "SMS-urile incluse trebuie să fie mai mult sau egale cu 0.")]

        public int SMSuriIncluse { get; set; }

        /// <summary>
        /// Gets or sets the TrafucDateIncluse.
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "Traficul de date inclus trebuie să fie mai mare sau egal cu 0.")]

        public decimal TraficDateInclus { get; set; }
        }
    
}
