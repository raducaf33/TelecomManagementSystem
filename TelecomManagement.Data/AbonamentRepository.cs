﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TelecomManagement.Domain;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomManagement.Data;
using TelecomManagement.Data.Interfaces;

namespace TelecomManagement.Data
{
    public class AbonamentRepository : RepositoryBase<Abonament>
    {
 
        public AbonamentRepository(TelecomContext context) : base(context)
        {
        }


    }
    }

