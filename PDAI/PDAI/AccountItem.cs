using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace PDAI
{
    class AccountItem
    {
        public Panel item { get; }
        public PictureBox image { get; }
        public string imagePath { get { return imgPath; } }
        public Button accountButton { get; }
        public Label name { get; }
        public Label maritalStatus { get; }
        public Label employeeRole { get; }
        public uint id { get { return thisId; } }
        public int locationX { set { item.Location = new Point(value, item.Location.Y); } get { return item.Location.X; } }
        public int locationY { set { item.Location = new Point(item.Location.X, value); } get { return item.Location.Y; } }
        public int width { set { item.Size = new Size(value, item.Height); } get { return item.Width; } }
        public int height { set { item.Size = new Size(item.Width, value); } get { return item.Height; } }

        Panel parent;
        Font_Class font;
        int fontSize = 12;
        Event clicked;
        uint thisId;
        string imgPath;
        
        public AccountItem(Panel container, Event clicked)
        {
            this.clicked = clicked;

            parent = container;
            font = new Font_Class();
            item = new Panel();
            image = new PictureBox();
            accountButton = new Button();
            name = new Label();
            maritalStatus = new Label();
            employeeRole = new Label();

        }


        public void Create(uint id, string imgPath, string employeeName, string maritalStatus, string role, bool accountCreated, bool activeAccount)
        {
            int lateral = 12;

            thisId = id;

           

            parent.Controls.Add(item);
            item.Click += new EventHandler(Clicked);
            item.BackColor = Color.White;
            item.BorderStyle = BorderStyle.FixedSingle;

            image.Size = new Size(item.Height - 2 * lateral, item.Height - 2 * lateral);
            image.Location = new Point(lateral, item.Height / 2 - image.Height / 2);
            item.Controls.Add(image);
            try { this.imgPath = IO_Class.FindFile(imgPath);  image.Image = new Bitmap(@"" + this.imgPath); } catch (Exception) { }
            image.SizeMode = PictureBoxSizeMode.StretchImage;
            image.BorderStyle = BorderStyle.Fixed3D;
            image.Click += new EventHandler(Clicked);


            if (accountCreated)
            {
                if (activeAccount) { accountButton.Text = "Alterar Conta"; accountButton.Name = "change"; }
                else { accountButton.Text = "Ativar Conta"; accountButton.Name = "activate";  }
            }
            else { accountButton.Text = "Criar Conta"; accountButton.Name = "create"; }
            accountButton.Size = new Size(item.Height - 4 * lateral, item.Height - 4 * lateral);
            accountButton.Location = new Point(item.Width - accountButton.Width - lateral, item.Height / 2 - accountButton.Height / 2);
            item.Controls.Add(accountButton);
            font.Size(accountButton, fontSize);


            int labelHeight = (accountButton.Height - 2) / 3;
            int labelWidth = 150;
            Label lName = new Label();
            lName.Text = "Nome:";
            lName.Size = new Size(labelWidth, labelHeight);
            lName.Location = new Point(image.Location.X + image.Width + 4 * lateral, accountButton.Location.Y + 1);
            item.Controls.Add(lName);
            font.Size(lName, fontSize);
            lName.Click += new EventHandler(Clicked);


            name.Text = employeeName.ToString();
            name.Size = new Size((accountButton.Location.X - 2 * lateral) - (lName.Location.X + lName.Width), labelHeight);
            name.Location = new Point(lName.Location.X + lName.Width, accountButton.Location.Y + 1);
            item.Controls.Add(name);
            font.Size(name, fontSize);
            name.Click += new EventHandler(Clicked);
           
            

            Label lEmployeeMaritalStatus = new Label();
            lEmployeeMaritalStatus.Text = "Estado Civil:";
            lEmployeeMaritalStatus.Size = new Size(labelWidth, labelHeight);
            lEmployeeMaritalStatus.Location = new Point(lName.Location.X, lName.Location.Y + labelHeight);
            item.Controls.Add(lEmployeeMaritalStatus);
            font.Size(lEmployeeMaritalStatus, fontSize);
            lEmployeeMaritalStatus.Click += new EventHandler(Clicked);

            this.maritalStatus.Text = maritalStatus.ToString().ToUpper();
            this.maritalStatus.Size = new Size(name.Width, labelHeight);
            this.maritalStatus.Location = new Point(lName.Location.X + lName.Width, lEmployeeMaritalStatus.Location.Y);
            item.Controls.Add(this.maritalStatus);
            font.Size(maritalStatus, fontSize);
            this.maritalStatus.Click += new EventHandler(Clicked);


            Label lemployeeRole = new Label();
            lemployeeRole.Text = "Cargo:";
            lemployeeRole.Size = new Size(labelWidth, labelHeight);
            lemployeeRole.Location = new Point(lName.Location.X, lEmployeeMaritalStatus.Location.Y + labelHeight);
            item.Controls.Add(lemployeeRole);
            font.Size(lemployeeRole, fontSize);
            lemployeeRole.Click += new EventHandler(Clicked);


            employeeRole.Text = role.ToString();
            employeeRole.Size = new Size(name.Width, labelHeight);
            employeeRole.Location = new Point(lName.Location.X + lName.Width, lemployeeRole.Location.Y);
            item.Controls.Add(employeeRole);
            font.Size(employeeRole, fontSize);
            employeeRole.Click += new EventHandler(Clicked);

            
        }



        public void Create(uint id, string imgPath, string employeeName, string maritalStatus, string role, option option)
        {
            int lateral = 12;

            thisId = id;

            parent.Controls.Add(item);
            item.Click += new EventHandler(Clicked);
            item.BackColor = Color.White;
            item.BorderStyle = BorderStyle.FixedSingle;

            image.Size = new Size(item.Height - 2 * lateral, item.Height - 2 * lateral);
            image.Location = new Point(lateral, item.Height / 2 - image.Height / 2);
            item.Controls.Add(image);
            try { this.imgPath = IO_Class.FindFile(imgPath); image.Image = new Bitmap(@"" + this.imgPath); } catch (Exception) { }
            image.SizeMode = PictureBoxSizeMode.StretchImage;
            image.BorderStyle = BorderStyle.Fixed3D;
            image.Click += new EventHandler(Clicked);

           


            accountButton.Size = new Size(item.Height - 4 * lateral, item.Height - 4 * lateral);
            accountButton.Location = new Point(item.Width - accountButton.Width - lateral, item.Height / 2 - accountButton.Height / 2);
           // accountButton.Click += new EventHandler();
            item.Controls.Add(accountButton);
            font.Size(accountButton, fontSize);


            switch (option)
            {
                case option.view:
                    accountButton.Image = Properties.Resources.papel; accountButton.Name = ""+option.view;
                    

                    break;
                case option.edit:
                    accountButton.Image = Properties.Resources.desenhar; accountButton.Name = ""+option.edit;
                    break;
                case option.delete:
                    
                    accountButton.Image = Properties.Resources.delete; accountButton.Name = "" + option.delete;
                    break;
            }
            
           


            int labelHeight = (accountButton.Height - 2) / 3;
            int labelWidth = 150;
            Label lName = new Label();
            lName.Text = "Nome:";
            lName.Size = new Size(labelWidth, labelHeight);
            lName.Location = new Point(image.Location.X + image.Width + 4 * lateral, accountButton.Location.Y + 1);
            item.Controls.Add(lName);
            font.Size(lName, fontSize);
            lName.Click += new EventHandler(Clicked);


            name.Text = employeeName.ToString();
            name.Size = new Size((accountButton.Location.X - 2 * lateral) - (lName.Location.X + lName.Width), labelHeight);
            name.Location = new Point(lName.Location.X + lName.Width, accountButton.Location.Y + 1);
            item.Controls.Add(name);
            font.Size(name, fontSize);
            name.Click += new EventHandler(Clicked);


            //Label lEmployeeMaritalStatus = new Label();
            //lEmployeeMaritalStatus.Text = "Estado Civil:";
            //lEmployeeMaritalStatus.Size = new Size(labelWidth, labelHeight);
            //lEmployeeMaritalStatus.Location = new Point(lName.Location.X, lName.Location.Y + labelHeight);
            //item.Controls.Add(lEmployeeMaritalStatus);
            //font.Size(lEmployeeMaritalStatus, fontSize);
            //lEmployeeMaritalStatus.Click += new EventHandler(Clicked);

            //this.maritalStatus.Text = maritalStatus.ToString().ToUpper();
            //this.maritalStatus.Size = new Size(name.Width, labelHeight);
            //this.maritalStatus.Location = new Point(lName.Location.X + lName.Width, lEmployeeMaritalStatus.Location.Y);
            //item.Controls.Add(this.maritalStatus);
            //font.Size(maritalStatus, fontSize);
            //this.maritalStatus.Click += new EventHandler(Clicked);


            Label lemployeeRole = new Label();
            lemployeeRole.Text = "Cargo:";
            lemployeeRole.Size = new Size(labelWidth, labelHeight);
            lemployeeRole.Location = new Point(lName.Location.X, lName.Location.Y + labelHeight);
            item.Controls.Add(lemployeeRole);
            font.Size(lemployeeRole, fontSize);
            lemployeeRole.Click += new EventHandler(Clicked);
           
           



            employeeRole.Text = role.ToString();
            employeeRole.Size = new Size(name.Width, labelHeight);
            employeeRole.Location = new Point(lName.Location.X + lName.Width, lemployeeRole.Location.Y);
            item.Controls.Add(employeeRole);
            font.Size(employeeRole, fontSize);
            employeeRole.Click += new EventHandler(Clicked);
        }


        private void Clicked(object sender, EventArgs e)
        {
            if (item.BackColor == Color.White) item.BackColor = Color.Beige;
            else item.BackColor = Color.White;
            clicked.Invoke(item, new EventArgs());
        }

    }
}
