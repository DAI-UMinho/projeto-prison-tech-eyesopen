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
        public VisualizarOcorrencia()
        {
            InitializeComponent();
            db = new Database();
            PreencherDataGrid();
        }

        public void PreencherDataGrid()
        {
            List<object> var = new List<object>();
            var = db.select.VisualizarOcorrencia();
            for(int i = 0; i<var.Count; i+  = 3)
            {
                dataGridView1.Rows.Add(var.ElementAt(i), var.ElementAt(i + 1), var.ElementAt(i + 2));
            }
        }
    }
}
