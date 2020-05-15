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
        CustomizableList employeeList;
        I_Person person;
        Font_Class font;
        Color color = Color.FromArgb(196, 196, 196);
        int lastItemIndex;
        option setOption;
        List<AccountItem> accountListItems;

        public I_PersonView()
        {
            container = new Panel();
            container.Location = new Point(0, 0);
            container.BackColor = Color.White;
            container.BorderStyle = BorderStyle.Fixed3D;

            font = new Font_Class();
            database = new Database();

        }

        public void Open(option option)
        {
            setOption = option;

            employeeList = new CustomizableList();
            container.Controls.Add(employeeList.container);
            employeeList.width = container.Width * 9 / 10;
            employeeList.height = container.Height * 9 / 10;
            employeeList.locationX = container.Width / 2 - employeeList.width / 2;
            employeeList.locationY = container.Height / 2 - employeeList.height / 2;
            lastItemIndex = database.select.Get_Employees(employeeList, option);
            employeeList.Update();
            accountListItems = employeeList.GetItems();
            foreach (AccountItem accountItem in accountListItems) { accountItem.accountButton.Click += new EventHandler(AccountButton_Click);  }



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
            //MessageBox.Show(((Button)sender).Name);

            switch (((Button)sender).Name)
            {
                case "view":
                    MessageBox.Show("Consultar Funcionario");
                    break;
                case "edit":
                    MessageBox.Show("Editar Funcionario");
                    break;
                case "delete":
                    MessageBox.Show("Apagar Funcionario");
                    break;
            }
            

        }

        private void Control_Disposed(object sender, EventArgs e)
        {
            Open(setOption);
            employeeList.ScrollToLastItem(lastItemIndex);
        }

    }
}
