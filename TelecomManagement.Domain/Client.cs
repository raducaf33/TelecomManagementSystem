﻿// <copyright file="Client.cs" company="Transilvania University Of Brasov">
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


namespace TelecomManagement.Domain
{
    public class Client
    {

          // Proprietăți
          [Key]
          [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public int Id { get; set; }

          public string Nume { get; set; }

          public string Prenume { get; set; }

          public string Email { get; set; }

          public string Telefon { get; set; }

          public string CNP { get; set; }

          public int UserId { get; set; }

          [ForeignKey("UserId")]
          public User User { get; set; }



          [NotMapped]
          public int Varsta => CalculeazaVarstaDinCNP(CNP);

        private int CalculeazaVarstaDinCNP(string cnp)
        {
            if (cnp.Length != 13)
                throw new ArgumentException("CNP-ul trebuie să aibă 13 caractere.");

            int sexSiSecol = int.Parse(cnp.Substring(0, 1));
            int an = int.Parse(cnp.Substring(1, 2));
            int luna = int.Parse(cnp.Substring(3, 2));
            int zi = int.Parse(cnp.Substring(5, 2));

            int sex = sexSiSecol % 2 == 1 ? 1 : 2; // 1 pentru masculin, 2 pentru feminin

            switch (sexSiSecol)
            {
                case 1:
                case 2:
                    an += 1900;
                    break;
                case 3:
                case 4:
                    an += 1800;
                    break;
                case 5:
                case 6:
                    an += 2000;
                    break;
                case 7:
                case 8:
                    throw new ArgumentException("CNP-ul este destinat străinilor și nu poate fi folosit pentru determinarea vârstei.");
                default:
                    throw new ArgumentException("CNP-ul este invalid.");
            }

            var dataNasteriiDateTime = new DateTime(an, luna, zi);
            var varsta = DateTime.Today.Year - dataNasteriiDateTime.Year;

            if (DateTime.Today < dataNasteriiDateTime.AddYears(varsta))
            {
                varsta--;
            }

            return varsta;
        }




    }


}
    
