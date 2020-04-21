﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PDAI
{
    enum AccessLevel { director, secretaria, recursosHumanos, guardaChefe, guarda }

    public partial class Main : Form
    {
        Screen screen;
        I_Login i_login;
        int width, height;
        
        

        public Main()
        {
            InitializeComponent();
            screen = new Screen(this);
            screen.full_Screen = true;
        }


        private void Init()
        {
            //i_login = new I_Login(this, width, height);
            I_HumanResources i_HumanResources = new I_HumanResources(this, width, height);
        }



        private void Main_Shown(object sender, EventArgs e)
        {
            width = Width - 16;
            height = Height - 40;
            Init();
        }
    }
}
