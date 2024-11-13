// <copyright file="FacturaRepository.cs" company="Transilvania University Of Brasov">
// Fintineru Raduca-Maria
// </copyright>
// <summary> Defines the Factura Repository class. </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomManagement.Domain;

namespace TelecomManagement.Data
{
    /// <summary>
    /// FacturaRepository class.
    /// </summary>
    public class FacturaRepository : RepositoryBase<Factura>
    {
        /// <summary>
        /// Initializes a new instance of the FacturaRepository class.
        /// </summary>


        public FacturaRepository(TelecomContext context) : base(context)
        {


        }

        public FacturaRepository() : base(new TelecomContext())
        {
        }



    }
}
