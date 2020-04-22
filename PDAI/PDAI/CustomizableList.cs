using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace PDAI
{
    class CustomizableList
    {
        public Panel container { get; }
        public int locationX { set { container.Location = new Point(value, container.Location.Y); } get { return container.Location.X; } }
        public int locationY { set { container.Location = new Point(container.Location.X, value); } get { return container.Location.Y; } }
        public int width { set { container.Size = new Size(value, container.Height); } get { return container.Width; } }
        public int height { set { container.Size = new Size(container.Width, value); } get { return container.Height; } }

        AccountItem item;
        Dictionary<Panel, uint> id;
        List<AccountItem> items;
        const int defaultHeight = 150;
        int index;
        

        public CustomizableList()
        {
            container = new Panel();
            container.Size = new Size(200, 400);
            container.Location = new Point(0, 0);
            container.BackColor = Color.White;
            container.BorderStyle = BorderStyle.FixedSingle;
            container.AutoScroll = true;

            items = new List<AccountItem>();
            id = new Dictionary<Panel, uint>();
        }



        public void AddItem(uint id, string imgPath, string employeeName, string maritalStatus, string role, bool accountCreated, bool activeAccount)
        {

            item = new AccountItem(container, ItemClicked);
            item.width = container.Width - 2;
            item.height = defaultHeight;
            item.Create(id, imgPath, employeeName, maritalStatus, role, accountCreated, activeAccount);
            this.id[item.item] = id;
            if (items.Count == 0) item.item.Location = new Point(0, 0);
            else item.item.Location = new Point(0, items[items.Count - 1].item.Location.Y + defaultHeight - 1);
            items.Add(item);

           // return item.accountButton;
        }



        public void Update()
        {
            bool done = false;
            int shrinker = 0;

            if (container.VerticalScroll.Visible && !done)
            {
                done = true;
                shrinker = 19;
                foreach (AccountItem thisItem in items)
                {
                    thisItem.item.Size = new Size(container.Width - shrinker, defaultHeight);
                    foreach (object control in thisItem.item.Controls)
                    {
                        if (control.GetType() == typeof(Button))
                        {
                            ((Button)control).Location = new Point(((Button)control).Location.X - shrinker, ((Button)control).Location.Y);
                        }
                        if (control.GetType() == typeof(Label)) ((Label)control).Size = new Size(((Label)control).Width - shrinker, ((Label)control).Height);
                    }
                }
            }
        }


        public List<AccountItem> GetItems()
        {
            return items;
        }

        public void ScrollToLastItem(int index)
        {
            this.index = index;
            items[index].item.BackColor = Color.Beige;
            try
            {
                container.VerticalScroll.Value = items[index].item.Location.Y;
                container.AutoScrollPosition = new Point(0, items[index].item.Location.Y);
            } catch (Exception) { }
            Thread setOriginalColor = new Thread(SetOriginalColor);
            setOriginalColor.Start();
        }


        private void ItemClicked(object sender, EventArgs e)
        {
            foreach (AccountItem item in items)
            {
                if (item.item != ((Panel)sender)) item.item.BackColor = Color.White;
            }
        }

        private void SetOriginalColor()
        {
            Thread.Sleep(2500);
            items[index].item.BackColor = Color.White;
        }


    }
}
