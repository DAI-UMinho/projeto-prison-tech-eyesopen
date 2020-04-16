using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace PDAI
{
    class EditPrisoner
    {
        Font_Class font;
        Panel employee_interface, save;
        PictureBox photo;
        Label lFullName, lBirthDate, lCC, lMaritalStatus;
        TextBox tFullName, tCC;
        ComboBox cbMaritalStatus;
        DateTimePicker tBirthDate;
        Button guardar, addImg, cancel;
        Database db;
        string type = "Prisioneiro", recordsFolder = "", select;
        int fontSize = 13, saveWidth, saveHeight;

        public EditPrisoner(Panel content_interface, int content_width, int content_height, string str)
        {
            font = new Font_Class();
            db = new Database();
            select = str;
            save = content_interface;
            saveHeight = content_height;
            saveWidth = content_width;

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
            lFullName.Text = "Nome Completo";
            font.Size(lFullName, fontSize);
            employee_interface.Controls.Add(lFullName);


            tFullName = new TextBox();
            tFullName.Size = new Size(lFullName.Width, lFullName.Height);
            tFullName.Location = new Point(lFullName.Location.X, lFullName.Location.Y + lFullName.Height);
            tFullName.Text = db.select.selecRecluso(str)[0].ToString();
            font.Size(tFullName, fontSize);
            employee_interface.Controls.Add(tFullName);



            lBirthDate = new Label();
            lBirthDate.Size = new Size(tFullName.Width, tFullName.Height);
            lBirthDate.Location = new Point(tFullName.Location.X, tFullName.Location.Y + tFullName.Height + 40);
            lBirthDate.Text = "Data Nascimento";
            font.Size(lBirthDate, fontSize);
            employee_interface.Controls.Add(lBirthDate);


            tBirthDate = new DateTimePicker();
            tBirthDate.Size = new Size(150, lFullName.Height);
            tBirthDate.Location = new Point(lBirthDate.Location.X, lBirthDate.Location.Y + lBirthDate.Height);
            tBirthDate.Format = DateTimePickerFormat.Short;
            font.Size(tBirthDate, fontSize);
            tBirthDate.Value = DateTime.Parse(db.select.selecRecluso(str)[1].ToString());
            employee_interface.Controls.Add(tBirthDate);



            lCC = new Label();
            lCC.Size = new Size(tFullName.Width, tFullName.Height);
            lCC.Location = new Point(tBirthDate.Location.X, tBirthDate.Location.Y + tBirthDate.Height + 40);
            lCC.Text = "Cartão Cidadão";
            font.Size(lCC, fontSize);
            employee_interface.Controls.Add(lCC);

            tCC = new TextBox();
            tCC.Size = new Size(200, lFullName.Height);
            tCC.Location = new Point(lCC.Location.X, lCC.Location.Y + lCC.Height);
            font.Size(tCC, fontSize);
            tCC.Text = db.select.selecRecluso(str)[2].ToString();
            employee_interface.Controls.Add(tCC);


            lMaritalStatus = new Label();
            lMaritalStatus.Size = new Size(tFullName.Width, tFullName.Height);
            lMaritalStatus.Location = new Point(tCC.Location.X, tCC.Location.Y + tCC.Height + 40);
            lMaritalStatus.Text = "Estado Civil";
            font.Size(lMaritalStatus, fontSize);
            employee_interface.Controls.Add(lMaritalStatus);

            cbMaritalStatus = new ComboBox();
            cbMaritalStatus.Size = new Size(200, lFullName.Height);
            cbMaritalStatus.Location = new Point(lMaritalStatus.Location.X, lMaritalStatus.Location.Y + lMaritalStatus.Height);
            font.Size(cbMaritalStatus, fontSize);
            employee_interface.Controls.Add(cbMaritalStatus);
            cbMaritalStatus.Items.Add("Solteiro(a)");
            cbMaritalStatus.Items.Add("Casado(a)");
            cbMaritalStatus.Items.Add("Divorciado(a)");
            cbMaritalStatus.Items.Add("Viúvo(a)");
            cbMaritalStatus.Items.Add("Separado(a)");
            cbMaritalStatus.SelectedItem = db.select.selecRecluso(str)[3].ToString();


            guardar = new Button();
            guardar.Size = new Size(150, 60);
            guardar.Location = new Point(cbMaritalStatus.Location.X, cbMaritalStatus.Location.Y + cbMaritalStatus.Height + 50);
            guardar.Text = "Guardar";
            font.Size(guardar, fontSize);
            employee_interface.Controls.Add(guardar);
            guardar.Click += new EventHandler(Guardar_Click);
            guardar.BackColor = Color.FromArgb(127, 127, 127);
            guardar.ForeColor = Color.White;
            guardar.Cursor = Cursors.Hand;

            addImg = new Button();
            addImg.Size = new Size(250, 60);
            addImg.Location = new Point(content_width * 1 / 30, content_height * 1 / 5);
            employee_interface.Controls.Add(addImg);
            addImg.Text = "Adicionar Imagem";
            addImg.BackColor = Color.FromArgb(127, 127, 127);
            addImg.ForeColor = Color.White;
            addImg.Cursor = Cursors.Hand;
            font.Size(addImg, fontSize);

            cancel = new Button();
            cancel.Size = new Size(150, 60);
            cancel.Location = new Point(cbMaritalStatus.Location.X + guardar.Width, cbMaritalStatus.Location.Y + cbMaritalStatus.Height + 50);
            cancel.Text = "Cancelar";
            font.Size(cancel, fontSize);
            employee_interface.Controls.Add(cancel);
            cancel.Click += new EventHandler(Cancel_Click);
            cancel.BackColor = Color.FromArgb(127, 127, 127);
            cancel.ForeColor = Color.White;
            cancel.Cursor = Cursors.Hand;




        }

        private void Guardar_Click(object sender, EventArgs e)
        {
            //Guarda foto na pasta registos

            db.update.Recluso(tFullName.Text, tBirthDate.Text, tCC.Text, cbMaritalStatus.Text, select);
            MessageBox.Show("Alterações guardadas com sucesso!!");
            select = tFullName.Text;
            save.Controls.Clear();
            viewPrisoner vp = new viewPrisoner(save, saveWidth, saveHeight, select);

        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Tem a certeza que quer proceder ??",
                         "",
                         MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                // If 'Yes', do something here.
                save.Controls.Clear();
                viewPrisoner vp = new viewPrisoner(save, saveWidth, saveHeight, select);

            }
            else
            {
                // If 'No', do something here.
            }
        }


    }
}
