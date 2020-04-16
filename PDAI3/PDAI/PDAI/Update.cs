using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PDAI
{
    class Update : Connection
    {
        public void Recluso(string nome, string dataNascimento, string cc, string estadoCivil, string str)
        {
            string date = Convert.ToDateTime(dataNascimento).ToString("yyyy-MM-dd");

            try
            {
                sql = "update Pessoa set nomeCompleto ='" + nome + "', dataNascimento ='" + date + "' , cc ='" + cc + "', estadoCivil ='" + estadoCivil + "' where nomeCompleto = '" + str + "'";

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
