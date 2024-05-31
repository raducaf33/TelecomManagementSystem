using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomManagement.Domain;

namespace TelecomManagement.Data
{
    public class PlataRepository : RepositoryBase<Plata>
    {
        


        public PlataRepository(TelecomContext context) : base(context)
        {
          

        }

        

    }
}
