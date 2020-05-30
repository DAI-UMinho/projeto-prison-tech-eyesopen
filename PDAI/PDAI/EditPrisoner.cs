using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using PDAI;
using System.IO;
using Emgu;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Diagnostics;

namespace PDAI
{
    class EditPrisioner
    {
        public Panel container { get; }
        public int locationX { set { container.Location = new Point(value, container.Location.Y); } get { return container.Location.X; } }
        public int locationY { set { container.Location = new Point(container.Location.X, value); } get { return container.Location.Y; } }
        public int width { set { container.Size = new Size(value, container.Height); } get { return container.Width; } }
        public int height { set { container.Size = new Size(container.Width, value); } get { return container.Height; } }

        Database db;
        Panel prisonerManager_interface, listPanel;
        PictureBox photo;
        DateTimePicker tBirthDate;
        TextBox tFullName, tCC;
        List<object> names = new List<object>();
        TableLayoutPanel tabela;
        ComboBox cbMaritalStatus;
        Select count = new Select();
        Button b, guardar, addImg;
        Label l, lFullName, lBirthDate, lCC, lMaritalStatus, titulo;
        ListView lv;
        Font_Class font;
        public static String select;
        Bitmap bitmapImage;
        Panel save, row;
        int saveWidth, saveHeight, fontSize = 13;
        double var;
        string label, nome, imgPath;

        Image<Bgr, byte> pPhoto;

        public EditPrisioner()
        {
            container = new Panel();
            db = new Database();
        }

        public void createTable()
        {
            Select s = new Select();
            names = s.Recluso();
            font = new Font_Class();


            for (int i = 0; i < names.Count; i++)
            {

                row = new Panel();
                tabela.Controls.Add(row, 0, i);
                row.Size = new Size(984, 60);
                row.BorderStyle = BorderStyle.FixedSingle;
                row.MouseHover += new EventHandler(row_MouseEnter);
                //row.MouseLeave += new EventHandler(row_MouseLeave);

                l = new Label();
                row.Controls.Add(l);
                l.Text = names[i].ToString();
                l.Size = new Size(899, 60);
                l.BackColor = Color.Transparent;
                font.Size(l, 13);
                l.TextAlign = ContentAlignment.MiddleLeft;
                l.MouseDoubleClick += new MouseEventHandler(l_MouseDoubleClick);
                l.Cursor = Cursors.Hand;
                l.Dock = DockStyle.Left;

                l.Name = "label" + i;


            }

            System.Diagnostics.Debug.WriteLine("ola");

        }


        private void row_MouseEnter(object sender, System.EventArgs e)
        {
            row.BackColor = Color.DimGray;
        }

        /*private void row_MouseLeave(object sender, EventArgs e)
        {
            row.BackColor = Color.Transparent;
        }*/




        private void l_MouseDoubleClick(Object sender, MouseEventArgs e)
        {
            Select s = new Select();
            select = (sender as Label).Text.ToString();
            container.Controls.Clear();

            photo = new PictureBox();
            photo.Size = new Size(250, 250);
            photo.Location = new Point(container.Width * 1 / 30, container.Height * 1 / 20);
            container.Controls.Add(photo);
            photo.Image = Properties.Resources.preso1;
            photo.SizeMode = PictureBoxSizeMode.StretchImage;
            photo.DoubleClick += new EventHandler(Photo_DoubleClick);

            if (db.select.prisionerPhoto(select) != null)
            {
                string[] filePaths = Directory.GetFiles(db.select.prisionerPhoto(select)[0].ToString());
                if(filePaths.Length != 0)
                {
                    pPhoto = new Image<Bgr, byte>(filePaths[0]);
                    photo.Image = pPhoto.Bitmap;
                }
                else
                {
                    photo.BackColor = Color.Beige;
                }
            }

            lFullName = new Label();
            lFullName.Size = new Size((container.Width - photo.Location.X - photo.Width - ((container.Width * 1 / 25))) * 7 / 10, 25);
            lFullName.Location = new Point(photo.Location.X + photo.Width + (container.Width * 2 / 25), container.Height * 1 / 10);
            lFullName.Text = "Nome Completo";
            font.Size(lFullName, fontSize);
            container.Controls.Add(lFullName);


            tFullName = new TextBox();
            tFullName.Size = new Size(lFullName.Width, lFullName.Height);
            tFullName.Location = new Point(lFullName.Location.X, lFullName.Location.Y + lFullName.Height);
            tFullName.Text = db.select.selecRecluso(select)[0].ToString();
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
            tBirthDate.Value = DateTime.Parse(db.select.selecRecluso(select)[1].ToString());
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
            tCC.Text = db.select.selecRecluso(select)[2].ToString();
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
            cbMaritalStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cbMaritalStatus.Items.Add("Solteiro(a)");
            cbMaritalStatus.Items.Add("Casado(a)");
            cbMaritalStatus.Items.Add("Divorciado(a)");
            cbMaritalStatus.Items.Add("Viúvo(a)");
            cbMaritalStatus.Items.Add("Separado(a)");
            cbMaritalStatus.SelectedItem = db.select.selecRecluso(select)[3].ToString();


            guardar = new Button();
            guardar.Size = new Size(150, 60);
            guardar.Location = new Point(cbMaritalStatus.Location.X, cbMaritalStatus.Location.Y + cbMaritalStatus.Height + 50);
            guardar.Text = "Guardar";
            font.Size(guardar, fontSize);
            container.Controls.Add(guardar);
            guardar.Click += new EventHandler(Guardar_Click);
            guardar.BackColor = Color.FromArgb(127, 127, 127);
            guardar.ForeColor = Color.White;
            guardar.Cursor = Cursors.Hand;

            addImg = new Button();
            addImg.Size = new Size(250, 60);
            addImg.Location = new Point(container.Width * 1 / 30, container.Height * 1 / 5);
            container.Controls.Add(addImg);
            addImg.Text = "Adicionar Imagem";
            addImg.BackColor = Color.FromArgb(127, 127, 127);
            addImg.ForeColor = Color.White;
            addImg.Cursor = Cursors.Hand;
            font.Size(addImg, fontSize);

        }

        public static Control GetUniqueControl(string controlName, Control.ControlCollection Controls)
        {
            var controls = Controls.Find(controlName, true);
            if (controls.Length > 0)
            {
                return controls[0];
            }
            return null;
        }

        public void Open()
        {
            font = new Font_Class();

            listPanel = new Panel();
            container.Controls.Add(listPanel);
            listPanel.Location = new Point((container.Width / 5), (container.Height / 14));
            listPanel.Size = new Size(993, 800);
            listPanel.BackColor = Color.White;


            tabela = new TableLayoutPanel();
            listPanel.Controls.Add(tabela);
            tabela.Dock = DockStyle.Top;
            tabela.Size = new Size(listPanel.Width, listPanel.Height);
            tabela.ColumnCount = 1;
            tabela.RowCount = count.Recluso().Count;
            tabela.AutoScroll = true;
            //tabela.CellBorderStyle = TableLayoutPanelCellBorderStyle.OutsetPartial;
            System.Diagnostics.Debug.WriteLine(Application.StartupPath);
            createTable();
        }

        private void Guardar_Click(object sender, EventArgs e)
        {
            if (tFullName.Text != string.Empty)
            {
                if (tBirthDate.Text != string.Empty)
                {
                    if(tCC.Text != string.Empty)
                    {
                        if (cbMaritalStatus.Text != string.Empty)
                        {
                            try
                            {
                                db.update.Recluso(tFullName.Text, tBirthDate.Text, tCC.Text, cbMaritalStatus.Text, select);
                                string[] filePaths = Directory.GetFiles(db.select.prisionerPhoto(select)[0].ToString());
                                if (filePaths is null)
                                {
                                    File.Delete(filePaths[0]);
                                }
                                IO_Class.CopyFile(imgPath, db.select.prisionerPhoto(select)[0].ToString());
                                MessageBox.Show("Alterações guardadas com sucesso!!");
                                select = tFullName.Text;
                                container.Controls.Clear();
                                Open();
                            }
                            catch (Exception) { MessageBox.Show("Ocorreu um erro."); }
                        }
                        else { MessageBox.Show("Campo estado civil obrigatório."); }
                    }
                    else { MessageBox.Show("Campo cartão cidadão obrigatório."); }
                }
                else { MessageBox.Show("Campo data de nascimento obrigatório."); }
            }
            else { MessageBox.Show("Campo nome obrigatório."); }
        }

        private void Photo_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            if (open.ShowDialog() == DialogResult.OK)
            {
                bitmapImage = new Bitmap(open.FileName);
                photo.Image = bitmapImage;
                imgPath = open.FileName;
            }
        }
    }
}