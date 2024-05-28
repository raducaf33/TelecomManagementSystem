using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TelecomManagement.Domain;
using System.Threading.Tasks;

namespace TelecomManagement.Data
{
    public class ContractRepository : RepositoryBase<Contract>

    {
        private string _connectionString;

        private readonly TelecomContext _context;

        public ContractRepository(TelecomContext context) : base(context)
      {
            _context = context;
        }

        public List<Contract> GetContractsByClientId(int clientId)
        {
            return _context.Contracte.Where(c => c.ClientId == clientId).ToList();
        }

        public void AdaugaContract(Contract contract)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Contracte (ClientId, AbonamentId, DataIncheiere, DataExpirare) VALUES (@ClientId, @AbonamentId, @DataIncheiere, @DataExpirare)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ClientId", contract.ClientId);
                command.Parameters.AddWithValue("@AbonamentId", contract.AbonamentId);
                command.Parameters.AddWithValue("@DataIncheiere", contract.DataIncheiere);
                command.Parameters.AddWithValue("@DataExpirare", contract.DataExpirare);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

       

    }
}
