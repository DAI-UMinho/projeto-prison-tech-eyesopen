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
    public partial class ApagarOcorrencia : Form
    {
        Database db;
        public ApagarOcorrencia()
        {
            InitializeComponent();
            button1.Text = "Apagar";
            db = new Database();
            PreencherDataGrid();
            button1.Enabled = false;
            Teste();
        }

        public void PreencherDataGrid()
        {
            List<object> var = new List<object>();
            var = db.select.VisualizarOcorrencia();
            for(int i=0; i<var.Count; i+= 3)
            {
                dataGridView1.Rows.Add(var.ElementAt(i), var.ElementAt(i + 1), var.ElementAt(i + 2));
            }
        }

        public void Teste()
        {
            while(dataGridView1.SelectedRows != null)
            {
                button1.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
            string a = Convert.ToString(selectedRow.Cells["idOcorrencia"].Value);
            db.delete.Ocorrencia(a);
            MessageBox.Show("Ocorrencia Eliminada");
            dataGridView1.Rows.Clear();
            PreencherDataGrid();
        }
    }
}
