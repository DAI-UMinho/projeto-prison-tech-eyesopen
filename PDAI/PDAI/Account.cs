﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace PDAI
{
    class Account
    {
        public Panel container { get; }
        public int locationX { set { container.Location = new Point(value, container.Location.Y); } get { return container.Location.X; } }
        public int locationY { set { container.Location = new Point(container.Location.X, value); } get { return container.Location.Y; } }
        public int width { set { container.Size = new Size(value, container.Height); } get { return container.Width; } }
        public int height { set { container.Size = new Size(container.Width, value); } get { return container.Height; } }
        string username;
        byte[] password;
        uint idAccount;
        Database database;
        Form form;
        Menu menu;
        Panel activeContainer;
        int formContainerWidth, formContainerHeight;
        Color color = Color.FromArgb(196, 196, 196);
        Dictionary<string, List<string>> privilegesRole;
        string privilegeRole;
        List<string> stringObject;
        Dictionary<string, object> disposeObject;

        public Account()
        {
            container = new Panel();
            container.BackColor = Color.White;
            database = new Database();
        }


        public Account(string username, byte[] password, uint idAccount)
        {
            container = new Panel();
            container.BackColor = Color.White;

            database = new Database();

            this.username = username;
            this.password = password;
            this.idAccount = idAccount;

            privilegesRole = new Dictionary<string, List<string>>();
            stringObject = new List<string>();
            disposeObject = new Dictionary<string, object>();
        }

   
        
        public void CreateAccount(uint idPerson, string username, string password)
        {
            this.username = username;
            if (VerifiedAdmin()) { CreateAdminAccount(username, password); }
            else
            {
                byte[] accountAccess = Encryption.AccountAccessEncryption(username + password);
                database.insert.Login(idPerson, username, accountAccess);
            }
        }


        public void LoadAccount()
        {
            if (!VerifiedAdmin())
            {
                privilegeRole = database.select.GetPrivilegeRole(idAccount);
                privilegesRole = database.select.GetPrivileges(database.select.GetIdPrivilegeRole(idAccount), privilegesRole);
            }
        }


        public void Open(Form form, int width, int height) 
        {
            this.form = form;
            formContainerWidth = width;
            formContainerHeight = height;

            menu = new Menu();
            menu.locationX = 0;
            menu.locationY = 0;
            menu.width = width / 8;
            menu.height = height;

            form.Controls.Add(container);
            container.Location = new Point(0, 0);
            container.Size = new Size(width, height);
            container.Controls.Add(menu.container);

            if (VerifiedAdmin())
            {
                Panel item = menu.AddItem("Contas", AccountList,MenuPosition.top);
                menu.AddItem("Definições", AccountSettings, MenuPosition.top);
                menu.AddItem("Sair", Logout,  MenuPosition.bottom);
                AccountList(item, new EventArgs());
            }
            else 
            {
                Panel currentItem = null;

                menu.AddItem("Sair", Logout, MenuPosition.bottom);
                foreach (string privilege in Rule.GetPrivileges())
                {
                    Array privilegeName = privilege.Split('-');
                    if (privilegeName.Length > 1)
                    {
                        if (privilegesRole[privilegeRole].Contains(privilege))
                        {
                            if (currentItem == null)
                            {
                                string mainPrivilege = "";
                                bool join = false;
                                foreach (string str in privilegeName.GetValue(0).ToString().Split(' '))
                                {
                                    if (join) { mainPrivilege += str; }
                                    join = true;
                                }
                                
                                currentItem = menu.AddItem(mainPrivilege, MenuPosition.top);
                                menu.AddSubItem(currentItem, privilege, Pages);
                            }
                            else
                            {
                                menu.AddSubItem(currentItem, privilege, Pages);
                            }
                        }
                    }
                    else { currentItem = null;  }
                }
            }

        }




        private bool VerifiedAdmin()
        {
            if (username == Rule.GetAdminUsername) return true; else return false;
        } 


        private void CreateAdminAccount(string username, string password)
        {
            byte[] accountAccess = Encryption.AccountAccessEncryption(username + password);
            uint id = database.insert.Person(Rule.GetRoles()[0].Split('.').GetValue(0).ToString(), DateTime.Now.ToString("yyyy-MM-dd"), "", "", Rule.GetRoles()[0].Split('.').GetValue(0).ToString());
            database.insert.Login(id, username, accountAccess);
        }


        private void AccountList(object sender, EventArgs e)
        {
            if (activeContainer != null) container.Controls.Remove(activeContainer);
            I_AccountList accountList = new I_AccountList();
            container.Controls.Add(accountList.container);
            accountList.locationX = menu.locationX + menu.width-2;
            accountList.locationY = 0;
            accountList.width = formContainerWidth - menu.width;
            accountList.height = formContainerHeight;
            accountList.Open();
            activeContainer = accountList.container;



        }



        private void AccountSettings(object sender, EventArgs e)
        {
            if (activeContainer != null) container.Controls.Remove(activeContainer);

            I_General general = new I_General();
            container.Controls.Add(general.container);
            general.locationX = menu.locationX + menu.width - 2;
            general.locationY = 0;
            general.width = formContainerWidth - menu.width;
            general.height = formContainerHeight;
            activeContainer = general.container;

      
            Role role = new Role();
            role.width = general.container.Width * 9 / 10;
            role.height = 400;
            role.Create();
            general.AddItem(role.container);

            MaritalStatus maritalStatus = new MaritalStatus();
            maritalStatus.width = general.container.Width * 9 / 10;
            maritalStatus.height = 400;
            maritalStatus.Create();
            general.AddItem(maritalStatus.container);


            PrivilegesRole privilegesRole = new PrivilegesRole();
            privilegesRole.width = general.container.Width * 9 / 10;
            privilegesRole.height = 600;
            privilegesRole.Create();
            general.AddItem(privilegesRole.container);

         


        }


        private void Logout(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Deseja terminar a sessão?", "Logout", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                form.Controls.Clear();
                I_Login i_login = new I_Login(form, formContainerWidth, formContainerHeight);
            }
          
        }


        private void Pages(object sender, EventArgs e)
        {
            string val = "";
            if (stringObject.Count != 0) val = stringObject[0];

            if (((Label)sender).Name == "Privilégio Estatística-Visualizar" || val == "Privilégio Estatística-Visualizar")
            {
                if (val == "Privilégio Estatística-Visualizar")
                {
                    ((StatisticsForm)disposeObject[val]).Dispose();
                    disposeObject.Remove(val);
                    stringObject.Remove(val);
                }
                else
                {
                    StatisticsForm statisticsForm = new StatisticsForm();
                    statisticsForm.Width = container.Width - menu.width;
                    statisticsForm.Show();
                    statisticsForm.Height = container.Height;
                    statisticsForm.Location = new Point(menu.locationX + menu.width, 23);
                    stringObject.Add(((Label)sender).Name);
                    disposeObject[((Label)sender).Name] = statisticsForm;
                }
            }


            if (((Label)sender).Name == "Privilégio Funcionário-Registar" || val == "Privilégio Funcionário-Registar")
            {
                if (val == "Privilégio Funcionário-Registar")
                {
                    ((I_Person)disposeObject[val]).container.Dispose();
                    disposeObject.Remove(val);
                    stringObject.Remove(val);
                }
                else
                {
                    I_Person person = new I_Person();
                    container.Controls.Add(person.container);
                    person.width = container.Width - menu.width;
                    person.height = container.Height;
                    person.locationX = menu.locationX + menu.width;
                    person.locationY = 0;
                    person.Open(true,false);
                    stringObject.Add(((Label)sender).Name);
                    disposeObject[((Label)sender).Name] = person;
                }

            }


            if (((Label)sender).Name == "Privilégio Recluso-Registar" || val == "Privilégio Recluso-Registar")
            {
                if (val == "Privilégio Recluso-Registar")
                {
                    ((I_Person)disposeObject[val]).container.Dispose();
                    disposeObject.Remove(val);
                    stringObject.Remove(val);
                }
                else
                {
                    I_Person person = new I_Person();
                    container.Controls.Add(person.container);
                    person.width = container.Width - menu.width;
                    person.height = container.Height;
                    person.locationX = menu.locationX + menu.width;
                    person.locationY = 0;
                    person.Open(false,false);
                    stringObject.Add(((Label)sender).Name);
                    disposeObject[((Label)sender).Name] = person;
                }

            }


            if (((Label)sender).Name == "Privilégio Recluso-Visualizar" || val == "Privilégio Recluso-Visualizar")
            {
                if (val == "Privilégio Recluso-Visualizar")
                {
                    ((PrisonersManager)disposeObject[val]).container.Dispose();
                    disposeObject.Remove(val);
                    stringObject.Remove(val);
                }
                else
                {
                    PrisonersManager pm = new PrisonersManager(container, container.Width, container.Height);
                    //container.Controls.Add(pm.container);
                    //pm.width = container.Width - menu.width;
                    //pm.height = container.Height;
                    //pm.locationX = menu.locationX + menu.width;
                    //pm.locationY = 0;
                    //pm.Open();
                    stringObject.Add(((Label)sender).Name);
                    disposeObject[((Label)sender).Name] = pm;
                }

            }

            if (((Label)sender).Name == "Privilégio Câmara-Visualizar Deteção" || val == "Privilégio Câmara-Visualizar Deteção")
            {
                if (val == "Privilégio Câmara-Visualizar Deteção")
                {
                    ((I_CamGallery)disposeObject[val]).container.Dispose();
                    disposeObject.Remove(val);
                    stringObject.Remove(val);
                }
                else
                {
                    I_CamGallery camGallery = new I_CamGallery(container, container.Width, container.Height);
                    //container.Controls.Add(pm.container);
                    //pm.width = container.Width - menu.width;
                    //pm.height = container.Height;
                    //pm.locationX = menu.locationX + menu.width;
                    //pm.locationY = 0;
                    //pm.Open();
                    stringObject.Add(((Label)sender).Name);
                    disposeObject[((Label)sender).Name] = camGallery;
                }

            }


            if (((Label)sender).Name == "Privilégio Ocorrência-Registar" || val == "Privilégio Ocorrência-Registar")
            {
                if (val == "Privilégio Ocorrência-Registar")
                {
                    ((Incidents)disposeObject[val]).container.Dispose();
                    disposeObject.Remove(val);
                    stringObject.Remove(val);
                }
                else
                {
                    Incidents incidents = new Incidents(container, container.Width, container.Height);
                   // container.Controls.Add(incidents.container);
                   // incidents.width = container.Width - menu.width;
                   // incidents.height = container.Height;
                   //incidents.locationX = menu.locationX + menu.width;
                   // incidents.locationY = 0;
                  /* incidents.Open(true, false)*/;
                    stringObject.Add(((Label)sender).Name);
                    disposeObject[((Label)sender).Name] = incidents;
                }

            }


        }


    }
}

//56AO4  user2
