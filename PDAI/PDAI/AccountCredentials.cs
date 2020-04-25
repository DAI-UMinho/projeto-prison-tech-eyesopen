using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace PDAI
{
    class AccountCredentials
    {

        public Panel container { get; }
        public int locationX { set { container.Location = new Point(value, container.Location.Y); } get { return container.Location.X; } }
        public int locationY { set { container.Location = new Point(container.Location.X, value); } get { return container.Location.Y; } }
        public int width { set { container.Size = new Size(value, container.Height); } get { return container.Width; } }
        public int height { set { container.Size = new Size(container.Width, value); } get { return container.Height; } }

        Font_Class font;
        Database database;
        int fontSize = 13;
        int margin = 50;
        uint idLogin;
        string username, password;
        TextBox tusermane,tpasswordOld, tpasswordNew, tpasswordNew2;

        public AccountCredentials(uint idLogin, string username, string password)
        {
            font = new Font_Class();
            database = new Database();
            container = new Panel();

            this.idLogin = idLogin;
            this.username = username;
            this.password = password;

        }


        public void Open()
        {

            Label title = new Label();
            title.Text = "Alterar nome do utilizador";
            title.ForeColor = Color.FromArgb(127, 127, 127);
            title.Size = new Size(container.Width, 30);
            title.Location = new Point(margin, container.Height * 1 / 20);
            container.Controls.Add(title);
            font.Size(title, fontSize+3);

            Panel line = new Panel();
            line.BackColor = Color.FromArgb(127, 127, 127);
            line.Size = new Size(container.Width - 2*margin, 1);
            line.BackColor = Color.Black;
            line.Location = new Point(margin, title.Location.Y + title.Height);
            container.Controls.Add(line);


            Label lusermane = new Label();
            lusermane.Text = "Nome do utilizador";
            lusermane.Size = new Size(250, 23);
            lusermane.Location = new Point(line.Location.X + 10, line.Location.Y + container.Height * 1/15);
            container.Controls.Add(lusermane);
            font.Size(lusermane, fontSize - 3);

            tusermane = new TextBox();
            tusermane.Text = username;
            tusermane.Size = new Size(250, 30);
            tusermane.Location = new Point(lusermane.Location.X, lusermane.Location.Y + lusermane.Height + 3);
           // tusermane.TextChanged += new EventHandler(Text_Changed);
            container.Controls.Add(tusermane);
            font.Size(tusermane, fontSize - 3);


            Button changeUsername = new Button();
            changeUsername.Text = "Alterar";
            changeUsername.Size = new Size(100, 50);
            changeUsername.Location = new Point(tusermane.Location.X, tusermane.Location.Y + tusermane.Height + container.Height * 1 / 20);
            changeUsername.Click += new EventHandler(ChangeUsername_Click);
            container.Controls.Add(changeUsername);
            font.Size(changeUsername, fontSize - 3);

            

            Label title2 = new Label();
            title2.Text = "Alterar palavra-passe";
            title2.ForeColor = Color.FromArgb(127, 127, 127);
            title2.Size = new Size(container.Width, 30);
            title2.Location = new Point(margin, changeUsername.Location.Y + changeUsername.Height + container.Height * 1 / 10);
            container.Controls.Add(title2);
            font.Size(title2, fontSize + 3);

            Panel line2 = new Panel();
            line2.BackColor = Color.FromArgb(127, 127, 127);
            line2.Size = new Size(container.Width - 2 * margin, 1);
            line2.BackColor = Color.Black;
            line2.Location = new Point(margin, title2.Location.Y + title2.Height);
            container.Controls.Add(line2);


            Label lpasswordOld = new Label();
            lpasswordOld.Text = "Palavra-passe atual";
            lpasswordOld.Size = new Size(250, 23);
            lpasswordOld.Location = new Point(lusermane.Location.X, line2.Location.Y + container.Height * 1 / 15);
            container.Controls.Add(lpasswordOld);
            font.Size(lpasswordOld, fontSize - 3);

            tpasswordOld = new TextBox();
            tpasswordOld.Size = new Size(250, 30);
            tpasswordOld.PasswordChar = '*';
            tpasswordOld.Location = new Point(lpasswordOld.Location.X, lpasswordOld.Location.Y + lpasswordOld.Height + 3);
            // tusermane.TextChanged += new EventHandler(Text_Changed);
            container.Controls.Add(tpasswordOld);
            font.Size(tpasswordOld, fontSize - 3);


            Label lpasswordNew = new Label();
            lpasswordNew.Text = "Palavra-passe nova";
            lpasswordNew.Size = new Size(250, 23);
            lpasswordNew.Location = new Point(tpasswordOld.Location.X, tpasswordOld.Location.Y + tpasswordOld.Height + container.Height * 1 / 20);
            container.Controls.Add(lpasswordNew);
            font.Size(lpasswordNew, fontSize - 3);

            tpasswordNew = new TextBox();
            tpasswordNew.Size = new Size(250, 30);
            tpasswordNew.PasswordChar = '*';
            tpasswordNew.Location = new Point(lpasswordNew.Location.X, lpasswordNew.Location.Y + lpasswordNew.Height + 3);
            // tusermane.TextChanged += new EventHandler(Text_Changed);
            container.Controls.Add(tpasswordNew);
            font.Size(tpasswordNew, fontSize - 3);

            Label lpasswordNew2 = new Label();
            lpasswordNew2.Text = "Repete a palavra-passe nova";
            lpasswordNew2.Size = new Size(250, 23);
            lpasswordNew2.Location = new Point(tpasswordNew.Location.X, tpasswordNew.Location.Y + tpasswordNew.Height + container.Height * 1 / 20);
            container.Controls.Add(lpasswordNew2);
            font.Size(lpasswordNew2, fontSize - 3);

            tpasswordNew2 = new TextBox();
            tpasswordNew2.Size = new Size(250, 30);
            tpasswordNew2.PasswordChar = '*';
            tpasswordNew2.Location = new Point(lpasswordNew2.Location.X, lpasswordNew2.Location.Y + lpasswordNew2.Height + 3);
            // tusermane.TextChanged += new EventHandler(Text_Changed);
            container.Controls.Add(tpasswordNew2);
            font.Size(tpasswordNew2, fontSize - 3);


            Button changePass = new Button();
            changePass.Text = "Alterar";
            changePass.Size = new Size(100, 50);
            changePass.Location = new Point(tpasswordNew2.Location.X, tpasswordNew2.Location.Y + tpasswordNew2.Height + container.Height * 1 / 20);
            changePass.Click += new EventHandler(ChangePassword_Click);
            container.Controls.Add(changePass);
            font.Size(changePass, fontSize - 3);


        }



        private void ChangeUsername_Click(object sender, EventArgs e)
        {
            if (tusermane.Text != username && tusermane.Text.Length <= 50)
            {
                if (!database.select.UsernameExists(tusermane.Text))
                {
                    if (database.update.Credentials(idLogin, tusermane.Text, Encryption.AccountAccessEncryption(tusermane.Text + password))) MessageBox.Show("Nome de utilizador alterado com sucesso!");
                    else MessageBox.Show("Ocorreu um erro. Não foi possível executar a alteração.");
                }
                else { MessageBox.Show("O Nome de utilizador já existe."); }
            }
            else { MessageBox.Show("Altere o nome de utilizador."); }
        }



        private void ChangePassword_Click(object sender, EventArgs e)
        {
            if (tpasswordOld.Text == password)
            {
                if (tpasswordNew2.Text == tpasswordNew.Text)
                {
                    if (database.update.Credentials(idLogin, tusermane.Text, Encryption.AccountAccessEncryption(tusermane.Text + tpasswordNew.Text)))
                    {
                        tpasswordOld.Text = "";
                        tpasswordNew.Text = "";
                        tpasswordNew2.Text = "";
                        MessageBox.Show("Palavra-passe alterada com sucesso!");
                    }
                    else { MessageBox.Show("Ocorreu um erro. Não foi possível executar a alteração."); }
                }
                else { MessageBox.Show("A nova palavra-passe não coincide"); }
            }
            else { MessageBox.Show("palavra-passe incorreta"); }
        }



    }
}
