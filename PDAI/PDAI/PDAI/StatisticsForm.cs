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
    public partial class StatisticsForm : Form
    {
      

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

     
    }
}
