using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace PDAI
{
    class MaritalStatus
    {
        public Panel container { get; }
        public int width { set { container.Size = new Size(value, container.Height); } get { return container.Width; } }
        public int height { set { container.Size = new Size(container.Width, value); } get { return container.Height; } }

        Database database;
        Font_Class font;
        Button add;
        TextBox tMaritalStatus;
        ListView_Class lv;
        List<string> maritalStatus;
        int fontSize = 16;

        public MaritalStatus()
        {
            container = new Panel();
            container.Size = new Size(200, 400);
            container.Location = new Point(0, 0);
            container.BackColor = Color.White;

            font = new Font_Class();
            database = new Database();
        }


        public void Create()
        {

            Label title = new Label();
            title.Text = "Adicionar/Remover  Estado Civil";
            title.ForeColor = Color.FromArgb(127, 127, 127);
            title.Size = new Size(container.Width, 30);
            title.Location = new Point(0, 0);
            container.Controls.Add(title);
            font.Size(title, fontSize);

            Panel line = new Panel();
            line.BackColor = Color.FromArgb(127, 127, 127);
            line.Size = new Size(container.Width, 1);
            line.BackColor = Color.Black;
            line.Location = new Point(0, title.Location.Y + title.Height);
            container.Controls.Add(line);


            Label lMaritalStatus = new Label();
            lMaritalStatus.Text = "Estado Civil";
            lMaritalStatus.Size = new Size(250, 23);
            lMaritalStatus.Location = new Point(0, line.Location.Y + 80);
            container.Controls.Add(lMaritalStatus);
            font.Size(lMaritalStatus, fontSize - 3);

            tMaritalStatus = new TextBox();
            tMaritalStatus.Size = new Size(250, 30);
            tMaritalStatus.Location = new Point(lMaritalStatus.Location.X, lMaritalStatus.Location.Y + lMaritalStatus.Height + 3);
            tMaritalStatus.TextChanged += new EventHandler(Text_Changed);
            container.Controls.Add(tMaritalStatus);
            font.Size(tMaritalStatus, fontSize - 3);


            add = new Button();
            add.Text = "Adicionar";
            add.Size = new Size(100, 50);
            add.Location = new Point(0, tMaritalStatus.Location.Y + tMaritalStatus.Height + 50);
            add.Click += new EventHandler(Button_Click);
            container.Controls.Add(add);
            font.Size(add, fontSize - 3);



            lv = new ListView_Class();
            lv.Location = new Point(tMaritalStatus.Location.X + tMaritalStatus.Width + container.Width * 1 / 6, lMaritalStatus.Location.Y);
            lv.Width = tMaritalStatus.Width;
            lv.Height = container.Height * 8 / 10 - lv.Location.Y;
            lv.add_Column("Estado Civil", lv.Width - 5);
            lv.Click += new EventHandler(Click);
            font.Size(lv, fontSize - 3);
            container.Controls.Add(lv);

            maritalStatus = database.select.GetMaritalStatus(lv);



        }



        private void Text_Changed(object sender, EventArgs e)
        {
            if (maritalStatus.Contains(((TextBox)sender).Text)) add.Text = "Remover";
            else add.Text = "Adicionar";
        }

        private void Click(object sender, EventArgs e)
        {
            tMaritalStatus.Text = ((ListView_Class)sender).Items[((ListView_Class)sender).getIndexSelectedItem()].Text;
        }



        private void Button_Click(object sender, EventArgs e)
        {
            if (add.Text == "Adicionar") { database.insert.MaritalStatus(tMaritalStatus.Text); }
            else { if (!database.select.UsedMaritalStatus(tMaritalStatus.Text)) database.delete.MaritalStatus(tMaritalStatus.Text);  else  MessageBox.Show("Não é possível eliminar este estado civil porque já está a ser usado por um funcionário."); }
            maritalStatus = new List<string>();
            maritalStatus = database.select.GetMaritalStatus(lv);
            tMaritalStatus.Text = "";
        }

    }
}
