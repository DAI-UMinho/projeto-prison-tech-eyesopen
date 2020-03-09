using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace PDAI
{
    class Insert : Connection
    {

        public void Person(string fullName, string birthDate, string cc, string maritalStatus, string type, string recordsFolder)
        {
            string date = Convert.ToDateTime(birthDate).ToString("yyyy-MM-dd");

            try
            {
                sql = "insert Pessoa (nomeCompleto, dataNascimento,cc,estadoCivil, tipo, pastaRegistos) values ('"+ fullName + "','"+ date + "','" + cc + "','" + maritalStatus + "','" + type + "','" + recordsFolder + "')";

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
