using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PDAI
{
    public partial class Edit_Incidents : Form
    {
        Database db;
        Menu menu;
        public Panel container { get; }
        List<string> stringObject;
        Dictionary<string, object> disposeObject;
        public Edit_Incidents()
        {
            InitializeComponent();
            db = new Database();
            PreencherDataGrid();
            container = new Panel();
            menu = new Menu();
        }

        public void PreencherDataGrid()
        {
            List<object> var = new List<object>();
            var = db.select.VisualizarOcorrencia();
            for (int i = 0; i < var.Count; i += 3)
            {
                dataGridView1.Rows.Add(var.ElementAt(i), var.ElementAt(i + 1), var.ElementAt(i + 2));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            container.Controls.Clear();
            List<Object> var = new List<object>();
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                string id = Convert.ToString(selectedRow.Cells["idOcorrencia"].Value);
                string dataOcorrencia = Convert.ToString(selectedRow.Cells["dataOcorrencia"].Value);
                string nomeCompleto = Convert.ToString(selectedRow.Cells["nomeCompleto"].Value);
                
                var = db.select.Edit_Incidents(id);
                var.Add(id);
                var.Add(nomeCompleto);
                var.Add(dataOcorrencia);

                //MessageBox.Show("" + var.ElementAt(0) + "" + "," + var.ElementAt(1)+"," + "" + var.ElementAt(2)+","+ var.ElementAt(3)+"," + var.ElementAt(4));
                Incidents incidents = new Incidents(var);
                /* container.Controls.Add(incidents.container);
                 incidents.width = container.Width - menu.width;
                 incidents.height = container.Height;
                 incidents.locationX = menu.locationX + menu.width;
                 incidents.locationY = 0;
                 incidents.Open();*/
                this.Controls.Clear();
                this.Controls.Add(incidents.container);




            }
        }
    }
}
