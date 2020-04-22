using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace PDAI
{
    class I_Person
    {
        public Panel container { get; }
        public int locationX { set { container.Location = new Point(value, container.Location.Y); } get { return container.Location.X; } }
        public int locationY { set { container.Location = new Point(container.Location.X, value); } get { return container.Location.Y; } }
        public int width { set { container.Size = new Size(value, container.Height); } get { return container.Width; } }
        public int height { set { container.Size = new Size(container.Width, value); } get { return container.Height; } }

        Font_Class font;
        Database database;
        PictureBox photo;
        Label lFullName, lBirthDate, lCC, lMaritalStatus;
        TextBox tFullName, tCC;
        ComboBox cbMaritalStatus ;
        DateTimePicker tBirthDate;
        Button registration;
        Database db;
        Bitmap bitmapImage;
        string type = "";
        int fontSize = 13;
        string path = Rule.TargetPath,imgPath;

        public I_Person()
        {
            font = new Font_Class();
            database = new Database();
            db = new Database();
            this.type = "Secretária";

            container = new Panel();
            photo = new PictureBox();
            photo.DoubleClick += new EventHandler(Photo2_DoubleClick);

        }


        public void Open()
        {

            
            photo.Size = new Size(250, 250);
            photo.Location = new Point(container.Width * 1 / 30, container.Height * 1 / 20);
            container.Controls.Add(photo);
           // photo.Image = Properties.Resources.Anotação_2020_03_07_232413;
            photo.SizeMode = PictureBoxSizeMode.StretchImage;
            photo.BorderStyle = BorderStyle.Fixed3D;
            //photo.BackColor = Color.Gray;
            photo.BackColor = Color.White;

            Label ladicionarImg = new Label();
            ladicionarImg.Size = new Size(photo.Width-1, photo.Height-1);
            ladicionarImg.Location = new Point(0,0);
            ladicionarImg.DoubleClick += new EventHandler(Photo_DoubleClick);
            ladicionarImg.Text = "Adicionar" + System.Environment.NewLine + "Imagem" + System.Environment.NewLine + System.Environment.NewLine + "(Duplo Click)";
            ladicionarImg.ForeColor = Color.LightGray;
            ladicionarImg.TextAlign = ContentAlignment.MiddleCenter;
            font.Size(ladicionarImg, fontSize);
            photo.Controls.Add(ladicionarImg);


            lFullName = new Label();
            lFullName.Size = new Size((container.Width - photo.Location.X - photo.Width - ((container.Width * 1 / 25))) * 7 / 10, 25);
            lFullName.Location = new Point(photo.Location.X + photo.Width + (container.Width * 2 / 25), photo.Location.Y);
            lFullName.Text = "Nome Completo";
            font.Size(lFullName, fontSize);
            container.Controls.Add(lFullName);


            tFullName = new TextBox();
            tFullName.Size = new Size(lFullName.Width, lFullName.Height);
            tFullName.Location = new Point(lFullName.Location.X, lFullName.Location.Y + lFullName.Height);
            font.Size(tFullName, fontSize);
            container.Controls.Add(tFullName);



            lBirthDate = new Label();
            lBirthDate.Size = new Size(tFullName.Width, tFullName.Height);
            lBirthDate.Location = new Point(tFullName.Location.X, tFullName.Location.Y + tFullName.Height + 40);
            lBirthDate.Text = "Data Nascimento";
            font.Size(lBirthDate, fontSize);
            container.Controls.Add(lBirthDate);


            tBirthDate = new DateTimePicker();
            tBirthDate.Size = new Size(150, lFullName.Height);
            tBirthDate.Location = new Point(lBirthDate.Location.X, lBirthDate.Location.Y + lBirthDate.Height);
            tBirthDate.Format = DateTimePickerFormat.Short;
            font.Size(tBirthDate, fontSize);
            container.Controls.Add(tBirthDate);



            lCC = new Label();
            lCC.Size = new Size(tFullName.Width, tFullName.Height);
            lCC.Location = new Point(tBirthDate.Location.X, tBirthDate.Location.Y + tBirthDate.Height + 40);
            lCC.Text = "Cartão Cidadão";
            font.Size(lCC, fontSize);
            container.Controls.Add(lCC);

            tCC = new TextBox();
            tCC.Size = new Size(200, lFullName.Height);
            tCC.Location = new Point(lCC.Location.X, lCC.Location.Y + lCC.Height);
            font.Size(tCC, fontSize);
            container.Controls.Add(tCC);


            lMaritalStatus = new Label();
            lMaritalStatus.Size = new Size(tFullName.Width, tFullName.Height);
            lMaritalStatus.Location = new Point(tCC.Location.X, tCC.Location.Y + tCC.Height + 40);
            lMaritalStatus.Text = "Estado Civil";
            font.Size(lMaritalStatus, fontSize);
            container.Controls.Add(lMaritalStatus);

            cbMaritalStatus = new ComboBox();
            cbMaritalStatus.Size = new Size(200, lFullName.Height);
            cbMaritalStatus.Location = new Point(lMaritalStatus.Location.X, lMaritalStatus.Location.Y + lMaritalStatus.Height);
            font.Size(cbMaritalStatus, fontSize);
            container.Controls.Add(cbMaritalStatus);
            List<string> maritalStatus = database.select.GetMaritalStatus();
            foreach (string item in maritalStatus)
            {
                cbMaritalStatus.Items.Add(item);
            }


            registration = new Button();
            registration.Size = new Size(150, 60);
            registration.Location = new Point(cbMaritalStatus.Location.X, cbMaritalStatus.Location.Y + cbMaritalStatus.Height + 50);
            registration.Text = "Registar";
            font.Size(registration, fontSize);
            container.Controls.Add(registration);
            registration.Click += new EventHandler(Registration_Click);
        }



        private void Registration_Click(object sender, EventArgs e)
        {

            type = "Secretária";
            uint id = db.insert.Person(tFullName.Text, tBirthDate.Text, tCC.Text, cbMaritalStatus.Text, type);
            if (IO_Class.CreateFolder(@""+path + id.ToString()))
            {
                db.update.Person(id, path + id.ToString());
                IO_Class.CopyFile(imgPath, path + id.ToString());
            }
           

            container.Dispose();

        }


        private void Photo_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            if (open.ShowDialog() == DialogResult.OK)
            {
                bitmapImage = new Bitmap(open.FileName);
                photo.Image = bitmapImage;
                photo.Controls.Remove((Label)sender);
                imgPath = open.FileName;
            }
        }

        private void Photo2_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            if (open.ShowDialog() == DialogResult.OK)
            {
                bitmapImage = new Bitmap(open.FileName);
                photo.Image = bitmapImage;
                photo.Controls.Remove((PictureBox)sender);
                imgPath = open.FileName;
            }
        }






    }
}
