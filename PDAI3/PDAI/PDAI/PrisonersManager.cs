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
        Panel save;
        int saveWidth, saveHeight;
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
            listPanel.Size = new Size(1000, 800);
            listPanel.BackColor = Color.FromArgb(196, 196, 196);

            tabela = new TableLayoutPanel();
            listPanel.Controls.Add(tabela);
            tabela.Dock = DockStyle.Top;
            tabela.Size = new Size(listPanel.Width, listPanel.Height);
            tabela.ColumnCount = 2;
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

                Button b = new Button();
                tabela.Controls.Add(b, 1, i);
                b.Text = "Eliminar";
                b.Size = new Size(85, 60);
                b.BackColor = Color.FromArgb(127, 127, 127);
                b.Click += new EventHandler(b_Click);

                l = new Label();
                tabela.Controls.Add(l, 0, i);
                l.Text = names[i].ToString();
                l.Size = new Size(899, 60);
                l.BackColor = Color.Transparent;
                font.Size(l, 13);
                l.TextAlign = ContentAlignment.MiddleLeft;
                l.MouseDoubleClick += new MouseEventHandler(l_MouseDoubleClick);

                b.Name = "btn" + i;
                l.Name = "label" + i;


            }

            System.Diagnostics.Debug.WriteLine("ola");
            System.Diagnostics.Debug.WriteLine(s.Recluso()[1]);

        }

        private void b_Click(object sender, EventArgs e)
        {
            
            //System.Diagnostics.Debug.WriteLine(tabela.GetRow(b));
        }

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
    }
}