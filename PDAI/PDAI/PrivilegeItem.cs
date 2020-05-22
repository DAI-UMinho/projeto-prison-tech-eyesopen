using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace PDAI
{
    class PrivilegeItem
    {

        public Panel container { get; }
        public int locationX { set { container.Location = new Point(value, container.Location.Y); } get { return container.Location.X; } }
        public int locationY { set { container.Location = new Point(container.Location.X, value); } get { return container.Location.Y; } }
        public int width { set { container.Size = new Size(value, container.Height); } get { return container.Width; } }
        public int height { set { container.Size = new Size(container.Width, value); } get { return container.Height; } }
        public Dictionary<string, List<CheckBox>> privilegesCheckBoxes { get;  }

        const int defaultHeight = 30;
        List<string> title;
        List<Panel> items;
        Font_Class font;
        Event eventCheckBoxChanged;
        Color color = Color.FromArgb(196, 196, 196);

        public PrivilegeItem(Event eventCheckBoxChanged)
        {
            this.eventCheckBoxChanged = eventCheckBoxChanged;

            privilegesCheckBoxes = new Dictionary<string, List<CheckBox>>();

            container = new Panel();
            container.Size = new Size(200, 400);
            container.Location = new Point(0, 0);
            container.BackColor = Color.White;
            container.BorderStyle = BorderStyle.FixedSingle;
            //container.AutoScroll = true;

            title = new List<string>();
            items = new List<Panel>();
            font = new Font_Class();
        }



        public void AddPrivilege(uint idPrivilegeRole, string privilege, string privilegeFullName, int fontSize, bool isTitle, bool isChecked, CheckBox checkBox)
        {

            container.Height += defaultHeight;
            Panel item = new Panel();
          //  item.BorderStyle = BorderStyle.FixedSingle;
            item.Size = new Size(container.Width, defaultHeight);
            item.BackColor = Color.White;
            if (items.Count == 0) item.Location = new Point(0,0);
            else item.Location = new Point(items[items.Count - 1].Location.X, items[items.Count - 1].Location.Y + items[items.Count - 1].Height - 1);
            container.Controls.Add(item);
            items.Add(item);

            Label lTitle = new Label();
            lTitle.Text = privilege;
            lTitle.ForeColor = Color.FromArgb(127, 127, 127);
            lTitle.Size = new Size(item.Width, item.Height);
            lTitle.Location = new Point(0, 0);
            font.Size(lTitle, fontSize);
            item.Controls.Add(lTitle);
            title.Add(lTitle.Text);


            lTitle.Size = new Size(item.Width - 40, item.Height);

            checkBox.Name = privilegeFullName;
            checkBox.Location = new Point(lTitle.Location.X + lTitle.Width, 0);
            checkBox.Checked = isChecked;
            checkBox.CheckStateChanged += new EventHandler(CheckState_Changed);
            item.Controls.Add(checkBox);

          

            if (!isTitle)
            {
                lTitle.Text = "   " + lTitle.Text;
                lTitle.ForeColor = Color.Black;
                privilegesCheckBoxes[privilegeFullName.Split('-').GetValue(0).ToString()].Add(checkBox);
            }
            else
            {
                 if (!privilegesCheckBoxes.ContainsKey(privilege)) privilegesCheckBoxes[privilege] = new List<CheckBox>();
                checkBox.Name = "title-" + checkBox.Name;
            }

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
                    item.Size = new Size(container.Width - shrinker, defaultHeight);
                    foreach (object control in item.Controls)
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


        public List<Panel> GetItems()
        {
            return items;
        }



        private void ItemClicked(object sender, EventArgs e)
        {
            foreach (Panel item in items)
            {
                if (item != ((Panel)sender)) item.BackColor = Color.White;
            }
        }



        private void CheckState_Changed(object sender, EventArgs e)
        {
            eventCheckBoxChanged.Invoke(sender, e); 
        }



    }
}
