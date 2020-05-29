using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace PDAI
{
    class Menu
    {
        public Panel container { get; }
        public int locationX { set { container.Location = new Point(value, container.Location.Y); } get { return container.Location.X; } }
        public int locationY { set { container.Location = new Point(container.Location.X, value); } get { return container.Location.Y; } }
        public int width { set { container.Size = new Size(value, container.Height); } get { return container.Width; } }
        public int height { set { container.Size = new Size(container.Width, value); } get { return container.Height; } }

        //Color color = Color.FromArgb(127, 127, 127);
        Color color = Color.Gray;
        Color colorHover = Color.FromArgb(112,219, 219);
        Color subItemColor = Color.FromArgb(112, 219, 219);
        List<Panel> itemsTop, itemsBottom;
        Font_Class font;
        int fontSize = 15;
        int itemWidth, itemHeight;

        public Menu()
        {

            container = new Panel();
            container.Location = new Point(0, 0);
            container.BackColor = color;
            container.BorderStyle = BorderStyle.Fixed3D;

            itemsTop = new List<Panel>();
            itemsBottom = new List<Panel>();
            font = new Font_Class();


        }


        public void Open()
        {
            itemWidth = container.Width - 5;
            itemHeight = container.Height / 10;
        }


        public Panel AddItem(string item, Event clickEvent, MenuPosition menuPosition)
        {
            Panel pane = new Panel();
            container.Controls.Add(pane);
            pane.Size = new Size(itemWidth, itemHeight);
            pane.BackColor = color;
            pane.BorderStyle = BorderStyle.FixedSingle;

            if (menuPosition == MenuPosition.top)
            {
                if (itemsTop.Count == 0) pane.Location = new Point(0, 0);
                else pane.Location = new Point(0, itemsTop[itemsTop.Count - 1].Location.Y + itemsTop[itemsTop.Count - 1].Height - 1);
                itemsTop.Add(pane);
            }
            else
            {
                if (itemsBottom.Count == 0) pane.Location = new Point(0, container.Height - pane.Height - 4);
                else pane.Location = new Point(0, itemsBottom[itemsBottom.Count - 1].Location.Y - pane.Height + 1);
                itemsBottom.Add(pane);
            }


            Button itemName = new Button();
            pane.Controls.Add(itemName);
            itemName.Text = item;
            font.Size(itemName, fontSize);
            itemName.ForeColor = Color.White;

            // itemName.BorderStyle = BorderStyle.FixedSingle;
            itemName.AutoSize = false;
            itemName.TextAlign = ContentAlignment.MiddleCenter;
            itemName.Dock = DockStyle.Fill;
            itemName.MouseHover += new EventHandler(Hover);
            itemName.MouseLeave += new EventHandler(Leave);
            itemName.Click += new EventHandler(clickEvent);

            return pane;
        }

        public Panel AddItem(string item, Event clickEvent, MenuPosition menuPosition, int width, int height)
        {
            Panel pane = new Panel();
            container.Controls.Add(pane);
            //int thisItemWidth = itemWidth;
            //int thisItemHeight = itemHeight;
            if (width != 0) itemWidth = width;
            if (height != 0) itemHeight = height;
            pane.Size = new Size(itemWidth, itemHeight);
            pane.BackColor = color;
            pane.BorderStyle = BorderStyle.FixedSingle;

            if (menuPosition == MenuPosition.top)
            {
                if (itemsTop.Count == 0) pane.Location = new Point(0, 0);
                else pane.Location = new Point(0, itemsTop[itemsTop.Count - 1].Location.Y + itemsTop[itemsTop.Count - 1].Height - 1);
                itemsTop.Add(pane);
            }
            else
            {
                if (itemsBottom.Count == 0) pane.Location = new Point(0, container.Height - pane.Height - 4);
                else pane.Location = new Point(0, itemsBottom[itemsBottom.Count - 1].Location.Y - pane.Height + 1);
                itemsBottom.Add(pane);
            }


            Button itemName = new Button();
            pane.Controls.Add(itemName);
            itemName.Text = item;
            font.Size(itemName, fontSize);
            itemName.ForeColor = Color.White;

            // itemName.BorderStyle = BorderStyle.FixedSingle;
            itemName.AutoSize = false;
            itemName.TextAlign = ContentAlignment.MiddleCenter;
            itemName.Dock = DockStyle.Fill;
            itemName.MouseHover += new EventHandler(Hover);
            itemName.MouseLeave += new EventHandler(Leave);
            itemName.Click += new EventHandler(clickEvent);
            itemName.Image = Properties.Resources.icon__2_;
            itemName.ImageAlign = ContentAlignment.MiddleLeft;
            itemName.TextAlign = ContentAlignment.MiddleRight;
            itemName.Font =new Font("Microsoft Sans Serif", 13);
        

            return pane;
        }





        public Panel AddItem(string item, MenuPosition menuPosition)
        {
            //itemWidth = container.Height / 10;
            //itemHeight = itemHeight * 2 / 3;

            Panel pane = new Panel();
            container.Controls.Add(pane);
            pane.Size = new Size(itemWidth, itemHeight);
            pane.BackColor = color;
            pane.BorderStyle = BorderStyle.FixedSingle;

            if (menuPosition == MenuPosition.top)
            {
                if (itemsTop.Count == 0) pane.Location = new Point(0, 0);
                else pane.Location = new Point(0, itemsTop[itemsTop.Count - 1].Location.Y + itemsTop[itemsTop.Count - 1].Height - 1);
                itemsTop.Add(pane);
                
            }
            else
            {
                if (itemsBottom.Count == 0) pane.Location = new Point(0, container.Height - pane.Height - 4);
                else pane.Location = new Point(0, itemsBottom[itemsBottom.Count - 1].Location.Y - pane.Height + 1);
                itemsBottom.Add(pane);
            }


            Button itemName = new Button();
            pane.Controls.Add(itemName);
            itemName.Text = item;
            itemName.Name = "+";
            font.Size(itemName, fontSize);
            itemName.ForeColor = Color.White;
            //itemName.BackColor = Color.White;
            itemName.Size = new Size(itemWidth, itemHeight);
            itemName.AutoSize = false;
            itemName.TextAlign = ContentAlignment.MiddleCenter;
            // itemName.Dock = DockStyle.Fill;
            itemName.MouseHover += new EventHandler(Hover);
            itemName.MouseLeave += new EventHandler(Leave);
            itemName.Click += new EventHandler(Slide);
           

            itemsTop.Add(pane);


            if (item == "Estatística")
            {
                itemName.Image = Properties.Resources.grafico;
                itemName.ImageAlign = ContentAlignment.MiddleLeft;
                itemName.TextAlign = ContentAlignment.MiddleCenter;
                itemName.Font = new Font("Microsoft Sans Serif", 14);
            }
            else if (item == "Câmara")
            {
                itemName.Image = Properties.Resources.camera_de_seguranca;
                itemName.ImageAlign = ContentAlignment.MiddleLeft;
                itemName.TextAlign = ContentAlignment.MiddleCenter;
                itemName.Font = new Font("Microsoft Sans Serif", 14);
            }
            else if (item == "Recluso")
            {
                itemName.Image = Properties.Resources.prisioneiro;
                itemName.ImageAlign = ContentAlignment.MiddleLeft;
                itemName.TextAlign = ContentAlignment.MiddleCenter;
                itemName.Font = new Font("Microsoft Sans Serif", 14);
            }
            else if (item == "Funcionário")
            {
                itemName.Image = Properties.Resources.biologo;
                itemName.ImageAlign = ContentAlignment.MiddleLeft;
                itemName.TextAlign = ContentAlignment.MiddleCenter;
                itemName.Font = new Font("Microsoft Sans Serif", 14);
            }
            else if (item == "Guarda")
            {
                itemName.Image = Properties.Resources.policial;
                itemName.ImageAlign = ContentAlignment.MiddleLeft;
                itemName.TextAlign = ContentAlignment.MiddleCenter;
                itemName.Font = new Font("Microsoft Sans Serif", 14);
            }
            else if (item == "Visita")
            {
                itemName.Image = Properties.Resources.visita;
                itemName.ImageAlign = ContentAlignment.MiddleLeft;
                itemName.TextAlign = ContentAlignment.MiddleCenter;
                itemName.Font = new Font("Microsoft Sans Serif", 14);
            }
            else if (item == "Ocorrência")
            {
                itemName.Image = Properties.Resources.assassinato;
                itemName.ImageAlign = ContentAlignment.MiddleLeft;
                itemName.TextAlign = ContentAlignment.MiddleCenter;
                itemName.Font = new Font("Microsoft Sans Serif", 14);
            }

            else if (item == "Alerta")
            {
                itemName.Image = Properties.Resources.alerta;
                itemName.ImageAlign = ContentAlignment.MiddleLeft;
                itemName.TextAlign = ContentAlignment.MiddleCenter;
                itemName.Font = new Font("Microsoft Sans Serif", 14);
            }
            else if (item == "Conta")
            {
                itemName.Image = Properties.Resources.do_utilizador;
                itemName.ImageAlign = ContentAlignment.MiddleLeft;
                itemName.TextAlign = ContentAlignment.MiddleCenter;
                itemName.Font = new Font("Microsoft Sans Serif", 14);
            }

            return pane;
           

        }




        public void AddSubItem(Panel item, string subItem, Event clickEvent)
        {
            int itemHeightSubItem = itemHeight * 2 / 3;

            int maxLocationY = 0;
            foreach (Button control in item.Controls)
            {
                int var = control.Location.Y + control.Height;
                if (maxLocationY < var) maxLocationY = var;
            }

            Button itemName = new Button();
            itemName.Text = subItem.Split('-').GetValue(1).ToString();
            itemName.Name = subItem;
            font.Size(itemName, fontSize - 3);
            itemName.Size = new Size(itemWidth, itemHeightSubItem);
            itemName.Location = new Point(0, maxLocationY);
            itemName.ForeColor = Color.White;
            //itemName.BackColor = subItemColor;
            itemName.AutoSize = false;
            itemName.TextAlign = ContentAlignment.MiddleCenter;
            // itemName.Dock = DockStyle.Fill;
            itemName.MouseHover += new EventHandler(Hover);
            itemName.MouseLeave += new EventHandler(Leave);
            itemName.Click += new EventHandler(clickEvent);

            item.Controls.Add(itemName);
            //  item.Height = itemDefaultHeight;

            //subItemName.Click += new EventHandler(clickEvent);

            if (subItem == "Privilégio Funcionário-Registar")
            {
                itemName.Image = Properties.Resources.mais__1_;
                itemName.ImageAlign = ContentAlignment.MiddleLeft;
                itemName.TextAlign = ContentAlignment.MiddleCenter;
                itemName.Font = new Font("Microsoft Sans Serif", 14);
            }
            else if (subItem == "Privilégio Recluso-Registar")
            {
                itemName.Image = Properties.Resources.mais__1_;
                itemName.ImageAlign = ContentAlignment.MiddleLeft;
                itemName.TextAlign = ContentAlignment.MiddleCenter;
                itemName.Font = new Font("Microsoft Sans Serif", 14);

            }

            else if (subItem == "Privilégio Visita-Registar")
            {
                itemName.Image = Properties.Resources.mais__1_;
                itemName.ImageAlign = ContentAlignment.MiddleLeft;
                itemName.TextAlign = ContentAlignment.MiddleCenter;
                itemName.Font = new Font("Microsoft Sans Serif", 14);
            }



            else if (subItem == "Privilégio Ocorrência-Registar")
            {
                itemName.Image = Properties.Resources.mais__1_;
                itemName.ImageAlign = ContentAlignment.MiddleLeft;
                itemName.TextAlign = ContentAlignment.MiddleCenter;
                itemName.Font = new Font("Microsoft Sans Serif", 14);
            }

            else if (subItem == "Privilégio Conta-Registar")
            {
                itemName.Image = Properties.Resources.mais__1_;
                itemName.ImageAlign = ContentAlignment.MiddleLeft;
                itemName.TextAlign = ContentAlignment.MiddleCenter;
                itemName.Font = new Font("Microsoft Sans Serif", 14);
            }
            else if (subItem == "Privilégio Ocorrência-Editar")
            {
                itemName.Image = Properties.Resources.lapis;
                itemName.ImageAlign = ContentAlignment.MiddleLeft;
                itemName.TextAlign = ContentAlignment.MiddleCenter;
                itemName.Font = new Font("Microsoft Sans Serif", 14);
            }

            else if (subItem == "Privilégio Funcionário-Editar")
            {
                itemName.Image = Properties.Resources.lapis;
                itemName.ImageAlign = ContentAlignment.MiddleLeft;
                itemName.TextAlign = ContentAlignment.MiddleCenter;
                itemName.Font = new Font("Microsoft Sans Serif", 14);
            }

            else if (subItem == "Privilégio Recluso-Editar")
            {
                itemName.Image = Properties.Resources.lapis;
                itemName.ImageAlign = ContentAlignment.MiddleLeft;
                itemName.TextAlign = ContentAlignment.MiddleCenter;
                itemName.Font = new Font("Microsoft Sans Serif", 14);
            }



            else if (subItem == "Privilégio Recluso-Editar")
            {
                itemName.Image = Properties.Resources.lapis;
                itemName.ImageAlign = ContentAlignment.MiddleLeft;
                itemName.TextAlign = ContentAlignment.MiddleCenter;
                itemName.Font = new Font("Microsoft Sans Serif", 14);
            }

            else if (subItem == "Privilégio Visita-Editar")
            {
                itemName.Image = Properties.Resources.lapis;
                itemName.ImageAlign = ContentAlignment.MiddleLeft;
                itemName.TextAlign = ContentAlignment.MiddleCenter;
                itemName.Font = new Font("Microsoft Sans Serif", 14);
            }

            else if (subItem == "Privilégio Conta-Editar")
            {
                itemName.Image = Properties.Resources.lapis;
                itemName.ImageAlign = ContentAlignment.MiddleLeft;
                itemName.TextAlign = ContentAlignment.MiddleCenter;
                itemName.Font = new Font("Microsoft Sans Serif", 14);
            }
            else if (subItem == "Privilégio Recluso-Consultar")
            {
                itemName.Image = Properties.Resources.portable_document_format;
                itemName.ImageAlign = ContentAlignment.MiddleLeft;
                itemName.TextAlign = ContentAlignment.MiddleCenter;
                itemName.Font = new Font("Microsoft Sans Serif", 14);
            }
            else if (subItem == "Privilégio Estatística-Consultar")
            {
                itemName.Image = Properties.Resources.portable_document_format;
                itemName.ImageAlign = ContentAlignment.MiddleLeft;
                itemName.TextAlign = ContentAlignment.MiddleCenter;
                itemName.Font = new Font("Microsoft Sans Serif", 14);
            }

            else if (subItem == "Privilégio Funcionário-Consultar")
            {
                itemName.Image = Properties.Resources.portable_document_format;
                itemName.ImageAlign = ContentAlignment.MiddleLeft;
                itemName.TextAlign = ContentAlignment.MiddleCenter;
                itemName.Font = new Font("Microsoft Sans Serif", 14);
            }

            else if (subItem == "Privilégio Guarda-Consultar")
            {
                itemName.Image = Properties.Resources.portable_document_format;
                itemName.ImageAlign = ContentAlignment.MiddleLeft;
                itemName.TextAlign = ContentAlignment.MiddleCenter;
                itemName.Font = new Font("Microsoft Sans Serif", 14);
            }

            else if (subItem == "Privilégio Prisioneiro-Consultar")
            {
                itemName.Image = Properties.Resources.portable_document_format;
                itemName.ImageAlign = ContentAlignment.MiddleLeft;
                itemName.TextAlign = ContentAlignment.MiddleCenter;
                itemName.Font = new Font("Microsoft Sans Serif", 14);
            }

            else if (subItem == "Privilégio Visita-Consultar")
            {
                itemName.Image = Properties.Resources.portable_document_format;
                itemName.ImageAlign = ContentAlignment.MiddleLeft;
                itemName.TextAlign = ContentAlignment.MiddleCenter;
                itemName.Font = new Font("Microsoft Sans Serif", 14);
            }

            else if (subItem == "Privilégio Alerta-Consultar")
            {
                itemName.Image = Properties.Resources.portable_document_format;
                itemName.ImageAlign = ContentAlignment.MiddleLeft;
                itemName.TextAlign = ContentAlignment.MiddleCenter;
                itemName.Font = new Font("Microsoft Sans Serif", 14);
            }

            else if (subItem == "Privilégio Ocorrência-Consultar")
            {
                itemName.Image = Properties.Resources.portable_document_format;
                itemName.ImageAlign = ContentAlignment.MiddleLeft;
                itemName.TextAlign = ContentAlignment.MiddleCenter;
                itemName.Font = new Font("Microsoft Sans Serif", 14);
            }

            else if (subItem == "Privilégio Câmara-Consultar")
            {
                itemName.Image = Properties.Resources.portable_document_format;
                itemName.ImageAlign = ContentAlignment.MiddleLeft;
                itemName.TextAlign = ContentAlignment.MiddleCenter;
                itemName.Font = new Font("Microsoft Sans Serif", 14);
            }

            else if (subItem == "Privilégio Câmara-Consultar Deteção")
            {
                itemName.Image = Properties.Resources.eye;
                itemName.ImageAlign = ContentAlignment.MiddleLeft;
                itemName.TextAlign = ContentAlignment.MiddleCenter;
                itemName.Font = new Font("Microsoft Sans Serif", 10);
            }

            else if (subItem == "Privilégio Conta-Alterar Credenciais")
            {
                itemName.Image = Properties.Resources.change;
                itemName.ImageAlign = ContentAlignment.MiddleLeft;
                itemName.TextAlign = ContentAlignment.MiddleCenter;
                itemName.Font = new Font("Microsoft Sans Serif", 10);
            }

        }



        private void Hover(object sender, EventArgs e)
        {
            ((Button)sender).BackColor = colorHover;
        }


        private void Leave(object sender, EventArgs e)
        {
            ((Button)sender).BackColor = color;
        }


        //private void SubLeave(object sender, EventArgs e)
        //{
        //    ((Label)sender).BackColor = subItemColor;
        //}


        private void Slide(object sender, EventArgs e)
        {
            foreach (Panel item in container.Controls)
            {
                item.Height = itemHeight;
            }

            int maxLocationY = 0;
            foreach (Button control in ((Button)sender).Parent.Controls)
            {
                int var = control.Location.Y + control.Height;
                if (maxLocationY < var) maxLocationY = var;
            }
            if (((Button)sender).Name == "+") { ((Button)sender).Name = "-"; ((Button)sender).Parent.Height = maxLocationY; }
            else { ((Button)sender).Name = "+"; ((Button)sender).Parent.Height = itemHeight; }

            foreach (Panel item in container.Controls)
            {
                item.Location = new Point(item.Location.X, item.Location.Y);
            }

        }



    }
}
