using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomManagement.Domain;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;

namespace TelecomManagement.Data
{
    public class UserRepository : RepositoryBase<User>
    {     

        public UserRepository(TelecomContext context) : base(context)
        {
            
        }

        
    }
}
