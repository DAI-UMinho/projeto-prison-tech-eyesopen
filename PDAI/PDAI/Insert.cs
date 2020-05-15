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

        public uint Person(string fullName, string birthDate, string cc, string maritalStatus, string type)
        {
            string date = Convert.ToDateTime(birthDate).ToString("yyyy-MM-dd");
            uint id = 0;
            try
            {
                if(maritalStatus == string.Empty) sql = "insert Pessoa (nomeCompleto, dataNascimento,cc, tipo) output inserted.id values ('" + fullName + "','" + date + "','" + cc + "','" + type + "')";
                else sql = "insert Pessoa (nomeCompleto, dataNascimento,cc,estadoCivil, tipo) output inserted.id values ('" + fullName + "','" + date + "','" + cc + "','" + maritalStatus + "','" + type + "')";

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



        public void Login(uint idPerson, string username, byte[] accountAccess)
        {
            try
            {
                sql = "insert Login (id,username,password) values (@id,@username,@accountAccess)";

                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();

                command = new System.Data.SqlClient.SqlCommand(sql, sqlConn);
                adapter = new SqlDataAdapter();
                adapter.InsertCommand = command;
                command.Parameters.Add("@id", SqlDbType.Int).Value = idPerson;
                command.Parameters.Add("@username", SqlDbType.NVarChar, 30).Value = username;
                command.Parameters.Add("@accountAccess", SqlDbType.VarBinary, 8000).Value = accountAccess;
                command.ExecuteNonQuery();
                command.Dispose();
                adapter.Dispose();
                sqlConn.Close();
            }
            catch (Exception e) { System.Windows.Forms.MessageBox.Show("" + e); };
        }



        public void SetRoles(List<string> roles)
        {
            try
            {
                foreach (string role in roles)
                {
                    sql = "insert Tipo (tipo,cargo) values (@tipo,@role)";

                    sqlConn = new SqlConnection(connectionString);
                    sqlConn.Open();
                    Array array = role.Split('.');
                    command = new System.Data.SqlClient.SqlCommand(sql, sqlConn);
                    adapter = new SqlDataAdapter();
                    adapter.InsertCommand = command;
                    command.Parameters.Add("@tipo", SqlDbType.NVarChar).Value = array.GetValue(0);
                    command.Parameters.Add("@role", SqlDbType.Bit).Value = Convert.ToByte(array.GetValue(1));
                    command.ExecuteNonQuery();
                    command.Dispose();
                    adapter.Dispose();
                    sqlConn.Close();
                }
            }
            catch (Exception e) { System.Windows.Forms.MessageBox.Show("" + e); };
        }


        public void SetMaritalStatus(List<string> maritalStatus)
        {
            try
            {
                foreach (string item in maritalStatus)
                {
                    sql = "insert EstadoCivil (estadoCivil) values (@estadoCivil)";

                    sqlConn = new SqlConnection(connectionString);
                    sqlConn.Open();
                    command = new System.Data.SqlClient.SqlCommand(sql, sqlConn);
                    adapter = new SqlDataAdapter();
                    adapter.InsertCommand = command;
                    command.Parameters.Add("@estadoCivil", SqlDbType.NVarChar).Value = item;
                    command.ExecuteNonQuery();
                    command.Dispose();
                    adapter.Dispose();
                    sqlConn.Close();
                }
            }
            catch (Exception e) { System.Windows.Forms.MessageBox.Show("" + e); };
        }


        public Dictionary<string, uint> SetPrivilegesRoles(List<string> privilegesRoles)
        {
            Dictionary<string, uint> privilegesRoleIds = new Dictionary<string, uint>();
            try
            {
                foreach (string privilegesRole in privilegesRoles)
                {
                    sql = "insert Papel (papel) output inserted.id values (@privilegesRole)";

                    sqlConn = new SqlConnection(connectionString);
                    sqlConn.Open();
                    command = new System.Data.SqlClient.SqlCommand(sql, sqlConn);
                    adapter = new SqlDataAdapter();
                    adapter.InsertCommand = command;
                    command.Parameters.Add("@privilegesRole", SqlDbType.NVarChar).Value = privilegesRole;
                    privilegesRoleIds[privilegesRole] = Convert.ToUInt32(command.ExecuteScalar());
                    command.Dispose();
                    adapter.Dispose();
                    sqlConn.Close();
                }
            }
            catch (Exception e) { System.Windows.Forms.MessageBox.Show("" + e); };

            return privilegesRoleIds;
        }


        public void SetPrivileges(uint idRole, List<string> privileges)
        {
            try
            {
                foreach (string item in privileges)
                {
                    sql = "insert Privilegio (idPapel, privilegio,ativo) values (@idPapel,@privilegio,0)";

                    sqlConn = new SqlConnection(connectionString);
                    sqlConn.Open();
                    command = new System.Data.SqlClient.SqlCommand(sql, sqlConn);
                    adapter = new SqlDataAdapter();
                    adapter.InsertCommand = command;
                    command.Parameters.Add("@idPapel", SqlDbType.Int).Value = idRole;
                    command.Parameters.Add("@privilegio", SqlDbType.NVarChar).Value = item;
                    command.ExecuteNonQuery();
                    command.Dispose();
                    adapter.Dispose();
                    sqlConn.Close();
                }
            }
            catch (Exception e) { System.Windows.Forms.MessageBox.Show("" + e); };
        }



        public void Role(string role)
        {
            try
            {
               
                sql = "insert Tipo (tipo,cargo) values (@tipo,1)";

                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                command = new System.Data.SqlClient.SqlCommand(sql, sqlConn);
                adapter = new SqlDataAdapter();
                adapter.InsertCommand = command;
                command.Parameters.Add("@tipo", SqlDbType.NVarChar).Value = role;
                command.ExecuteNonQuery();
                command.Dispose();
                adapter.Dispose();
                sqlConn.Close();
                
            }
            catch (Exception e) { System.Windows.Forms.MessageBox.Show("" + e); };
        }




        public void MaritalStatus(string maritalStatus)
        {
            try
            {

                sql = "insert EstadoCivil (estadoCivil) values (@estadoCivil)";

                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                command = new System.Data.SqlClient.SqlCommand(sql, sqlConn);
                adapter = new SqlDataAdapter();
                adapter.InsertCommand = command;
                command.Parameters.Add("@estadoCivil", SqlDbType.NVarChar).Value = maritalStatus;
                command.ExecuteNonQuery();
                command.Dispose();
                adapter.Dispose();
                sqlConn.Close();

            }
            catch (Exception e) { System.Windows.Forms.MessageBox.Show("" + e); };
        }


        public uint PrivilegesRole(string privilegesRole)
        {
            uint id=0;

            try
            {

                sql = "insert Papel (papel) output inserted.id values (@papel)";

                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                command = new System.Data.SqlClient.SqlCommand(sql, sqlConn);
                adapter = new SqlDataAdapter();
                adapter.InsertCommand = command;
                command.Parameters.Add("@papel", SqlDbType.NVarChar).Value = privilegesRole;
                id = Convert.ToUInt32(command.ExecuteScalar()); 
                command.Dispose();
                adapter.Dispose();
                sqlConn.Close();

            }
            catch (Exception e) { System.Windows.Forms.MessageBox.Show("" + e); };

            return id;
        }


        public void AssocRole(uint idLogin, uint idRole)
        {
            try
            {
                sql = "insert AssocPapel (idLogin,idPapel) values (@idLogin,@idPapel)";

                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                command = new System.Data.SqlClient.SqlCommand(sql, sqlConn);
                adapter = new SqlDataAdapter();
                adapter.InsertCommand = command;
                command.Parameters.Add("@idLogin", SqlDbType.Int).Value = idLogin;
                command.Parameters.Add("@idPapel", SqlDbType.Int).Value = idRole;
                command.ExecuteNonQuery();
                command.Dispose();
                adapter.Dispose();
                sqlConn.Close();
                
            }
            catch (Exception e) { System.Windows.Forms.MessageBox.Show("" + e); };
        }

        public void Ocorrencia(string idPessoa, string dataOcorrencia, string motivo, string descricao, int codigoOcorrencia)
        {
            try
            {
                string sql = "INSERT INTO ocorrencia (idPessoa, dataRegisto, dataOcorrencia, motivo, descricao, codigoOcorrencia)" +
                            " values(@pessoa,getDate(),@dOcorrencia,@motivo,@descricao,@codigoOcorrencia);";
                SqlConnection sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                command = new System.Data.SqlClient.SqlCommand(sql, sqlConn);
                adapter = new SqlDataAdapter();
                adapter.InsertCommand = command;
                command.Parameters.Add("@pessoa", SqlDbType.Int).Value = idPessoa;
                command.Parameters.Add("@dOcorrencia", SqlDbType.DateTime).Value = dataOcorrencia;
                command.Parameters.Add("@motivo", SqlDbType.NVarChar, 30).Value = motivo;
                command.Parameters.Add("@descricao", SqlDbType.NVarChar, 100).Value = descricao;
                command.Parameters.Add("@codigoOcorrencia", SqlDbType.Int).Value = codigoOcorrencia;
                command.ExecuteNonQuery();
                command.Dispose();
                adapter.Dispose();
                sqlConn.Close();
            }
            catch (AccessViolationException ex)
            {
                throw ex;
            }

            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Reconhecimento(string idPessoa)
        {
            try
            {
                string sql = "insert into Reconhecimento(idOcorrencia, idPessoa) " +
                    "values((select MAX(id) from Ocorrencia), " + idPessoa +")";
                SqlConnection sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                command = new System.Data.SqlClient.SqlCommand(sql, sqlConn);
                adapter = new SqlDataAdapter();
                adapter.InsertCommand = command;
                //command.Parameters.Add("@pessoa", SqlDbType.Int).Value = idPessoa;
                command.ExecuteNonQuery();
                command.Dispose();
                adapter.Dispose();
                sqlConn.Close();
            }

            catch (AccessViolationException ex)
            {
                throw ex;
            }

            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Visit(uint idVisitedPrisioner, string fullName, string visitDate)
        {
            bool val = true;
            try
            {
                string date = Convert.ToDateTime(visitDate).ToString("yyyy-MM-dd");
                sql = "insert Visita (idPessoa, nome, dataVisita) values ("+ idVisitedPrisioner + ",'"+ fullName + "', '"+ date + "')";
                //  System.Diagnostics.Debug.WriteLine("---------" + visitedPrisioner + "  " + fullName + "  " + dateNow + "  " + date);
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
            catch (Exception e) { val = false; };

            return val;
        }

    }
}
