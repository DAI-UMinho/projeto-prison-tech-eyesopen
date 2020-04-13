using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Data.SqlClient;

namespace PDAI
{
    class Prisoner
    {
        Font_Class font;
        Panel employee_interface;
        PictureBox photo;
        Label lFullName, lBirthDate, lCC, lMaritalStatus;
        TextBox tFullName, tCC;
        ComboBox cbMaritalStatus;
        DateTimePicker tBirthDate;
        Button registration, addImg;
        Database db;
        string type = "Prisioneiro", recordsFolder = "";
        int fontSize = 13;

        public Prisoner(Panel content_interface, int content_width, int content_height)
        {
            font = new Font_Class();
            db = new Database();

            employee_interface = new Panel();
            employee_interface.Size = new Size(content_width, content_height);
            employee_interface.Location = new Point(0, 0);
            content_interface.Controls.Add(employee_interface);
            employee_interface.BackColor = Color.FromArgb(196, 196, 196);

            photo = new PictureBox();
            photo.Size = new Size(250, 250);
            photo.Location = new Point(content_width * 1 / 30, content_height * 1 / 20);
            employee_interface.Controls.Add(photo);
            //photo.BackColor = Color.Beige;
            photo.Image = Properties.Resources.preso1;
            photo.SizeMode = PictureBoxSizeMode.StretchImage;


            lFullName = new Label();
            lFullName.Size = new Size((content_width - photo.Location.X - photo.Width - ((content_width * 1 / 25))) * 7 / 10, 25);
            lFullName.Location = new Point(photo.Location.X + photo.Width + (content_width * 2 / 25), content_height * 1 / 10);
            lFullName.Text = "Nome Completo";
            font.Size(lFullName, fontSize);
            employee_interface.Controls.Add(lFullName);


            tFullName = new TextBox();
            tFullName.Size = new Size(lFullName.Width, lFullName.Height);
            tFullName.Location = new Point(lFullName.Location.X, lFullName.Location.Y + lFullName.Height);
            font.Size(tFullName, fontSize);
            employee_interface.Controls.Add(tFullName);



            lBirthDate = new Label();
            lBirthDate.Size = new Size(tFullName.Width, tFullName.Height);
            lBirthDate.Location = new Point(tFullName.Location.X, tFullName.Location.Y + tFullName.Height + 40);
            lBirthDate.Text = "Data Nascimento";
            font.Size(lBirthDate, fontSize);
            employee_interface.Controls.Add(lBirthDate);


            tBirthDate = new DateTimePicker();
            tBirthDate.Size = new Size(150, lFullName.Height);
            tBirthDate.Location = new Point(lBirthDate.Location.X, lBirthDate.Location.Y + lBirthDate.Height);
            tBirthDate.Format = DateTimePickerFormat.Short;
            font.Size(tBirthDate, fontSize);
            employee_interface.Controls.Add(tBirthDate);
            tBirthDate.MinDate = new DateTime(1900, 1, 1);
            tBirthDate.MaxDate = new DateTime(2005, 1, 1);



            lCC = new Label();
            lCC.Size = new Size(tFullName.Width, tFullName.Height);
            lCC.Location = new Point(tBirthDate.Location.X, tBirthDate.Location.Y + tBirthDate.Height + 40);
            lCC.Text = "Cartão Cidadão";
            font.Size(lCC, fontSize);
            employee_interface.Controls.Add(lCC);

            tCC = new TextBox();
            tCC.Size = new Size(200, lFullName.Height);
            tCC.Location = new Point(lCC.Location.X, lCC.Location.Y + lCC.Height);
            font.Size(tCC, fontSize);
            employee_interface.Controls.Add(tCC);


            lMaritalStatus = new Label();
            lMaritalStatus.Size = new Size(tFullName.Width, tFullName.Height);
            lMaritalStatus.Location = new Point(tCC.Location.X, tCC.Location.Y + tCC.Height + 40);
            lMaritalStatus.Text = "Estado Civil";
            font.Size(lMaritalStatus, fontSize);
            employee_interface.Controls.Add(lMaritalStatus);

            cbMaritalStatus = new ComboBox();
            cbMaritalStatus.Size = new Size(200, lFullName.Height);
            cbMaritalStatus.Location = new Point(lMaritalStatus.Location.X, lMaritalStatus.Location.Y + lMaritalStatus.Height);
            font.Size(cbMaritalStatus, fontSize);
            employee_interface.Controls.Add(cbMaritalStatus);
            cbMaritalStatus.Items.Add("Solteiro(a)");
            cbMaritalStatus.Items.Add("Casado(a)");
            cbMaritalStatus.Items.Add("Divorciado(a)");
            cbMaritalStatus.Items.Add("Viúvo(a)");
            cbMaritalStatus.Items.Add("Separado(a)");


            registration = new Button();
            registration.Size = new Size(150, 60);
            registration.Location = new Point(cbMaritalStatus.Location.X, cbMaritalStatus.Location.Y + cbMaritalStatus.Height + 50);
            registration.Text = "Registar";
            font.Size(registration, fontSize);
            employee_interface.Controls.Add(registration);
            registration.Click += new EventHandler(Registration_Click);
            registration.BackColor = Color.FromArgb(127, 127, 127);
            registration.ForeColor = Color.White;
            registration.Cursor = Cursors.Hand;

            addImg = new Button();
            addImg.Size = new Size(250, 60);
            addImg.Location = new Point(content_width * 1 / 30, content_height * 1 / 5);
            employee_interface.Controls.Add(addImg);
            addImg.Text = "Adicionar Imagem";
            addImg.BackColor = Color.FromArgb(127, 127, 127);
            addImg.ForeColor = Color.White;
            addImg.Cursor = Cursors.Hand;
            font.Size(addImg, fontSize);



        }

        private void Registration_Click(object sender, EventArgs e)
        {

            //Guarda foto na pasta registos

            try

            {
                if (cbMaritalStatus.SelectedItem != null)
                {
                    if (tFullName.Text != null && tFullName.Text.Length > 10 && !(tFullName.Text.Any(char.IsDigit)))
                    {
                        if (tCC.Text != null && tCC.Text.All(char.IsDigit) && tCC.Text.Length == 8)
                        {
                            db.insert.Person(tFullName.Text, tBirthDate.Text, tCC.Text, cbMaritalStatus.Text, type, "/pasta/Recluso");
                            MessageBox.Show("Registo efetuado");
                        }
                        else
                        {
                            MessageBox.Show("Preencha Corretamente o CC");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Preencha Corretamente o Nome Completo");
                    }
                }
                else
                {
                    MessageBox.Show("Escolha uma Opcao do Estado Civil");
                }

            }
            

            catch (AccessViolationException ex)
            {
                System.Windows.Forms.MessageBox.Show("" + ex);
            }

            catch(SqlException ex)
            {
                System.Windows.Forms.MessageBox.Show("" + ex);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("" + ex);
            }
            

        }


    }
}
