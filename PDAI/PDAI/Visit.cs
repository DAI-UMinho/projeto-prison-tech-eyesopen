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
        Panel visit_interface;
        PictureBox photo;
        Label lFullName, lVisitDate, lCC, lPrisionerVisited;
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
            Ids = db.select.visitedPrisionerId(cbPrisionerVisited.Text);
            if(db.insert.Visit(Convert.ToUInt32(Ids[0]), tFullName.Text, tVisitDate.Text)) MessageBox.Show("Visita adicionada com sucesso!!", "", MessageBoxButtons.OK);
            else MessageBox.Show("Ocorreu um erro. Não foi possível registar a visita.", "", MessageBoxButtons.OK);
            //  System.Diagnostics.Debug.WriteLine( Ids[0].ToString() + "  " + tFullName.Text + "  " + thisDay.ToString("d") + "  " + tVisitDate.Text);
        }

        public void Open()
        {

            lFullName = new Label();
            lFullName.Size = new Size(500, 20);
            lFullName.Location = new Point(container.Width * 1 / 20, container.Height * 1 / 10);
            lFullName.Text = "Nome Completo";
            font.Size(lFullName, fontSize);
            container.Controls.Add(lFullName);


            tFullName = new TextBox();
            tFullName.Size = new Size(lFullName.Width, lFullName.Height);
            tFullName.Location = new Point(lFullName.Location.X, lFullName.Location.Y + lFullName.Height);
            font.Size(tFullName, fontSize);
            container.Controls.Add(tFullName);



            lVisitDate = new Label();
            lVisitDate.Size = new Size(tFullName.Width, tFullName.Height);
            lVisitDate.Location = new Point(tFullName.Location.X, tFullName.Location.Y + tFullName.Height + 40);
            lVisitDate.Text = "Data da Visita";
            font.Size(lVisitDate, fontSize);
            container.Controls.Add(lVisitDate);


            tVisitDate = new DateTimePicker();
            tVisitDate.Size = new Size(150, lFullName.Height);
            tVisitDate.Location = new Point(lVisitDate.Location.X, lVisitDate.Location.Y + lVisitDate.Height);
            tVisitDate.Format = DateTimePickerFormat.Short;
            font.Size(tVisitDate, fontSize);
            container.Controls.Add(tVisitDate);

            lPrisionerVisited = new Label();
            lPrisionerVisited.Size = new Size(tVisitDate.Width, tVisitDate.Height);
            lPrisionerVisited.Location = new Point(tVisitDate.Location.X, tVisitDate.Location.Y + tVisitDate.Height + 40);
            lPrisionerVisited.Text = "Recluso Visitado";
            font.Size(lPrisionerVisited, fontSize);
            container.Controls.Add(lPrisionerVisited);

            cbPrisionerVisited = new ComboBox();
            cbPrisionerVisited.Size = new Size(200, lFullName.Height);
            cbPrisionerVisited.Location = new Point(lPrisionerVisited.Location.X, lPrisionerVisited.Location.Y + lPrisionerVisited.Height);
            font.Size(cbPrisionerVisited, fontSize);
            container.Controls.Add(cbPrisionerVisited);
            names = db.select.Recluso();
            for (int i = 0; i < names.Count; i++)
            {
                cbPrisionerVisited.Items.Add(names[i].ToString());
            }



            registration = new Button();
            registration.Size = new Size(150, 60);
            registration.Location = new Point(cbPrisionerVisited.Location.X, cbPrisionerVisited.Location.Y + cbPrisionerVisited.Height + 50);
            registration.Text = "Registar";
            font.Size(registration, fontSize);
            container.Controls.Add(registration);
            registration.Click += new EventHandler(Registration_Click);
            registration.BackColor = Color.FromArgb(127, 127, 127);
            registration.ForeColor = Color.White;
            registration.Cursor = Cursors.Hand;
        }
    }
}
