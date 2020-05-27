using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Timers;
using System.IO;
using Emgu.CV;
using Emgu.CV.Face;
using Emgu.CV.Structure;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Controls;


namespace PDAI
{


    class Account
    {
        public Panel container { get; }
        public int locationX { set { container.Location = new System.Drawing.Point(value, container.Location.Y); } get { return container.Location.X; } }
        public int locationY { set { container.Location = new System.Drawing.Point(container.Location.X, value); } get { return container.Location.Y; } }
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
        AForge.Controls.PictureBox pictureBox1,logo;
        Dictionary<string, object> disposeObject;
        public List<Bitmap> image { get; set; }
        System.Timers.Timer timer1;
        Label titulo;
        int fontSize = 13;
        Font_Class font;
       


        //string[] pictures = Directory.GetFiles(@"C:\Users\ASUS\Desktop\Nova pasta (4)");


        //Array[] images = { "log2.jpg", "log1.jpg" };

        int i = 0;

        //private int count = -1;

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
            font = new Font_Class();
            image = new List <Bitmap>();
            image.Add(Properties.Resources.log3png);
            image.Add(Properties.Resources.xixa);
            //image.Add(Properties.Resources.log1);
            image.Add(Properties.Resources.Violencia_1);
            image.Add(Properties.Resources.Homi);


            logo = new AForge.Controls.PictureBox();
            pictureBox1 = new AForge.Controls.PictureBox();
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
            container.Location = new System.Drawing.Point(0, 0);
            container.Size = new Size(width, height);
            container.Controls.Add(menu.container);







            pictureBox1.Size = new Size(1100,600);
            pictureBox1.Location = new System.Drawing.Point(320,50);
            container.Controls.Add(pictureBox1);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.BorderStyle = BorderStyle.Fixed3D;
            pictureBox1.BackColor = Color.White;

            pictureBox1.Image = image[image.Count - 4];







            System.Timers.Timer timer1 = new System.Timers.Timer();
            timer1 = new System.Timers.Timer(5000);
            // Hook up the Elapsed event for the timer. 
            timer1.Elapsed += new ElapsedEventHandler(timer1_Tick);

            timer1.AutoReset = true;
            timer1.Enabled = true;

            //logo.Size = new Size(170,170);
            //logo.Location = new System.Drawing.Point(1350, 0);
            //container.Controls.Add(logo);
            //logo.SizeMode = PictureBoxSizeMode.StretchImage;
            //logo.BorderStyle = BorderStyle.None;
            //logo.BackColor = Color.White;
            //logo.Image = Properties.Resources.log2;


            //titulo = new Label();
            //container.Controls.Add(titulo);
            //titulo.Size = new Size(700, 150);
            //titulo.Location = new System.Drawing.Point(570, 0);
            //font.Size(titulo, fontSize);
            //titulo.Text = "EyesOpen";
            //titulo.Font = new Font("Cambria", 55, FontStyle.Bold);
            //titulo.ForeColor = Color.Black;
            //titulo.SendToBack();



            //pictureBox1.Image = Image.FromFile("Resources/" + images[0]);






            if (VerifiedAdmin())
            {
                Panel item = menu.AddItem("Contas", AccountList, MenuPosition.top);
                menu.AddItem("Definições", AccountSettings, MenuPosition.top);
                menu.AddItem("Terminar sessão", Logout, MenuPosition.bottom, 0, 60);
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
                    else { currentItem = null; }
                }
            }

        }


        private void timer1_Tick(object sender, EventArgs e)
        {

           

            pictureBox1.Image = image[i];
            if(i== image.Count -1)
            {
                i = 0;
            } else { 
            i++;
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
            accountList.locationX = menu.locationX + menu.width - 2;
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

            if (((Button)sender).Name == "Privilégio Estatística-Consultar" || val == "Privilégio Estatística-Consultar")
            {
                if (val == "Privilégio Estatística-Consultar")
                {
                    ((StatisticsForm)disposeObject[val]).Dispose();
                    disposeObject.Remove(val);
                    stringObject.Remove(val);
                }
                else
                {
                    StatisticsForm statisticsForm = new StatisticsForm();
                    statisticsForm.TopLevel = false;
                    statisticsForm.FormBorderStyle = FormBorderStyle.None;
                    statisticsForm.Width = container.Width - menu.width;
                    statisticsForm.Height = container.Height;
                    statisticsForm.Location = new System.Drawing.Point(menu.locationX + menu.width, 23);
                    container.Controls.Add(statisticsForm);
                    statisticsForm.BringToFront();
                    statisticsForm.Show();
                    pictureBox1.Hide();



                    stringObject.Add(((Button)sender).Name);
                    disposeObject[((Button)sender).Name] = statisticsForm;
                   
                }
            }


            if (((Button)sender).Name == "Privilégio Funcionário-Registar" || val == "Privilégio Funcionário-Registar")
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
                    person.Open(true, false);
                    stringObject.Add(((Button)sender).Name);
                    disposeObject[((Button)sender).Name] = person;
                    pictureBox1.Hide();
                }

            }


            if (((Button)sender).Name == "Privilégio Recluso-Registar" || val == "Privilégio Recluso-Registar")
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
                    person.Open(false, false);
                    stringObject.Add(((Button)sender).Name);
                    disposeObject[((Button)sender).Name] = person;
                    pictureBox1.Hide();
                }

            }


            if (((Button)sender).Name == "Privilégio Conta-Alterar Credenciais" || val == "Privilégio Conta-Alterar Credenciais")
            {
                if (val == "Privilégio Conta-Alterar Credenciais")
                {
                    ((AccountCredentials)disposeObject[val]).container.Dispose();
                    disposeObject.Remove(val);
                    stringObject.Remove(val);
                }
                else
                {
                    AccountCredentials accountCredentials = new AccountCredentials(idAccount, username, password);
                    container.Controls.Add(accountCredentials.container);
                    accountCredentials.width = container.Width - menu.width;
                    accountCredentials.height = container.Height;
                    accountCredentials.locationX = menu.locationX + menu.width;
                    accountCredentials.locationY = 0;
                    accountCredentials.Open();
                    stringObject.Add(((Button)sender).Name);
                    disposeObject[((Button)sender).Name] = accountCredentials;
                    pictureBox1.Hide();
                }

            }

            if (((Button)sender).Name == "Privilégio Funcionário-Consultar" || val == "Privilégio Funcionário-Consultar")
            {
                if (val == "Privilégio Funcionário-Consultar")
                {
                    ((I_PersonView)disposeObject[val]).container.Dispose();
                    disposeObject.Remove(val);
                    stringObject.Remove(val);
                }
                else
                {
                    I_PersonView employee = new I_PersonView();
                    container.Controls.Add(employee.container);
                    employee.width = container.Width - menu.width;
                    employee.height = container.Height;
                    employee.locationX = menu.locationX + menu.width;
                    employee.locationY = 0;
                    employee.Open(option.view);
                    stringObject.Add(((Button)sender).Name);
                    disposeObject[((Button)sender).Name] = employee;
                    pictureBox1.Hide();
                }

            }


            if (((Button)sender).Name == "Privilégio Funcionário-Editar" || val == "Privilégio Funcionário-Editar")
            {
                if (val == "Privilégio Funcionário-Editar")
                {
                    ((I_PersonView)disposeObject[val]).container.Dispose();
                    disposeObject.Remove(val);
                    stringObject.Remove(val);
                }
                else
                {
                    I_PersonView employee = new I_PersonView();
                    container.Controls.Add(employee.container);
                    employee.width = container.Width - menu.width;
                    employee.height = container.Height;
                    employee.locationX = menu.locationX + menu.width;
                    employee.locationY = 0;
                    employee.Open(option.edit);
                    stringObject.Add(((Button)sender).Name);
                    disposeObject[((Button)sender).Name] = employee;
                    pictureBox1.Hide();
                }

            }



            if (((Button)sender).Name == "Privilégio Funcionário-Apagar" || val == "Privilégio Funcionário-Apagar")
            {
                if (val == "Privilégio Funcionário-Apagar")
                {
                    ((I_PersonView)disposeObject[val]).container.Dispose();
                    disposeObject.Remove(val);
                    stringObject.Remove(val);
                }
                else
                {
                    I_PersonView employee = new I_PersonView();
                    container.Controls.Add(employee.container);
                    employee.width = container.Width - menu.width;
                    employee.height = container.Height;
                    employee.locationX = menu.locationX + menu.width;
                    employee.locationY = 0;
                    employee.Open(option.delete);
                    stringObject.Add(((Button)sender).Name);
                    disposeObject[((Button)sender).Name] = employee;
                    pictureBox1.Hide();
                }

            }



            if (((Button)sender).Name == "Privilégio Ocorrência-Registar" || val == "Privilégio Ocorrência-Registar")
            {
                if (val == "Privilégio Ocorrência-Registar")
                {
                    ((Incidents)disposeObject[val]).container.Dispose();
                    disposeObject.Remove(val);
                    stringObject.Remove(val);
                }
                else
                {
                    Incidents incidents = new Incidents();
                    container.Controls.Add(incidents.container);
                    incidents.width = container.Width - menu.width;
                    incidents.height = container.Height;
                    incidents.locationX = menu.locationX + menu.width;
                    incidents.locationY = 0;
                    incidents.Open();
                    stringObject.Add(((Button)sender).Name);
                    disposeObject[((Button)sender).Name] = incidents;
                    pictureBox1.Hide();
                }

            }


            if (((Button)sender).Name == "Privilégio Recluso-Consultar" || val == "Privilégio Recluso-Consultar")
            {
                if (val == "Privilégio Recluso-Consultar")
                {
                    ((PrisonersManager)disposeObject[val]).container.Dispose();
                    disposeObject.Remove(val);
                    stringObject.Remove(val);
                }
                else
                {
                    PrisonersManager pm = new PrisonersManager();
                    container.Controls.Add(pm.container);
                    pm.width = container.Width - menu.width;
                    pm.height = container.Height;
                    pm.locationX = menu.locationX + menu.width;
                    pm.locationY = 0;
                    pm.Open();
                    stringObject.Add(((Button)sender).Name);
                    disposeObject[((Button)sender).Name] = pm;
                    pictureBox1.Hide();
                }

            }

            if (((Button)sender).Name == "Privilégio Câmara-Consultar Deteção" || val == "Privilégio Câmara-Consultar Deteção")
            {
                if (val == "Privilégio Câmara-Consultar Deteção")
                {
                    ((I_CamGallery)disposeObject[val]).container.Dispose();
                    disposeObject.Remove(val);
                    stringObject.Remove(val);
                }
                else
                {
                    I_CamGallery camGallery = new I_CamGallery();
                    container.Controls.Add(camGallery.container);
                    camGallery.width = container.Width - menu.width;
                    camGallery.height = container.Height;
                    camGallery.locationX = menu.locationX + menu.width;
                    camGallery.locationY = 0;
                    camGallery.Open();
                    stringObject.Add(((Button)sender).Name);
                    disposeObject[((Button)sender).Name] = camGallery;
                    pictureBox1.Hide();
                }

            }

            if (((Button)sender).Name == "Privilégio Recluso-Editar" || val == "Privilégio Recluso-Editar")
            {
                if (val == "Privilégio Recluso-Editar")
                {
                    ((EditPrisioner)disposeObject[val]).container.Dispose();
                    disposeObject.Remove(val);
                    stringObject.Remove(val);
                }
                else
                {
                    EditPrisioner ep = new EditPrisioner();
                    container.Controls.Add(ep.container);
                    ep.width = container.Width - menu.width;
                    ep.height = container.Height;
                    ep.locationX = menu.locationX + menu.width;
                    ep.locationY = 0;
                    ep.Open();
                    stringObject.Add(((Button)sender).Name);
                    disposeObject[((Button)sender).Name] = ep;
                    pictureBox1.Hide();
                }

            }

            if (((Button)sender).Name == "Privilégio Recluso-Apagar" || val == "Privilégio Recluso-Apagar")
            {
                if (val == "Privilégio Recluso-Apagar")
                {
                    ((DeletePrisioner)disposeObject[val]).container.Dispose();
                    disposeObject.Remove(val);
                    stringObject.Remove(val);
                }
                else
                {
                    DeletePrisioner dp = new DeletePrisioner();
                    container.Controls.Add(dp.container);
                    dp.width = container.Width - menu.width;
                    dp.height = container.Height;
                    dp.locationX = menu.locationX + menu.width;
                    dp.locationY = 0;
                    dp.Open();
                    stringObject.Add(((Button)sender).Name);
                    disposeObject[((Button)sender).Name] = dp;
                    pictureBox1.Hide();
                }

            }

            if (((Button)sender).Name == "Privilégio Visita-Registar" || val == "Privilégio Visita-Registar")
            {
                if (val == "Privilégio Visita-Registar")
                {
                    ((Visit)disposeObject[val]).container.Dispose();
                    disposeObject.Remove(val);
                    stringObject.Remove(val);
                }
                else
                {
                    Visit visit = new Visit();
                    container.Controls.Add(visit.container);
                    visit.width = container.Width - menu.width;
                    visit.height = container.Height;
                    visit.locationX = menu.locationX + menu.width;
                    visit.locationY = 0;
                    visit.Open();
                    stringObject.Add(((Button)sender).Name);
                    disposeObject[((Button)sender).Name] = visit;
                    pictureBox1.Hide();
                }

            }

            if (((Button)sender).Name == "Privilégio Visita-Consultar" || val == "Privilégio Visita-Consultar")
            {
                if (val == "Privilégio Visita-Consultar")
                {
                    ((VisitManager)disposeObject[val]).container.Dispose();
                    disposeObject.Remove(val);
                    stringObject.Remove(val);
                }
                else
                {
                    VisitManager VM = new VisitManager();
                    container.Controls.Add(VM.container);
                    VM.width = container.Width - menu.width;
                    VM.height = container.Height;
                    VM.locationX = menu.locationX + menu.width;
                    VM.locationY = 0;
                    VM.Open();
                    stringObject.Add(((Button)sender).Name);
                    disposeObject[((Button)sender).Name] = VM;
                    pictureBox1.Hide();
                }

            }

            if (((Button)sender).Name == "Privilégio Visita-Apagar" || val == "Privilégio Visita-Apagar")
            {
                if (val == "Privilégio Visita-Apagar")
                {
                    ((DeleteVisit)disposeObject[val]).container.Dispose();
                    disposeObject.Remove(val);
                    stringObject.Remove(val);
                }
                else
                {
                    DeleteVisit dv = new DeleteVisit();
                    container.Controls.Add(dv.container);
                    dv.width = container.Width - menu.width;
                    dv.height = container.Height;
                    dv.locationX = menu.locationX + menu.width;
                    dv.locationY = 0;
                    dv.Open();
                    stringObject.Add(((Button)sender).Name);
                    disposeObject[((Button)sender).Name] = dv;
                    pictureBox1.Hide();
                }

            }

            if (((Button)sender).Name == "Privilégio Visita-Editar" || val == "Privilégio Visita-Editar")
            {
                if (val == "Privilégio Visita-Editar")
                {
                    ((EditVisit)disposeObject[val]).container.Dispose();
                    disposeObject.Remove(val);
                    stringObject.Remove(val);
                }
                else
                {
                    EditVisit ev = new EditVisit();
                    container.Controls.Add(ev.container);
                    ev.width = container.Width - menu.width;
                    ev.height = container.Height;
                    ev.locationX = menu.locationX + menu.width;
                    ev.locationY = 0;
                    ev.Open();
                    stringObject.Add(((Button)sender).Name);
                    disposeObject[((Button)sender).Name] = ev;
                    pictureBox1.Hide();
                }

            }

            if (((Button)sender).Name == "Privilégio Câmara-Consultar" || val == "Privilégio Câmara-Consultar")
            {
                if (val == "Privilégio Câmara-Consultar")
                {
                    ((viewCamNRec)disposeObject[val]).container.Dispose();
                    disposeObject.Remove(val);
                    stringObject.Remove(val);
                }
                else
                {
                    viewCamNRec vcnorecognition = new viewCamNRec();
                    container.Controls.Add(vcnorecognition.container);
                    vcnorecognition.width = container.Width - menu.width;
                    vcnorecognition.height = container.Height;
                    vcnorecognition.locationX = menu.locationX + menu.width;
                    vcnorecognition.locationY = 0;
                    vcnorecognition.Open();
                    stringObject.Add(((Button)sender).Name);
                    disposeObject[((Button)sender).Name] = vcnorecognition;
                    pictureBox1.Hide();
                }

            }


        }


    }
}
