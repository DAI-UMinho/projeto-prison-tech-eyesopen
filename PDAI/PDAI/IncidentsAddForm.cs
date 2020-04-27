﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PDAI
{
    public partial class IncidentsAddForm : Form
    {
        public RichTextBox pList;
        public IncidentsAddForm(List<object> v, RichTextBox l)
        {
            InitializeComponent();
            pList = l;
            for (int i = 0; i < v.Count; i += 2)
            {
                this.dataGridView1.Rows.Add(v.ElementAt(i), v.ElementAt(i + 1));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int row = dataGridView1.CurrentCell.RowIndex;
            if (row >= dataGridView1.RowCount - 1)
            {
                MessageBox.Show("Nao foi selecionado nenhum recluso!");
            }
            else
            {
                string texto;
                texto = "" + dataGridView1.Rows[row].Cells[0].Value + "-" + dataGridView1.Rows[row].Cells[1].Value;
                pList.Text = texto;
                this.Dispose();
            }
        }
    }
}