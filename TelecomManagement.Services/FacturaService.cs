// <copyright file="FacturaService.cs" company="Transilvania University Of Brasov">
// Fintineru Raduca-Maria
// </copyright>
// <summary> Defines the Factura Service class. </summary>

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
    /// FacturaService Class which implements ServiceBase Class.
    /// </summary>
    public class FacturaService : ServiceBase<Factura>
    {


        /// <summary>
        /// Initializes a new instance of the FacturaService class
        /// </summary>
        public FacturaService(IRepositoryBase<Factura> facturaRepository) :
            base(facturaRepository)
        {
        }


    }
}
