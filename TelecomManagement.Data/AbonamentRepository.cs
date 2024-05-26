using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TelecomManagement.Domain;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelecomManagement.Data
{
    public class AbonamentRepository : RepositoryBase<Abonament>
    {
        private string _connectionString;
        public AbonamentRepository(TelecomContext context) : base(context)
        {
        }

        public void AdaugaAbonament(Abonament abonament)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Abonaments (Nume, Pret,MinuteIncluse, SMSuriIncluse, TraficDateInclus) VALUES (@Nume, @Pret, @MinuteIncluse,@SMSuriIncluse, @TraficDateInclus)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Nume", abonament.Nume);
                command.Parameters.AddWithValue("@Pret", abonament.Pret);
                command.Parameters.AddWithValue("@MinuteIncluse", abonament.MinuteIncluse);
                command.Parameters.AddWithValue("@SMSuriIncluse", abonament.SMSuriIncluse);
                command.Parameters.AddWithValue("@TraficDateInclus", abonament.TraficDateInclus);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public List<Abonament> GetAllAbonamente()
        {
            return GetRecords().ToList();
        }



    }
    }

