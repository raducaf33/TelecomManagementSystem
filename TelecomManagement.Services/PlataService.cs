// <copyright file="PlataService.cs" company="Transilvania University Of Brasov">
// Fintineru Raduca-Maria
// </copyright>
// <summary> Defines the Plata Service class. </summary>

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


    /// <summary>
    /// PlataService Class which implements ServiceBase Class.
    /// </summary>
    public class PlataService : ServiceBase<Plata>
    {
        private readonly IRepositoryBase<Plata> plataRepository;

        /// <summary>
        /// Initializes a new instance of the PlataService class
        /// </summary>
        public PlataService(IRepositoryBase<Plata> plataRepository) :
            base(plataRepository)
        {
        }

        /// <summary>
        /// Creates a bool function to check if the Payments has been done for the contract
        /// </summary>


        public bool EstePlataFacutaPentruContract(int contractId)
        {
            var plati = plataRepository.GetAll();
            return plati.Any(plata => plata.ContractId == contractId && plata.EstePlatita);
        }
    }
}
