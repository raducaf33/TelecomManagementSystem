using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomManagement.Domain;

namespace TelecomManagement.Data
{
    public class BonusRepository : RepositoryBase<Bonus>
    {
        private string _connectionString;
        private readonly TelecomContext _context;
        public BonusRepository(TelecomContext context) : base(context)
        {
            _context = context;
        }

        public List<Bonus> GetAll()
        {
            return _context.Bonus.ToList();
        }

    }
}

