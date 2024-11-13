// <copyright file="BonusService.cs" company="Transilvania University Of Brasov">
// Fintineru Raduca-Maria
// </copyright>
// <summary> Defines the Bonus Service class. </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomManagement.Data;
using TelecomManagement.Domain;
using TelecomManagement.Services.Base;

namespace TelecomManagement.Services
{


    /// <summary>
    /// BonusService Class which implements ServiceBase Class.
    /// </summary>
    public class BonusService : ServiceBase<Bonus>
    {


        /// <summary>
        /// Initializes a new instance of the BonusService class
        /// </summary>
        public BonusService(IRepositoryBase<Bonus>  bonusRepository) :
            base(bonusRepository)
        {
        }



    }
}
