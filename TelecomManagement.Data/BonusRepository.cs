// <copyright file="BonusRepository.cs" company="Transilvania University Of Brasov">
// Fintineru Raduca-Maria
// </copyright>
// <summary> Defines the Bonus Repository class. </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TelecomManagement.Domain;

namespace TelecomManagement.Data
{
    /// <summary>
    /// BonusRepository class.
    /// </summary>
    public class BonusRepository : RepositoryBase<Bonus>
    {
        /// <summary>
        /// Initializes a new instance of the BonusRepository class.
        /// </summary>

        public BonusRepository(TelecomContext context) : base(context)
        {
          
        }

  

    }
}

