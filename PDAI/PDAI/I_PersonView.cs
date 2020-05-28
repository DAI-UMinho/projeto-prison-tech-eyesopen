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
        Label titulo;
        int fontSize = 13;

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

            switch (option)
            {
                case option.viewEmployee:
                    titulo = new Label();
                    container.Controls.Add(titulo);
                    titulo.Size = new Size(700, 100);
                    titulo.Location = new Point(450, 0);
                    font.Size(titulo, fontSize);
                    titulo.Text = "Consultar Funcionário";
                    titulo.Font = new Font("Sitka Banner", 30, FontStyle.Bold);
                    titulo.ForeColor = Color.DarkBlue;
                    titulo.SendToBack();
                    break;

                case option.editEmployee:
                    titulo = new Label();
                    container.Controls.Add(titulo);
                    titulo.Size = new Size(700, 100);
                    titulo.Location = new Point(450, 0);
                    font.Size(titulo, fontSize);
                    titulo.Text = "Editar Funcionário";
                    titulo.Font = new Font("Sitka Banner", 30, FontStyle.Bold);
                    titulo.ForeColor = Color.DarkBlue;
                    titulo.SendToBack();
                    break;

                case option.deleteEmployee:
                    titulo = new Label();
                    container.Controls.Add(titulo);
                    titulo.Size = new Size(700, 100);
                    titulo.Location = new Point(450, 0);
                    font.Size(titulo, fontSize);
                    titulo.Text = "Apagar Funcionário";
                    titulo.Font = new Font("Sitka Banner", 30, FontStyle.Bold);
                    titulo.ForeColor = Color.DarkBlue;
                    titulo.SendToBack();
                    break;

            }

            switch (option)
            {
                case option.viewPrisoner:
                    titulo = new Label();
                    container.Controls.Add(titulo);
                    titulo.Size = new Size(700, 100);
                    titulo.Location = new Point(450, 0);
                    font.Size(titulo, fontSize);
                    titulo.Text = "Consultar Prisioneiro";
                    titulo.Font = new Font("Sitka Banner", 30, FontStyle.Bold);
                    titulo.ForeColor = Color.DarkBlue;
                    titulo.SendToBack();
                    break;

                case option.editPrisoner:
                    titulo = new Label();
                    container.Controls.Add(titulo);
                    titulo.Size = new Size(700, 100);
                    titulo.Location = new Point(450, 0);
                    font.Size(titulo, fontSize);
                    titulo.Text = "Editar Prisioneiro";
                    titulo.Font = new Font("Sitka Banner", 30, FontStyle.Bold);
                    titulo.ForeColor = Color.DarkBlue;
                    titulo.SendToBack();
                    break;

                case option.deletePrisoner:
                    titulo = new Label();
                    container.Controls.Add(titulo);
                    titulo.Size = new Size(700, 100);
                    titulo.Location = new Point(450, 0);
                    font.Size(titulo, fontSize);
                    titulo.Text = "Apagar Prisioneiro";
                    titulo.Font = new Font("Sitka Banner", 30, FontStyle.Bold);
                    titulo.ForeColor = Color.DarkBlue;
                    titulo.SendToBack();
                    break;

            }
            personList = new CustomizableList();
            container.Controls.Add(personList.container);
            personList.width = container.Width * 9 / 10;
            personList.height = container.Height * 8 / 11;
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
                    MessageBox.Show("Editar Funcionario");
                    break;
                case "deleteEmployee":
                    if (MessageBox.Show("Tem certeza que deseja eliminar o Funcionário?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        MessageBox.Show("Apagar Funcionario");
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
                    MessageBox.Show("Editar recluso");
                    break;
                case "deletePrisoner":
                    MessageBox.Show("Apagar recluso");
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
