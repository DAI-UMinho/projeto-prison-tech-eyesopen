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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.gunaLabel1 = new Guna.UI.WinForms.GunaLabel();
            this.incidents_Place = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.incidents_month = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.gunaPanel1 = new Guna.UI.WinForms.GunaPanel();
            this.gunaLabel2 = new Guna.UI.WinForms.GunaLabel();
            this.gunaPanel2 = new Guna.UI.WinForms.GunaPanel();
            this.gunaPanel3 = new Guna.UI.WinForms.GunaPanel();
            this.gunaLabel3 = new Guna.UI.WinForms.GunaLabel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
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
            this.gunaLabel1.Location = new System.Drawing.Point(317, 97);
            this.gunaLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.gunaLabel1.Name = "gunaLabel1";
            this.gunaLabel1.Size = new System.Drawing.Size(0, 20);
            this.gunaLabel1.TabIndex = 0;
            this.gunaLabel1.Click += new System.EventHandler(this.gunaLabel1_Click);
            // 
            // incidents_Place
            // 
            this.incidents_Place.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            chartArea1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            chartArea1.Name = "ChartArea1";
            this.incidents_Place.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.incidents_Place.Legends.Add(legend1);
            this.incidents_Place.Location = new System.Drawing.Point(1043, 334);
            this.incidents_Place.Margin = new System.Windows.Forms.Padding(4);
            this.incidents_Place.Name = "incidents_Place";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;
            series1.LabelBackColor = System.Drawing.Color.Transparent;
            series1.Legend = "Legend1";
            series1.Name = "Ocorrências";
            this.incidents_Place.Series.Add(series1);
            this.incidents_Place.Size = new System.Drawing.Size(929, 588);
            this.incidents_Place.TabIndex = 2;
            this.incidents_Place.Text = "chart2";
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title1.Name = "Número de Ocorrências por Setor";
            title1.Text = "Número de Ocorrências por Setor";
            this.incidents_Place.Titles.Add(title1);
            // 
            // incidents_month
            // 
            this.incidents_month.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(196)))), ((int)(((byte)(196)))));
            this.incidents_month.BorderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(196)))), ((int)(((byte)(196)))));
            chartArea2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(196)))), ((int)(((byte)(196)))));
            chartArea2.BackSecondaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(196)))), ((int)(((byte)(196)))));
            chartArea2.Name = "ChartArea1";
            this.incidents_month.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.incidents_month.Legends.Add(legend2);
            this.incidents_month.Location = new System.Drawing.Point(118, 397);
            this.incidents_month.Margin = new System.Windows.Forms.Padding(4);
            this.incidents_month.Name = "incidents_month";
            this.incidents_month.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            series2.BorderColor = System.Drawing.Color.Transparent;
            series2.ChartArea = "ChartArea1";
            series2.Color = System.Drawing.Color.Crimson;
            series2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series2.LabelBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            series2.Legend = "Legend1";
            series2.Name = "Ocorrências";
            this.incidents_month.Series.Add(series2);
            this.incidents_month.Size = new System.Drawing.Size(917, 588);
            this.incidents_month.TabIndex = 3;
            this.incidents_month.Text = "chart1";
            title2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            title2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title2.Name = "teste";
            title2.Text = "Número de Ocorrências por Mês";
            this.incidents_month.Titles.Add(title2);
            this.incidents_month.Click += new System.EventHandler(this.chart1_Click);
            // 
            // gunaPanel1
            // 
            this.gunaPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(75)))));
            this.gunaPanel1.Controls.Add(this.gunaLabel2);
            this.gunaPanel1.Location = new System.Drawing.Point(117, 68);
            this.gunaPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.gunaPanel1.Name = "gunaPanel1";
            this.gunaPanel1.Size = new System.Drawing.Size(573, 148);
            this.gunaPanel1.TabIndex = 4;
            this.gunaPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.gunaPanel1_Paint);
            // 
            // gunaLabel2
            // 
            this.gunaLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gunaLabel2.ForeColor = System.Drawing.Color.White;
            this.gunaLabel2.Location = new System.Drawing.Point(4, 1);
            this.gunaLabel2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.gunaLabel2.Name = "gunaLabel2";
            this.gunaLabel2.Size = new System.Drawing.Size(445, 85);
            this.gunaLabel2.TabIndex = 0;
            this.gunaLabel2.Text = "Reclusos Detetatos pelas Camaras (%)";
            this.gunaLabel2.Click += new System.EventHandler(this.gunaLabel2_Click);
            // 
            // gunaPanel2
            // 
            this.gunaPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(180)))));
            this.gunaPanel2.Location = new System.Drawing.Point(1399, 68);
            this.gunaPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.gunaPanel2.Name = "gunaPanel2";
            this.gunaPanel2.Size = new System.Drawing.Size(573, 148);
            this.gunaPanel2.TabIndex = 5;
            // 
            // gunaPanel3
            // 
            this.gunaPanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(77)))), ((int)(((byte)(81)))));
            this.gunaPanel3.Controls.Add(this.gunaLabel3);
            this.gunaPanel3.Location = new System.Drawing.Point(760, 68);
            this.gunaPanel3.Margin = new System.Windows.Forms.Padding(4);
            this.gunaPanel3.Name = "gunaPanel3";
            this.gunaPanel3.Size = new System.Drawing.Size(573, 148);
            this.gunaPanel3.TabIndex = 5;
            // 
            // gunaLabel3
            // 
            this.gunaLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gunaLabel3.ForeColor = System.Drawing.Color.White;
            this.gunaLabel3.Location = new System.Drawing.Point(4, 1);
            this.gunaLabel3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.gunaLabel3.Name = "gunaLabel3";
            this.gunaLabel3.Size = new System.Drawing.Size(425, 85);
            this.gunaLabel3.TabIndex = 1;
            this.gunaLabel3.Text = "Número de Camaras Ligadas";
            // 
            // comboBox1
            // 
            this.comboBox1.AccessibleName = "combo";
            this.comboBox1.DropDownWidth = 121;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(402, 334);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 24);
            this.comboBox1.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(552, 334);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // StatisticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(196)))), ((int)(((byte)(196)))));
            this.ClientSize = new System.Drawing.Size(1942, 1047);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.gunaPanel2);
            this.Controls.Add(this.gunaPanel3);
            this.Controls.Add(this.gunaPanel1);
            this.Controls.Add(this.incidents_month);
            this.Controls.Add(this.incidents_Place);
            this.Controls.Add(this.gunaLabel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "StatisticsForm";
            this.Text = "StatisticsForm";
            this.Load += new System.EventHandler(this.StatisticsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.incidents_Place)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.incidents_month)).EndInit();
            this.gunaPanel1.ResumeLayout(false);
            this.gunaPanel3.ResumeLayout(false);
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
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button1;
    }
}