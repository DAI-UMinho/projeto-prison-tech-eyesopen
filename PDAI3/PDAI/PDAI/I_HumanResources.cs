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
      
        Button statistics, cams, employees, prisoners, incident, Logout;
        Panel menu, content_interface;
        Font_Class font;
        I_CamGallery camGallery;
        Color color = Color.FromArgb(127, 127, 127);
        int content_height, content_width, fontSize = 12, width, height;
        Form form;

        public I_HumanResources(Form form, int width, int height)
        {

            font = new Font_Class();
            this.form = form;
            this.width = width;
            this.height = height;

            menu = new Panel();
            form.Controls.Add(menu);
            menu.Size = new Size(width * 1/11, height);
            menu.Location = new Point(0, 0);
            menu.BackColor = Color.White;
            menu.BorderStyle = BorderStyle.Fixed3D;


            int buttonHeight = 80;
            int buttonWidth = menu.Width - 5;
            int buttonLocationX = 1;
            statistics = new Button();
            menu.Controls.Add(statistics);
            statistics.Size = new Size(buttonWidth, buttonHeight);
            statistics.Location = new Point(buttonLocationX, 0);
            statistics.Click += new EventHandler(Statistics_Click);
            font.Size(statistics, fontSize);
            statistics.Text = "Estatísticas";
            statistics.BackColor = color;


            cams = new Button();
            menu.Controls.Add(cams);
            cams.Size = new Size(buttonWidth, buttonHeight);
            cams.Location = new Point(buttonLocationX, statistics.Location.Y + statistics.Height);
            cams.Click += new EventHandler(Cams_Click);
            font.Size(cams, fontSize);
            cams.Text = "Câmaras";
            cams.BackColor = color;

            employees = new Button();
            menu.Controls.Add(employees);
            employees.Size = new Size(buttonWidth, buttonHeight);
            employees.Location = new Point(buttonLocationX, cams.Location.Y + cams.Height);
            employees.Click += new EventHandler(Employees_Click);
            font.Size(employees, fontSize);
            employees.Text = "Funcionário";
            employees.BackColor = color;

            prisoners = new Button();
            menu.Controls.Add(prisoners);
            prisoners.Size = new Size(buttonWidth, buttonHeight);
            prisoners.Location = new Point(buttonLocationX, employees.Location.Y + employees.Height);
            prisoners.Click += new EventHandler(Prisoners_Click);
            font.Size(prisoners, fontSize);
            prisoners.Text = "Prisioneiro";
            prisoners.BackColor = color;


            incident = new Button();
            menu.Controls.Add(incident);
            incident.Size = new Size(buttonWidth, buttonHeight);
            incident.Location = new Point(buttonLocationX, prisoners.Location.Y + prisoners.Height);
            incident.Click += new EventHandler(Incident_Click);
            font.Size(incident, fontSize);
            incident.Text = "Ocorrência";
            incident.BackColor = color;


            Logout = new Button();
            menu.Controls.Add(Logout);
            Logout.Size = new Size(buttonWidth, buttonHeight);
            Logout.Location = new Point(buttonLocationX, height- Logout.Height);
            Logout.Click += new EventHandler(Logout_Click);
            font.Size(Logout, fontSize);
            Logout.Text = "Sair";
            Logout.BackColor = color;



            content_interface = new Panel();
            form.Controls.Add(content_interface);
            content_interface.Size = new Size(width - menu.Width, height);
            content_interface.Location = new Point(menu.Location.X+ menu.Width, 0);
            content_interface.BorderStyle = BorderStyle.Fixed3D;
            content_interface.BackColor = Color.White;


            content_width = content_interface.Width;
            content_height = content_interface.Height;

            camGallery = new I_CamGallery(content_interface);

            InitialPage();
        }

        private void Statistics_Click(object sender, EventArgs e)
        {
            InitialPage();
        }

        private void InitialPage()
        {
            Stop_Last_Task();
            openChildForm(new StatisticsForm());
        }


        private void Cams_Click(object sender, EventArgs e)
        {
            Stop_Last_Task();

            //foreach (PictureBox contentor_cam in camGallery.contentor_cams)
            //{
            //    content_interface.Controls.Add(contentor_cam);
            //}
            if(camGallery.contentor_cams.Count < 10) camGallery.AddNewCam(0, "webcam");

            camGallery.Start();


        }


        private void Employees_Click(object sender, EventArgs e)
        {
            Stop_Last_Task();

            Employee employee = new Employee(content_interface,content_width,content_width);


        }


        private void Prisoners_Click(object sender, EventArgs e)
        {
            Stop_Last_Task();

            Prisoner prisoner = new Prisoner(content_interface, content_width, content_width);

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



        private void Stop_Cams()
        {
            

            //camsList[0].StopWebcam();
            //foreach (Cam c in camsList)
            //{
            //    c.StopWebcam();
            //}
        }


        private void Stop_Last_Task()
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

    }
}
