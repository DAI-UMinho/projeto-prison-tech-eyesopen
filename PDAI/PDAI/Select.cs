using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace PDAI
{
    class Select : Connection
    {

        public object Recluso()
        {
            object recluso = new object();
            try
            {
                sql = "";

                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                command = new SqlCommand(sql, sqlConn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    recluso = reader[0];
                }
                reader.Close();
                command.Dispose();
                sqlConn.Close();
            }
            catch (Exception e) { System.Windows.Forms.MessageBox.Show("" + e); };

            return recluso;
        }

    }
}
