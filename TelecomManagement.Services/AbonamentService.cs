using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TelecomManagement.Data;
using TelecomManagement.Data.Interfaces;
using TelecomManagement.Domain;
using TelecomManagement.Services.Base;

namespace TelecomManagement.Services
{
    public class AbonamentService : ServiceBase<Abonament>
    {

        
            /// <summary>
            /// Initializes a new instance of the <see cref="StockService"/> class.
            /// </summary>
            /// <param name="stockRepository">The stock repository.</param>
            public AbonamentService(AbonamentRepository abonamentRepository) :
                base(abonamentRepository)
            {
            }
        

    }
}

