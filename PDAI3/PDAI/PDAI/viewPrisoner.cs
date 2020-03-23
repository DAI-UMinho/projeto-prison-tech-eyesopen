using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using PDAI;

namespace PDAI
{
    class viewPrisoner
    {
        Font_Class font;
        Panel employee_interface;
        PictureBox photo;
        Label lFullName, lBirthDate, lCC, lMaritalStatus, tFullName, tCC, cbMaritalStatus, tBirthDate;
        Button registration, addImg;
        Database db;
        string type = "Prisioneiro", recordsFolder = "";
        int fontSize = 13;

        public viewPrisoner(Panel content_interface, int content_width, int content_height, string str)
        {
            PrisonersManager pm = new PrisonersManager();

            font = new Font_Class();
            db = new Database();

            employee_interface = new Panel();
            employee_interface.Size = new Size(content_width, content_height);
            employee_interface.Location = new Point(0, 0);
            content_interface.Controls.Add(employee_interface);
            employee_interface.BackColor = Color.FromArgb(196, 196, 196);

            photo = new PictureBox();
            photo.Size = new Size(250, 250);
            photo.Location = new Point(content_width * 1 / 30, content_height * 1 / 20);
            employee_interface.Controls.Add(photo);
            //photo.BackColor = Color.Beige;
            photo.Image = Properties.Resources.preso1;
            photo.SizeMode = PictureBoxSizeMode.StretchImage;


            lFullName = new Label();
            lFullName.Size = new Size((content_width - photo.Location.X - photo.Width - ((content_width * 1 / 25))) * 7 / 10, 25);
            lFullName.Location = new Point(photo.Location.X + photo.Width + (content_width * 2 / 25), content_height * 1 / 10);
            lFullName.Text = "Nome Completo:";
            font.Size(lFullName, fontSize);
            employee_interface.Controls.Add(lFullName);


            tFullName = new Label();
            tFullName.Size = new Size(lFullName.Width, lFullName.Height);
            tFullName.Location = new Point(lFullName.Location.X, lFullName.Location.Y + lFullName.Height);
            tFullName.Text = db.select.selecRecluso(str)[0].ToString();
            font.Size(tFullName, fontSize);
            employee_interface.Controls.Add(tFullName);



            lBirthDate = new Label();
            lBirthDate.Size = new Size(tFullName.Width, tFullName.Height);
            lBirthDate.Location = new Point(tFullName.Location.X, tFullName.Location.Y + tFullName.Height + 40);
            font.Size(lBirthDate, fontSize);
            lBirthDate.Text = "Data de Nascimento:";
            employee_interface.Controls.Add(lBirthDate);


            tBirthDate = new Label();
            tBirthDate.Size = new Size(150, lFullName.Height);
            tBirthDate.Location = new Point(lBirthDate.Location.X, lBirthDate.Location.Y + lBirthDate.Height);
            font.Size(tBirthDate, fontSize);
            employee_interface.Controls.Add(tBirthDate);
            tBirthDate.Text = db.select.selecRecluso(str)[1].ToString();



            lCC = new Label();
            lCC.Size = new Size(tFullName.Width, tFullName.Height);
            lCC.Location = new Point(tBirthDate.Location.X, tBirthDate.Location.Y + tBirthDate.Height + 40);
            font.Size(lCC, fontSize);
            employee_interface.Controls.Add(lCC);
            lCC.Text = "Cartão de Cidadão:";

            tCC = new Label();
            tCC.Size = new Size(200, lFullName.Height);
            tCC.Location = new Point(lCC.Location.X, lCC.Location.Y + lCC.Height);
            font.Size(tCC, fontSize);
            employee_interface.Controls.Add(tCC);
            tCC.Text = db.select.selecRecluso(str)[2].ToString();;


            lMaritalStatus = new Label();
            lMaritalStatus.Size = new Size(tFullName.Width, tFullName.Height);
            lMaritalStatus.Location = new Point(tCC.Location.X, tCC.Location.Y + tCC.Height + 40);
            font.Size(lMaritalStatus, fontSize);
            employee_interface.Controls.Add(lMaritalStatus);
            lMaritalStatus.Text = "Estado Civil:";

            cbMaritalStatus = new Label();
            cbMaritalStatus.Size = new Size(200, lFullName.Height);
            cbMaritalStatus.Location = new Point(lMaritalStatus.Location.X, lMaritalStatus.Location.Y + lMaritalStatus.Height);
            font.Size(cbMaritalStatus, fontSize);
            employee_interface.Controls.Add(cbMaritalStatus);
            cbMaritalStatus.Text = db.select.selecRecluso(str)[3].ToString();

        }

        private void Registration_Click(object sender, EventArgs e)
        {
            //Guarda foto na pasta registos
         
            db.insert.Person(tFullName.Text, tBirthDate.Text, tCC.Text, cbMaritalStatus.Text, type, "/pasta/Recluso");

        }


    }
}