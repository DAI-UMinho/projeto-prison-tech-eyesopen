using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace PDAI
{
    class Connection
    {

        protected string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=DAI_Database;Integrated Security=True;";

        protected SqlConnection sqlConn;
        protected SqlCommand command;
        protected SqlDataAdapter adapter;
        protected SqlDataReader reader;
        protected string sql;

    }
}
