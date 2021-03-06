﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.IO;

namespace PDAI
{
    class I_Person
    {
        public Panel container { get; }
        public int locationX { set { container.Location = new Point(value, container.Location.Y); } get { return container.Location.X; } }
        public int locationY { set { container.Location = new Point(container.Location.X, value); } get { return container.Location.Y; } }
        public int width { set { container.Size = new Size(value, container.Height); } get { return container.Width; } }
        public int height { set { container.Size = new Size(container.Width, value); } get { return container.Height; } }
        public string pageTitle { set;get; }

        Font_Class font;
        Database database;
        PictureBox photo;
        Label lFullName, lBirthDate, lCC, lMaritalStatus, lRole, titulo;
        TextBox tFullName, tCC;
        ComboBox cbMaritalStatus, cbRole;
        DateTimePicker tBirthDate;
        Button registration;
        Bitmap bitmapImage;
        string type = "";
        int fontSize = 13;
        string path = Rule.TargetPath, imgPath;
        bool employee, callback;
        Label ladicionarImg;
        Panel editPanelBorder, editPanel;
        uint idPerson;
        string pictureName;

        public I_Person()
        {
            font = new Font_Class();
            database = new Database();


            container = new Panel();
            photo = new PictureBox();
            photo.DoubleClick += new EventHandler(Photo2_DoubleClick);

        }


        public void Open(bool employee, bool callback)
        {
            this.employee = employee;
            this.callback = callback;


            editPanelBorder = new Panel();
            container.Controls.Add(editPanelBorder);
            editPanelBorder.Location = new Point((container.Width / 8), (container.Width / 14));
            editPanelBorder.Size = new Size(1000, 650);
            editPanelBorder.BackColor = Color.Black;
            editPanelBorder.SendToBack();

            editPanel = new Panel();
            editPanelBorder.Controls.Add(editPanel);
            editPanel.Location = new Point((container.Width / 20), (container.Width / 14));
            editPanel.Size = new Size(1000, 650);
            editPanel.BackColor = Color.FromArgb(242, 242, 242);
            editPanel.BringToFront();
            editPanel.Dock = DockStyle.Fill;

            photo.Size = new Size(250, 250);
            photo.Location = new Point(container.Width * 1 / 30, container.Height * 2 / 20);
            photo.SizeMode = PictureBoxSizeMode.StretchImage;
            photo.BorderStyle = BorderStyle.Fixed3D;
            photo.BackColor = Color.White;
            editPanel.Controls.Add(photo);

            ladicionarImg = new Label();
            ladicionarImg.Size = new Size(photo.Width - 1, photo.Height - 1);
            ladicionarImg.Location = new Point(0, 0);
            ladicionarImg.DoubleClick += new EventHandler(Photo_DoubleClick);
            ladicionarImg.Text = "Adicionar" + System.Environment.NewLine + "Imagem" + System.Environment.NewLine + System.Environment.NewLine + "(Duplo Click)";
            ladicionarImg.ForeColor = Color.LightGray;
            ladicionarImg.TextAlign = ContentAlignment.MiddleCenter;
            font.Size(ladicionarImg, fontSize);
            photo.Controls.Add(ladicionarImg);


            lFullName = new Label();
            lFullName.Size = new Size((container.Width - photo.Location.X - photo.Width - ((container.Width * 1 / 25))) * 7 / 10, 25);
            lFullName.Location = new Point(photo.Location.X + photo.Width + (container.Width * 2 / 25), photo.Location.Y);
            lFullName.Text = "Nome Completo";
            font.Size(lFullName, fontSize);
            lFullName.Font = new Font("SansSerif", 15, FontStyle.Bold);
            editPanel.Controls.Add(lFullName);


            tFullName = new TextBox();
            tFullName.Size = new Size(lFullName.Width - 180, lFullName.Height);
            tFullName.Location = new Point(lFullName.Location.X, lFullName.Location.Y + lFullName.Height + 10);
            font.Size(tFullName, fontSize);
            editPanel.Controls.Add(tFullName);



            lBirthDate = new Label();
            lBirthDate.Size = new Size(tFullName.Width, tFullName.Height);
            lBirthDate.Location = new Point(tFullName.Location.X, tFullName.Location.Y + tFullName.Height + container.Height * 1 / 20);
            lBirthDate.Text = "Data Nascimento";
            font.Size(lBirthDate, fontSize);
            lBirthDate.Font = new Font("SansSerif", 15, FontStyle.Bold);
            editPanel.Controls.Add(lBirthDate);


            tBirthDate = new DateTimePicker();
            tBirthDate.Size = new Size(150, lFullName.Height);
            tBirthDate.Location = new Point(lBirthDate.Location.X, lBirthDate.Location.Y + lBirthDate.Height + 10);
            tBirthDate.Format = DateTimePickerFormat.Short;
            tBirthDate.MaxDate = new DateTime(DateTime.Today.Year - 18, DateTime.Today.Month, DateTime.Today.Day);
            font.Size(tBirthDate, fontSize);
            editPanel.Controls.Add(tBirthDate);



            lCC = new Label();
            lCC.Size = new Size(tFullName.Width, tFullName.Height);
            lCC.Location = new Point(tBirthDate.Location.X, tBirthDate.Location.Y + tBirthDate.Height + container.Height * 1 / 20);
            lCC.Text = "Cartão Cidadão";
            font.Size(lCC, fontSize);
            lCC.Font = new Font("SansSerif", 15, FontStyle.Bold);
            editPanel.Controls.Add(lCC);

            tCC = new TextBox();
            tCC.Size = new Size(200, lFullName.Height);
            tCC.Location = new Point(lCC.Location.X, lCC.Location.Y + lCC.Height + 10);
            font.Size(tCC, fontSize);
            editPanel.Controls.Add(tCC);

            titulo = new Label();
            container.Controls.Add(titulo);
            titulo.Size = new Size(700, 100);
            titulo.Location = new Point(450, 0);
            font.Size(titulo, fontSize);
            titulo.Text = pageTitle;
            titulo.Font = new Font("Cambria", 30, FontStyle.Bold);
            titulo.ForeColor = Color.DarkBlue;
            titulo.SendToBack();


            lMaritalStatus = new Label();
            lMaritalStatus.Size = new Size(tFullName.Width, tFullName.Height);
            lMaritalStatus.Location = new Point(tCC.Location.X, tCC.Location.Y + tCC.Height + container.Height * 1 / 20);
            lMaritalStatus.Text = "Estado Civil";
            font.Size(lMaritalStatus, fontSize);
            lMaritalStatus.Font = new Font("SansSerif", 15, FontStyle.Bold);
            editPanel.Controls.Add(lMaritalStatus);

            cbMaritalStatus = new ComboBox();
            cbMaritalStatus.Size = new Size(200, lFullName.Height);
            cbMaritalStatus.Location = new Point(lMaritalStatus.Location.X, lMaritalStatus.Location.Y + lMaritalStatus.Height + 10);
            font.Size(cbMaritalStatus, fontSize);
            editPanel.Controls.Add(cbMaritalStatus);
            cbMaritalStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            List<string> maritalStatus = database.select.GetMaritalStatus();
            foreach (string item in maritalStatus)
            {
                cbMaritalStatus.Items.Add(item);
            }

            int varLocationX = cbMaritalStatus.Location.X;
            int varLocationY = cbMaritalStatus.Location.Y;
            int varLocationHeight = cbMaritalStatus.Height;
            if (employee)
            {
                lRole = new Label();
                lRole.Size = new Size(tFullName.Width, tFullName.Height);
                lRole.Location = new Point(cbMaritalStatus.Location.X, cbMaritalStatus.Location.Y + cbMaritalStatus.Height + container.Height * 1 / 20);
                lRole.Text = "Cargo";
                font.Size(lRole, fontSize);
                lRole.Font = new Font("SansSerif", 15, FontStyle.Bold);
                editPanel.Controls.Add(lRole);

                cbRole = new ComboBox();
                cbRole.Size = new Size(200, lFullName.Height);
                cbRole.Location = new Point(lRole.Location.X, lRole.Location.Y + lRole.Height + 10);
                font.Size(cbRole, fontSize);
                editPanel.Controls.Add(cbRole);
                cbRole.DropDownStyle = ComboBoxStyle.DropDownList;
                List<string> roles = database.select.GetRoles();
                foreach (string item in roles)
                {
                    cbRole.Items.Add(item);
                }

                varLocationX = cbRole.Location.X;
                varLocationY = cbRole.Location.Y;
                varLocationHeight = cbRole.Height;
            }


            registration = new Button();
            registration.Size = new Size(150, 60);
            registration.Location = new Point(varLocationX + 400, varLocationY + varLocationHeight + container.Height * 1 / 30);
            registration.Text = "Registar";
            font.Size(registration, fontSize);
            registration.TextAlign = ContentAlignment.MiddleCenter;
            editPanel.Controls.Add(registration);
            registration.BackColor = Color.FromArgb(255, 255, 255);

            registration.Click += new EventHandler(Registration_Click);
        }


        public void Load(AccountItem accountItem, option value)
        {
            idPerson = accountItem.id;

            if (accountItem.imagePath != String.Empty)
            {
                pictureName = accountItem.imagePath;
                bitmapImage = new Bitmap(accountItem.imagePath);
                photo.Image = bitmapImage;
                photo.Controls.Remove(ladicionarImg);
            }

            tFullName.Text = accountItem.name.Text;
            try { tBirthDate.Text = accountItem.birthDate; } catch (Exception) { }
            tCC.Text = accountItem.cc;
            cbMaritalStatus.Text = accountItem.maritalStatus.Text;
            if (employee) cbRole.Text = accountItem.employeeRole.Text;

            switch (value)
            {
                case option.view:
                    registration.Text = "Voltar";
                    registration.Click -= new EventHandler(Registration_Click);
                    registration.Click += new EventHandler(Dispose_Click);
                    tFullName.ReadOnly = true;
                    tBirthDate.Enabled = false;
                    tCC.Enabled = false;
                    cbMaritalStatus.Enabled = false;
                    if (employee) cbRole.Enabled = false;
                    ladicionarImg.DoubleClick -= new EventHandler(Photo_DoubleClick);
                    photo.DoubleClick -= new EventHandler(Photo2_DoubleClick);
                    break;

                case option.edit:
                    registration.Text = "Editar";
                    break;

            }
            

        }



        private void Registration_Click(object sender, EventArgs e)
        {
            if (tFullName.Text != string.Empty)
            {
                if (tBirthDate.Text != string.Empty)
                {
                    if (tCC.Text != string.Empty)
                    {
                        if (cbMaritalStatus.Text != string.Empty)
                        {
                            if (IsAllDigits(tCC.Text))
                            {
                                if (tCC.Text.Length == 8)
                                {
                                    if (!employee) type = "Prisioneiro";
                                    else if (cbRole.Text != string.Empty) type = cbRole.Text;

                                    if (type != string.Empty)
                                    {
                                        try
                                        {
                                            uint id = 0;
                                            if (registration.Text == "Registar")
                                            {

                                                id = database.insert.Person(tFullName.Text, tBirthDate.Text, tCC.Text, cbMaritalStatus.Text, type);
                                                if (IO_Class.CreateFolder(@"" + path + id.ToString()))
                                                {
                                                    database.update.PersonFolder(id, path + id.ToString());
                                                    MessageBox.Show("Registado com sucesso!");
                                                    IO_Class.CopyFile(imgPath, path + id.ToString());
                                                    tFullName.Text = "";
                                                    tBirthDate.Text = "";
                                                    tCC.Text = "";
                                                    cbMaritalStatus.Items.Clear();
                                                    photo.Image = null;
                                                    if (employee) cbRole.Items.Clear();

                                                }
                                            }
                                            else
                                            {
                                                try
                                                {
                                                    if (!File.Exists(path + idPerson.ToString()))
                                                    {
                                                        IO_Class.CreateFolder(@"" + path + idPerson.ToString());
                                                        database.update.PersonFolder(idPerson, path + idPerson.ToString());
                                                    }
                                                    IO_Class.DeleteFile(pictureName);
                                                    database.update.Person(idPerson, tFullName.Text, tBirthDate.Text, tCC.Text, cbMaritalStatus.Text, type);
                                                    IO_Class.CopyFile(imgPath, path + idPerson.ToString());
                                                    MessageBox.Show("Alterado com sucesso!");
                                                }
                                                catch (Exception eu) { MessageBox.Show(""+eu); }
                                                
                                            }

                                            if (callback) container.Dispose();
                                        }
                                        catch (Exception) { MessageBox.Show("Ocorreu um erro."); }
                                    }
                                    else { MessageBox.Show("Campo cargo obrigatório."); }
                                }
                                else { MessageBox.Show("Introduza um cartão de cidadão válido"); }
                            }
                            else { MessageBox.Show("Campo estado civil só pode conter números."); }
                        }
                        else { MessageBox.Show("Campo estado civil obrigatório."); }
                    }
                    else { MessageBox.Show("Campo cartão cidadão obrigatório."); }
                }
                else { MessageBox.Show("Campo data de nascimento obrigatório."); }
            }
            else { MessageBox.Show("Campo nome obrigatório."); }
        }


        private void Photo_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            if (open.ShowDialog() == DialogResult.OK)
            {
                try { photo.Image.Dispose(); } catch (Exception) { }
                bitmapImage = new Bitmap(open.FileName);
                photo.Image = bitmapImage;
                photo.Controls.Remove((Label)sender);
                imgPath = open.FileName;
            }
        }

        private void Photo2_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            if (open.ShowDialog() == DialogResult.OK)
            {
                try { photo.Image.Dispose(); } catch (Exception) { }
                bitmapImage = new Bitmap(open.FileName);
                photo.Image = bitmapImage;
                photo.Controls.Remove((PictureBox)sender);
                imgPath = open.FileName;
            }
        }

        bool IsAllDigits(string s)
        {
            foreach (char c in s)
            {
                if (!char.IsDigit(c))
                    return false;
            }
            return true;
        }

        private void Dispose_Click(object sender, EventArgs e)
        {
            container.Dispose();
        }

    }
}
