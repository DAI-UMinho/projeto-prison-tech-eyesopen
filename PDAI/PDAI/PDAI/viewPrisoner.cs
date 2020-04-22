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
        Panel employee_interface, save, editPanel, editPanelBorder;
        PictureBox photo;
        Label lFullName, lBirthDate, lCC, lMaritalStatus, tFullName, tCC, cbMaritalStatus, tBirthDate;
        Button edit, addImg, back;
        Database db;
        string type = "Prisioneiro", recordsFolder = "", select;
        int fontSize = 13, saveWidth, saveHeight;

        public viewPrisoner(Panel content_interface, int content_width, int content_height, string str)
        {
            select = str;
            save = content_interface;
            saveWidth = content_width;
            saveHeight = content_height;

            PrisonersManager pm = new PrisonersManager();

            font = new Font_Class();
            db = new Database();

            employee_interface = new Panel();
            employee_interface.Size = new Size(content_width, content_height);
            employee_interface.Location = new Point(0, 0);
            content_interface.Controls.Add(employee_interface);
            employee_interface.BackColor = Color.FromArgb(196, 196, 196);

            back = new Button();
            employee_interface.Controls.Add(back);
            //back.Image = Properties.Resources.;
            //back.SizeMode = PictureBoxSizeMode.StretchImage;


            editPanelBorder = new Panel();
            employee_interface.Controls.Add(editPanelBorder);
            editPanelBorder.Location = new Point((content_width / 5), (content_width / 14));
            editPanelBorder.Size = new Size(1000, 707);
            editPanelBorder.BackColor = Color.Black;
            editPanelBorder.SendToBack();

            editPanel = new Panel();
            editPanelBorder.Controls.Add(editPanel);
            editPanel.Location = new Point((content_width / 5), (content_width / 14));
            editPanel.Size = new Size(993, 700);
            editPanel.BackColor = Color.FromArgb(128, 128, 128);
            editPanel.BringToFront();
            editPanel.Dock = DockStyle.Fill;

            photo = new PictureBox();
            photo.Size = new Size(250, 250);
            photo.Location = new Point(content_width * 1 / 8, content_height * 1 / 8);
            editPanel.Controls.Add(photo);
            //photo.BackColor = Color.Beige;
            photo.Image = Properties.Resources.preso1;
            photo.SizeMode = PictureBoxSizeMode.StretchImage;


            lFullName = new Label();
            lFullName.Size = new Size((content_width - photo.Location.X - photo.Width - ((content_width * 1 / 25))) * 7 / 10, 25);
            lFullName.Location = new Point(photo.Location.X + photo.Width + (content_width * 2 / 25), content_height * 1 / 10);
            lFullName.Text = "Nome Completo:";
            font.Size(lFullName, fontSize);
            editPanel.Controls.Add(lFullName);
            lFullName.ForeColor = Color.FromArgb(192, 192, 192);

            tFullName = new Label();
            tFullName.Size = new Size(lFullName.Width, lFullName.Height);
            tFullName.Location = new Point(lFullName.Location.X, lFullName.Location.Y + lFullName.Height);
            tFullName.Text = db.select.selecRecluso(str)[0].ToString();
            font.Size(tFullName, fontSize);
            editPanel.Controls.Add(tFullName);
            tFullName.ForeColor = Color.White;



            lBirthDate = new Label();
            lBirthDate.Size = new Size(tFullName.Width, tFullName.Height);
            lBirthDate.Location = new Point(tFullName.Location.X, tFullName.Location.Y + tFullName.Height + 40);
            font.Size(lBirthDate, fontSize);
            lBirthDate.Text = "Data de Nascimento:";
            editPanel.Controls.Add(lBirthDate);
            lBirthDate.ForeColor = Color.FromArgb(192, 192, 192);


            tBirthDate = new Label();
            tBirthDate.Size = new Size(150, lFullName.Height);
            tBirthDate.Location = new Point(lBirthDate.Location.X, lBirthDate.Location.Y + lBirthDate.Height);
            font.Size(tBirthDate, fontSize);
            editPanel.Controls.Add(tBirthDate);
            tBirthDate.Text = db.select.selecRecluso(str)[1].ToString();
            tBirthDate.ForeColor = Color.White;



            lCC = new Label();
            lCC.Size = new Size(tFullName.Width, tFullName.Height);
            lCC.Location = new Point(tBirthDate.Location.X, tBirthDate.Location.Y + tBirthDate.Height + 40);
            font.Size(lCC, fontSize);
            editPanel.Controls.Add(lCC);
            lCC.Text = "Cartão de Cidadão:";
            lCC.ForeColor = Color.FromArgb(192, 192, 192);

            tCC = new Label();
            tCC.Size = new Size(200, lFullName.Height);
            tCC.Location = new Point(lCC.Location.X, lCC.Location.Y + lCC.Height);
            font.Size(tCC, fontSize);
            editPanel.Controls.Add(tCC);
            tCC.Text = db.select.selecRecluso(str)[2].ToString();
            tCC.ForeColor = Color.White;



            lMaritalStatus = new Label();
            lMaritalStatus.Size = new Size(tFullName.Width, tFullName.Height);
            lMaritalStatus.Location = new Point(tCC.Location.X, tCC.Location.Y + tCC.Height + 40);
            font.Size(lMaritalStatus, fontSize);
            editPanel.Controls.Add(lMaritalStatus);
            lMaritalStatus.Text = "Estado Civil:";
            lMaritalStatus.ForeColor = Color.FromArgb(192, 192, 192);

            cbMaritalStatus = new Label();
            cbMaritalStatus.Size = new Size(200, lFullName.Height);
            cbMaritalStatus.Location = new Point(lMaritalStatus.Location.X, lMaritalStatus.Location.Y + lMaritalStatus.Height);
            font.Size(cbMaritalStatus, fontSize);
            editPanel.Controls.Add(cbMaritalStatus);
            cbMaritalStatus.Text = db.select.selecRecluso(str)[3].ToString();
            cbMaritalStatus.ForeColor = Color.White;

            edit = new Button();
            edit.Size = new Size(150, 60);
            edit.Location = new Point(editPanel.Width + editPanel.Width * 1/5, content_height);
            edit.Text = "Editar";
            font.Size(edit, fontSize);
            employee_interface.Controls.Add(edit);
            edit.Click += new EventHandler(Edit_Click);
            edit.BackColor = Color.FromArgb(127, 127, 127);
            edit.ForeColor = Color.White;
            edit.Cursor = Cursors.Hand;

        }

        private void Edit_Click(object sender, EventArgs e)
        {
            save.Controls.Clear();
            EditPrisoner ep = new EditPrisoner(save, saveWidth, saveHeight, select);
        }


    }
}