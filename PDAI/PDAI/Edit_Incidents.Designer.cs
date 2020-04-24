namespace PDAI
{
    partial class Edit_Incidents
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.nomeCompleto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idOcorrencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataOcorrencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nomeCompleto,
            this.idOcorrencia,
            this.dataOcorrencia});
            this.dataGridView1.Location = new System.Drawing.Point(159, 76);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(499, 446);
            this.dataGridView1.TabIndex = 0;
            // 
            // nomeCompleto
            // 
            this.nomeCompleto.HeaderText = "nomeCompleto";
            this.nomeCompleto.MinimumWidth = 6;
            this.nomeCompleto.Name = "nomeCompleto";
            this.nomeCompleto.ReadOnly = true;
            this.nomeCompleto.Width = 125;
            // 
            // idOcorrencia
            // 
            this.idOcorrencia.HeaderText = "idOcorrencia";
            this.idOcorrencia.MinimumWidth = 6;
            this.idOcorrencia.Name = "idOcorrencia";
            this.idOcorrencia.ReadOnly = true;
            this.idOcorrencia.Width = 125;
            // 
            // dataOcorrencia
            // 
            this.dataOcorrencia.HeaderText = "dataOcorrencia";
            this.dataOcorrencia.MinimumWidth = 6;
            this.dataOcorrencia.Name = "dataOcorrencia";
            this.dataOcorrencia.ReadOnly = true;
            this.dataOcorrencia.Width = 125;
            // 
            // Edit_Incidents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 592);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Edit_Incidents";
            this.Text = "Edit_Incidents";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn nomeCompleto;
        private System.Windows.Forms.DataGridViewTextBoxColumn idOcorrencia;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataOcorrencia;
    }
}