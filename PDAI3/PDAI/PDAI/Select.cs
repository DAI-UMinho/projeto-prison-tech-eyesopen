﻿using System;
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


    }
}
