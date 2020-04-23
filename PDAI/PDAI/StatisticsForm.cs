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
    public partial class StatisticsForm : Form
    {

        Database db;

        int jan = 15;
        int fev = 10;
        int mar = 5;
        int apr = 15;
        int may = 12;
        int jun = 10;
        int jul = 9;
        int aug = 6;
        int sep = 13;
        int oct = 20;
        int nov = 10;
        int dec = 1;


        public StatisticsForm()
        {
            InitializeComponent();
            monthChartValues();
            placeChartValues();
            this.Name = "StatisticsForm";

        }

        private void StatisticsForm_Load(object sender, EventArgs e)
        {

        }

        private void gunaLabel1_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {
            // ver se grafico esta vazio,se estiver mandar mensagem para selecionar ano
            // caso contrario faz nada
        }

        private void gunaPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void gunaLabel2_Click(object sender, EventArgs e)
        {

        }

        private void monthChartValues()
        {
            incidents_month.Series["Ocorrências"].Points.AddXY("Janeiro", jan);
            incidents_month.Series["Ocorrências"].Points.AddXY("Fevereiro", fev);
            incidents_month.Series["Ocorrências"].Points.AddXY("Março", mar);
            incidents_month.Series["Ocorrências"].Points.AddXY("Abril", apr);
            incidents_month.Series["Ocorrências"].Points.AddXY("Maio", may);
            incidents_month.Series["Ocorrências"].Points.AddXY("Junho", jun);
            incidents_month.Series["Ocorrências"].Points.AddXY("Julho", jul);
            incidents_month.Series["Ocorrências"].Points.AddXY("Agosto", aug);
            incidents_month.Series["Ocorrências"].Points.AddXY("Setembro", sep);
            incidents_month.Series["Ocorrências"].Points.AddXY("Outubro", oct);
            incidents_month.Series["Ocorrências"].Points.AddXY("Novembro", nov);
            incidents_month.Series["Ocorrências"].Points.AddXY("Dezembro", dec);
        }

        private void placeChartValues()
        {
            incidents_Place.Series["Ocorrências"].Points.AddXY("Cam 1", 50);
            incidents_Place.Series["Ocorrências"].Points.AddXY("Cam 2", 70);

        }

        private void button1_Click(object sender, EventArgs e)
        {

            incidents_month.Series.Clear();
            incidents_month.Series.Add("Ocorrencias");
            List<object> var = new List<object>();
            var = db.select.OcorrenciaMes((int)comboBox1.SelectedItem);

            //insere meses com dados e depois preenche restantes meses a zero
            bool flag = false;
            for (int i = 1; i < 13; i++)
            {
                for (int j = 0; j < var.Count(); j += 2)
                {
                    if ((int)var.ElementAt(j) == i)
                    {
                        flag = true;
                        incidents_month.Series["Ocorrencias"].Points.AddXY((int)var.ElementAt(j + 1), "" + var.ElementAt(j));
                        break;
                    }
                }
                if (flag == false)
                {
                    incidents_month.Series["Ocorrencias"].Points.AddXY(i, "0");
                }
                flag = false;
            }
        }

        public void Preenche_ComboBox()
        {
            List<object> var = new List<object>();
            var = db.select.PreencherComboBox();
            for (int i = 0; i < var.Count(); i++)
            {
                comboBox1.Items.Add(var[i]);
            }

        }

    }
}
