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
    public partial class VisualizarOcorrencia : Form
    {
        Database db;
        List<object> var;
        public VisualizarOcorrencia()
        {
            InitializeComponent();
            db = new Database();
            PreencherDataGrid();
        }

        public void PreencherDataGrid()
        {
            
            var = db.select.VisualizarOcorrencia();
            for(int i = 0; i<var.Count; i+=4)
            {
                dataGridView1.Rows.Add(var.ElementAt(i), var.ElementAt(i + 2));
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectedRow = dataGridView1.SelectedCells[0].OwningRow.Index + 1;
           
            object value = var.ElementAt((4 * selectedRow) - 1);
            
            int idOcorrencia = Convert.ToInt32(var.ElementAt((4*selectedRow)-1));
            List<object> tryAgain = new List<object>();
            List<object> lol = new List<object>();
            List<object> des = new List<object>();
            tryAgain = db.select.GetInterveniente(idOcorrencia);
            des = db.select.getDescricao(idOcorrencia);
            string descricao = "" + des.ElementAt(0);
            lol = db.select.GetMaisIntervenientes(idOcorrencia);
            string texto = "" + tryAgain.ElementAt(0);
            for(int i = 0; i<lol.Count;i++)
            {
                    texto += " , " + lol.ElementAt(i);
            }

            richTextBox1.Text = texto;
            richTextBox2.Text = descricao;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
