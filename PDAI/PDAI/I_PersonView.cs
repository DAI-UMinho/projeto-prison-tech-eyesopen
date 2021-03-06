﻿using System;
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
        bool setThisAsDisposed;

        public I_PersonView()
        {
            container = new Panel();
            container.Location = new Point(0, 0);
            container.BackColor = Color.White;
            container.BorderStyle = BorderStyle.Fixed3D;
            container.Disposed += new EventHandler(ThisWasDisposed);

            font = new Font_Class();
            database = new Database();
            getAccountItem = new Dictionary<Button, AccountItem>();
            setThisAsDisposed = false;
        }

        public void Open(option option)
        {
            setOption = option;
            container.Controls.Clear();

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
                    titulo.Text = "Eliminar Funcionário";
                    titulo.Font = new Font("Sitka Banner", 30, FontStyle.Bold);
                    titulo.ForeColor = Color.DarkBlue;
                    titulo.SendToBack();
                    break;

                case option.viewPrisoner:
                    titulo = new Label();
                    container.Controls.Add(titulo);
                    titulo.Size = new Size(700, 100);
                    titulo.Location = new Point(450, 0);
                    font.Size(titulo, fontSize);
                    titulo.Text = "Consultar Recluso";
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
                    titulo.Text = "Editar Recluso";
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
                    titulo.Text = "Eliminar Recluso";
                    titulo.Font = new Font("Sitka Banner", 30, FontStyle.Bold);
                    titulo.ForeColor = Color.DarkBlue;
                    titulo.SendToBack();
                    break;

                case option.viewPrisonGuard:
                    titulo = new Label();
                    container.Controls.Add(titulo);
                    titulo.Size = new Size(700, 100);
                    titulo.Location = new Point(450, 0);
                    font.Size(titulo, fontSize);
                    titulo.Text = "Consultar Guarda";
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
            else if(option == option.viewPrisonGuard)
                lastItemIndex = database.select.Get_PrisonGuard(personList, option);
            else
                lastItemIndex = database.select.Get_Prisoners(personList, option);

            personList.Update();
            accountListItems = personList.GetItems();
            foreach (AccountItem accountItem in accountListItems) { accountItem.accountButton.Click += new EventHandler(AccountButton_Click); getAccountItem[accountItem.accountButton] = accountItem; }

        }


        private void AccountButton_Click(object sender, EventArgs e)
        {
            switch (((Button)sender).Name)
            {
                case "viewEmployee":
                    I_Person viewPerson = new I_Person();
                    viewPerson.pageTitle = "Consultar Funcionário";
                    container.Controls.Add(viewPerson.container);
                    viewPerson.width = container.Width;
                    viewPerson.height = container.Height;
                    viewPerson.locationY = 0;
                    viewPerson.Open(true, false);
                    viewPerson.Load(getAccountItem[((Button)sender)],option.view);
                    viewPerson.container.BringToFront();
                    break;
                case "editEmployee":
                    
                    foreach (object pictureBox in ((Button)sender).Parent.Controls) { if (pictureBox.GetType() == typeof(PictureBox)) { try { ((PictureBox)pictureBox).Image.Dispose(); } catch (Exception) { } ((PictureBox)pictureBox).Dispose(); } }
                    I_Person editPerson = new I_Person();
                    editPerson.pageTitle = "Editar Funcionário";
                    container.Controls.Add(editPerson.container);
                    editPerson.width = container.Width;
                    editPerson.height = container.Height;
                    editPerson.locationY = 0;
                    editPerson.Open(true, true);
                    editPerson.Load(getAccountItem[((Button)sender)], option.edit);
                    editPerson.container.BringToFront();
                    editPerson.container.Disposed += new EventHandler(ContainerGotFocus);


                    break;
                case "deleteEmployee":
                    DialogResult dialogResult = MessageBox.Show("Tem a certeza que pretende eliminar o funcionário?", "Eliminar funcionário", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        try
                        {
                            database.delete.Person(getAccountItem[((Button)sender)].id);
                            MessageBox.Show("Eliminado com sucesso.");
                            Open(setOption);
                        }
                        catch (Exception) { MessageBox.Show("Não foi possível eliminar."); }
                       
                    }
                    break;
                case "viewPrisoner":
                    foreach (object pictureBox in ((Button)sender).Parent.Controls) { if (pictureBox.GetType() == typeof(PictureBox)) { try { ((PictureBox)pictureBox).Image.Dispose(); } catch (Exception) { } ((PictureBox)pictureBox).Dispose(); } }
                    I_Person viewPrisoner = new I_Person();
                    viewPrisoner.pageTitle = "Consultar Recluso";
                    container.Controls.Add(viewPrisoner.container);
                    viewPrisoner.width = container.Width;
                    viewPrisoner.height = container.Height;
                    viewPrisoner.locationY = 0;
                    viewPrisoner.Open(false, false);
                    viewPrisoner.Load(getAccountItem[((Button)sender)], option.view);
                    viewPrisoner.container.BringToFront();
                    break;
                case "editPrisoner":
                    I_Person editPrisoner = new I_Person();
                    editPrisoner.pageTitle = "Editar Recluso";
                    container.Controls.Add(editPrisoner.container);
                    editPrisoner.width = container.Width;
                    editPrisoner.height = container.Height;
                    editPrisoner.locationY = 0;
                    editPrisoner.Open(false, true);
                    editPrisoner.Load(getAccountItem[((Button)sender)], option.edit);
                    editPrisoner.container.BringToFront();
                    editPrisoner.container.Disposed += new EventHandler(ContainerGotFocus);
                    break;
                case "deletePrisoner":
                    dialogResult = MessageBox.Show("Tem a certeza que pretende eliminar o recluso?", "Eliminar funcionário", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        try
                        {
                            database.delete.Person(getAccountItem[((Button)sender)].id);
                            MessageBox.Show("Eliminado com sucesso.");
                            Open(setOption);
                        }
                        catch (Exception) { MessageBox.Show("Não foi possível eliminar."); }
                    }
                    break;
                case "viewPrisonGuard":
                    I_Person viewPrisonGuard = new I_Person();
                    viewPrisonGuard.pageTitle = "Consultar Guarda";
                    container.Controls.Add(viewPrisonGuard.container);
                    viewPrisonGuard.width = container.Width;
                    viewPrisonGuard.height = container.Height;
                    viewPrisonGuard.locationY = 0;
                    viewPrisonGuard.Open(true, false);
                    viewPrisonGuard.Load(getAccountItem[((Button)sender)], option.view);
                    viewPrisonGuard.container.BringToFront();
                    break;
            }
            

        }

        private void Control_Disposed(object sender, EventArgs e)
        {
            Open(setOption);
            personList.ScrollToLastItem(lastItemIndex);
        }


        private void ContainerGotFocus(object sender, EventArgs e)
        {
           if(!setThisAsDisposed) Open(setOption);
        }

        private void ThisWasDisposed(object sender, EventArgs e)
        {
            setThisAsDisposed = true;
        }

        public void SetAsDisposed()
        {
            setThisAsDisposed = true;
        }

    }
}
