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
    public partial class Main : Form
    {
        Screen screen;
        I_Login i_login;
        I_Administration i_administration;
        string user, password;
        int width, height;

        public Main()
        {
            InitializeComponent();
            screen = new Screen(this);
            screen.full_Screen = true; 
        }


        private void Init()
        {
            i_login = new I_Login(this, width, height);
            i_login.login.Click += new EventHandler(Login_Click);
            i_login.lforgottenPass.Click += new EventHandler(ForgottenPassword_Click);

            //  I_Administration a = new I_Administration(this, width, height);



        }


     
        private void Login_Click(object sender, EventArgs e)
        {
            user = i_login.user;
            password = i_login.password;
            Controls.Clear();
            i_administration = new I_Administration(this, width, height);
        }


        private void ForgottenPassword_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chamar função");
        }




        private void Main_Shown(object sender, EventArgs e)
        {
            width = Width - 16;
            height = Height - 40;
            Init();
        }
    }
}
