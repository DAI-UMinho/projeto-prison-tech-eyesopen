using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace PDAI
{
    class Update : Connection
    {
        public void Person(uint id, string recordsFolder)
        {
            try
            {
                sql = "update Pessoa set pastaRegistos = '" + recordsFolder + "' where id = " + id;

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



        public void Privileges(uint id, string privilege, bool active)
        {
            try
            {
                byte activeVal = 0;
                if (active) activeVal = 1;

                sql = "update Privilegio set ativo = " + activeVal + " where idPapel = " + id + " and privilegio = '"+ privilege + "'";

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




        public void AssocRole(uint idLogin, uint idRole)
        {
            try
            {

                sql = "update AssocPapel set idPapel = "+ idRole + " where idLogin = "+ idLogin;

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



        public bool Credentials(uint idLogin, string username, byte[] accountAccess)
        {
            bool var = true;
            try
            {
                sql = "update Login set username = @username,password= @accountAccess where id = @id";

                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();

                command = new System.Data.SqlClient.SqlCommand(sql, sqlConn);
                adapter = new SqlDataAdapter();
                adapter.InsertCommand = command;
                command.Parameters.Add("@id", SqlDbType.Int).Value = idLogin;
                command.Parameters.Add("@username", SqlDbType.NVarChar, 30).Value = username;
                command.Parameters.Add("@accountAccess", SqlDbType.VarBinary, 8000).Value = accountAccess;
                command.ExecuteNonQuery();
                command.Dispose();
                adapter.Dispose();
                sqlConn.Close();
            }
            catch (Exception e) { var = false; };

            return var;
        }




        public void UpdateNewPrivileges(List<string> privileges, List<uint> ids)
        {
            try
            {
                List<string> newListPrivileges = new List<string>();
                foreach (string privilege in privileges)
                {
                    if (Convert.ToInt32(privilege.Split('-').Count()) > 1) newListPrivileges.Add(privilege);
                }
                List<string> selectedPrivileges = new List<string>();
                foreach (uint id in ids)
                {
                    sql = "select privilegio  from Papel inner join Privilegio on Papel.id = Privilegio.idPapel where Papel.id = " + id;

                    sqlConn = new SqlConnection(connectionString);
                    sqlConn.Open();
                    command = new SqlCommand(sql, sqlConn);
                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        selectedPrivileges.Add(reader[0].ToString());
                    }
                    reader.Close();
                    command.Dispose();
                    sqlConn.Close();

                    foreach (string privilege in newListPrivileges)
                    {
                        if (!selectedPrivileges.Contains(privilege))
                        {
                            sql = "insert Privilegio (idPapel, privilegio, ativo) values ("+ id + ",'"+ privilege + "',0) ";

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
                    }

                    foreach (string privilege in selectedPrivileges)
                    {
                        if (!newListPrivileges.Contains(privilege))
                        {
                            sql = "delete Privilegio where idPapel = " + id + " and privilegio = '"+ privilege + "'" ;

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
                    }

                }
            }
            catch (Exception e) { System.Windows.Forms.MessageBox.Show("" + e); };
        }

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

        public void Visita(string id, string nome, string idRecluso, string dataVsista)
        {
            string date = Convert.ToDateTime(dataVsista).ToString("yyyy-MM-dd");

            try
            {
                sql = "update Visita set  idPessoa ='" + idRecluso + "', nome ='" + nome + "', dataVisita ='" + date + "' where id = '" + id + "'";

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
