using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomManagement.Data;
using TelecomManagement.Domain;

namespace TelecomManagement.Services
{
    public class BonusService
    {
        
            private readonly BonusRepository _bonusRepository;

            public BonusService(BonusRepository bonusRepository)
            {
                _bonusRepository = bonusRepository;
            }

            public List<Bonus> GetAllBonuses()
            {
                return _bonusRepository.GetAll();
            }
        
    }
}
