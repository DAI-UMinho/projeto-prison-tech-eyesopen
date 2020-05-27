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
    class viewAlert
    {
        public Panel container { get; }
        public int locationX { set { container.Location = new Point(value, container.Location.Y); } get { return container.Location.X; } }
        public int locationY { set { container.Location = new Point(container.Location.X, value); } get { return container.Location.Y; } }
        public int width { set { container.Size = new Size(value, container.Height); } get { return container.Width; } }
        public int height { set { container.Size = new Size(container.Width, value); } get { return container.Height; } }

        Database db;
        Panel prisonerManager_interface, listPanel, editPanelBorder, editPanel, peoplePanel;
        List<object> names = new List<object>();
        TableLayoutPanel tabela;
        Select count = new Select();
        Button b;
        Label l, alertNumber, alertNumberOutput, dataRegisto, dataRegistoOutput, detectedPeople, detectedPeopleOutput, lMaritalStatus, cbMaritalStatus;
        ListView lv;
        Font_Class font;
        public static String select;
        Panel save, row;
        int fontSize = 13, saveWidth, saveHeight;
        double var;
        string label, nome;
        PictureBox photo;
        Image<Bgr, byte> pPhoto;

        public viewAlert()
        {

            container = new Panel();

        }

        public void createTable()
        {
            Select s = new Select();
            names = s.Alert();
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


            alertNumber = new Label();
            alertNumber.Size = new Size((container.Width - ((container.Width * 1 / 25))) * 7 / 10, 25);
            alertNumber.Location = new Point((container.Width * 2 / 25), container.Width * 1 / 10);
            alertNumber.Text = "Alerta nº:";
            font.Size(alertNumber, fontSize);
            editPanel.Controls.Add(alertNumber);
            alertNumber.ForeColor = Color.FromArgb(192, 192, 192);

            alertNumberOutput = new Label();
            alertNumberOutput.Size = new Size(alertNumber.Size.Width, alertNumber.Size.Height);
            alertNumberOutput.Location = new Point(alertNumber.Location.X, alertNumber.Location.Y + alertNumber.Size.Height);
            alertNumberOutput.Text = select;
            font.Size(alertNumberOutput, fontSize);
            editPanel.Controls.Add(alertNumberOutput);
            alertNumberOutput.ForeColor = Color.White;


            dataRegisto = new Label();
            dataRegisto.Size = new Size(alertNumberOutput.Size.Width, alertNumberOutput.Size.Height);
            dataRegisto.Location = new Point(alertNumberOutput.Location.X, alertNumberOutput.Location.Y + alertNumberOutput.Size.Height + 15);
            dataRegisto.Text = "Data de Registo:";
            font.Size(dataRegisto, fontSize);
            editPanel.Controls.Add(dataRegisto);
            dataRegisto.ForeColor = Color.FromArgb(192, 192, 192);


            dataRegistoOutput = new Label();
            dataRegistoOutput.Size = new Size(150, alertNumber.Size.Height);
            dataRegistoOutput.Location = new Point(dataRegisto.Location.X, dataRegisto.Location.Y + dataRegisto.Size.Height);
            font.Size(dataRegistoOutput, fontSize);
            editPanel.Controls.Add(dataRegistoOutput);
            dataRegistoOutput.Text = s.timeAlert(select)[0].ToString();
            dataRegistoOutput.ForeColor = Color.White;

            detectedPeople = new Label();
            detectedPeople.Size = new Size(alertNumberOutput.Size.Width, alertNumberOutput.Size.Height);
            detectedPeople.Location = new Point(dataRegistoOutput.Location.X, dataRegistoOutput.Location.Y + dataRegistoOutput.Size.Height + 15);
            font.Size(detectedPeople, fontSize);
            editPanel.Controls.Add(detectedPeople);
            detectedPeople.Text = "Pessoas Detetadas:";
            detectedPeople.ForeColor = Color.FromArgb(192, 192, 192);


            detectedPeopleOutput = new Label();
            detectedPeopleOutput.Size = new Size(editPanel.Size.Width, editPanel.Size.Height / 2);
            detectedPeopleOutput.Location = new Point(detectedPeople.Location.X, detectedPeople.Location.Y + detectedPeople.Size.Height);
            font.Size(detectedPeopleOutput, fontSize);
            editPanel.Controls.Add(detectedPeopleOutput);
            detectedPeopleOutput.ForeColor = Color.White;

            for (int i = 0; i < s.peopleAlert(select).Count; i++)
            {
                detectedPeopleOutput.Text = detectedPeopleOutput.Text + s.peopleNamesAlert(s.peopleAlert(select)[i].ToString())[0].ToString() + "\n";
            }

            System.Diagnostics.Debug.WriteLine(dataRegistoOutput.Text);
            System.Diagnostics.Debug.WriteLine(detectedPeopleOutput.Text);

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
            tabela.RowCount = count.Alert().Count;
            tabela.AutoScroll = true;
            //tabela.CellBorderStyle = TableLayoutPanelCellBorderStyle.OutsetPartial;

            createTable();
        }
    }
}