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
        public Button login { get; }
        public string user { get { return tuser.Text; } }
        public string password { get { return tpassword.Text; } }
        public Label lforgottenPass { get; }

        Panel panel1, panel2;
        PictureBox iconBox;
        Image icon;
        Color color = Color.FromArgb(119, 190, 255);
        Label luser, lpassword, lcontacts0, lcontacts1, lcontacts2, ladress;
        TextBox tuser, tpassword;
        Font_Class font;
        int fontSize = 12;

        public I_Login(Form form, int width, int height)
        {
            font = new Font_Class();

            
            panel1 = new Panel();
            form.Controls.Add(panel1);
            panel1.Size = new Size(width, 2);
            panel1.Location = new Point(0, height * 1 / 10);
            panel1.BackColor = color;

            panel2 = new Panel();
            form.Controls.Add(panel2);
            panel2.Size = new Size(panel1.Width, 2);
            panel2.Location = new Point(0, height * 8 / 10);
            panel2.BackColor = color;

            icon = new Bitmap(Properties.Resources.IconEyesOpen);
            iconBox = new PictureBox();
            form.Controls.Add(iconBox);
            iconBox.Size = new Size(icon.Width, height * 1 / 10);
            iconBox.Location = new Point(0, 0);
            iconBox.Image = icon;

            luser = new Label();
            form.Controls.Add(luser);
            luser.Size = new Size(50,20);
            luser.Location = new Point((width / 2)-(luser.Width/2), height * 2/7);
            luser.Text = "Login";
            font.Size(luser, fontSize);

            tuser = new TextBox();
            form.Controls.Add(tuser);
            tuser.Size = new Size(300, 60);
            tuser.Location = new Point((width / 2) - (tuser.Width / 2), luser.Location.Y + luser.Height);
            font.Size(tuser, fontSize);


            lpassword = new Label();
            form.Controls.Add(lpassword);
            lpassword.Size = new Size(80, 20);
            lpassword.Location = new Point((width / 2) - (lpassword.Width / 2), tuser.Location.Y + tuser.Height + 50);
            lpassword.Text = "Password";
            font.Size(lpassword, fontSize);

            tpassword = new TextBox();
            form.Controls.Add(tpassword);
            tpassword.Size = new Size(tuser.Width, 60);
            tpassword.Location = new Point((width / 2) - (tpassword.Width / 2), lpassword.Location.Y + lpassword.Height);
            font.Size(tpassword, fontSize);


            login = new Button();
            form.Controls.Add(login);
            login.Size = new Size(100, 40);
            login.Location = new Point((width / 2) - (login.Width / 2), tpassword.Location.Y + tpassword.Height + 30);
            font.Size(login, fontSize);
            login.Text = "Entrar";


            lforgottenPass = new Label();
            form.Controls.Add(lforgottenPass);
            lforgottenPass.Size = new Size(145, 20);
            lforgottenPass.Location = new Point((width / 2) - (lforgottenPass.Width / 2) , login.Location.Y + login.Height + 10);
            lforgottenPass.Text = "Esqueceu-se da password?";
            lforgottenPass.ForeColor = color;
            font.Size(lforgottenPass, 8);


            lcontacts0 = new Label();
            form.Controls.Add(lcontacts0);
            lcontacts0.Size = new Size(300, 20);
            lcontacts0.Location = new Point((width * 3/4), panel2.Location.Y+10);
            lcontacts0.Text = "Contactos:";
            lcontacts0.ForeColor = Color.Black;
            font.Size(lcontacts0, 9);


            lcontacts1 = new Label();
            form.Controls.Add(lcontacts1);
            lcontacts1.Size = new Size(300, 20);
            lcontacts1.Location = new Point((width * 3 / 4), lcontacts0.Location.Y + lcontacts0.Height + 15);
            lcontacts1.Text = "Email: EyesOpen@gmail.com";
            lcontacts1.ForeColor = Color.Black;
            font.Size(lcontacts1, 9);

            lcontacts2 = new Label();
            form.Controls.Add(lcontacts2);
            lcontacts2.Size = new Size(300, 20);
            lcontacts2.Location = new Point((width * 3 / 4), lcontacts1.Location.Y + lcontacts1 .Height+ 5);
            lcontacts2.Text = "Telefone: 253464656";
            lcontacts2.ForeColor = Color.Black;
            font.Size(lcontacts2, 9);

            ladress = new Label();
            form.Controls.Add(ladress);
            ladress.Size = new Size(300, 20);
            ladress.Location = new Point(100, panel2.Location.Y + 10);
            ladress.Text = "Morada: Campus de Azurém, 4800-058 Guimarães";
            ladress.ForeColor = Color.Black;
            font.Size(ladress, 9);

        }


     


    }
}
