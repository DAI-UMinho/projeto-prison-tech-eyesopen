using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace PDAI
{
    class I_Account
    {
        public Panel container { get; }
        public int locationX { set { container.Location = new Point(value, container.Location.Y); } get { return container.Location.X; } }
        public int locationY { set { container.Location = new Point(container.Location.X, value); } get { return container.Location.Y; } }
        public int width { set { container.Size = new Size(value, container.Height); } get { return container.Width; } }
        public int height { set { container.Size = new Size(container.Width, value); } get { return container.Height; } }

        Account account;
        Font_Class font;
        Database database;
        PictureBox photo;
        Label lFullName, lPrivilegeRole, lRole;
        TextBox tFullName, tRole;
        ComboBox cbPrivilegeRole;
        Button registration, credentials;
        Dictionary<string, uint> idsPrivilegeRoles;
        Dictionary<string, List<string>> privilegesRole;
        int fontSize = 13;
        string path = Rule.TargetPath;
        AccountItem accountItem;
        string currentRole;
        ObjectList privilegesList;
        Dictionary<CheckBox, CheckBox> GetTitleCheckBox;
        CheckBox currentTitleCheckBox;
        Dictionary<CheckBox, PrivilegeItem> GetPrivilegeItem;
        bool autoCheck;
        List<string> allPrivileges;
        string user, pass, privilegeRole;

        public I_Account(AccountItem accountItem)
        {
            this.accountItem = accountItem;

            font = new Font_Class();
            database = new Database();

            container = new Panel();
            container.BackColor = Color.White;
            photo = new PictureBox();

            account = new Account();
            privilegesList = new ObjectList();
            idsPrivilegeRoles = new Dictionary<string, uint>();
            privilegesRole = new Dictionary<string, List<string>>();
            GetTitleCheckBox = new Dictionary<CheckBox, CheckBox>();
            GetPrivilegeItem = new Dictionary<CheckBox, PrivilegeItem>();
            autoCheck = false;
            allPrivileges = new List<string>();
            foreach (string privilege in Rule.GetPrivileges())
            {
                Array arrayTest = privilege.Split('-');
                if (arrayTest.Length > 1)
                {
                    allPrivileges.Add(privilege);
                }
            }

        }


        public void Open()
        {
          

            photo.Size = new Size(250, 250);
            photo.Location = new Point(container.Width * 1 / 30, container.Height * 1 / 20);
            container.Controls.Add(photo);
            // photo.Image = Properties.Resources.Anotação_2020_03_07_232413;
            photo.SizeMode = PictureBoxSizeMode.StretchImage;
            photo.BorderStyle = BorderStyle.Fixed3D;
            //photo.BackColor = Color.Gray;
            photo.BackColor = Color.White;
            try { photo.Image = new Bitmap(@"" + accountItem.imagePath); } catch (Exception) { }

            if (photo.Image == null)
            {
                Label ladicionarImg = new Label();
                ladicionarImg.Size = new Size(photo.Width - 1, photo.Height - 1);
                ladicionarImg.Location = new Point(0, 0);
                //ladicionarImg.DoubleClick += new EventHandler(Photo_DoubleClick);
                ladicionarImg.Text = "Sem" + System.Environment.NewLine + "Imagem";
                ladicionarImg.ForeColor = Color.LightGray;
                ladicionarImg.TextAlign = ContentAlignment.MiddleCenter;
                font.Size(ladicionarImg, fontSize);
                photo.Controls.Add(ladicionarImg);
            }




            lFullName = new Label();
            lFullName.Size = new Size((container.Width - photo.Location.X - photo.Width - ((container.Width * 1 / 25))) * 7 / 10, 25);
            lFullName.Location = new Point(photo.Location.X + photo.Width + (container.Width * 2 / 25), photo.Location.Y);
            lFullName.Text = "Nome Completo";
            font.Size(lFullName, fontSize);
            container.Controls.Add(lFullName);


            tFullName = new TextBox();
            tFullName.ReadOnly = true;
            tFullName.Text = accountItem.name.Text;
            tFullName.Size = new Size(lFullName.Width, lFullName.Height);
            tFullName.Location = new Point(lFullName.Location.X, lFullName.Location.Y + lFullName.Height);
            font.Size(tFullName, fontSize);
            container.Controls.Add(tFullName);

            lRole = new Label();
            lRole.Size = new Size(tFullName.Width, tFullName.Height);
            lRole.Location = new Point(tFullName.Location.X, tFullName.Location.Y + tFullName.Height + 40);
            lRole.Text = "Cargo";
            font.Size(lRole, fontSize);
            container.Controls.Add(lRole);

            tRole = new TextBox();
            tRole.ReadOnly = true;
            tRole.Text = accountItem.employeeRole.Text;
            tRole.Size = new Size(150, lFullName.Height);
            tRole.Location = new Point(lRole.Location.X, lRole.Location.Y + lRole.Height);
            font.Size(tRole, fontSize);
            container.Controls.Add(tRole);



            lPrivilegeRole = new Label();
            lPrivilegeRole.Size = new Size(tFullName.Width, tFullName.Height);
            lPrivilegeRole.Location = new Point(tFullName.Location.X, tRole.Location.Y + tRole.Height + 40);
            lPrivilegeRole.Text = "Papel";
            font.Size(lPrivilegeRole, fontSize);
            container.Controls.Add(lPrivilegeRole);

            cbPrivilegeRole = new ComboBox();
            cbPrivilegeRole.Size = new Size(350, lFullName.Height);
            cbPrivilegeRole.Location = new Point(lPrivilegeRole.Location.X, lPrivilegeRole.Location.Y + lPrivilegeRole.Height);
            cbPrivilegeRole.TextChanged += new EventHandler(PrivilegeRoleChanged);
            font.Size(cbPrivilegeRole, fontSize);
            container.Controls.Add(cbPrivilegeRole);
            idsPrivilegeRoles = database.select.GetPrivilegesRole(cbPrivilegeRole);
            //for (int i = 0; i < cbPrivilegeRole.Items.Count; i++)
            //{
            //    privilegesRole = database.select.GetPrivileges(privilegeRoleIds[cbPrivilegeRole.Items[i].ToString()], privilegesRole);
            //}

            privilegeRole = database.select.GetPrivilegeRole(accountItem.id);

            registration = new Button();
            registration.Size = new Size(150, 60);
            registration.Location = new Point(tRole.Location.X, container.Height- registration.Height - 10);
            if (privilegeRole == string.Empty) { registration.Text = "Registar"; registration.Click += new EventHandler(credentials_Click); }
            else { registration.Text = "Alterar"; cbPrivilegeRole.Text = privilegeRole; currentRole = cbPrivilegeRole.Text; }
            font.Size(registration, fontSize);
            container.Controls.Add(registration);
            registration.Click += new EventHandler(Registration_Click);

            credentials = new Button();
            credentials.Size = new Size(150, 60);
            credentials.Location = new Point(registration.Location.X + registration.Width + 10, container.Height - credentials.Height - 10);
            credentials.Text = "Credenciais";
            font.Size(credentials, fontSize);
            container.Controls.Add(credentials);
            credentials.Click += new EventHandler(credentials_Click);

            if (privilegeRole == string.Empty)
            {
                credentials.Visible = false;
            }
        }


        private void credentials_Click(object sender, EventArgs e)
        {
            user = "user" + accountItem.id;
            Random random = new Random();
            pass = "";
            for (int i = 0; i < 5; i++)
            {
                if (random.Next(2) == 0)
                {
                    pass += random.Next(9);
                }
                else
                {
                    pass += (char)(random.Next(26) + 65);
                }
            }

            if (privilegeRole != string.Empty)
            {
                DialogResult dialogResult = MessageBox.Show("Já existem credenciais da conta.\nAo prosseguir irá gerar novas credenciais automáticamente.\n\nPretende continuar?", "Credenciais", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    byte[] accountAccess = Encryption.AccountAccessEncryption(user + pass);
                    database.update.Credentials(accountItem.id, user, accountAccess);
                    MessageBox.Show("CREDENCIAIS\n\nIdentificação: " + user + "\nPassword: " + pass);
                }
            }

        }


        private void Registration_Click(object sender, EventArgs e)
        {  
            try
            {
                if (cbPrivilegeRole.Text != string.Empty)
                {
                    uint idRole = 0;
                    if (!database.select.PrivilegeRoleExists(cbPrivilegeRole.Text))
                    {
                        privilegesRole[cbPrivilegeRole.Text] = privilegesRole[currentRole];
                        currentRole = cbPrivilegeRole.Text;

                        idRole = database.insert.PrivilegesRole(cbPrivilegeRole.Text);
                        database.insert.SetPrivileges(idRole, allPrivileges);
                    }
                    else { idRole = database.select.GetIdPrivilegeRole(cbPrivilegeRole.Text); }
                        

                    idsPrivilegeRoles = database.select.GetPrivilegesRole(cbPrivilegeRole);

                    foreach (string privilege in allPrivileges)
                    {
                        try
                        {
                            database.update.Privileges(Convert.ToUInt32(idsPrivilegeRoles[cbPrivilegeRole.Text]), privilege, privilegesRole[cbPrivilegeRole.Text].Contains(privilege));
                        }
                        catch (Exception err) { MessageBox.Show(err + ""); }
                    }

                    if (registration.Text == "Registar")
                    {
                        //insert login
                        registration.Click -= new EventHandler(credentials_Click);
                        account.CreateAccount(accountItem.id, user, pass);
                        //--------

                        database.insert.AssocRole(accountItem.id, idRole);

                        ((Button)sender).Text = "Alterar";
                        credentials.Visible = true;
                        MessageBox.Show("Conta criada com sucesso.\n\nCREDENCIAIS\n\nIdentificação: " + user + "\nPassword: " + pass);
                    }
                    else
                    {
                        database.update.AssocRole(accountItem.id, idRole);
                        MessageBox.Show("Conta alterada com sucesso.");
                    }

                    privilegesRole = new Dictionary<string, List<string>>();
                    PrivilegeRoleChanged(cbPrivilegeRole, new EventArgs());
                }
               
            }
            catch (Exception) { MessageBox.Show("Ocorreu um erro."); }

        }


        private void PrivilegeRoleChanged(object sender, EventArgs e)
        {

            if (idsPrivilegeRoles.ContainsKey(((ComboBox)sender).Text))
            {
                //privilegesRole = database.select.GetPrivileges(Convert.ToUInt32(idsPrivilegeRoles[((ComboBox)sender).Text]), privilegesRole);
                privilegesRole = database.select.GetPrivileges(database.select.GetIdPrivilegeRole(((ComboBox)sender).Text), privilegesRole);
                createPrivilegeList(Convert.ToUInt32(idsPrivilegeRoles[((ComboBox)sender).Text]));
            }
        }



        private void createPrivilegeList(uint id)
        {

            if (cbPrivilegeRole.Items.Count != 0)
            {
                
                currentRole = cbPrivilegeRole.Text;

                if (container.Contains(privilegesList.container)) container.Controls.Remove(privilegesList.container);
                privilegesList = new ObjectList();
                privilegesList.container.Visible = false;
                privilegesList.locationX = cbPrivilegeRole.Location.X;
                privilegesList.locationY = cbPrivilegeRole.Location.Y + cbPrivilegeRole.Height + 10;
                privilegesList.width = tFullName.Width;
                privilegesList.height = registration.Location.Y - privilegesList.locationY-10;
                container.Controls.Add(privilegesList.container);

                string val = "";
                PrivilegeItem privileges = new PrivilegeItem(CheckBoxContentChanged);
                foreach (string privilege in Rule.GetPrivileges())
                {
                    string privilegeTitle = privilege.Split('-').GetValue(0).ToString();

                    if (val == privilegeTitle)
                    {
                        string thePrivilege = privilege.Split('-').GetValue(1).ToString();
                        CheckBox checkBox = new CheckBox();
                        privileges.AddPrivilege(id, thePrivilege, privilege, 10, false, privilegesRole[cbPrivilegeRole.Text].Contains(privilege), checkBox);
                        GetTitleCheckBox[checkBox] = currentTitleCheckBox;
                        if (privilegesRole[cbPrivilegeRole.Text].Contains(privilege)) setPrivilegeTitleAsChecked(checkBox);

                    }
                    else
                    {
                        privileges = new PrivilegeItem(CheckBoxContentChanged);
                        privileges.locationX = 0;
                        privileges.locationY = 0;
                        privileges.width = privilegesList.width;
                        privileges.height = 0;
                        privilegesList.AddItem(privileges.container);
                        CheckBox checkBox = new CheckBox();
                        privileges.AddPrivilege(id, privilegeTitle, privilege, 12, true, false, checkBox);
                        //privileges.AddPrivilege(id, thePrivilege, privilege, 10, false, privilegesRole[lv.Items[index].SubItems[0].Text].Contains(privilege));
                        val = privilegeTitle;
                        GetPrivilegeItem[checkBox] = privileges;
                        currentTitleCheckBox = checkBox;

                    }
                }

                privilegesList.Update();
                privileges.Update();
                privilegesList.container.Visible = true;
            }
        }


        private void CheckBoxContentChanged(object sender, EventArgs e)
        {
            
            if (((CheckBox)sender).Name.Split('-').GetValue(0).ToString() == "title")
            {
                if (!autoCheck)
                    foreach (CheckBox ckb in GetPrivilegeItem[((CheckBox)sender)].privilegesCheckBoxes[((CheckBox)sender).Name.Split('-').GetValue(1).ToString()])
                    {
                        if (((CheckBox)sender).Checked) ckb.Checked = true;
                        else ckb.Checked = false;
                    }
            }
            else
            {
                if (((CheckBox)sender).Checked) { privilegesRole[currentRole].Add(((CheckBox)sender).Name); setPrivilegeTitleAsChecked(((CheckBox)sender)); }
                else
                {
                   // MessageBox.Show("currentRole: " + currentRole);
                    privilegesRole[currentRole].Remove(((CheckBox)sender).Name);
                    int counter = 0;
                    foreach (CheckBox ckb in GetPrivilegeItem[GetTitleCheckBox[((CheckBox)sender)]].privilegesCheckBoxes[GetTitleCheckBox[((CheckBox)sender)].Name.Split('-').GetValue(1).ToString()])
                    {
                        if (ckb.Checked) counter++;
                    }
                    if (counter == 0)
                    {
                        autoCheck = true;
                        GetTitleCheckBox[((CheckBox)sender)].Checked = false;
                    }
                }
            }
          
            if (!autoCheck)
            {
                if (database.select.PrivilegeRoleExists(cbPrivilegeRole.Text))
                {
                    byte val = database.select.GetNextCustomizedRole(cbPrivilegeRole.Text);
                    if (val == 1) cbPrivilegeRole.Text = cbPrivilegeRole.Text + " Personalizado";
                    else cbPrivilegeRole.Text = cbPrivilegeRole.Text + " Personalizado " + val ;

                    privilegesRole[cbPrivilegeRole.Text] = privilegesRole[currentRole];
                    currentRole = cbPrivilegeRole.Text;

                }
            }
         
            autoCheck = false;
        }


        private void setPrivilegeTitleAsChecked(CheckBox checkBox)
        {
            if (!GetTitleCheckBox[checkBox].Checked)
            {
                autoCheck = true;
                GetTitleCheckBox[checkBox].Checked = true;
            }
        }









    }
}
