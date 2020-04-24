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

namespace PDAI
{
    class PrisonersManager
    {
        public Panel container { get; }
        public int locationX { set { container.Location = new Point(value, container.Location.Y); } get { return container.Location.X; } }
        public int locationY { set { container.Location = new Point(container.Location.X, value); } get { return container.Location.Y; } }
        public int width { set { container.Size = new Size(value, container.Height); } get { return container.Width; } }
        public int height { set { container.Size = new Size(container.Width, value); } get { return container.Height; } }

        Database db;
        Panel prisonerManager_interface, listPanel, editPanelBorder, editPanel;
        List<object> names = new List<object>();
        TableLayoutPanel tabela;
        Select count = new Select();
        Button b;
        Label l, lFullName, tFullName, lBirthDate, tBirthDate, lCC, tCC, lMaritalStatus, cbMaritalStatus;
        ListView lv;
        Font_Class font;
        public static String select;
        Panel save, row;
        int fontSize = 13, saveWidth, saveHeight;
        double var;
        string label, nome;
        PictureBox photo;
        Image<Bgr, byte> pPhoto;

        public PrisonersManager()
        {

            container = new Panel();

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
            System.Diagnostics.Debug.WriteLine(s.selecRecluso(select)[0].ToString());

            editPanelBorder = new Panel();
            container.Controls.Add(editPanelBorder);
            editPanelBorder.Location = new Point((container.Width / 5), (container.Width / 14));
            editPanelBorder.Size = new Size(1000, 707);
            editPanelBorder.BackColor = Color.Black;
            editPanelBorder.SendToBack();

            editPanel = new Panel();
            editPanelBorder.Controls.Add(editPanel);
            editPanel.Location = new Point((container.Width / 5), (container.Width / 14));
            editPanel.Size = new Size(993, 700);
            editPanel.BackColor = Color.FromArgb(128, 128, 128);
            editPanel.BringToFront();
            editPanel.Dock = DockStyle.Fill;

            photo = new PictureBox();
            photo.Size = new Size(250, 250);
            photo.Location = new Point(container.Width * 1 / 8, container.Width * 1 / 8);
            editPanel.Controls.Add(photo);
            //photo.BackColor = Color.Beige;
            photo.Image = Properties.Resources.preso1;
            photo.SizeMode = PictureBoxSizeMode.StretchImage;

            if (s.prisionerPhoto(select) != null)
            {
                string[] filePaths = Directory.GetFiles(s.prisionerPhoto(select)[0].ToString());
                pPhoto = new Image<Bgr, byte>(filePaths[0]);
                photo.Image = pPhoto.Bitmap;
            }


            lFullName = new Label();
            lFullName.Size = new Size((container.Width - photo.Location.X - photo.Width - ((container.Width * 1 / 25))) * 7 / 10, 25);
            lFullName.Location = new Point(photo.Location.X + photo.Width + (container.Width * 2 / 25), container.Width * 1 / 10);
            lFullName.Text = "Nome Completo:";
            font.Size(lFullName, fontSize);
            editPanel.Controls.Add(lFullName);
            lFullName.ForeColor = Color.FromArgb(192, 192, 192);

            tFullName = new Label();
            tFullName.Size = new Size(lFullName.Width, lFullName.Height);
            tFullName.Location = new Point(lFullName.Location.X, lFullName.Location.Y + lFullName.Height);
            tFullName.Text = s.selecRecluso(select)[0].ToString();
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
            tBirthDate.Text = s.selecRecluso(select)[1].ToString();
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
            tCC.Text = s.selecRecluso(select)[2].ToString();
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
            cbMaritalStatus.Text = s.selecRecluso(select)[3].ToString();
            cbMaritalStatus.ForeColor = Color.White;

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
            container.Controls.Clear();

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

            createTable();
        }
    }
}