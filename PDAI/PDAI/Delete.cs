﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PDAI
{
    class Delete : Connection
    {

        public void Recluso(uint id)
        {
            try
            {
                sql = "";

                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();

                command = new System.Data.SqlClient.SqlCommand(sql, sqlConn);
                adapter = new SqlDataAdapter();
                adapter.InsertCommand = command;
                command.ExecuteNonQuery();
                command.Dispose();
                adapter.Dispose();
                sqlConn.Close();
            }
            catch (Exception e) { System.Windows.Forms.MessageBox.Show("" + e); };
        }
    }
}
