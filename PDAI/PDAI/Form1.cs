using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PDAI
{
    enum AccessLevel {administrador, director, secretaria, recursosHumanos, guardaChefe, guarda }
    enum MenuPosition { top, bottom }
    delegate void Event(object sender, EventArgs e);

   

    public partial class Main : Form
    {
        Screen screen;
        I_Login i_login;
        Database database;
        int width, height;
        

        public Main()
        {
            InitializeComponent();
            screen = new Screen(this);
            screen.full_Screen = true;
            database = new Database();
            SetDefaultRules();

        }


        private void SetDefaultRules()
        {
            if (!database.select.RolesExists())
                database.insert.SetRoles(Rule.GetRoles());
            if (!database.select.MaritalStatusExists())
                database.insert.SetMaritalStatus(Rule.GetMaritalStatus());
            if (!database.select.PrivilegesRolesExists())
            {
                Dictionary<string, uint> privilegesRoleIds = database.insert.SetPrivilegesRoles(Rule.GetPrivilegesRoles());

                foreach (string privilegesRole in Rule.GetPrivilegesRoles())
                {
                    database.insert.SetPrivileges(privilegesRoleIds[privilegesRole], Rule.GetPrivileges());
                    if (privilegesRole == "Diretor")
                    {
                        foreach (string privilege in Rule.GetPrivileges_Diretor())
                        {
                            database.update.Privileges(privilegesRoleIds[privilegesRole], privilege, true);
                        }
                    }

                    if (privilegesRole == "Gestor R.H.")
                    {
                        foreach (string privilege in Rule.GetPrivileges_GestorRH())
                        {
                            database.update.Privileges(privilegesRoleIds[privilegesRole], privilege, true);
                        }
                    }

                    if (privilegesRole == "Secretária")
                    {
                        foreach (string privilege in Rule.GetPrivileges_Secretaria())
                        {
                            database.update.Privileges(privilegesRoleIds[privilegesRole], privilege, true);
                        }
                    }

                    if (privilegesRole == "Guarda-Chefe")
                    {
                        foreach (string privilege in Rule.GetPrivileges_GuardaChefe())
                        {
                            database.update.Privileges(privilegesRoleIds[privilegesRole], privilege, true);
                        }
                    }

                    if (privilegesRole == "Guarda")
                    {
                        foreach (string privilege in Rule.GetPrivileges_Guarda())
                        {
                            database.update.Privileges(privilegesRoleIds[privilegesRole], privilege, true);
                        }
                    }

                }
               
            }
            if (!database.select.AdminExists())
            { 
                Account adminAccount = new Account();
                adminAccount.CreateAccount(0, Rule.GetAdminUsername, Rule.GetAdminPassword);
            }
            IO_Class.CreateFolder(@"C:\EyesOpen\");
            List<uint> ids = database.select.GetId_AddAccountPrivileges(Rule.GetPrivileges().Count);
            if (ids.Count > 0) database.update.UpdateNewPrivileges(Rule.GetPrivileges(), ids);
        }


        private void Init()
        {
            i_login = new I_Login(this, width, height);
        }



        private void Main_Shown(object sender, EventArgs e)
        {
            width = Width - 16;
            height = Height - 40;
            Init();
        }
    }
}
