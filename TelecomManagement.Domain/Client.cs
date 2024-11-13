// <copyright file="Client.cs" company="Transilvania University Of Brasov">
// Fintineru Raduca-Maria
// </copyright>
// <summary> Defines the Client class. </summary>

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Client class.
/// </summary>
namespace TelecomManagement.Domain
{
    public class Client
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
        /// Gets or sets the Prenume.
        /// </summary>

        public string Prenume { get; set; }

        /// <summary>
        /// Gets or sets the Email.
        /// </summary>

        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the Telefon.
        /// </summary>

        public string Telefon { get; set; }

        /// <summary>
        /// Gets or sets the CNP.
        /// </summary>

        public string CNP { get; set; }

        /// <summary>
        /// Gets or sets the UserId.
        /// </summary>

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        /// <summary>
        /// Defines the Varsta and returns output from method CalaculateAgeFromCnp.
        /// </summary>

        [NotMapped]
        public int Varsta
        {
            get
            {
                return CalculateAgeFromCnp(CNP, DateTime.Today);
            }
        }

        /// <summary>
        /// Calculates the Age by CNP.
        /// </summary>

        public int CalculateAgeFromCnp(string cnp, DateTime currentDate)
        {
            if (cnp.Length != 13)
            {
                throw new ArgumentException("CNP-ul trebuie să aibă 13 caractere.");
            }

            int sexAndCentury = int.Parse(cnp.Substring(0, 1));
            int year = int.Parse(cnp.Substring(1, 2));
            int month = int.Parse(cnp.Substring(3, 2));
            int day = int.Parse(cnp.Substring(5, 2));

            int birthYear = 0;

            // Determinarea secolului în funcție de prima cifră din CNP
            switch (sexAndCentury)
            {
                case 1:
                case 2:
                    birthYear = 1900 + year;
                    break;
                case 3:
                case 4:
                    birthYear = 1800 + year;
                    break;
                case 5:
                case 6:
                    birthYear = 2000 + year;
                    break;
                default:
                    throw new ArgumentException("Prima cifră a CNP-ului nu este validă.");
            }

            DateTime birthDate;
            try
            {
                birthDate = new DateTime(birthYear, month, day);
            }
            catch (Exception)
            {
                throw new ArgumentException("Data nașterii nu este validă.");
            }

            int age = currentDate.Year - birthDate.Year;

            if (birthDate > currentDate.AddYears(-age))
            {
                age--;
            }

            return age;
        }
    }
}
    
