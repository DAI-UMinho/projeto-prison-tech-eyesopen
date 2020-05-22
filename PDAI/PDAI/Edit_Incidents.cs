using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PDAI
{
    public partial class Edit_Incidents : Form
    {
        Database db;
        List<Object> var;
        public Edit_Incidents()
        {
            InitializeComponent();
            db = new Database();
            PreencherDataGrid();
            button3.Enabled = false;
            button2.Enabled = false;
            var = new List<object>();
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
            
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                string id = Convert.ToString(selectedRow.Cells["idOcorrencia"].Value);
                string dataOcorrencia = Convert.ToString(selectedRow.Cells["dataOcorrencia"].Value);
                string nomeCompleto = Convert.ToString(selectedRow.Cells["nomeCompleto"].Value);
                
                var = db.select.Edit_Incidents(id);
                int idPessoa = (int)var.ElementAt(0);
                string descricao = (string)var.ElementAt(1);
                var.Add(id);
                richTextBox1.Text = "" + idPessoa;
                richTextBox2.Text = descricao;
                button3.Enabled = true;
                button2.Enabled = true;

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string[] idPessoas = richTextBox1.Text.Split('-');
            string idPessoa = idPessoas[0];
            string data;
            data = "" + dateTimePicker1.Value.Year + "-" + dateTimePicker1.Value.Month + "-" + dateTimePicker1.Value.Day + " " + dateTimePicker2.Value.Hour + ":" + dateTimePicker2.Value.Minute +
                ":" + dateTimePicker2.Value.Second;

            string descricao = richTextBox2.Text;
            int codigoOcorrencia = 0;
            string id = (string) var.ElementAt(2);
            try
            {
                if (idPessoa.Length > 0 && data.Length > 0 && descricao.Length > 0)
                {
                    if (descricao.Length <= 100)
                    {
                        db.update.Ocorrencia(idPessoa, descricao, id);
                        MessageBox.Show("Registo efetuado");

                        if (idPessoas.Length > 2)
                        {
                            int i = 2;
                            while (i < idPessoas.Length)
                            {
                                string idP = idPessoas[i];
                                db.insert.Reconhecimento(id);

                                i += 2;
                                MessageBox.Show("Registou mais que um recluso");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Descricao contem demasiados carateres!");
                    }
                }
                else
                {
                    MessageBox.Show("Por Favor Preencha todos os campos!");
                }
            }
            catch (AccessViolationException ex)
            {
                System.Windows.Forms.MessageBox.Show("" + ex);
            }
            catch (SqlException ex)
            {
                System.Windows.Forms.MessageBox.Show("" + ex);
            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("" + ex);
            }
            finally
            {
                richTextBox1.Text = null;
                richTextBox2.Text = null;

            }

        
    }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<object> var = new List<object>();
            var = db.select.Reclusos();
            //github
            if (var.Count == 0)
            {
                MessageBox.Show("Nao existem reclusos na base de dados.");
                return;
            }
            IncidentsAddForm f1 = new IncidentsAddForm(var, richTextBox1);
            f1.Show();
        }
    }
}
