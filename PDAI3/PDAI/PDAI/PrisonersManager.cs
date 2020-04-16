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
    class PrisonersManager
    {
        Database db;
        Panel prisonerManager_interface, listPanel;
        List<object> names = new List<object>();
        TableLayoutPanel tabela;
        Select count = new Select();
        Button b;
        Label l;
        ListView lv;
        Font_Class font;
        public static String select;
        Panel save, row;
        int saveWidth, saveHeight;
        double var;
        string label, nome;
        public PrisonersManager(Panel content_interface, int content_width, int content_height)
        {
            save = content_interface;
            saveWidth = content_width;
            saveHeight = content_height;

            prisonerManager_interface = new Panel();
            prisonerManager_interface.Size = new Size(content_width, content_height);
            prisonerManager_interface.Location = new Point(0, 0);
            content_interface.Controls.Add(prisonerManager_interface);
            prisonerManager_interface.BackColor = Color.FromArgb(196, 196, 196);

            listPanel = new Panel();
            prisonerManager_interface.Controls.Add(listPanel);
            listPanel.Location = new Point((prisonerManager_interface.Width / 5), (prisonerManager_interface.Height / 14));
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

                Button b = new Button();
                row.Controls.Add(b);
                b.Text = "Eliminar";
                b.Size = new Size(85, 60);
                b.BackColor = Color.FromArgb(127, 127, 127);
                b.Click += new EventHandler(b_Click);
                b.Cursor = Cursors.Hand;
                b.Dock = DockStyle.Left;

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

                b.Name = "btn" + i;
                l.Name = "label" + i;


            }

            System.Diagnostics.Debug.WriteLine("ola");

        }

        private void b_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Tem a certeza que quer proceder com a eliminação ??",
                                     "",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                // If 'Yes', do something here.
                var = Char.GetNumericValue((sender as Button).Name.ToString(), 3);
                label = "label" + var;
                System.Diagnostics.Debug.WriteLine(label);
                var control = tabela.Controls.Find(label, true)[0];
                nome = control.Text.ToString();
                System.Diagnostics.Debug.WriteLine(nome);
                Delete d = new Delete();
                d.Recluso(nome);
                save.Controls.Clear();
                PrisonersManager pm = new PrisonersManager(save, saveWidth, saveHeight);
                MessageBox.Show("Eliminado com Sucesso !! ");

            }
            else
            {
                // If 'No', do something here.
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

        public PrisonersManager()
        {
        }


        private void l_MouseDoubleClick(Object sender, MouseEventArgs e)
        {
            Select s = new Select();
            select = (sender as Label).Text.ToString();
            save.Controls.Clear();
            viewPrisoner vp = new viewPrisoner(save, saveWidth, saveHeight, select);
            System.Diagnostics.Debug.WriteLine(select);
            System.Diagnostics.Debug.WriteLine(s.selecRecluso(select)[2]);
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
    }
}