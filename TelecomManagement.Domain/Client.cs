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

              var dataNasterii = cnp.Substring(1, 6);

              int an = int.Parse(dataNasterii.Substring(0, 2));
              int luna = int.Parse(dataNasterii.Substring(2, 2));
              int zi = int.Parse(dataNasterii.Substring(4, 2));

              if (cnp[0] == '1' || cnp[0] == '2')
              {
                  an += 1900;
              }
              else if (cnp[0] == '5' || cnp[0] == '6')
              {
                  an += 2000;
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
    
