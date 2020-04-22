using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace PDAI
{
    class I_Login
    {
        public Panel loginPanel { get; }
        public Button login { get; }

        Panel panel1, panel2, panelLine1, panelLine2, panelLine3;
        PictureBox iconBox, iconBox2;
        Image icon, icon2;
        Color color = Color.FromArgb(127, 127, 127);
        Label luser, lpassword, lEmail, lcontactContent, lLocation, lcontact, lLocationContent, lEmailContent;
        TextBox tuser, tpassword;
        Font_Class font;
        int fontSize = 13, width, height;
        Form form;
        Database database;

        public I_Login(Form form, int width, int height)
        {
            
            this.form = form;
            this.width = width;
            this.height = height;

            font = new Font_Class();
            database = new Database();

            loginPanel = new Panel();
            form.Controls.Add(loginPanel);
            loginPanel.Size = new Size(width, height);
            loginPanel.Location = new Point(0, 0);
            loginPanel.BackColor = Color.White;


            panel1 = new Panel();
            loginPanel.Controls.Add(panel1);
            panel1.Size = new Size(width, height * 1 / 10);
            panel1.Location = new Point(0, 0);
            panel1.BackColor = color;

            icon = new Bitmap(Properties.Resources.log1);
            iconBox = new PictureBox();
            panel1.Controls.Add(iconBox);
            iconBox.Size = new Size(icon.Width, icon.Height);
            iconBox.Location = new Point(width/2- icon.Width/2, panel1.Height/2- icon.Height/2);
            iconBox.Image = icon;




            luser = new Label();
            loginPanel.Controls.Add(luser);
            luser.Size = new Size(300, 20);
            luser.Location = new Point(width* 3/ 5, height * 2 / 7);
            luser.Text = "Número de Identificação:";
            font.Size(luser, fontSize);

            tuser = new TextBox();
            loginPanel.Controls.Add(tuser);
            tuser.Size = new Size(luser.Width, 60);
            tuser.Location = new Point(luser.Location.X, luser.Location.Y + luser.Height);
            font.Size(tuser, fontSize+5);


            lpassword = new Label();
            loginPanel.Controls.Add(lpassword);
            lpassword.Size = new Size(luser.Width, 20);
            lpassword.Location = new Point(luser.Location.X, tuser.Location.Y + tuser.Height + 50);
            lpassword.Text = "Password:";
            font.Size(lpassword, fontSize);

            tpassword = new TextBox();
            loginPanel.Controls.Add(tpassword);
            tpassword.Size = new Size(tuser.Width, 60);
            tpassword.Location = new Point(luser.Location.X, lpassword.Location.Y + lpassword.Height);
            tpassword.PasswordChar = '*';
            font.Size(tpassword, fontSize+5);


            login = new Button();
            loginPanel.Controls.Add(login);
            login.Size = new Size(120, 40);
            login.Location = new Point(luser.Location.X + luser.Width/2 - login.Width/2, tpassword.Location.Y + tpassword.Height + 30);
            font.Size(login, fontSize+2);
            login.Text = "Login";
            login.BackColor = color;
            login.FlatStyle = FlatStyle.Flat;
            login.FlatAppearance.BorderColor = Color.Black;
            login.FlatAppearance.BorderSize = 1;
            login.Click += new EventHandler(Hash);


            panel2 = new Panel();
            loginPanel.Controls.Add(panel2);
            panel2.Size = new Size(panel1.Width, height * 2 / 10);
            panel2.Location = new Point(0, height * 8 / 10);
            panel2.BackColor = color;


            icon2 = new Bitmap(Properties.Resources.log2);
            int widthDiff = 0;
            if (icon2.Height > (panel2.Location.Y - panel1.Height)*8/10) widthDiff = icon2.Height - (panel2.Location.Y - panel1.Height) * 8 / 10;
            iconBox2 = new PictureBox();
            loginPanel.Controls.Add(iconBox2);
            iconBox2.Size = new Size(icon2.Width- widthDiff, icon2.Height- widthDiff);
            iconBox2.Location = new Point(width/4 - iconBox2.Width/2, panel1.Height + (panel2.Location.Y - panel1.Height)/2 - iconBox2.Height/2);
            iconBox2.Image = icon2;
            iconBox2.SizeMode = PictureBoxSizeMode.StretchImage;


          
            lcontact = new Label();
            panel2.Controls.Add(lcontact);
            lcontact.Size = new Size(300, 20);
            lcontact.Location = new Point(100, panel2.Height * 1 / 5);
            lcontact.Text = "CONTACTO";
            lcontact.ForeColor = Color.White;
            font.Size(lcontact, fontSize);

            lcontactContent = new Label();
            panel2.Controls.Add(lcontactContent);
            lcontactContent.Size = new Size(300, 20);
            lcontactContent.Location = new Point(lcontact.Location.X, lcontact.Location.Y + lcontact.Height + 10);
            lcontactContent.Text = "253464656";
            lcontactContent.ForeColor = Color.Black;
            font.Size(lcontactContent, fontSize);

            panelLine1 = new Panel();
            panel2.Controls.Add(panelLine1);
            panelLine1.Size = new Size(30,2);
            panelLine1.Location = new Point(lcontact.Location.X, lcontact.Location.Y- panelLine1.Height);
            panelLine1.BackColor = Color.LightBlue;



            lLocation = new Label();
            panel2.Controls.Add(lLocation);
            lLocation.Size = new Size(300, 20);
            lLocation.Location = new Point(width / 2 - lLocation.Width / 2, lcontact.Location.Y);
            lLocation.Text = "LOCALIZAÇÃO";
            lLocation.ForeColor = Color.White;
            font.Size(lLocation, fontSize);
            //lLocation.Font = new Font(lLocation.Font, FontStyle.Bold);

            lLocationContent = new Label();
            panel2.Controls.Add(lLocationContent);
            lLocationContent.Size = new Size(300, 40);
            lLocationContent.Location = new Point(lLocation.Location.X, lLocation.Location.Y + lLocation.Height + 10);
            lLocationContent.Text = "Campus de Azurém, 4800-058 Guimarães";
            lLocationContent.ForeColor = Color.Black;
            font.Size(lLocationContent, fontSize);

            panelLine2 = new Panel();
            panel2.Controls.Add(panelLine2);
            panelLine2.Size = new Size(30, 2);
            panelLine2.Location = new Point(lLocation.Location.X, lLocation.Location.Y - panelLine2.Height);
            panelLine2.BackColor = Color.LightBlue;


            lEmail = new Label();
            panel2.Controls.Add(lEmail);
            lEmail.Size = new Size(300, 20);
            lEmail.Location = new Point((width * 3 / 4), lcontact.Location.Y);
            lEmail.Text = "EMAIL";
            lEmail.ForeColor = Color.White;
            font.Size(lEmail, fontSize);
            //lLocation.Font = new Font(lLocation.Font, FontStyle.Bold);

            lEmailContent = new Label();
            panel2.Controls.Add(lEmailContent);
            lEmailContent.Size = new Size(300, 40);
            lEmailContent.Location = new Point(lEmail.Location.X, lEmail.Location.Y + lEmail.Height + 10);
            lEmailContent.Text = "eyesopen@gmail.com";
            lEmailContent.ForeColor = Color.Black;
            font.Size(lEmailContent, fontSize);

            panelLine3 = new Panel();
            panel2.Controls.Add(panelLine3);
            panelLine3.Size = new Size(30, 2);
            panelLine3.Location = new Point(lEmail.Location.X, lEmail.Location.Y - panelLine3.Height);
            panelLine3.BackColor = Color.LightBlue;

        }

        private void Hash(object sender, EventArgs e)
        {
            if (tuser.Text != string.Empty && tpassword.Text != string.Empty)
            {
                List<object> databaseValues = database.select.Login(tuser.Text);
                if (databaseValues.Count != 0)
                {
                    if (Encryption.CheckAccountAccess((byte[])databaseValues[0], Encryption.AccountAccessEncryption(tuser.Text + tpassword.Text))) Logged(tuser.Text, (byte[])databaseValues[0],Convert.ToUInt32(databaseValues[1]));
                    else MessageBox.Show("Utilizador ou password incorreto!");
                }
                else MessageBox.Show("Utilizador ou password incorreto!");
            }
        }


        private void Logged(string username, byte[] password, uint idAccount)
        {
            form.Controls.Clear();
            Account account = new Account(username, password, idAccount);
            account.LoadAccount();
            account.Open(form, width, height);
        }


        //Logged(EnumConverter<AccessLevel>(databaseValues[1]));
        private T EnumConverter<T>(object o)
        {
            T enumVal = (T)Enum.Parse(typeof(T), o.ToString());
            return enumVal;
        }


    }
}
