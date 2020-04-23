using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace PDAI
{
    class Connection
    {


        //protected string connectionString = @"Data Source=DESKTOP-S8JGCRT;Initial Catalog=DAI_Database;User ID=sa;Password=dai";
        //protected string connectionString = @"Data Source=LAPTOP-G0967CH5;Initial Catalog=DAI_Database;User ID=teste;Password=password";
        protected string connectionString = @"Data Source=OXYZ\PROJECTDEV;Initial Catalog=DAI_Database;User ID=sa;Password=ProjectDev";
        //protected string connectionString = @"Data Source=DESKTOP-SBQKKVO\DAI_SERVER;Initial Catalog=DAI_Database;User ID=sa;Password=dai";


        protected SqlConnection sqlConn;
        protected SqlCommand command;
        protected SqlDataAdapter adapter;
        protected SqlDataReader reader;
        protected string sql;

    }
}

