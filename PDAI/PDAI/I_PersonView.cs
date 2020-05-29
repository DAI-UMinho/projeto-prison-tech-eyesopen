using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace PDAI
{
    class I_PersonView
    {

        public Panel container { get; }
        public int locationX { set { container.Location = new Point(value, container.Location.Y); } get { return container.Location.X; } }
        public int locationY { set { container.Location = new Point(container.Location.X, value); } get { return container.Location.Y; } }
        public int width { set { container.Size = new Size(value, container.Height); } get { return container.Width; } }
        public int height { set { container.Size = new Size(container.Width, value); } get { return container.Height; } }

        Database database;
        CustomizableList personList;
        I_Person person;
        Font_Class font;
        Color color = Color.FromArgb(196, 196, 196);
        int lastItemIndex;
        option setOption;
        List<AccountItem> accountListItems;
        Dictionary<Button, AccountItem> getAccountItem;

        public I_PersonView()
        {
            container = new Panel();
            container.Location = new Point(0, 0);
            container.BackColor = Color.White;
            container.BorderStyle = BorderStyle.Fixed3D;

            font = new Font_Class();
            database = new Database();
            getAccountItem = new Dictionary<Button, AccountItem>();
        }

        public void Open(option option)
        {
            setOption = option;


            personList = new CustomizableList();
            container.Controls.Add(personList.container);
            personList.width = container.Width * 9 / 10;
            personList.height = container.Height * 9 / 10;
            personList.locationX = container.Width / 2 - personList.width / 2;
            personList.locationY = container.Height / 2 - personList.height / 2;

            if (option == option.viewEmployee || option == option.editEmployee || option == option.deleteEmployee)
                lastItemIndex = database.select.Get_Employees(personList, option);
            else
                lastItemIndex = database.select.Get_Prisoners(personList, option);

            personList.Update();
            accountListItems = personList.GetItems();
            foreach (AccountItem accountItem in accountListItems) { accountItem.accountButton.Click += new EventHandler(AccountButton_Click); getAccountItem[accountItem.accountButton] = accountItem; }

        }





        private void Add_Employee(object sender, EventArgs e)
        {
            //container.Controls.Clear();

            //person = new I_Person();
            //container.Controls.Add(person.container);
            //person.width = container.Width * 8 / 10;
            //person.height = container.Height * 8 / 10;
            //person.locationX = container.Width / 2 - person.width / 2;
            //person.locationY = container.Height / 2 - person.height / 2; ;
            //person.Open(true, true);
            //person.container.Disposed += new EventHandler(Control_Disposed);
            // person.container.BackColor = Color.White;


            // PictureBox photo = new PictureBox();
            // photo.Size = new Size(person.locationY, person.locationY);
            // photo.Location = new Point(person.locationX, person.locationY / 2 - photo.Height / 2);
            // container.Controls.Add(photo);
            // photo.Image = Properties.Resources.Seta1;
            // photo.SizeMode = PictureBoxSizeMode.StretchImage;
            //// photo.BorderStyle = BorderStyle.Fixed3D;
            // photo.BackColor = color;
        }


        private void AccountButton_Click(object sender, EventArgs e)
        {
            switch (((Button)sender).Name)
            {
                case "viewEmployee":
                    I_Person viewPerson = new I_Person();
                    container.Controls.Add(viewPerson.container);
                    viewPerson.width = container.Width;
                    viewPerson.height = container.Height;
                    viewPerson.locationY = 0;
                    viewPerson.Open(true, false);
                    viewPerson.Load(getAccountItem[((Button)sender)]);
                    viewPerson.container.BringToFront();
                    break;
                case "editEmployee":
                    I_Person editPerson = new I_Person();
                    container.Controls.Add(editPerson.container);
                    editPerson.width = container.Width;
                    editPerson.height = container.Height;
                    editPerson.locationY = 0;
                    editPerson.Open(true, false);
                    editPerson.Load(getAccountItem[((Button)sender)]);
                    editPerson.container.BringToFront();
                    break;
                case "deleteEmployee":
                    DialogResult dialogResult = MessageBox.Show("Tem a certeza que pretende eliminar o funcionário?", "Eliminar funcionário", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        MessageBox.Show("\"Eliminado com sucesso.\"");
                    }
                    break;
                case "viewPrisoner":
                    I_Person viewPrisoner = new I_Person();
                    container.Controls.Add(viewPrisoner.container);
                    viewPrisoner.width = container.Width;
                    viewPrisoner.height = container.Height;
                    viewPrisoner.locationY = 0;
                    viewPrisoner.Open(false, false);
                    viewPrisoner.Load(getAccountItem[((Button)sender)]);
                    viewPrisoner.container.BringToFront();
                    break;
                case "editPrisoner":
                    I_Person editPrisoner = new I_Person();
                    container.Controls.Add(editPrisoner.container);
                    editPrisoner.width = container.Width;
                    editPrisoner.height = container.Height;
                    editPrisoner.locationY = 0;
                    editPrisoner.Open(false, false);
                    editPrisoner.Load(getAccountItem[((Button)sender)]);
                    editPrisoner.container.BringToFront();
                    break;
                    break;
                case "deletePrisoner":
                    dialogResult = MessageBox.Show("Tem a certeza que pretende eliminar o recluso?", "Eliminar funcionário", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        MessageBox.Show("\"Eliminado com sucesso.\"");
                    }
                    break;
            }
            

        }

        private void Control_Disposed(object sender, EventArgs e)
        {
            Open(setOption);
            personList.ScrollToLastItem(lastItemIndex);
        }

    }
}
