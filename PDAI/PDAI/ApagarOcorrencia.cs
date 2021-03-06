﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PDAI
{
    public partial class ApagarOcorrencia : Form
    {
        Database db;
        List<object> var;
        
        public ApagarOcorrencia()
        {
            InitializeComponent();
            button1.Text = "Eliminar";
            db = new Database();
            var = new List<object>();

            PreencherDataGrid();
            button1.Enabled = false;
            
        }

        public void PreencherDataGrid()
        {
            try
            {
                
                var = db.select.VisualizarOcorrencia();
                for (int i = 0; i < var.Count; i += 4)
                {
                    dataGridView1.Rows.Add(var.ElementAt(i), var.ElementAt(i + 1), var.ElementAt(i + 2));
                }
            }
            catch (SqlException es)
            {
                System.Windows.Forms.MessageBox.Show("" + es);
            }

            catch (Exception es)
            {
                System.Windows.Forms.MessageBox.Show("" + es);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            int selectedrow = dataGridView1.SelectedCells[0].RowIndex + 1;
            object id = var.ElementAt(selectedrow*4-1);
            string a = Convert.ToString(id);
            db.delete.Ocorrencia(a);
            MessageBox.Show("Ocorrencia Eliminada");
            dataGridView1.Rows.Clear();
            PreencherDataGrid();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }
    }
}
