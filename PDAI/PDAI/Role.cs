using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace PDAI
{
    class Role
    {

        public Panel container { get; }
        public int width { set { container.Size = new Size(value, container.Height); } get { return container.Width; } }
        public int height { set { container.Size = new Size(container.Width, value); } get { return container.Height; } }

        Database database;
        Font_Class font;
        Button add;
        TextBox tRole;
        ListView_Class lv;
        List<string> roles;
        int fontSize = 16;

        public Role()
        {
            container = new Panel();
            container.Size = new Size(200, 400);
            container.Location = new Point(0, 0);
            container.BackColor = Color.White;

            font = new Font_Class();
            database = new Database();
        }


        public void Open()
        {

            Label title = new Label();
            title.Text = "Adicionar/Remover  Cargo";
            title.ForeColor = Color.FromArgb(127, 127, 127);
            title.Size = new Size(container.Width, 30);
            title.Location = new Point(0,0);
            container.Controls.Add(title);
            font.Size(title, fontSize);

            Panel line = new Panel();
            line.BackColor = Color.FromArgb(127, 127, 127);
            line.Size = new Size(container.Width, 1);
            line.BackColor = Color.Black;
            line.Location = new Point(0, title.Location.Y + title.Height);
            container.Controls.Add(line);


            Label lRole = new Label();
            lRole.Text = "Cargo";
            lRole.Size = new Size(100, 23);
            lRole.Location = new Point(0, line.Location.Y + 80);
            container.Controls.Add(lRole);
            font.Size(lRole, fontSize-3);

            tRole = new TextBox();
            tRole.Size = new Size(250, 30);
            tRole.Location = new Point(lRole.Location.X, lRole.Location.Y + lRole.Height+3);
            tRole.TextChanged += new EventHandler(Text_Changed);
            container.Controls.Add(tRole);
            font.Size(tRole, fontSize - 3);


            add = new Button();
            add.Text = "Adicionar";
            add.Size = new Size(100, 50);
            add.Location = new Point(0, tRole.Location.Y + tRole.Height + 50);
            add.Click += new EventHandler(Button_Click);
            container.Controls.Add(add);
            font.Size(add, fontSize - 3);



            lv = new ListView_Class();
            lv.Location = new Point(tRole.Location.X + tRole.Width + container.Width * 1 / 6, lRole.Location.Y);
            lv.Width = tRole.Width;
            lv.Height = container.Height * 8/10 - lv.Location.Y;
            lv.add_Column("Cargo", lv.Width-5);
            lv.Click += new EventHandler(Click);
            font.Size(lv, fontSize - 3);
            container.Controls.Add(lv);

            roles = database.select.GetRoles(lv); 



        }



        private void Text_Changed(object sender, EventArgs e)
        {
            if(roles.Contains(((TextBox)sender).Text)) add.Text = "Remover";
            else add.Text = "Adicionar";
        }

        private void Click(object sender, EventArgs e)
        {
            tRole.Text = ((ListView_Class)sender).Items[((ListView_Class)sender).getIndexSelectedItem()].Text;
        }



        private void Button_Click(object sender, EventArgs e)
        {
            if (add.Text == "Adicionar") { database.insert.Role(tRole.Text); }
            else { if (!database.select.UsedRole(tRole.Text)) database.delete.Role(tRole.Text); else MessageBox.Show("Não é possível eliminar este cargo porque já está a ser usado por um funcionário."); }
            roles = new List<string>();
            roles = database.select.GetRoles(lv);
            tRole.Text = "";
        }

    }
}
