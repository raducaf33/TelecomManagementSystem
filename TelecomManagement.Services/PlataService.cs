using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomManagement.Domain;
using TelecomManagement.Services.Base;
using TelecomManagement.Data;

namespace TelecomManagement.Services
{
    public class PlataService : ServiceBase<Plata>
    {
        private readonly IRepositoryBase<Plata> plataRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="StockService"/> class.
        /// </summary>
        /// <param name="stockRepository">The stock repository.</param>
        public PlataService(IRepositoryBase<Plata> plataRepository) :
            base(plataRepository)
        {
        }




        public bool EstePlataFacutaPentruContract(int contractId)
        {
            var plati = plataRepository.GetAll();
            return plati.Any(plata => plata.ContractId == contractId && plata.EstePlatita);
        }
    }
}
