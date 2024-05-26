using System.Collections.Generic;
using System.Data;
using System.Linq;
using TelecomManagement.Data;
using TelecomManagement.Domain;

namespace TelecomManagement.Services
{
    public class AbonamentService : ViewModelBase
    {

        private readonly TelecomContext _telecomContext;
        private readonly AbonamentRepository _abonamentRepository;


        public AbonamentService(AbonamentRepository abonamentRepository)
        {
            _abonamentRepository = abonamentRepository;
        }

     
      

    }
}

