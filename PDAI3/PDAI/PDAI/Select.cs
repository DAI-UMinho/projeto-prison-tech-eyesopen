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
                sql = "select nomeCompleto from Pessoa";

                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                command = new SqlCommand(sql, sqlConn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                  
                    recluso = reader[0];
                }
                
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
                reader.Close();
                command.Dispose();
                sqlConn.Close();
            }
            return recluso;
        }

       

        public List<object> Login(string username)
        {
            List<object> var = new List<object>();
            try
            {

                sql = "select password, accessLevel from Login where username = '" + username + "'";

                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                command = new SqlCommand(sql, sqlConn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var.Add(reader[0]);
                    var.Add(reader[1]);
                }
                
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
                reader.Close();
                command.Dispose();
                sqlConn.Close();
            }
            return var;
        }

        public List<object> Reclusos()
        {
            List<object> var = new List<object>();
            string sql = "select id,nomeCompleto from pessoa where tipo = 'Prisioneiro';";
            try
            {

                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                command = new SqlCommand(sql, sqlConn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var.Add(reader[0]);
                    var.Add(reader[1]);
                }

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
            finally
            {
                reader.Close();
                command.Dispose();
                sqlConn.Close();
            }
            return var;
        }

        public List<object> Top10()
        {
            List<object> var = new List<object>();
            sql = "select o.idPessoa, COUNT(o.id) as contagem, p.nomeCompleto " +
                "from Ocorrencia o, Pessoa p " +
                "where o.idPessoa = p.id " +
                "GROUP BY YEAR(o.dataOcorrencia), o.idPessoa, p.nomeCompleto " +
                "ORDER BY contagem DESC; ";
            try
            {
                
                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                command = new SqlCommand(sql, sqlConn);
                reader = command.ExecuteReader();
                while(reader.Read())
                {
                    var.Add(reader[0]);
                    var.Add(reader[1]);
                    var.Add(reader[2]);

                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConn.Close();
                reader.Close();
            }
            return var;
        }

        public List<object> OcorrenciaMes(int ano)
        {
            
            List<object> var = new List<object>();
            sql = "select COUNT(id) as contagem, MONTH(dataOcorrencia) AS mesOcorrencia from Ocorrencia" +
                " where YEAR(dataOcorrencia) = " + ano + " GROUP BY MONTH(dataOcorrencia) ORDER BY mesOcorrencia ASC; ";
            try
            {
                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                command = new SqlCommand(sql, sqlConn);
                reader = command.ExecuteReader();
                while(reader.Read())
                {
                    var.Add(reader[0]);
                    var.Add(reader[1]);
                }

            }
            catch(Exception ex)
            {

            }
            finally
            {
                sqlConn.Close();
                reader.Close();
            }
            return var;
        }

        public List<object> PreencherComboBox()
        {
            List<object> var = new List<object>();
            sql = "select distinct year(dataOcorrencia) as ano from Ocorrencia Order by ano ASC;";
            try
            {
                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                command = new SqlCommand(sql, sqlConn);
                reader = command.ExecuteReader();
                while(reader.Read())
                {
                    var.Add(reader[0]);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConn.Close();
                command.Dispose();
                reader.Close();
            }
            return var;
        }


    }
}
