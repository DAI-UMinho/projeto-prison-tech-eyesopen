using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace PDAI
{
    class Visit
    {

        public Panel container { get; }
        public int locationX { set { container.Location = new Point(value, container.Location.Y); } get { return container.Location.X; } }
        public int locationY { set { container.Location = new Point(container.Location.X, value); } get { return container.Location.Y; } }
        public int width { set { container.Size = new Size(value, container.Height); } get { return container.Width; } }
        public int height { set { container.Size = new Size(container.Width, value); } get { return container.Height; } }

        Font_Class font;
        Panel visit_interface, editPanelBorder,editPanel;
        PictureBox photo;
        Label lFullName, lVisitDate, lCC, lPrisionerVisited, titulo;
        TextBox tFullName, tCC;
        ComboBox cbPrisionerVisited;
        DateTimePicker tVisitDate;
        Button registration, addImg;
        Database db;
        string type = "Funcionario", recordsFolder = "";
        int fontSize = 13;
        List<object> names = new List<object>();
        List<object> Ids = new List<object>();

        public Visit()
        {
            font = new Font_Class();
            db = new Database();
            container = new Panel();
        }



        private void Registration_Click(object sender, EventArgs e)
        {
            if (tFullName.Text != string.Empty)
            {
                if (tVisitDate.Text != string.Empty)
                {
                    if (cbPrisionerVisited.Text != string.Empty)
                    {
                        Ids = db.select.visitedPrisionerId(cbPrisionerVisited.Text);
                        if (db.insert.Visit(Convert.ToUInt32(Ids[0]), tFullName.Text, tVisitDate.Text)) MessageBox.Show("Visita adicionada com sucesso!!", "", MessageBoxButtons.OK);
                        else MessageBox.Show("Ocorreu um erro. Não foi possível registar a visita.", "", MessageBoxButtons.OK);
                    }
                    else { MessageBox.Show("Campo Recluso Visitado obrigatório."); }
                }
                else { MessageBox.Show("Campo Data da Visita obrigatório."); }
            }
            else { MessageBox.Show("Campo Nome Completo obrigatório."); }
        }

        public void Open()
        {
            

            editPanelBorder = new Panel();
            container.Controls.Add(editPanelBorder);
            editPanelBorder.Location = new Point((container.Width / 5), (container.Width / 14));
            //editPanelBorder.Size = new Size(1000, 707);
            editPanelBorder.Size = new Size(750, 600);
            editPanelBorder.BackColor = Color.Black;
            editPanelBorder.SendToBack();

            editPanel = new Panel();
            editPanelBorder.Controls.Add(editPanel);
            editPanel.Location = new Point((container.Width / 5), (container.Width / 14));
            //editPanel.Size = new Size(993, 700);
            editPanel.Size = new Size(750, 600);
            editPanel.BackColor = Color.FromArgb(242, 242, 242);
            editPanel.BringToFront();
            editPanel.Dock = DockStyle.Fill;

            lFullName = new Label();
            lFullName.Size = new Size(500, 25);
            lFullName.Location = new Point(container.Width * 1 / 20, container.Height * 1 / 10);
            lFullName.Text = "Nome Completo";
            font.Size(lFullName, fontSize);
            lFullName.Font = new Font("SansSerif", 15, FontStyle.Bold);
            //container.Controls.Add(lFullName);
            editPanel.Controls.Add(lFullName);


            tFullName = new TextBox();
            tFullName.Size = new Size(lFullName.Width, lFullName.Height);
            tFullName.Location = new Point(lFullName.Location.X, lFullName.Location.Y + lFullName.Height + 10);
            font.Size(tFullName, fontSize);
            //container.Controls.Add(tFullName);
            editPanel.Controls.Add(tFullName);



            titulo = new Label();
            container.Controls.Add(titulo);
            titulo.Size = new Size(700, 100);
            titulo.Location = new Point(450, 0);
            font.Size(titulo, fontSize);
            titulo.Text = "Registar Visita";
            titulo.Font = new Font("Cambria", 30, FontStyle.Bold);
            titulo.ForeColor = Color.DarkBlue;
            titulo.SendToBack();
            


            lVisitDate = new Label();
            lVisitDate.Size = new Size(tFullName.Width, tFullName.Height);
            lVisitDate.Location = new Point(tFullName.Location.X, tFullName.Location.Y + tFullName.Height + 40);
            lVisitDate.Text = "Data da Visita";
           
            font.Size(lVisitDate, fontSize);
            lVisitDate.Font = new Font("SansSerif", 15, FontStyle.Bold);
            //container.Controls.Add(lVisitDate);
            editPanel.Controls.Add(lVisitDate);


            tVisitDate = new DateTimePicker();
            tVisitDate.Size = new Size(150, lFullName.Height);
            tVisitDate.Location = new Point(lVisitDate.Location.X, lVisitDate.Location.Y + lVisitDate.Height + 10);
            tVisitDate.Format = DateTimePickerFormat.Short;
            font.Size(tVisitDate, fontSize);
            //container.Controls.Add(tVisitDate);
            tVisitDate.MaxDate = DateTime.Today;
            editPanel.Controls.Add(tVisitDate);

            lPrisionerVisited = new Label();
            lPrisionerVisited.Size = new Size(tVisitDate.Width, tVisitDate.Height);
            lPrisionerVisited.Location = new Point(tVisitDate.Location.X, tVisitDate.Location.Y + tVisitDate.Height + 40);
            lPrisionerVisited.Text = "Recluso Visitado";
            
            font.Size(lPrisionerVisited, fontSize);
            lPrisionerVisited.Font = new Font("SansSerif", 15, FontStyle.Bold);
            //container.Controls.Add(lPrisionerVisited);
            editPanel.Controls.Add(lPrisionerVisited);

            cbPrisionerVisited = new ComboBox();
            cbPrisionerVisited.Size = new Size(lFullName.Width - 200 , lFullName.Height);
            cbPrisionerVisited.DropDownStyle = ComboBoxStyle.DropDownList;
            cbPrisionerVisited.Location = new Point(lPrisionerVisited.Location.X, lPrisionerVisited.Location.Y + lPrisionerVisited.Height + 10);
            font.Size(cbPrisionerVisited, fontSize);
            //container.Controls.Add(cbPrisionerVisited);
            editPanel.Controls.Add(cbPrisionerVisited);
            names = db.select.Recluso();
            for (int i = 0; i < names.Count; i++)
            {
                cbPrisionerVisited.Items.Add(names[i].ToString());
            }



            registration = new Button();
            registration.Size = new Size(150, 60);
            registration.Location = new Point(cbPrisionerVisited.Location.X + 200, cbPrisionerVisited.Location.Y + cbPrisionerVisited.Height + 100);
            registration.Text = "Registar";
            font.Size(registration, fontSize);
            //container.Controls.Add(registration);
            registration.Click += new EventHandler(Registration_Click);
            
            registration.TextAlign = ContentAlignment.MiddleCenter;
            editPanel.Controls.Add(registration);
          

            //registration.Font = new Font("Microsoft Sans Serif", 16, FontStyle.Bold);
            //registration.ForeColor = Color.White;
            registration.Cursor = Cursors.Hand;
        }
    }
}
