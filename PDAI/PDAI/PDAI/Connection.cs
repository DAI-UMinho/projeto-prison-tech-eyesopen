using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace PDAI
{
    class Connection
    {
<<<<<<< Updated upstream

        protected string connectionString = @"Data Source=DESKTOP-SBQKKVO\DAI_SERVER;Initial Catalog=DAI_Database;User ID=sa;Password=dai";
=======
        
        protected string connectionString = @"Data Source=LAPTOP-G0967CH5;Initial Catalog=DAI_Database;User ID=teste;Password=password";
>>>>>>> Stashed changes

        protected SqlConnection sqlConn;
        protected SqlCommand command;
        protected SqlDataAdapter adapter;
        protected SqlDataReader reader;
        protected string sql;

    }
}
