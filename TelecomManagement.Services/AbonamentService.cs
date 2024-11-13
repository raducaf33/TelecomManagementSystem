// <copyright file="AbonamentService.cs" company="Transilvania University Of Brasov">
// Fintineru Raduca-Maria
// </copyright>
// <summary> Defines the Abonament Service class. </summary>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TelecomManagement.Data;
using TelecomManagement.Domain;
using TelecomManagement.Services.Base;

namespace TelecomManagement.Services
{

    /// <summary>
    /// AbonamentService Class which implements ServiceBase Class.
    /// </summary>
    
    public class AbonamentService : ServiceBase<Abonament>
    {


        /// <summary>
        /// Initializes a new instance of the AbonamentService class
        /// </summary>
        public AbonamentService(AbonamentRepository abonamentRepository) :
                base(abonamentRepository)
            {
            }
        

    }
}

