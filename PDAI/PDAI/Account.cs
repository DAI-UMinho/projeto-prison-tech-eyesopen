using System;
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
        string username, password;
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
        Object disposeObject;

        public Account()
        {
            container = new Panel();
            container.BackColor = Color.White;
            database = new Database();
        }


        public Account(string username, string password, uint idAccount)
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
            menu.Open();

            form.Controls.Add(container);
            container.Location = new Point(0, 0);
            container.Size = new Size(width, height);
            container.Controls.Add(menu.container);

            if (VerifiedAdmin())
            {
                Panel item = menu.AddItem("Contas", AccountList,MenuPosition.top);
                menu.AddItem("Definições", AccountSettings, MenuPosition.top);
                menu.AddItem("Alterar Credenciais", AccountCredentials, MenuPosition.top);
                menu.AddItem("Terminar sessão", Logout,  MenuPosition.bottom,0,60);
                AccountList(item, new EventArgs());
            }
            else 
            {
                Panel currentItem = null;

                menu.AddItem("Terminar sessão", Logout, MenuPosition.bottom, 0, 60);
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
            I_AccountList accountList = new I_AccountList("");
            container.Controls.Add(accountList.container);
            accountList.locationX = menu.locationX + menu.width-2;
            accountList.locationY = 0;
            accountList.width = formContainerWidth - menu.width;
            accountList.height = formContainerHeight;
            accountList.Open();
            activeContainer = accountList.container;



        }


        private void AccountCredentials(object sender, EventArgs e)
        {

            if (activeContainer != null) container.Controls.Remove(activeContainer);
            AccountCredentials accountCredentials = new AccountCredentials(idAccount, username, password);
            disposeObject = accountCredentials;
            container.Controls.Add(accountCredentials.container);
            accountCredentials.width = container.Width - menu.width;
            accountCredentials.height = container.Height;
            accountCredentials.locationX = menu.locationX + menu.width;
            accountCredentials.locationY = 0;
            accountCredentials.AdminAccount = true;
            accountCredentials.Open();
            activeContainer = accountCredentials.container;

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
            role.Open();
            general.AddItem(role.container);

            MaritalStatus maritalStatus = new MaritalStatus();
            maritalStatus.width = general.container.Width * 9 / 10;
            maritalStatus.height = 400;
            maritalStatus.Open();
            general.AddItem(maritalStatus.container);


            PrivilegesRole privilegesRole = new PrivilegesRole();
            privilegesRole.width = general.container.Width * 9 / 10;
            privilegesRole.height = 600;
            privilegesRole.Open();
            general.AddItem(privilegesRole.container);
        }


        private void Logout(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Deseja terminar a sessão?", "Terminar sessão", MessageBoxButtons.YesNo);
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

            if (disposeObject.GetType() == typeof(StatisticsForm)) ((StatisticsForm)disposeObject).Dispose();
            else if (disposeObject.GetType() == typeof(I_Person)) ((I_Person)disposeObject).container.Dispose();
            else if (disposeObject.GetType() == typeof(AccountCredentials)) ((AccountCredentials)disposeObject).container.Dispose();
            else if (disposeObject.GetType() == typeof(I_PersonView)) ((I_PersonView)disposeObject).container.Dispose();
            else if (disposeObject.GetType() == typeof(VisualizarOcorrencia)) ((VisualizarOcorrencia)disposeObject).Dispose();
            else if (disposeObject.GetType() == typeof(PrisonersManager)) ((PrisonersManager)disposeObject).container.Dispose();
            else if (disposeObject.GetType() == typeof(I_CamGallery)) ((I_CamGallery)disposeObject).container.Dispose();
            else if (disposeObject.GetType() == typeof(EditPrisioner)) ((EditPrisioner)disposeObject).container.Dispose();
            else if (disposeObject.GetType() == typeof(DeletePrisioner)) ((DeletePrisioner)disposeObject).container.Dispose();
            else if (disposeObject.GetType() == typeof(Visit)) ((Visit)disposeObject).container.Dispose();
            else if (disposeObject.GetType() == typeof(VisitManager)) ((VisitManager)disposeObject).container.Dispose();
            else if (disposeObject.GetType() == typeof(DeleteVisit)) ((DeleteVisit)disposeObject).container.Dispose();
            else if (disposeObject.GetType() == typeof(EditVisit)) ((EditVisit)disposeObject).container.Dispose();
            else if (disposeObject.GetType() == typeof(viewCamNRec)) ((viewCamNRec)disposeObject).container.Dispose();
            else if (disposeObject.GetType() == typeof(Edit_Incidents)) ((Edit_Incidents)disposeObject).Dispose();
            else if (disposeObject.GetType() == typeof(Incidents)) ((Incidents)disposeObject).container.Dispose();




            if (((Button)sender).Name == "Privilégio Estatística-Consultar" || val == "Privilégio Estatística-Consultar")
            {

                    StatisticsForm statisticsForm = new StatisticsForm();
                    disposeObject = statisticsForm;
                    statisticsForm.TopLevel = false;
                    statisticsForm.FormBorderStyle = FormBorderStyle.None;
                    statisticsForm.Width = container.Width - menu.width;
                    statisticsForm.Height = container.Height;
                    statisticsForm.Location = new Point(menu.locationX + menu.width, 23);
                    container.Controls.Add(statisticsForm);
                    statisticsForm.BringToFront();
                    statisticsForm.Show();

            }


            if (((Button)sender).Name == "Privilégio Funcionário-Registar" || val == "Privilégio Funcionário-Registar")
            {
     
                I_Person person = new I_Person();
                disposeObject = person;
                container.Controls.Add(person.container);
                person.width = container.Width - menu.width;
                person.height = container.Height;
                person.locationX = menu.locationX + menu.width;
                person.locationY = 0;
                person.Open(true,false);

            }


            if (((Button)sender).Name == "Privilégio Recluso-Registar" || val == "Privilégio Recluso-Registar")
            {

                I_Person person = new I_Person();
                disposeObject = person;
                container.Controls.Add(person.container);
                person.width = container.Width - menu.width;
                person.height = container.Height;
                person.locationX = menu.locationX + menu.width;
                person.locationY = 0;
                person.Open(false,false);

            }


            if (((Button)sender).Name == "Privilégio Conta-Alterar Credenciais" || val == "Privilégio Conta-Alterar Credenciais")
            {

                AccountCredentials accountCredentials = new AccountCredentials(idAccount,username,password);
                disposeObject = accountCredentials;
                container.Controls.Add(accountCredentials.container);
                accountCredentials.width = container.Width - menu.width;
                accountCredentials.height = container.Height;
                accountCredentials.locationX = menu.locationX + menu.width;
                accountCredentials.locationY = 0;
                accountCredentials.Open();

            }

            if (((Button)sender).Name == "Privilégio Funcionário-Consultar" || val == "Privilégio Funcionário-Consultar")
            {

                I_PersonView employee = new I_PersonView();
                disposeObject = employee;
                container.Controls.Add(employee.container);
                employee.width = container.Width - menu.width;
                employee.height = container.Height;
                employee.locationX = menu.locationX + menu.width;
                employee.locationY = 0;
                employee.Open(option.viewEmployee);

            }


            if (((Button)sender).Name == "Privilégio Funcionário-Editar" || val == "Privilégio Funcionário-Editar")
            {

                I_PersonView employee = new I_PersonView();
                disposeObject = employee;
                container.Controls.Add(employee.container);
                employee.width = container.Width - menu.width;
                employee.height = container.Height;
                employee.locationX = menu.locationX + menu.width;
                employee.locationY = 0;
                employee.Open(option.editEmployee);

            }



            if (((Button)sender).Name == "Privilégio Funcionário-Eliminar" || val == "Privilégio Funcionário-Eliminar")
            {

                I_PersonView employee = new I_PersonView();
                disposeObject = employee;
                container.Controls.Add(employee.container);
                employee.width = container.Width - menu.width;
                employee.height = container.Height;
                employee.locationX = menu.locationX + menu.width;
                employee.locationY = 0;
                employee.Open(option.deleteEmployee);

            }



            if (((Button)sender).Name == "Privilégio Ocorrência-Registar" || val == "Privilégio Ocorrência-Registar")
            {

                Incidents incidents = new Incidents();
                disposeObject = incidents;
                container.Controls.Add(incidents.container);
                incidents.width = container.Width - menu.width;
                incidents.height = container.Height;
                incidents.locationX = menu.locationX + menu.width;
                incidents.locationY = 0;
                incidents.Open();

            }


            if (((Button)sender).Name == "Privilégio Recluso-Consultar" || val == "Privilégio Recluso-Consultar")
            {

                I_PersonView prisoner = new I_PersonView();
                disposeObject = prisoner;
                container.Controls.Add(prisoner.container);
                prisoner.width = container.Width - menu.width;
                prisoner.height = container.Height;
                prisoner.locationX = menu.locationX + menu.width;
                prisoner.locationY = 0;
                prisoner.Open(option.viewPrisoner);


            }

            if (((Button)sender).Name == "Privilégio Recluso-Editar" || val == "Privilégio Recluso-Editar")
            {

                //EditPrisioner ep = new EditPrisioner();
                //disposeObject = ep;
                //container.Controls.Add(ep.container);
                //ep.width = container.Width - menu.width;
                //ep.height = container.Height;
                //ep.locationX = menu.locationX + menu.width;
                //ep.locationY = 0;
                //ep.Open();

                I_PersonView employee = new I_PersonView();
                disposeObject = employee;
                container.Controls.Add(employee.container);
                employee.width = container.Width - menu.width;
                employee.height = container.Height;
                employee.locationX = menu.locationX + menu.width;
                employee.locationY = 0;
                employee.Open(option.editPrisoner);

            }

            if (((Button)sender).Name == "Privilégio Recluso-Eliminar" || val == "Privilégio Recluso-Eliminar")
            {

                //DeletePrisioner dp = new DeletePrisioner();
                //disposeObject = dp;
                //container.Controls.Add(dp.container);
                //dp.width = container.Width - menu.width;
                //dp.height = container.Height;
                //dp.locationX = menu.locationX + menu.width;
                //dp.locationY = 0;
                //dp.Open();

                I_PersonView employee = new I_PersonView();
                disposeObject = employee;
                container.Controls.Add(employee.container);
                employee.width = container.Width - menu.width;
                employee.height = container.Height;
                employee.locationX = menu.locationX + menu.width;
                employee.locationY = 0;
                employee.Open(option.deletePrisoner);

            }

            if (((Button)sender).Name == "Privilégio Câmara-Consultar Deteção" || val == "Privilégio Câmara-Consultar Deteção")
            {

                I_CamGallery camGallery = new I_CamGallery();
                disposeObject = camGallery;
                container.Controls.Add(camGallery.container);
                camGallery.width = container.Width - menu.width;
                camGallery.height = container.Height;
                camGallery.locationX = menu.locationX + menu.width;
                camGallery.locationY = 0;
                camGallery.Open();

            }

           

            if (((Button)sender).Name == "Privilégio Visita-Registar" || val == "Privilégio Visita-Registar")
            {

                Visit visit = new Visit();
                disposeObject = visit;
                container.Controls.Add(visit.container);
                visit.width = container.Width - menu.width;
                visit.height = container.Height;
                visit.locationX = menu.locationX + menu.width;
                visit.locationY = 0;
                visit.Open();

            }

            if (((Button)sender).Name == "Privilégio Visita-Consultar" || val == "Privilégio Visita-Consultar")
            {

                VisitManager VM = new VisitManager();
                disposeObject = VM;
                container.Controls.Add(VM.container);
                VM.width = container.Width - menu.width;
                VM.height = container.Height;
                VM.locationX = menu.locationX + menu.width;
                VM.locationY = 0;
                VM.Open();

            }

            if (((Button)sender).Name == "Privilégio Visita-Eliminar" || val == "Privilégio Visita-Eliminar")
            {

                DeleteVisit dv = new DeleteVisit();
                disposeObject = dv;
                container.Controls.Add(dv.container);
                dv.width = container.Width - menu.width;
                dv.height = container.Height;
                dv.locationX = menu.locationX + menu.width;
                dv.locationY = 0;
                dv.Open();


            }

            if (((Button)sender).Name == "Privilégio Visita-Editar" || val == "Privilégio Visita-Editar")
            {

                EditVisit ev = new EditVisit();
                disposeObject = ev;
                container.Controls.Add(ev.container);
                ev.width = container.Width - menu.width;
                ev.height = container.Height;
                ev.locationX = menu.locationX + menu.width;
                ev.locationY = 0;
                ev.Open();


            }

            if (((Button)sender).Name == "Privilégio Câmara-Consultar" || val == "Privilégio Câmara-Consultar")
            {

                viewCamNRec vcnorecognition = new viewCamNRec();
                disposeObject = vcnorecognition;
                container.Controls.Add(vcnorecognition.container);
                vcnorecognition.width = container.Width - menu.width;
                vcnorecognition.height = container.Height;
                vcnorecognition.locationX = menu.locationX + menu.width;
                vcnorecognition.locationY = 0;
                vcnorecognition.Open();


            }





            if (((Button)sender).Name == "Privilégio Ocorrência-Consultar" || val == "Privilégio Ocorrência-Consultar")
            {


                VisualizarOcorrencia VS = new VisualizarOcorrencia();
                disposeObject = VS;
                VS.TopLevel = false;
                VS.FormBorderStyle = FormBorderStyle.None;
                VS.Width = container.Width - menu.width;
                VS.Height = container.Height;
                VS.Location = new Point(menu.locationX + menu.width, 23);
                container.Controls.Add(VS);
               // VS.BringToFront();
                VS.Show();


            }



            if (((Button)sender).Name == "Privilégio Ocorrência-Editar" || val == "Privilégio Ocorrência-Editar")
            {

                Edit_Incidents ED = new Edit_Incidents();
                disposeObject = ED;
                ED.TopLevel = false;
                ED.FormBorderStyle = FormBorderStyle.None;
                ED.Width = container.Width - menu.width;
                ED.Height = container.Height;
                ED.Location = new Point(menu.locationX + menu.width, 23);
                container.Controls.Add(ED);
                //ED.BringToFront();
                ED.Show();

            }



            if (((Button)sender).Name == "Privilégio Ocorrência-Apagar" || val == "Privilégio Ocorrência-Apagar")
            {

                ApagarOcorrencia AP = new ApagarOcorrencia();
                disposeObject = AP;
                AP.TopLevel = false;
                AP.FormBorderStyle = FormBorderStyle.None;
                AP.Width = container.Width - menu.width;
                AP.Height = container.Height;
                AP.Location = new Point(menu.locationX + menu.width, 23);
                container.Controls.Add(AP);
                AP.BringToFront();
                AP.Show();

            }






        }


    }
}
