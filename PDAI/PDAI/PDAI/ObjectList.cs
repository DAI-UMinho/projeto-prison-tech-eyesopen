using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace PDAI
{
    class ObjectList
    {
        public Panel container { get; }
        public int locationX { set { container.Location = new Point(value, container.Location.Y); } get { return container.Location.X; } }
        public int locationY { set { container.Location = new Point(container.Location.X, value); } get { return container.Location.Y; } }
        public int width { set { container.Size = new Size(value, container.Height); } get { return container.Width; } }
        public int height { set { container.Size = new Size(container.Width, value); } get { return container.Height; } }

        List<Panel> items;
        int defaultHeight;
        int index;


        public ObjectList()
        {
            container = new Panel();
            container.Size = new Size(200, 400);
            container.Location = new Point(0, 0);
            container.BackColor = Color.White;
            container.BorderStyle = BorderStyle.FixedSingle;
            container.AutoScroll = true;

            items = new List<Panel>();

        }



        public void AddItem(Panel item)
        {
            defaultHeight = item.Height;
            if (items.Count == 0) item.Location = new Point(-1, -1);
            else item.Location = new Point(items[items.Count - 1].Location.X, items[items.Count - 1].Location.Y + items[items.Count - 1].Height - 1);
            items.Add(item);
            container.Controls.Add(item);

        }



        public void Update()
        {
            bool done = false;
            int shrinker = 0;

            if (container.VerticalScroll.Visible && !done)
            {
                done = true;
                shrinker = 19;
                foreach (Panel item in items)
                {
                    item.Size = new Size(container.Width - shrinker, item.Height);
                    //foreach (object control in item.Controls)
                    //{
                    //    if (control.GetType() == typeof(Button))
                    //    {
                    //        ((Button)control).Location = new Point(((Button)control).Location.X - shrinker, ((Button)control).Location.Y);
                    //    }
                    //    if (control.GetType() == typeof(Label)) ((Label)control).Size = new Size(((Label)control).Width - shrinker, ((Label)control).Height);
                    //}
                }
            }
        }


        public List<Panel> GetItems()
        {
            return items;
        }

        public void ScrollToLastItem(int index)
        {
            this.index = index;
            items[index].BackColor = Color.Beige;
            container.VerticalScroll.Value = items[index].Location.Y;
            container.AutoScrollPosition = new Point(0, items[index].Location.Y);
            Thread setOriginalColor = new Thread(SetOriginalColor);
            setOriginalColor.Start();
        }


        private void ItemClicked(object sender, EventArgs e)
        {
            foreach (Panel item in items)
            {
                if (item != ((Panel)sender)) item.BackColor = Color.White;
            }
        }

        private void SetOriginalColor()
        {
            Thread.Sleep(2500);
            items[index].BackColor = Color.White;
        }

    }
}
