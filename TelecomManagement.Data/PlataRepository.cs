// <copyright file="PlataRepository.cs" company="Transilvania University Of Brasov">
// Fintineru Raduca-Maria
// </copyright>
// <summary> Defines the Plata Repository class. </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomManagement.Domain;

namespace TelecomManagement.Data
{
    /// <summary>
    /// PlataRepository class.
    /// </summary>
    public class PlataRepository : RepositoryBase<Plata>
    {


        /// <summary>
        /// Initializes a new instance of the PlataRepository class.
        /// </summary>
        public PlataRepository(TelecomContext context) : base(context)
        {
          

        }

        

    }
}
