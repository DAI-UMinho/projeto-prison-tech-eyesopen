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
        List<object> var;
        string id;
        public Edit_Incidents()
        {
            InitializeComponent();
            db = new Database();
            PreencherDataGrid();
            button3.Enabled = false;
            button2.Enabled = false;
            dateTimePicker1.Enabled = false;
            dateTimePicker2.Enabled = false;
            //var = new List<object>();
        }

        public void PreencherDataGrid()
        {
            
            var = db.select.VisualizarOcorrencia();
            
            for (int i = 0; i < var.Count; i += 4)
            {
                dataGridView1.Rows.Add(var.ElementAt(i), var.ElementAt(i + 1), var.ElementAt(i + 2));
            }
        }

        
            

        

            private void button1_Click(object sender, EventArgs e)
        {
            
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].OwningRow.Index;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                //string id = Convert.ToString(selectedRow.Cells["idOcorrencia"].Value);
                string dataOcorrencia = Convert.ToString(selectedRow.Cells["dataOcorrencia"].Value);
                string nomeCompleto = Convert.ToString(selectedRow.Cells["Interveniente"].Value);
                string[] dt = dataOcorrencia.Split(null);
               
                id = "" + var.ElementAt(4 * (selectedrowindex+1) - 1);
                List<object> lol = new List<object>();
                lol = db.select.Edit_Incidents(id);
                
                string nome = ""+lol.ElementAt(1);
                int idPessoa = (int)lol.ElementAt(0);
                string descricao = (string)lol.ElementAt(2);
                dateTimePicker1.Value = DateTime.Parse(dt[0]);
                dateTimePicker2.Value = DateTime.Parse(dt[1]);
                richTextBox1.Text = "" + nome;
                richTextBox2.Text = descricao;
                button3.Enabled = true;
                button2.Enabled = true;
                richTextBox1.Enabled = true;
                richTextBox2.Enabled = true;
                dateTimePicker1.Enabled = true;
                dateTimePicker2.Enabled = true;

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
            try
            {
                if (idPessoa.Length > 0 && data.Length > 0 && descricao.Length > 0)
                {
                    if (descricao.Length <= 100)
                    {
                        db.update.Ocorrencia(idPessoa, descricao, id);
                        MessageBox.Show("Alterado com sucesso");

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
                richTextBox1.Enabled = false;
                richTextBox2.Enabled = false;
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;

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
