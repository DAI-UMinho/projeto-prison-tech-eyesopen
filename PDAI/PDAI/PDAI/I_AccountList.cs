using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace PDAI
{
    class I_AccountList
    {
        public Panel container { get; }
        public int locationX { set { container.Location = new Point(value, container.Location.Y); } get { return container.Location.X; } }
        public int locationY { set { container.Location = new Point(container.Location.X, value); } get { return container.Location.Y; } }
        public int width { set { container.Size = new Size(value, container.Height); } get { return container.Width; } }
        public int height { set { container.Size = new Size(container.Width, value); } get { return container.Height; } }

        Database database;
        CustomizableList accountList;
        Label addEmployee;
        I_Person person;
        Font_Class font;
        Dictionary<Button, AccountItem> getAccountItem;
        Color color = Color.FromArgb(196, 196, 196);
        List<AccountItem> accountListItems;
        int lastItemIndex;

        public I_AccountList()
        {
            container = new Panel();
            container.Location = new Point(0, 0);
            container.BackColor = Color.White;
            container.BorderStyle = BorderStyle.Fixed3D;

            font = new Font_Class();
            database = new Database();
            getAccountItem = new Dictionary<Button, AccountItem>();

        }

        public void Open()
        {



            accountList = new CustomizableList();
            container.Controls.Add(accountList.container);
            accountList.width = container.Width * 9 / 10;
            accountList.height = container.Height * 9 / 10;
            accountList.locationX = container.Width / 2 - accountList.width / 2;
            accountList.locationY = container.Height / 2 - accountList.height / 2;
            lastItemIndex = database.select.Get_Accounts(accountList);
            accountList.Update();
            accountListItems = accountList.GetItems();
            foreach (AccountItem accountItem in accountListItems) { accountItem.accountButton.Click += new EventHandler(AccountButton_Click); getAccountItem[accountItem.accountButton] = accountItem; }



            addEmployee = new Label();
            addEmployee.Text = "+ Adicionar funcionário";
            addEmployee.ForeColor = Color.Blue;
            addEmployee.Click += new EventHandler(Add_Employee);
            container.Controls.Add(addEmployee);
            addEmployee.Location = new Point(accountList.locationX, accountList.locationY - 20);
            addEmployee.Size = new Size(200, 20);
            font.Size(addEmployee, 10);


        }


        private void Add_Employee(object sender, EventArgs e)
        {
            container.Controls.Clear();

            person = new I_Person();
            container.Controls.Add(person.container);
            person.width = container.Width * 8 / 10;
            person.height = container.Height * 8 / 10;
            person.locationX = container.Width/2 - person.width/2;
            person.locationY = container.Height / 2 - person.height / 2; ;
            person.Open();
            person.container.Disposed += new EventHandler(Control_Disposed);
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
                case "create":
                    container.Controls.Clear();
                    I_Account add_Account = new I_Account(getAccountItem[((Button)sender)]);
                    container.Controls.Add(add_Account.container);
                    add_Account.width = container.Width * 8 / 10;
                    add_Account.height = container.Height * 8 / 10;
                    add_Account.locationX = container.Width / 2 - add_Account.width / 2;
                    add_Account.locationY = container.Height / 2 - add_Account.height / 2; ;
                    add_Account.Open();
                  //  person.container.Disposed += new EventHandler(Control_Disposed);

                    break;
                case "change":

                    container.Controls.Clear();
                    I_Account alter_Account = new I_Account(getAccountItem[((Button)sender)]);
                    container.Controls.Add(alter_Account.container);
                    alter_Account.width = container.Width * 8 / 10;
                    alter_Account.height = container.Height * 8 / 10;
                    alter_Account.locationX = container.Width / 2 - alter_Account.width / 2;
                    alter_Account.locationY = container.Height / 2 - alter_Account.height / 2; ;
                    alter_Account.Open();

                    break;
                case "activate":
                    MessageBox.Show("Ativar conta");
                    break;
            }

        }

        private void Control_Disposed(object sender, EventArgs e)
        {
            Open();
            accountList.ScrollToLastItem(lastItemIndex);
        }


    }
}
