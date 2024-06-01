﻿// <copyright file="BonusRepository.cs" company="Transilvania University Of Brasov">
// Fintineru Raduca-Maria
// </copyright>
// <summary> Defines the Bonus Repository class. </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomManagement.Data.Interfaces;
using TelecomManagement.Domain;

namespace TelecomManagement.Data
{
    public class BonusRepository : RepositoryBase<Bonus>
    {
        
        public BonusRepository(TelecomContext context) : base(context)
        {
          
        }

  

    }
}

