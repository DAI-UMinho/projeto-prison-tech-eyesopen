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
        public Edit_Incidents()
        {
            InitializeComponent();
            db = new Database();
            PreencherDataGrid();
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


                Incidents ola = new Incidents(var);
            }
        }
    }
}
