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
                sql = "select nomeCompleto from Pessoa where tipo = 'Prisioneiro'";

                
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



    }
}
