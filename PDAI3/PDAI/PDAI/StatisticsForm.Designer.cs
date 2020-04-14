namespace PDAI
{
    partial class StatisticsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title5 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title6 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.gunaLabel1 = new Guna.UI.WinForms.GunaLabel();
            this.incidents_Place = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.incidents_month = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.gunaPanel1 = new Guna.UI.WinForms.GunaPanel();
            this.gunaLabel2 = new Guna.UI.WinForms.GunaLabel();
            this.gunaPanel2 = new Guna.UI.WinForms.GunaPanel();
            this.gunaPanel3 = new Guna.UI.WinForms.GunaPanel();
            this.gunaLabel3 = new Guna.UI.WinForms.GunaLabel();
            this.gunaLabel4 = new Guna.UI.WinForms.GunaLabel();
            ((System.ComponentModel.ISupportInitialize)(this.incidents_Place)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.incidents_month)).BeginInit();
            this.gunaPanel1.SuspendLayout();
            this.gunaPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // gunaLabel1
            // 
            this.gunaLabel1.AutoSize = true;
            this.gunaLabel1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gunaLabel1.Location = new System.Drawing.Point(238, 79);
            this.gunaLabel1.Name = "gunaLabel1";
            this.gunaLabel1.Size = new System.Drawing.Size(0, 15);
            this.gunaLabel1.TabIndex = 0;
            this.gunaLabel1.Click += new System.EventHandler(this.gunaLabel1_Click);
            // 
            // incidents_Place
            // 
            this.incidents_Place.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            chartArea5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            chartArea5.Name = "ChartArea1";
            this.incidents_Place.ChartAreas.Add(chartArea5);
            legend5.Name = "Legend1";
            this.incidents_Place.Legends.Add(legend5);
            this.incidents_Place.Location = new System.Drawing.Point(782, 271);
            this.incidents_Place.Name = "incidents_Place";
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;
            series5.LabelBackColor = System.Drawing.Color.Transparent;
            series5.Legend = "Legend1";
            series5.Name = "Ocorrências";
            this.incidents_Place.Series.Add(series5);
            this.incidents_Place.Size = new System.Drawing.Size(697, 478);
            this.incidents_Place.TabIndex = 2;
            this.incidents_Place.Text = "chart2";
            title5.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title5.Name = "Número de Ocorrências por Setor";
            title5.Text = "Número de Ocorrências por Setor";
            this.incidents_Place.Titles.Add(title5);
            // 
            // incidents_month
            // 
            this.incidents_month.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(196)))), ((int)(((byte)(196)))));
            this.incidents_month.BorderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(196)))), ((int)(((byte)(196)))));
            chartArea6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(196)))), ((int)(((byte)(196)))));
            chartArea6.BackSecondaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(196)))), ((int)(((byte)(196)))));
            chartArea6.Name = "ChartArea1";
            this.incidents_month.ChartAreas.Add(chartArea6);
            legend6.Name = "Legend1";
            this.incidents_month.Legends.Add(legend6);
            this.incidents_month.Location = new System.Drawing.Point(88, 271);
            this.incidents_month.Name = "incidents_month";
            this.incidents_month.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            series6.BorderColor = System.Drawing.Color.Transparent;
            series6.ChartArea = "ChartArea1";
            series6.Color = System.Drawing.Color.Crimson;
            series6.Font = new System.Drawing.Font("Montserrat", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series6.LabelBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            series6.Legend = "Legend1";
            series6.Name = "Ocurrências";
            this.incidents_month.Series.Add(series6);
            this.incidents_month.Size = new System.Drawing.Size(688, 478);
            this.incidents_month.TabIndex = 3;
            this.incidents_month.Text = "chart1";
            title6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            title6.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title6.Name = "teste";
            title6.Text = "Número de Ocurrências por Mês";
            this.incidents_month.Titles.Add(title6);
            this.incidents_month.Click += new System.EventHandler(this.chart1_Click);
            // 
            // gunaPanel1
            // 
            this.gunaPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(75)))));
            this.gunaPanel1.Controls.Add(this.gunaLabel2);
            this.gunaPanel1.Location = new System.Drawing.Point(88, 55);
            this.gunaPanel1.Name = "gunaPanel1";
            this.gunaPanel1.Size = new System.Drawing.Size(430, 120);
            this.gunaPanel1.TabIndex = 4;
            this.gunaPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.gunaPanel1_Paint);
            // 
            // gunaLabel2
            // 
            this.gunaLabel2.Font = new System.Drawing.Font("Montserrat", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gunaLabel2.ForeColor = System.Drawing.Color.White;
            this.gunaLabel2.Location = new System.Drawing.Point(3, 1);
            this.gunaLabel2.Name = "gunaLabel2";
            this.gunaLabel2.Size = new System.Drawing.Size(334, 69);
            this.gunaLabel2.TabIndex = 0;
            this.gunaLabel2.Text = "Reclusos Detetatos pelas Camaras (%)";
            this.gunaLabel2.Click += new System.EventHandler(this.gunaLabel2_Click);
            // 
            // gunaPanel2
            // 
            this.gunaPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(180)))));
            this.gunaPanel2.Location = new System.Drawing.Point(1049, 55);
            this.gunaPanel2.Name = "gunaPanel2";
            this.gunaPanel2.Size = new System.Drawing.Size(430, 120);
            this.gunaPanel2.TabIndex = 5;
            // 
            // gunaPanel3
            // 
            this.gunaPanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(77)))), ((int)(((byte)(81)))));
            this.gunaPanel3.Controls.Add(this.gunaLabel4);
            this.gunaPanel3.Controls.Add(this.gunaLabel3);
            this.gunaPanel3.Location = new System.Drawing.Point(570, 55);
            this.gunaPanel3.Name = "gunaPanel3";
            this.gunaPanel3.Size = new System.Drawing.Size(430, 120);
            this.gunaPanel3.TabIndex = 5;
            // 
            // gunaLabel3
            // 
            this.gunaLabel3.Font = new System.Drawing.Font("Montserrat", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gunaLabel3.ForeColor = System.Drawing.Color.White;
            this.gunaLabel3.Location = new System.Drawing.Point(3, 1);
            this.gunaLabel3.Name = "gunaLabel3";
            this.gunaLabel3.Size = new System.Drawing.Size(319, 69);
            this.gunaLabel3.TabIndex = 1;
            this.gunaLabel3.Text = "Número de Camaras Ligadas";
            // 
            // gunaLabel4
            // 
            this.gunaLabel4.AutoSize = true;
            this.gunaLabel4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gunaLabel4.Location = new System.Drawing.Point(346, 93);
            this.gunaLabel4.Name = "gunaLabel4";
            this.gunaLabel4.Size = new System.Drawing.Size(68, 15);
            this.gunaLabel4.TabIndex = 2;
            this.gunaLabel4.Text = "gunaLabel4";
            this.gunaLabel4.Click += new System.EventHandler(this.gunaLabel4_Click);
            // 
            // StatisticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(196)))), ((int)(((byte)(196)))));
            this.ClientSize = new System.Drawing.Size(1573, 851);
            this.Controls.Add(this.gunaPanel2);
            this.Controls.Add(this.gunaPanel3);
            this.Controls.Add(this.gunaPanel1);
            this.Controls.Add(this.incidents_month);
            this.Controls.Add(this.incidents_Place);
            this.Controls.Add(this.gunaLabel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "StatisticsForm";
            this.Text = "StatisticsForm";
            this.Load += new System.EventHandler(this.StatisticsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.incidents_Place)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.incidents_month)).EndInit();
            this.gunaPanel1.ResumeLayout(false);
            this.gunaPanel3.ResumeLayout(false);
            this.gunaPanel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI.WinForms.GunaLabel gunaLabel1;
        private System.Windows.Forms.DataVisualization.Charting.Chart incidents_Place;
        private System.Windows.Forms.DataVisualization.Charting.Chart incidents_month;
        private Guna.UI.WinForms.GunaPanel gunaPanel1;
        private Guna.UI.WinForms.GunaPanel gunaPanel2;
        private Guna.UI.WinForms.GunaPanel gunaPanel3;
        private Guna.UI.WinForms.GunaLabel gunaLabel2;
        private Guna.UI.WinForms.GunaLabel gunaLabel3;
        private Guna.UI.WinForms.GunaLabel gunaLabel4;
    }
}