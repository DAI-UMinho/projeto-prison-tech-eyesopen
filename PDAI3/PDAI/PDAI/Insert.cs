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
            }

            catch (AccessViolationException ex)
            {
                throw ex;
            }
            catch(SqlException ex)
            {
                throw ex;
            }

            catch (Exception ex) 
            { 
                throw ex; 
            }

            finally
            {
                command.Dispose();
                adapter.Dispose();
                sqlConn.Close();
            }

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
            }
            catch(AccessViolationException ex)
            {
                throw ex;
            }

            catch(SqlException ex)
            {
                throw ex;
            }

            catch (Exception ex) 
            {
                throw ex; 
            }

            finally
            {
                command.Dispose();
                adapter.Dispose();
                sqlConn.Close();
            }
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
            catch(AccessViolationException ex)
            {
                throw ex;
            }

            catch(SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }






    }
}
