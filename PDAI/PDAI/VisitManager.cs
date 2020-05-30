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
    class VisitManager
    {
        public Panel container { get; }
        public int locationX { set { container.Location = new Point(value, container.Location.Y); } get { return container.Location.X; } }
        public int locationY { set { container.Location = new Point(container.Location.X, value); } get { return container.Location.Y; } }
        public int width { set { container.Size = new Size(value, container.Height); } get { return container.Width; } }
        public int height { set { container.Size = new Size(container.Width, value); } get { return container.Height; } }

        Database db;
        Panel visitManager_interface, listPanel, editPanelBorder, editPanel;
        List<object> names = new List<object>();
        TableLayoutPanel tabela;
        Select count = new Select();
        Button b;
        Label l, ldata, lId, lFullName, tFullName, lVisitDate, tVisitDate, lPrisionerVisited, cbPrisionerVisited, titulo;
        ListView lv;
        Font_Class font;
        public static String select;
        Panel save, row;
        int saveWidth, saveHeight;
        string var;
        string label, nome, id_visit;
        int id, fontSize = 13;
        public VisitManager()
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

                ldata = new Label();
                row.Controls.Add(ldata);
                ldata.Text = "Data: " + names[i + 1].ToString();
                ldata.Size = new Size(450, 60);
                ldata.BackColor = Color.Transparent;
                font.Size(ldata, 13);
                ldata.TextAlign = ContentAlignment.MiddleLeft;
                ldata.Cursor = Cursors.Hand;
                ldata.Dock = DockStyle.Left;

                l = new Label();
                row.Controls.Add(l);
                l.Text = "Visitante: " + names[i].ToString();
                l.Size = new Size(450, 60);
                l.BackColor = Color.Transparent;
                font.Size(l, 13);
                l.TextAlign = ContentAlignment.MiddleLeft;
                l.MouseDoubleClick += new MouseEventHandler(l_MouseDoubleClick);
                l.Cursor = Cursors.Hand;
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

                System.Diagnostics.Debug.WriteLine(" " + names[i].ToString() + " " + names[i + 1].ToString() + " " + names[i + 2].ToString());
            }

            

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

            //var = Char.GetNumericValue((sender as Label).Name.ToString(), 9);
            var = (sender as Label).Name.ToString().Substring(9);
            label = "Id" + var;
            var control = tabela.Controls.Find(label, true)[0];
            id_visit = control.Text.ToString();


            editPanelBorder = new Panel();
            container.Controls.Add(editPanelBorder);
            editPanelBorder.Location = new Point((container.Width / 5), (container.Width / 14));
            //editPanelBorder.Size = new Size(1000, 707);
            editPanelBorder.Size = new Size(700, 600);
            editPanelBorder.BackColor = Color.Black;
            editPanelBorder.SendToBack();

            

            editPanel = new Panel();
            editPanelBorder.Controls.Add(editPanel);
            editPanel.Location = new Point((container.Width / 5), (container.Width / 14));
            //editPanel.Size = new Size(993, 700);
            editPanel.Size = new Size(700, 600);
            editPanel.BackColor = Color.FromArgb(128, 128, 128);
            editPanel.BringToFront();
            editPanel.Dock = DockStyle.Fill;

            lFullName = new Label();
            lFullName.Size = new Size(500, 20);
            lFullName.Location = new Point(container.Width * 1 / 5, container.Height * 1 / 10);
            lFullName.Text = "Nome Completo:";
            font.Size(lFullName, fontSize);
            editPanel.Controls.Add(lFullName);
            lFullName.ForeColor = Color.FromArgb(192, 192, 192);


            tFullName = new Label();
            tFullName.Size = new Size(lFullName.Width, lFullName.Height);
            tFullName.Location = new Point(lFullName.Location.X, lFullName.Location.Y + lFullName.Height);
            font.Size(tFullName, fontSize);
            tFullName.Text = db.select.selecVisita(id_visit)[1].ToString();
            editPanel.Controls.Add(tFullName);
            tFullName.ForeColor = Color.White;



            lVisitDate = new Label();
            lVisitDate.Size = new Size(tFullName.Width, tFullName.Height);
            lVisitDate.Location = new Point(tFullName.Location.X, tFullName.Location.Y + tFullName.Height + 40);
            lVisitDate.Text = "Data da Visita:";
            font.Size(lVisitDate, fontSize);
            editPanel.Controls.Add(lVisitDate);
            lVisitDate.ForeColor = Color.FromArgb(192, 192, 192);


            tVisitDate = new Label();
            tVisitDate.Size = new Size(150, lFullName.Height);
            tVisitDate.Location = new Point(lVisitDate.Location.X, lVisitDate.Location.Y + lVisitDate.Height);
            font.Size(tVisitDate, fontSize);
            tVisitDate.Text = db.select.selecVisita(id_visit)[3].ToString();
            editPanel.Controls.Add(tVisitDate);
            tVisitDate.ForeColor = Color.White;

            lPrisionerVisited = new Label();
            lPrisionerVisited.Size = new Size(tVisitDate.Width, tVisitDate.Height);
            lPrisionerVisited.Location = new Point(tVisitDate.Location.X, tVisitDate.Location.Y + tVisitDate.Height + 40);
            lPrisionerVisited.Text = "Recluso Visitado:";
            font.Size(lPrisionerVisited, fontSize);
            editPanel.Controls.Add(lPrisionerVisited);
            lPrisionerVisited.ForeColor = Color.FromArgb(192, 192, 192);

            cbPrisionerVisited = new Label();
            cbPrisionerVisited.Size = new Size(200, lFullName.Height);
            cbPrisionerVisited.Location = new Point(lPrisionerVisited.Location.X, lPrisionerVisited.Location.Y + lPrisionerVisited.Height);
            font.Size(cbPrisionerVisited, fontSize);
            editPanel.Controls.Add(cbPrisionerVisited);
            cbPrisionerVisited.Text = db.select.selecReclusoVisitado(Int32.Parse(db.select.selecVisita(id_visit)[0].ToString()))[0].ToString();
            cbPrisionerVisited.ForeColor = Color.White;


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

            titulo = new Label();
            container.Controls.Add(titulo);
            titulo.Size = new Size(700, 100);
            titulo.Location = new Point(450, 0);
            font.Size(titulo, fontSize);
            titulo.Text = "Consultar Visita";
            titulo.Font = new Font("Sitka Banner", 30, FontStyle.Bold);
            titulo.ForeColor = Color.DarkBlue;
            titulo.SendToBack();
            

            listPanel = new Panel();
            container.Controls.Add(listPanel);
            listPanel.Location = new Point((container.Width / 8), (container.Height / 8));
            listPanel.Size = new Size(993, 800);
            listPanel.BackColor = Color.White;


            tabela = new TableLayoutPanel();
            listPanel.Controls.Add(tabela);
            tabela.Dock = DockStyle.Top;
            tabela.Size = new Size(listPanel.Width, listPanel.Height);
            tabela.ColumnCount = 1;
            tabela.RowCount = count.Visit().Count;
            tabela.AutoScroll = true;
            createTable();

            font = new Font_Class();

            titulo = new Label();
            container.Controls.Add(titulo);
            titulo.Size = new Size(700, 100);
            titulo.Location = new Point(450, 0);
            font.Size(titulo, fontSize);
            titulo.Text = "Consultar Visita";
            titulo.Font = new Font("Sitka Banner", 30, FontStyle.Bold);
            titulo.ForeColor = Color.DarkBlue;
            titulo.SendToBack();
        }
    }
}
