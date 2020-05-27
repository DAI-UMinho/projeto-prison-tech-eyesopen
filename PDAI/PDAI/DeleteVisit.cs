using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using PDAI;

namespace PDAI
{
    class DeleteVisit
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
        List<object> names = new List<object>();
        TableLayoutPanel tabela;
        ComboBox cbMaritalStatus;
        Select count = new Select();
        Button b, guardar, addImg;
        Label l, ldata, lId, lFullName, tFullName, lVisitDate, tVisitDate, lPrisionerVisited, cbPrisionerVisited;
        ListView lv;
        Font_Class font;
        public static String select;
        Panel save, row;
        int saveWidth, saveHeight, fontSize = 13;
        string var;
        string label, nome, id_visit;
        public DeleteVisit()
        {
            container = new Panel();
            db = new Database();
        }

        public void createTable()
        {
            Select s = new Select();
            names = s.Visit();
            font = new Font_Class();


            for (int i = 0; i < names.Count; i = (i + 3))
            {

                row = new Panel();
                tabela.Controls.Add(row, 0, i);
                row.Size = new Size(984, 60);
                row.BorderStyle = BorderStyle.FixedSingle;
                row.MouseHover += new EventHandler(row_MouseEnter);
                //row.MouseLeave += new EventHandler(row_MouseLeave);

                Button b = new Button();
                row.Controls.Add(b);
                b.Image = Properties.Resources.delete;
                //b.Text = "Eliminar";
                b.Size = new Size(85, 60);
                //b.BackColor = Color.FromArgb(127, 127, 127);
                b.Click += new EventHandler(b_Click);
                b.Cursor = Cursors.Hand;
                b.Dock = DockStyle.Right;

                ldata = new Label();
                row.Controls.Add(ldata);
                ldata.Text = "Data: " + names[i + 1].ToString();
                ldata.Size = new Size(400, 60);
                ldata.BackColor = Color.Transparent;
                font.Size(ldata, 13);
                ldata.TextAlign = ContentAlignment.MiddleLeft;
                ldata.Dock = DockStyle.Left;

                l = new Label();
                row.Controls.Add(l);
                l.Text = "Visitante: " + names[i].ToString();
                l.Size = new Size(400, 60);
                l.BackColor = Color.Transparent;
                font.Size(l, 13);
                l.TextAlign = ContentAlignment.MiddleLeft;
                l.Dock = DockStyle.Left;

                lId = new Label();
                lId.Text = names[i + 2].ToString();
                row.Controls.Add(lId);
                lId.BackColor = Color.Transparent;
                lId.Dock = DockStyle.Left;
                lId.Visible = false;

                ldata.Name = "labelData" + i;
                l.Name = "labelNome" + i;
                lId.Name = "Id" + i;
                b.Name = "btn" + i;


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

        private void b_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Tem a certeza que quer proceder com a eliminação ??",
                                     "",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                // If 'Yes', do something here.
                //var = Char.GetNumericValue((sender as Button).Name.ToString(), 3);
                var = (sender as Label).Name.ToString().Substring(9);
                label = "Id" + var;
                var control = tabela.Controls.Find(label, true)[0];
                id_visit = control.Text.ToString();

                System.Diagnostics.Debug.WriteLine(nome);
                Delete d = new Delete();
                d.visita(id_visit);
                container.Controls.Clear();
                Open();

            }
            else
            {
                // If 'No', do something here.
            }

        }
    }
}
