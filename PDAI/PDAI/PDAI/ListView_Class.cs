using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PDAI
{
    class ListView_Class : ListView
    {
        private Form form;

        public ListView_Class()
        {
            View = View.Details;
            FullRowSelect = true;
            GridLines = true;
            HideSelection = false;
                
        }

        public ListView_Class(Form form)
        {
            this.form = form;
            View = View.Details;
            FullRowSelect = true;
            GridLines = true;
            form.Controls.Add(this);
            HideSelection = false;

        }

        public ListView_Class(Panel panel)
        {
            View = View.Details;
            FullRowSelect = true;
            GridLines = true;
            panel.Controls.Add(this);
            HideSelection = false;
        }

        public void add_Column(string nome, int tamanho) { Columns.Add(nome, tamanho); }
        public void set_Size(int width, int height) { Size = new System.Drawing.Size(width, height); }
        public void set_Location(int x, int y) { Location = new System.Drawing.Point(x, y); }

        public int getIndexSelectedItem()
        {
            int val = -1;
            foreach (ListViewItem item in this.SelectedItems)
            {
                val = item.Index;
            }
            return val;
        }


        public List<int> getIndexSelectedItems()
        {
            List<int> val = new List<int>();
            foreach (ListViewItem item in this.SelectedItems)
            {
                val.Add(item.Index);
            }
            return val;
        }




        public void set_ItemAsSelected(int index)
        {
            this.Items[index].Selected = true;
        }




    }
    
}
