using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomManagement.Domain;

namespace TelecomManagement.Data
{
    public class ClientRepository : RepositoryBase<Client>

    {

       private string _connectionString;
        private readonly TelecomContext _context;
        public ClientRepository(TelecomContext context) : base(context)
        {
            _context = context;
        }


        public void AdaugaClient(Client client)
            {
            if (client.Varsta < 18)
            {
                throw new InvalidOperationException("Clientul trebuie să aibă cel puțin 18 ani pentru a putea încheia un abonament.");
            }

            using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "INSERT INTO Clienti (Nume, Prenume, Email, Telefon, CNP, UserId) VALUES (@Nume, @Prenume, @Email, @Telefon, @CNP, @UserId)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Nume", client.Nume); 
                    command.Parameters.AddWithValue("@Prenume", client.Prenume);
                    command.Parameters.AddWithValue("@Email", client.Email);
                    command.Parameters.AddWithValue("@Telefon", client.Telefon);
                    command.Parameters.AddWithValue("@CNP", client.CNP);
                    command.Parameters.AddWithValue("@UserId", client.UserId);




                connection.Open();
                command.ExecuteNonQuery();

                
            }
            }

       public Client GetClientByUserId(int userId)
        {
            // Căutăm primul client cu userul dat
            var client = _context.Clienti.FirstOrDefault(c => c.UserId == userId);
            return client;
        }
      
       

        // Implementează metodele pentru actualizare, ștergere, citire etc.
    }
    }
    

