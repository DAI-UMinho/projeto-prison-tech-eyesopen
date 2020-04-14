using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace PDAI
{
    class I_HumanResources
    {

        Button statistics, cams, employees, prisoners, incident, employeesDrop2, employeesDrop1, prisonersDrop2, prisonersDrop1, Logout;
        Panel menu, content_interface, employeesPanel, prisonersPanel;
        Font_Class font;
        I_CamGallery camGallery;
        Color color = Color.FromArgb(127, 127, 127);
        Color colorBtn = Color.FromArgb(196, 196, 196);
        int content_height, content_width, fontSize = 14, width, height;
        Form form;

        public I_HumanResources(Form form, int width, int height)
        {

            font = new Font_Class();
            this.form = form;
            this.width = width;
            this.height = height;

            menu = new Panel();
            form.Controls.Add(menu);
            menu.Size = new Size(width * 1 / 11, height);
            menu.Location = new Point(0, 0);
            menu.BackColor = color;
            menu.BorderStyle = BorderStyle.None;


            int buttonHeight = 80;
            int buttonWidth = menu.Width - 5;
            int buttonLocationX = 1;

            incident = new Button();
            menu.Controls.Add(incident);
            incident.Size = new Size(buttonWidth, buttonHeight);
            incident.Location = new Point(buttonLocationX, 0 /*prisionersPanel.Location.Y + prisionersPanel.Height*/);
            incident.Click += new EventHandler(Incident_Click);
            font.Size(incident, fontSize);
            incident.Text = "Ocorrência";
            incident.BackColor = color;
            incident.Dock = DockStyle.Top;
            incident.FlatStyle = FlatStyle.Flat;
            incident.Cursor = Cursors.Hand;
            incident.FlatAppearance.BorderSize = 0;
            incident.ForeColor = Color.White;

            Panel divider3 = new Panel();
            menu.Controls.Add(divider3);
            divider3.Size = new Size(0, 1);
            divider3.Dock = DockStyle.Top;
            divider3.BackColor = Color.Black;

            prisonersPanel = new Panel();
            menu.Controls.Add(prisonersPanel);
            prisonersPanel.Size = new Size(buttonWidth, (2 * buttonHeight));
            prisonersPanel.Location = new Point(buttonLocationX, 0);
            prisonersPanel.BackColor = colorBtn;
            prisonersPanel.Dock = DockStyle.Top;
            prisonersPanel.Visible = false;

            prisonersDrop2 = new Button();
            prisonersPanel.Controls.Add(prisonersDrop2);
            prisonersDrop2.Size = new Size(buttonWidth, buttonHeight);
            prisonersDrop2.Location = new Point(buttonLocationX, prisonersPanel.Location.Y);
            font.Size(prisonersDrop2, fontSize);
            prisonersDrop2.Text = "Gerir";
            prisonersDrop2.BackColor = colorBtn;
            prisonersDrop2.Dock = DockStyle.Top;
            prisonersDrop2.FlatStyle = FlatStyle.Flat;
            prisonersDrop2.Cursor = Cursors.Hand;
            prisonersDrop2.Click += new EventHandler(prisonersDrop2_Click);
            prisonersDrop2.FlatAppearance.BorderSize = 0;
            prisonersDrop2.ForeColor = Color.White;

            /*Panel dividerDrop = new Panel();
            prisonersPanel.Controls.Add(dividerDrop);
            dividerDrop.Size = new Size(0, 1);
            dividerDrop.Dock = DockStyle.Top;
            dividerDrop.BackColor = Color.Black;*/

            prisonersDrop1 = new Button();
            prisonersPanel.Controls.Add(prisonersDrop1);
            prisonersDrop1.Size = new Size(buttonWidth, buttonHeight);
            prisonersDrop1.Location = new Point(buttonLocationX, prisonersPanel.Location.Y);
            font.Size(prisonersDrop1, fontSize);
            prisonersDrop1.Text = "Registar";
            prisonersDrop1.BackColor = colorBtn;
            prisonersDrop1.Dock = DockStyle.Top;
            prisonersDrop1.FlatStyle = FlatStyle.Flat;
            prisonersDrop1.Cursor = Cursors.Hand;
            prisonersDrop1.Click += new EventHandler(prisonersDrop1_Click);
            prisonersDrop1.FlatAppearance.BorderSize = 0;
            prisonersDrop1.ForeColor = Color.White;


            prisoners = new Button();
            menu.Controls.Add(prisoners);
            prisoners.Size = new Size(buttonWidth, buttonHeight);
            prisoners.Location = new Point(buttonLocationX, 0 /*employeesPanel.Location.Y + employeesPanel.Height*/);
            prisoners.Click += new EventHandler(Prisoners_Click);
            font.Size(prisoners, fontSize);
            prisoners.Text = "Prisioneiro";
            prisoners.BackColor = color;
            prisoners.Dock = DockStyle.Top;
            prisoners.FlatStyle = FlatStyle.Flat;
            prisoners.Cursor = Cursors.Hand;
            prisoners.FlatAppearance.BorderSize = 0;
            prisoners.ForeColor = Color.White;

            Panel divider2 = new Panel();
            menu.Controls.Add(divider2);
            divider2.Size = new Size(0, 1);
            divider2.Dock = DockStyle.Top;
            divider2.BackColor = Color.Black;

            employeesPanel = new Panel();
            menu.Controls.Add(employeesPanel);
            employeesPanel.Size = new Size(buttonWidth, (2 * buttonHeight));
            employeesPanel.Location = new Point(buttonLocationX, 0);
            employeesPanel.BackColor = colorBtn;
            employeesPanel.Dock = DockStyle.Top;
            employeesPanel.Visible = false;

            employeesDrop2 = new Button();
            employeesPanel.Controls.Add(employeesDrop2);
            employeesDrop2.Size = new Size(buttonWidth, buttonHeight);
            employeesDrop2.Location = new Point(buttonLocationX, employeesPanel.Location.Y);
            font.Size(employeesDrop2, fontSize);
            employeesDrop2.Text = "Gerir";
            employeesDrop2.BackColor = colorBtn;
            employeesDrop2.Dock = DockStyle.Top;
            employeesDrop2.FlatStyle = FlatStyle.Flat;
            employeesDrop2.Cursor = Cursors.Hand;
            employeesDrop2.Click += new EventHandler(employeesDrop2_Click);
            employeesDrop2.FlatAppearance.BorderSize = 0;
            employeesDrop2.ForeColor = Color.White;

            /*Panel dividerDrop1 = new Panel();
            employeesPanel.Controls.Add(dividerDrop1);
            dividerDrop1.Size = new Size(0, 1);
            dividerDrop1.Dock = DockStyle.Top;
            dividerDrop1.BackColor = Color.Black;*/

            employeesDrop1 = new Button();
            employeesPanel.Controls.Add(employeesDrop1);
            employeesDrop1.Size = new Size(buttonWidth, buttonHeight);
            employeesDrop1.Location = new Point(buttonLocationX, employeesPanel.Location.Y);
            font.Size(employeesDrop1, fontSize);
            employeesDrop1.Text = "Registar";
            employeesDrop1.BackColor = colorBtn;
            employeesDrop1.Dock = DockStyle.Top;
            employeesDrop1.FlatStyle = FlatStyle.Flat;
            employeesDrop1.Cursor = Cursors.Hand;
            employeesDrop1.Click += new EventHandler(employeesDrop1_Click);
            employeesDrop1.FlatAppearance.BorderSize = 0;
            employeesDrop1.ForeColor = Color.White;

            employees = new Button();
            menu.Controls.Add(employees);
            employees.Size = new Size(buttonWidth, buttonHeight);
            employees.Location = new Point(buttonLocationX, 0 /*camsPanel.Location.Y + camsPanel.Height*/);
            employees.Click += new EventHandler(Employees_Click);
            font.Size(employees, fontSize);
            employees.Text = "Funcionário";
            employees.BackColor = color;
            employees.Dock = DockStyle.Top;
            employees.FlatStyle = FlatStyle.Flat;
            employees.Cursor = Cursors.Hand;
            employees.FlatAppearance.BorderSize = 0;
            employees.ForeColor = Color.White;

            Panel divider1 = new Panel();
            menu.Controls.Add(divider1);
            divider1.Size = new Size(0, 1);
            divider1.Dock = DockStyle.Top;
            divider1.BackColor = Color.Black;

            cams = new Button();
            menu.Controls.Add(cams);
            cams.Size = new Size(buttonWidth, buttonHeight);
            cams.Location = new Point(buttonLocationX, 0 /*statisticsPanel.Location.Y + statisticsPanel.Height*/);
            cams.Click += new EventHandler(Cams_Click);
            font.Size(cams, fontSize);
            cams.Text = "Câmaras";
            cams.BackColor = color;
            cams.Dock = DockStyle.Top;
            cams.FlatStyle = FlatStyle.Flat;
            cams.Cursor = Cursors.Hand;
            cams.FlatAppearance.BorderSize = 0;
            cams.ForeColor = Color.White;

            Panel divider = new Panel();
            menu.Controls.Add(divider);
            divider.Size = new Size(0, 1);
            divider.Dock = DockStyle.Top;
            divider.BackColor = Color.Black;

            statistics = new Button();
            menu.Controls.Add(statistics);
            statistics.Size = new Size(buttonWidth, buttonHeight);
            statistics.Location = new Point(buttonLocationX, 0);
            statistics.Click += new EventHandler(Statistics_Click);
            font.Size(statistics, fontSize);
            statistics.Text = "Estatísticas";
            statistics.BackColor = color;
            statistics.Dock = DockStyle.Top;
            statistics.FlatStyle = FlatStyle.Flat;
            statistics.Cursor = Cursors.Hand;
            statistics.FlatAppearance.BorderSize = 0;
            statistics.ForeColor = Color.White;


            Logout = new Button();
            menu.Controls.Add(Logout);
            Logout.Size = new Size(buttonWidth, buttonHeight);
            Logout.Location = new Point(buttonLocationX, height - Logout.Height);
            Logout.Click += new EventHandler(Logout_Click);
            font.Size(Logout, fontSize);
            Logout.Text = "Sair";
            Logout.BackColor = color;
            Logout.Dock = DockStyle.Bottom;
            Logout.FlatStyle = FlatStyle.Flat;
            Logout.Cursor = Cursors.Hand;
            Logout.FlatAppearance.BorderSize = 0;
            Logout.ForeColor = Color.White;



            content_interface = new Panel();
            form.Controls.Add(content_interface);
            content_interface.Size = new Size(width - menu.Width, height);
            content_interface.Location = new Point(menu.Location.X + menu.Width, 0);
            content_interface.BorderStyle = BorderStyle.None;
            content_interface.BackColor = Color.White;


            content_width = content_interface.Width;
            content_height = content_interface.Height;


            InitialPage();
        }

        private void Statistics_Click(object sender, EventArgs e)
        {
            InitialPage();
            hideSubMenu();
            //I_CamGallery i = new I_CamGallery();
            //i.StopCameras();
        }

        private void InitialPage()
        {
            Stop_Last_Task();
            hideSubMenu();
            openChildForm(new StatisticsForm());

        }


        private void Cams_Click(object sender, EventArgs e)
        {
            Stop_Last_Task();
            hideSubMenu();

            I_CamGallery camsGallery = new I_CamGallery(content_interface, content_width, content_width);



        }


        private void Employees_Click(object sender, EventArgs e)
        {
            //Stop_Last_Task();

            //Employee employee = new Employee(content_interface,content_width,content_width);

            showSubMenu(employeesPanel);

        }


        private void Prisoners_Click(object sender, EventArgs e)
        {
            //Stop_Last_Task();

            //Prisoner prisoner = new Prisoner(content_interface, content_width, content_width);

            showSubMenu(prisonersPanel);
        }


        private void Incident_Click(object sender, EventArgs e)
        {
            Stop_Last_Task();
            Incidents incidents = new Incidents(content_interface, content_width, content_width);

        }


        private void Logout_Click(object sender, EventArgs e)
        {
            form.Controls.Clear();
            I_Login i_login = new I_Login(form, width, height);
        }

        private void prisonersDrop2_Click(object sender, EventArgs e)
        {
            Stop_Last_Task();
            PrisonersManager pm = new PrisonersManager(content_interface, content_width, content_width);

        }

        private void prisonersDrop1_Click(object sender, EventArgs e)
        {
            Stop_Last_Task();
            Prisoner prisoner = new Prisoner(content_interface, content_width, content_width);
        }

        private void employeesDrop1_Click(object sender, EventArgs e)
        {
            Stop_Last_Task();
            Employee employee = new Employee(content_interface, content_width, content_width);
        }

        private void employeesDrop2_Click(object sender, EventArgs e)
        {

        }

        private void Stop_Cams()
        {


            //camsList[0].StopWebcam();
            //foreach (Cam c in camsList)
            //{
            //    c.StopWebcam();
            //}
        }


        public void Stop_Last_Task()
        {
            content_interface.Controls.Clear();
            Stop_Cams();

        }

        public void openChildForm(Form childForm)
        {

            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Size = new Size(content_width, content_height);
            childForm.Dock = DockStyle.Fill;
            content_interface.Controls.Add(childForm);
            childForm.BringToFront();
            childForm.Show();
        }

        //Codigo relativo aos btns de registar e gerir
        private void hideSubMenu()
        {
            if (employeesPanel.Visible == true)
                employeesPanel.Visible = false;
            if (prisonersPanel.Visible == true)
                prisonersPanel.Visible = false;

        }

        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hideSubMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }

    }
}
