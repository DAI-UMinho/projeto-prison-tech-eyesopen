using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PdfSharp;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp.Pdf.IO;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;



namespace PDAI
{
    public partial class StatisticsForm : Form
    {
        
        Database db;


        public StatisticsForm()
        {
            InitializeComponent();
            //monthChartValues();
            this.db = new Database();
            Preenche_ComboBox();
            PreencherDataGrid();

        }

        private void StatisticsForm_Load(object sender, EventArgs e)
        {
            //limpar dados todos
            incidents_month.Series.Clear();
            
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
        private void button1_Click(object sender, EventArgs e)
        {
            
            incidents_month.Series.Clear();
            incidents_month.Series.Add("Ocorrencias");
            List<object> var = new List<object>();
            var = db.select.OcorrenciaMes((int)comboBox1.SelectedItem);
            
            //insere meses com dados e depois preenche restantes meses a zero
            bool flag = false;
            for(int i=1; i<13; i++)
            {
                for(int j=0; j<var.Count(); j+=2)
                {
                   if((int) var.ElementAt(j) == i )
                    {
                        flag = true;
                        incidents_month.Series["Ocorrencias"].Points.AddXY((int)var.ElementAt(j + 1), "" + var.ElementAt(j));
                        break;
                    }
                }
                if(flag == false)
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
            for(int i=0; i<var.Count(); i++)
            {
                comboBox1.Items.Add(var[i]);
            }

        }

        public void PreencherDataGrid()
        {
            List<object> var = new List<object>();
            var = db.select.Top10();

            
                for (int i = 0; i < var.Count; i += 3)
                {
                    this.dataGridView1.Rows.Add(var.ElementAt(i), var.ElementAt(i + 1), var.ElementAt(i+2));
                }
            

            

        }

        private void button2_Click(object sender, EventArgs e)
        {

            string input = Microsoft.VisualBasic.Interaction.InputBox("Insira o nome do relatorio:",
                       "Nome Relatorio",
                       "NovoRelatorio"
                       );
            if (input.Length > 0 && incidents_month.IsDisposed)
            {
                
                PdfDocument document = new PdfDocument();
                document.Info.Title = "Created with PDFsharp";
                // Create an empty page
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);
                // Create a font
                XFont font = new XFont("Verdana", 25, XFontStyle.BoldItalic);
                // Draw the text

                gfx.DrawString("Relatório", font, XBrushes.Fuchsia,
                new XRect(0, 0, page.Width, 30),
                XStringFormats.Center);



                MemoryStream ms = new MemoryStream();
                incidents_month.SaveImage(ms, ChartImageFormat.Png);
                XImage xfoto = XImage.FromStream(ms);
                gfx.DrawImage(xfoto, 100, 50, 350, 300);

                //Resize DataGridView to full height.
                //int height = dataGridView1.Height - 500;

                //dataGridView1.Height = dataGridView1.RowCount * dataGridView1.RowTemplate.Height;
                gfx.DrawString("TOP 10 Ocorrencias", font, XBrushes.Black, new XRect(0, 250, page.Width, 280), XStringFormats.Center);
                //Create a Bitmap and draw the DataGridView on it.
                Bitmap bitmap = new Bitmap(this.dataGridView1.Width, this.dataGridView1.Height);
                dataGridView1.DrawToBitmap(bitmap, new Rectangle(0, 0, this.dataGridView1.Width, this.dataGridView1.Height));

                //Resize DataGridView back to original height.

                //dataGridView1.Height = height;

                MemoryStream ms2 = new MemoryStream();
                bitmap.Save(ms2, System.Drawing.Imaging.ImageFormat.Jpeg);
                xfoto = XImage.FromStream(ms2);
                double comeco = (page.Width - xfoto.PointWidth) / 2;
                gfx.DrawImage(xfoto, new XRect(comeco, 450, xfoto.PointWidth, xfoto.PointHeight));
                // Save the document...
                string filename = "C:\\Users\\bitok\\Desktop\\" + input + ".pdf";

                document.Save(filename);
                MessageBox.Show("feito");
            }
            else
            {
                MessageBox.Show("Por favor crie o grafico de ocorrencias");
            }
        }
    }
}
