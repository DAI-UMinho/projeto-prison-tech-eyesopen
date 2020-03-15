using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace PDAI
{
    class Insert : Connection
    {

        public uint Person(string fullName, string birthDate, string cc, string maritalStatus, string type, string recordsFolder)
        {
            string date = Convert.ToDateTime(birthDate).ToString("yyyy-MM-dd");
            uint id = 0;
            try
            {
                sql = "insert Pessoa (nomeCompleto, dataNascimento,cc,estadoCivil, tipo, pastaRegistos) output inserted.id values ('"+ fullName + "','"+ date + "','" + cc + "','" + maritalStatus + "','" + type + "','" + recordsFolder + "')";

                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();

                command = new System.Data.SqlClient.SqlCommand(sql, sqlConn);
                adapter = new SqlDataAdapter();
                adapter.InsertCommand = command;
                id = Convert.ToUInt32(command.ExecuteScalar());
                command.Dispose();
                adapter.Dispose();
                sqlConn.Close();
            }
            catch (Exception e) { System.Windows.Forms.MessageBox.Show("" + e); };

            return id;
        }



        public void Login(uint idPerson, string username, byte[] accountAccess, AccessLevel accessLevel)
        {
            try
            {
                sql = "insert Login (idPessoa,username,password,accessLevel) values (@idPerson,@username,@accountAccess,@accessLevel)";

                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();

                command = new System.Data.SqlClient.SqlCommand(sql, sqlConn);
                adapter = new SqlDataAdapter();
                adapter.InsertCommand = command;
                command.Parameters.Add("@idPerson", SqlDbType.Int).Value = idPerson;
                command.Parameters.Add("@username", SqlDbType.NVarChar, 30).Value = username;
                command.Parameters.Add("@accountAccess", SqlDbType.VarBinary, 8000).Value = accountAccess;
                command.Parameters.Add("@accessLevel", SqlDbType.NVarChar, 30).Value = accessLevel;
                command.ExecuteNonQuery();
                command.Dispose();
                adapter.Dispose();
                sqlConn.Close();
            }
            catch (Exception e) { System.Windows.Forms.MessageBox.Show("" + e); };
        }






    }
}
