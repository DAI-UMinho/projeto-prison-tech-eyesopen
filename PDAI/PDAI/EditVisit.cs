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
    class EditVisit
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
        Button b, save;
        Label l, ldata, lId, lFullName, lVisitDate, lPrisionerVisited, titulo;
        TextBox tFullName;
        DateTimePicker tVisitDate;
        ComboBox cbPrisionerVisited;
        ListView lv;
        Font_Class font;
        public static String select;
        Panel row;
        int saveWidth, saveHeight;
        string var;
        string label, nome, id_visit;
        int id, fontSize = 13;
        public EditVisit()
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
            System.Diagnostics.Debug.WriteLine(label);
            var control = tabela.Controls.Find(label, true)[0];
            id_visit = control.Text.ToString();


            lFullName = new Label();
            lFullName.Size = new Size(500, 20);
            lFullName.Location = new Point(container.Width * 1 / 20, container.Height * 1 / 10);
            lFullName.Text = "Nome Completo";
            font.Size(lFullName, fontSize);
            container.Controls.Add(lFullName);


            tFullName = new TextBox();
            tFullName.Size = new Size(lFullName.Width, lFullName.Height);
            tFullName.Location = new Point(lFullName.Location.X, lFullName.Location.Y + lFullName.Height);
            font.Size(tFullName, fontSize);
            container.Controls.Add(tFullName);
            tFullName.Text = db.select.selecVisita(id_visit)[1].ToString();



            lVisitDate = new Label();
            lVisitDate.Size = new Size(tFullName.Width, tFullName.Height);
            lVisitDate.Location = new Point(tFullName.Location.X, tFullName.Location.Y + tFullName.Height + 40);
            lVisitDate.Text = "Data da Visita";
            font.Size(lVisitDate, fontSize);
            container.Controls.Add(lVisitDate);


            tVisitDate = new DateTimePicker();
            tVisitDate.Size = new Size(150, lFullName.Height);
            tVisitDate.Location = new Point(lVisitDate.Location.X, lVisitDate.Location.Y + lVisitDate.Height);
            tVisitDate.Format = DateTimePickerFormat.Short;
            font.Size(tVisitDate, fontSize);
            container.Controls.Add(tVisitDate);
            tVisitDate.Value = DateTime.Parse(db.select.selecVisita(id_visit)[3].ToString());

            lPrisionerVisited = new Label();
            lPrisionerVisited.Size = new Size(tVisitDate.Width, tVisitDate.Height);
            lPrisionerVisited.Location = new Point(tVisitDate.Location.X, tVisitDate.Location.Y + tVisitDate.Height + 40);
            lPrisionerVisited.Text = "Recluso Visitado";
            font.Size(lPrisionerVisited, fontSize);
            container.Controls.Add(lPrisionerVisited);

            cbPrisionerVisited = new ComboBox();
            cbPrisionerVisited.Size = new Size(200, lFullName.Height);
            cbPrisionerVisited.Location = new Point(lPrisionerVisited.Location.X, lPrisionerVisited.Location.Y + lPrisionerVisited.Height);
            font.Size(cbPrisionerVisited, fontSize);
            container.Controls.Add(cbPrisionerVisited);
            cbPrisionerVisited.DropDownStyle = ComboBoxStyle.DropDownList;
            names = db.select.reclusoVisits();
            for (int i = 0; i < names.Count; i++)
            {
                cbPrisionerVisited.Items.Add(names[i].ToString());
            }
            cbPrisionerVisited.SelectedItem = db.select.selecReclusoVisitado(Int32.Parse(db.select.selecVisita(id_visit)[0].ToString()))[0].ToString();



            save = new Button();
            save.Size = new Size(150, 60);
            save.Location = new Point(cbPrisionerVisited.Location.X, cbPrisionerVisited.Location.Y + cbPrisionerVisited.Height + 50);
            save.Text = "Guardar";
            font.Size(save, fontSize);
            container.Controls.Add(save);
            save.Click += new EventHandler(Save_Click);
            save.BackColor = Color.FromArgb(127, 127, 127);
            save.ForeColor = Color.White;
            save.Cursor = Cursors.Hand;
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
            listPanel.Location = new Point((container.Width / 8), (container.Height / 8));
            listPanel.Size = new Size(993, 800);
            listPanel.BackColor = Color.White;

            titulo = new Label();
            container.Controls.Add(titulo);
            titulo.Size = new Size(700, 100);
            titulo.Location = new Point(450, 0);
            font.Size(titulo, fontSize);
            titulo.Text = "Editar Visita";
            titulo.Font = new Font("Sitka Banner", 30, FontStyle.Bold);
            titulo.ForeColor = Color.DarkBlue;
            titulo.SendToBack();

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
            titulo.Text = "Editar Visita";
            titulo.Font = new Font("Sitka Banner", 30, FontStyle.Bold);
            titulo.ForeColor = Color.DarkBlue;
            titulo.SendToBack();
        }

        public void Save_Click(object sender, EventArgs e)
        {
            if (tFullName.Text != string.Empty)
            {
                if (tVisitDate.Text != string.Empty)
                {
                    db.update.Visita(id_visit, tFullName.Text.ToString(), db.select.visitedPrisionerId(cbPrisionerVisited.Text.ToString())[0].ToString(), tVisitDate.Value.ToString());
                    MessageBox.Show("Alterações guardadas com sucesso!!");
                    //select = tFullName.Text;
                    container.Controls.Clear();
                    Open();
                }
                else { MessageBox.Show("Campo Data da Visita obrigatório."); }
            }
            else { MessageBox.Show("Campo Nome Completo obrigatório."); }
        }
    }
}
