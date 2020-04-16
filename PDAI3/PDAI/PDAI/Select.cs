using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace PDAI
{
    class Select : Connection
    {

        public List<object> Recluso()
        {

            List<object> recluso = new List<object>();
            try
            {
                sql = "select nomeCompleto from dbo.Pessoa where tipo='Prisioneiro' and ativo='1'";

                
                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                command = new SqlCommand(sql, sqlConn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                        recluso.Add(reader.GetValue(0));
                }
                reader.Close();
                command.Dispose();
                sqlConn.Close();
            }
            catch (Exception e) { System.Windows.Forms.MessageBox.Show("" + e); };

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
                reader.Close();
                command.Dispose();
                sqlConn.Close();
            }
            catch (Exception e) { System.Windows.Forms.MessageBox.Show("" + e); };

            return var;
        }

        public Double Count()
        {

            Double counter = new Double();
            try
            {
                sql = "select count (nomeCompleto) from Pessoa where tipo = 'Prisioneiro'";


                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                command = new SqlCommand(sql, sqlConn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                counter = reader.GetInt32(0);
                }
                reader.Close();
                command.Dispose();
                sqlConn.Close();
            }
            catch (Exception e) { System.Windows.Forms.MessageBox.Show("" + e); };

            return counter;
        }

        public List<object> selecRecluso(String nome)
        {

            List<object> recluso = new List<object>();
            try
            {


                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                command = new SqlCommand("select nomeCompleto, dataNascimento, cc, estadoCivil from Pessoa where nomeCompleto = '" + nome + "'", sqlConn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    recluso.Add(reader.GetValue(0));
                    recluso.Add(reader.GetValue(1));
                    recluso.Add(reader.GetValue(2));
                    recluso.Add(reader.GetValue(3));
                }
                reader.Close();
                command.Dispose();
                sqlConn.Close();
            }
            catch (Exception e) { System.Windows.Forms.MessageBox.Show("" + e); };

            return recluso;
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
                reader.Close();
                command.Dispose();
                sqlConn.Close();
            }
            catch (Exception e) { System.Windows.Forms.MessageBox.Show("" + e); };

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
