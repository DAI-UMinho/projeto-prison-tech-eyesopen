﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace PDAI
{
    class Connection
    {
        
        protected string connectionString = @"Data Source=OXYZ\PROJECTDEV;Initial Catalog=DAI_Database;User ID=sa;Password=ProjectDev";

        protected SqlConnection sqlConn;
        protected SqlCommand command;
        protected SqlDataAdapter adapter;
        protected SqlDataReader reader;
        protected string sql;

    }
}