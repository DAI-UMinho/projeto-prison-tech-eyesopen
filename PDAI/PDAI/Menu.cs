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
        public int locationY { set { container.Location = new Point(container.Location.X,value); } get { return container.Location.Y; } }
        public int width { set { container.Size = new Size(value, container.Height); } get { return container.Width; } }
        public int height { set { container.Size = new Size(container.Width ,value); } get { return container.Height; } }

        //Color color = Color.FromArgb(127, 127, 127);
        Color color = Color.Gray;
        Color colorHover = Color.FromArgb(196, 196, 196);
        Color subItemColor = Color.FromArgb(189, 195, 197);
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
                else pane.Location = new Point(0, itemsBottom[itemsBottom.Count - 1].Location.Y - pane.Height +1);
                itemsBottom.Add(pane);
            }


            Label itemName = new Label();
            pane.Controls.Add(itemName);
            itemName.Text = item;
            font.Size(itemName, fontSize);
            itemName.ForeColor = Color.White;
            
            itemName.BorderStyle = BorderStyle.FixedSingle;
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


            Label itemName = new Label();
            pane.Controls.Add(itemName);
            itemName.Text = item;
            font.Size(itemName, fontSize);
            itemName.ForeColor = Color.White;

            itemName.BorderStyle = BorderStyle.FixedSingle;
            itemName.AutoSize = false;
            itemName.TextAlign = ContentAlignment.MiddleCenter;
            itemName.Dock = DockStyle.Fill;
            itemName.MouseHover += new EventHandler(Hover);
            itemName.MouseLeave += new EventHandler(Leave);
            itemName.Click += new EventHandler(clickEvent);

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


            Label itemName = new Label();
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

            return pane;
        }




        public void AddSubItem(Panel item, string subItem, Event clickEvent)
        {
            int itemHeightSubItem = itemHeight * 2 / 3;

            int maxLocationY = 0;
            foreach (Label control in item.Controls)
            {
                int var = control.Location.Y + control.Height;
                if (maxLocationY < var) maxLocationY = var;
            }

            Label itemName = new Label();
            itemName.Text = subItem.Split('-').GetValue(1).ToString();
            itemName.Name = subItem;
            font.Size(itemName, fontSize-3);
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

        }



        private void Hover(object sender, EventArgs e)
        {
            ((Label)sender).BackColor = colorHover;
        }


        private void Leave(object sender, EventArgs e)
        {
            ((Label)sender).BackColor = color;
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
            foreach (Label control in ((Label)sender).Parent.Controls)
            {
                int var = control.Location.Y + control.Height;
                if (maxLocationY < var) maxLocationY = var;
            }
            if (((Label)sender).Name == "+") { ((Label)sender).Name = "-"; ((Label)sender).Parent.Height = maxLocationY;  }
            else { ((Label)sender).Name = "+"; ((Label)sender).Parent.Height = itemHeight;  }

            foreach (Panel item in container.Controls)
            {
                item.Location = new Point(item.Location.X, item.Location.Y);
            }

        }
            
   

    }
}
