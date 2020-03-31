using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PDAI
{
    class I_editar
    {
        Font_Class font;
        PictureBox photo;
        Label lFullName, lBirthDate, lCC, lMaritalStatus;
        TextBox tFullName, tCC;
        ComboBox cbMaritalStatus;
        DateTimePicker tBirthDate;
        Button Alterarpw, Editar;
        int content_height, content_width, fontSize = 12, width, height;
        Form form;

        public I_editar(Form form, int width, int height)
        {

            font = new Font_Class();
            this.form = form;
            this.width = width;
            this.height = height;


            photo = new PictureBox();
            photo.Size = new Size(250, 250);
            photo.Location = new Point(content_width * 1 / 30, content_height * 1 / 20);
            //photo.BackColor = Color.Beige;
            photo.Image = Properties.Resources.preso1;
            photo.SizeMode = PictureBoxSizeMode.StretchImage;


            lFullName = new Label();
            lFullName.Size = new Size((content_width - photo.Location.X - photo.Width - ((content_width * 1 / 25))) * 7 / 10, 25);
            lFullName.Location = new Point(photo.Location.X + photo.Width + (content_width * 2 / 25), content_height * 1 / 10);
            lFullName.Text = "Nome Completo";
            font.Size(lFullName, fontSize);



            tFullName = new TextBox();
            tFullName.Size = new Size(lFullName.Width, lFullName.Height);
            tFullName.Location = new Point(lFullName.Location.X, lFullName.Location.Y + lFullName.Height);
            font.Size(tFullName, fontSize);




            lBirthDate = new Label();
            lBirthDate.Size = new Size(tFullName.Width, tFullName.Height);
            lBirthDate.Location = new Point(tFullName.Location.X, tFullName.Location.Y + tFullName.Height + 40);
            lBirthDate.Text = "Data Nascimento";
            font.Size(lBirthDate, fontSize);



            tBirthDate = new DateTimePicker();
            tBirthDate.Size = new Size(150, lFullName.Height);
            tBirthDate.Location = new Point(lBirthDate.Location.X, lBirthDate.Location.Y + lBirthDate.Height);
            tBirthDate.Format = DateTimePickerFormat.Short;
            font.Size(tBirthDate, fontSize);




            lCC = new Label();
            lCC.Size = new Size(tFullName.Width, tFullName.Height);
            lCC.Location = new Point(tBirthDate.Location.X, tBirthDate.Location.Y + tBirthDate.Height + 40);
            lCC.Text = "Cartão Cidadão";
            font.Size(lCC, fontSize);


            tCC = new TextBox();
            tCC.Size = new Size(200, lFullName.Height);
            tCC.Location = new Point(lCC.Location.X, lCC.Location.Y + lCC.Height);
            font.Size(tCC, fontSize);



            lMaritalStatus = new Label();
            lMaritalStatus.Size = new Size(tFullName.Width, tFullName.Height);
            lMaritalStatus.Location = new Point(tCC.Location.X, tCC.Location.Y + tCC.Height + 40);
            lMaritalStatus.Text = "Estado Civil";
            font.Size(lMaritalStatus, fontSize);


            cbMaritalStatus = new ComboBox();
            cbMaritalStatus.Size = new Size(200, lFullName.Height);
            cbMaritalStatus.Location = new Point(lMaritalStatus.Location.X, lMaritalStatus.Location.Y + lMaritalStatus.Height);
            font.Size(cbMaritalStatus, fontSize);

            cbMaritalStatus.Items.Add("Solteiro(a)");
            cbMaritalStatus.Items.Add("Casado(a)");
            cbMaritalStatus.Items.Add("Divorciado(a)");
            cbMaritalStatus.Items.Add("Viúvo(a)");
            cbMaritalStatus.Items.Add("Separado(a)");


            Alterarpw = new Button();
            Alterarpw.Size = new Size(150, 60);
            Alterarpw.Location = new Point(cbMaritalStatus.Location.X, cbMaritalStatus.Location.Y + cbMaritalStatus.Height + 50);
            Alterarpw.Text = "Alterar palavra-passe";
            font.Size(Alterarpw, fontSize);


            Editar = new Button();
            Editar.Size = new Size(150, 60);
            Alterarpw.Location = new Point(Alterarpw.Location.X, Alterarpw.Location.Y + Alterarpw.Height + 50);
            Alterarpw.Text = "Alterar palavra-passe";
            font.Size(Alterarpw, fontSize);


        }
    }
}
