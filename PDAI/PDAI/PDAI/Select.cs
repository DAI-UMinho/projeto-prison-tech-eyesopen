using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PDAI
{
    class Select : Connection
    {

        public List<object> Login(string username)
        {
            List<object> var = new List<object>();
            try
            {

                sql = "select password, id from Login where username = '" + username + "'";

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


        public bool RolesExists()
        {
            bool val = false;

            try
            {

                sql = "select Count(tipo) from Tipo";

                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                command = new SqlCommand(sql, sqlConn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (Convert.ToByte(reader[0]) > 0) val = true;
                }
                reader.Close();
                command.Dispose();
                sqlConn.Close();
            }
            catch (Exception e) { System.Windows.Forms.MessageBox.Show("" + e); };

            return val;
        }



        public bool MaritalStatusExists()
        {
            bool val = false;

            try
            {

                sql = "select Count(estadoCivil) from EstadoCivil";

                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                command = new SqlCommand(sql, sqlConn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (Convert.ToByte(reader[0]) > 0) val = true;
                }
                reader.Close();
                command.Dispose();
                sqlConn.Close();
            }
            catch (Exception e) { System.Windows.Forms.MessageBox.Show("" + e); };

            return val;
        }


        public bool PrivilegesRolesExists()
        {
            bool val = false;

            try
            {

                sql = "select Count(papel) from Papel";

                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                command = new SqlCommand(sql, sqlConn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (Convert.ToByte(reader[0]) > 0) val = true;
                }
                reader.Close();
                command.Dispose();
                sqlConn.Close();
            }
            catch (Exception e) { System.Windows.Forms.MessageBox.Show("" + e); };

            return val;
        }




        public bool AdminExists()
        {
            bool val = false;

            try
            {

                sql = "select count(id) from Pessoa where tipo = 'Administrador'";

                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                command = new SqlCommand(sql, sqlConn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (Convert.ToByte(reader[0]) > 0) val = true;
                }
                reader.Close();
                command.Dispose();
                sqlConn.Close();
            }
            catch (Exception e) { System.Windows.Forms.MessageBox.Show("" + e); };

            return val;
        }




        public int Get_Accounts(CustomizableList list)
        {
            int index = 0, counter = 0;
            try
            {
                sql = "select Pessoa.id, pastaRegistos, nomeCompleto, estadoCivil, tipo, Login.id, ativo from Pessoa left join Login on Pessoa.id = Login.id where tipo <> 'Administrador' and tipo <> 'Prisioneiro' order by nomeCompleto"; // order by nomeCompleto

                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                command = new SqlCommand(sql, sqlConn);
                reader = command.ExecuteReader();
                bool accountCreated = false;
                uint maxId = 0;
                while (reader.Read())
                {
                   
                    if (Convert.ToUInt32(reader[0]) > maxId) { maxId = Convert.ToUInt32(reader[0]); index = counter; }
                    if (!(reader.IsDBNull(5))) accountCreated = true;
                    list.AddItem(Convert.ToUInt32(reader[0]),reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), accountCreated, Convert.ToBoolean(reader[6]));
                    accountCreated = false;
                    counter++;
                   // System.Windows.Forms.MessageBox.Show(Convert.ToUInt32(reader[0]) + " > " + maxId + "  Index= " + index);
                }
                reader.Close();
                command.Dispose();
                sqlConn.Close();
 
            }
            catch (Exception e) { System.Windows.Forms.MessageBox.Show("" + e); };

            return index;
        }


        public List<string> GetRoles(ListView_Class lv)
        {
            lv.Items.Clear();
            List<string> roles = new List<string>();
            try
            {
                sql = "select tipo from Tipo where cargo = 1"; 

                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                command = new SqlCommand(sql, sqlConn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    lv.Items.Add(reader[0].ToString());
                    roles.Add(reader[0].ToString());
                }
                reader.Close();
                command.Dispose();
                sqlConn.Close();

            }
            catch (Exception e) { System.Windows.Forms.MessageBox.Show("" + e); };
            return roles;
        }


        public List<string> GetMaritalStatus(ListView_Class lv)
        {
            lv.Items.Clear();
            List<string> maritalStatus = new List<string>();
            try
            {
                sql = "select estadoCivil from EstadoCivil"; 

                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                command = new SqlCommand(sql, sqlConn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    lv.Items.Add(reader[0].ToString());
                    maritalStatus.Add(reader[0].ToString());
                }
                reader.Close();
                command.Dispose();
                sqlConn.Close();

            }
            catch (Exception e) { System.Windows.Forms.MessageBox.Show("" + e); };
            return maritalStatus;
        }


        public List<string> GetMaritalStatus()
        {
            List<string> maritalStatus = new List<string>();
            try
            {
                sql = "select estadoCivil from EstadoCivil";

                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                command = new SqlCommand(sql, sqlConn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    maritalStatus.Add(reader[0].ToString());
                }
                reader.Close();
                command.Dispose();
                sqlConn.Close();

            }
            catch (Exception e) { System.Windows.Forms.MessageBox.Show("" + e); };

            return maritalStatus;
        }




        public void GetPrivilegesRole(ListView_Class lv)
        {
            lv.Items.Clear();
            try
            {
                sql = "select papel, Papel.id from Papel";

                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                command = new SqlCommand(sql, sqlConn);
                reader = command.ExecuteReader();
                string[] row = new string[1];
                while (reader.Read())
                {
                    row[0] = reader[1].ToString();
                    lv.Items.Add(new System.Windows.Forms.ListViewItem(reader[0].ToString())).SubItems.AddRange(row);
                }
                reader.Close();
                command.Dispose();
                sqlConn.Close();

            }
            catch (Exception e) { System.Windows.Forms.MessageBox.Show("" + e); };
        }


        public Dictionary<string, uint> GetPrivilegesRole(ComboBox comboBox)
        {
            comboBox.Items.Clear();
            Dictionary<string,uint> ids = new Dictionary<string, uint>();
            try
            {
                sql = "select papel, Papel.id from Papel";

                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                command = new SqlCommand(sql, sqlConn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    comboBox.Items.Add(reader[0].ToString());
                    ids[reader[0].ToString()] = Convert.ToUInt32(reader[1]);
                }
                reader.Close();
                command.Dispose();
                sqlConn.Close();

            }
            catch (Exception e) { System.Windows.Forms.MessageBox.Show("" + e); };

            return ids;
        }



        public Dictionary<string, List<string>> GetPrivileges(uint idRole, Dictionary<string, List<string>> privilegesRole)
        {
            Dictionary<string, List<string>> privilegesList = privilegesRole;
            try
            {
                sql = "select papel, privilegio, ativo from Papel inner join Privilegio on Papel.id = Privilegio.idPapel";

                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                command = new SqlCommand(sql, sqlConn);
                reader = command.ExecuteReader();
                string[] row = new string[1];
                while (reader.Read())
                {
                    if (!privilegesList.ContainsKey(reader[0].ToString())) privilegesList[reader[0].ToString()] = new List<string>();
                    if (!Convert.ToBoolean(reader[2])){if (privilegesList[reader[0].ToString()].Contains(reader[1].ToString())) privilegesList[reader[0].ToString()].Remove(reader[1].ToString());}
                    else{if (!privilegesList[reader[0].ToString()].Contains(reader[1].ToString())) privilegesList[reader[0].ToString()].Add(reader[1].ToString());}
                }
                reader.Close();
                command.Dispose();
                sqlConn.Close();

            }
            catch (Exception e) { System.Windows.Forms.MessageBox.Show("" + e); };

            return privilegesList;
        }



        public bool UsedRole(string role)
        {
            bool val = false;

            try
            {

                sql = "select count(id) from Pessoa where tipo = '"+ role + "'";

                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                command = new SqlCommand(sql, sqlConn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (Convert.ToByte(reader[0]) > 0) val = true;
                }
                reader.Close();
                command.Dispose();
                sqlConn.Close();
            }
            catch (Exception e) { System.Windows.Forms.MessageBox.Show("" + e); };

            return val;
        }



        public bool UsedMaritalStatus(string maritalStatus)
        {
            bool val = false;

            try
            {

                sql = "select count(id) from Pessoa where estadoCivil = '" + maritalStatus + "'";

                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                command = new SqlCommand(sql, sqlConn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (Convert.ToByte(reader[0]) > 0) val = true;
                }
                reader.Close();
                command.Dispose();
                sqlConn.Close();
            }
            catch (Exception e) { System.Windows.Forms.MessageBox.Show("" + e); };

            return val;
        }



        public bool PrivilegeRoleExists(string custimizedRole)
        {
            bool val = false;

            try
            {
                sql = "select count(id) from Papel where papel = '" + custimizedRole + "'";

                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                command = new SqlCommand(sql, sqlConn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (Convert.ToByte(reader[0]) > 0) val = true;
                }
                reader.Close();
                command.Dispose();
                sqlConn.Close();
            }
            catch (Exception e) { System.Windows.Forms.MessageBox.Show("" + e); };

            return val;
        }


        public byte GetNextCustomizedRole(string custimizedRole)
        {
            byte val = 0;

            //   MessageBox.Show(custimizedRole.Substring(0, custimizedRole.Length - 1) + "%");

            try
            {

                sql = "select Count(id) from Papel where papel like '" + custimizedRole.Substring(0, custimizedRole.Length - 1) +"%" +"'";

                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                command = new SqlCommand(sql, sqlConn);
                reader = command.ExecuteReader();
              
                while (reader.Read())
                {
                    val = Convert.ToByte(reader[0]);
                }
                reader.Close();
                command.Dispose();
                sqlConn.Close();
            }
            catch (Exception e) { System.Windows.Forms.MessageBox.Show("" + e); };

            return val;
        }


        

        public string GetPrivilegeRole(uint idLogin)
        {
            string val = "";

            try
            {

                sql = "select papel from Login inner join (AssocPapel inner join Papel on Papel.id = AssocPapel.idPapel) on Login.id = AssocPapel.idLogin where idLogin = "+ idLogin;

                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                command = new SqlCommand(sql, sqlConn);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    val = reader[0].ToString();
                }
                reader.Close();
                command.Dispose();
                sqlConn.Close();
            }
            catch (Exception e) { System.Windows.Forms.MessageBox.Show("" + e); };

            return val;
        }


        public uint GetIdPrivilegeRole(uint idLogin)
        {
            uint val = 0;

            try
            {

                sql = "select Papel.id from Login inner join (AssocPapel inner join Papel on Papel.id = AssocPapel.idPapel) on Login.id = AssocPapel.idLogin where idLogin = " + idLogin;

                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                command = new SqlCommand(sql, sqlConn);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    val = Convert.ToUInt32(reader[0]);
                }
                reader.Close();
                command.Dispose();
                sqlConn.Close();
            }
            catch (Exception e) { System.Windows.Forms.MessageBox.Show("" + e); };

            return val;
        }


        public uint GetIdPrivilegeRole(string privilegeRole)
        {
            uint val = 0;

            try
            {

                sql = "select id from Papel  where papel = '"+ privilegeRole + "'";

                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                command = new SqlCommand(sql, sqlConn);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    val = Convert.ToUInt32(reader[0]);
                }
                reader.Close();
                command.Dispose();
                sqlConn.Close();
            }
            catch (Exception e) { System.Windows.Forms.MessageBox.Show("" + e); };

            return val;
        }


        public List<uint> GetId_AddAccountPrivileges(int currentNumber)
        {
            List<uint> ids = new List<uint>();

            try
            {

                sql = "select Papel.id from Papel inner join Privilegio on Papel.id = Privilegio.idPapel group by Papel.id having count(Privilegio.id) <> " + currentNumber;

                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                command = new SqlCommand(sql, sqlConn);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ids.Add(Convert.ToUInt32(reader[0]));
                }
                reader.Close();
                command.Dispose();
                sqlConn.Close();
            }
            catch (Exception e) { System.Windows.Forms.MessageBox.Show("" + e); };

            return ids;
        }

        public List<object> selecReclusoVisitado(int id)
        {

            List<object> recluso = new List<object>();
            try
            {


                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                command = new SqlCommand("select nomeCompleto from Pessoa where id = '" + id + "'", sqlConn);
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
