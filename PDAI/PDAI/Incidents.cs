using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Data.SqlClient;
using System.Globalization;

namespace PDAI
{
    class Incidents
    {
        public Panel container { get; }
        public int locationX { set { container.Location = new Point(value, container.Location.Y); } get { return container.Location.X; } }
        public int locationY { set { container.Location = new Point(container.Location.X, value); } get { return container.Location.Y; } }
        public int width { set { container.Size = new Size(value, container.Height); } get { return container.Width; } }
        public int height { set { container.Size = new Size(container.Width, value); } get { return container.Height; } }

        Font_Class font;
        Database database;
        RichTextBox description;
        RichTextBox pList;
        string[] t = new string[0];
        Label lDescription;
        DateTimePicker date;
        DateTimePicker hour;
        Label lDate;
        Label lHour;
        Button register;
        Button add;
        Label Titulo;
        int listX = 200;
        int listY = 35;
        int fontSize = 13;
        Color color = Color.FromArgb(127, 127, 127);


        public Incidents()
        {
            font = new Font_Class();
            database = new Database();
            container = new Panel();
        }

        public Incidents(List<object> aux)
        {
            font = new Font_Class();
            database = new Database();
            container = new Panel();
            Open();
            register.Text = "Editar";
            description.Text = "" + aux.ElementAt(1);
            pList.Text = "" + aux.ElementAt(0) + " - " + aux.ElementAt(3);
            date.Value = DateTime.ParseExact((string) aux.ElementAt(4), "yyyy-MM-dd", CultureInfo.InvariantCulture);
            hour.Value = DateTime.ParseExact((string)aux.ElementAt(4), "HH:mm", CultureInfo.InvariantCulture);
        }

        public void Open()
        {
            
            description = new RichTextBox();
            container.Controls.Add(description);
            description.Size = new Size(600, 300);
            description.Location = new Point(350, 410);


            lDescription = new Label();
            container.Controls.Add(lDescription);
            lDescription.Size = new Size(100, 50);
            lDescription.Location = new Point(350, 355);
            lDescription.Text = "Descrição";
            lDescription.BorderStyle = BorderStyle.None;
            font.Size(lDescription, fontSize);

            date = new DateTimePicker();
            container.Controls.Add(date);
            date.Size = new Size(200, 50);
            date.Location = new Point(350, 295);
            date.Format = DateTimePickerFormat.Short;

            hour = new DateTimePicker();
            container.Controls.Add(hour);
            hour.Size = new Size(200, 50);
            hour.Location = new Point(750, 295);
            hour.Format = DateTimePickerFormat.Custom;
            hour.CustomFormat = "HH:mm tt";
            hour.Value = DateTime.Now.Date;
            hour.ShowUpDown = true;

            lDate = new Label();
            container.Controls.Add(lDate);
            lDate.Size = new Size(100, 50);
            lDate.Location = new Point(350, 250);
            lDate.Text = "Data";
            lDate.BorderStyle = BorderStyle.None;
            font.Size(lDate, fontSize);

            Titulo = new Label();
            container.Controls.Add(Titulo);
            Titulo.Size = new Size(900, 100);
            Titulo.Location = new Point(0, 0);
            font.Size(Titulo, fontSize);
            Titulo.Text = "Registar Ocorrência";
            Titulo.Font = new Font("Arial", 40, FontStyle.Bold);
            Titulo.SendToBack();

            lHour = new Label();
            container.Controls.Add(lHour);
            lHour.Size = new Size(100, 50);
            lHour.Location = new Point(750, 250);
            lHour.Text = "Hora";
            lHour.BorderStyle = BorderStyle.None;
            font.Size(lHour, fontSize);


            register = new Button();
            register.Size = new Size(150, 60);
            register.Location = new Point(875, 720);
            register.Text = "Registar";
            font.Size(register, fontSize);
            container.Controls.Add(register);
            register.Click += new EventHandler(Register_Click);
            register.BackColor = color;

            add = new Button();
            add.Size = new Size(150, 60);
            add.Location = new Point(350, 75);
            add.Text = "Adicionar Intervenientes";
            font.Size(add, fontSize);
            container.Controls.Add(add);
            add.Click += new EventHandler(Add_Click);
            add.BackColor = color;

            listPrisioners(t);
        }


        private void Register_Click(object sender, EventArgs e)
        {
            string[] idPessoas = pList.Text.Split('-');
                string idPessoa = idPessoas[0];
                string data;
                data = "" + date.Value.Year + "-" + date.Value.Month + "-" + date.Value.Day + " " + hour.Value.Hour + ":" + hour.Value.Minute +
                    ":" + hour.Value.Second;

                string motivo = lDescription.Text;
                string descricao = description.Text;
                int codigoOcorrencia = 0; 
                try
                {
                    if (idPessoa.Length > 0 && data.Length > 0 && descricao.Length > 0)
                    {
                        if (descricao.Length <= 100)
                        {
                            database.insert.Ocorrencia(idPessoa, data, motivo, descricao, codigoOcorrencia);
                            MessageBox.Show("Registo efetuado");
                            if(idPessoas.Length>2)
                                {
                            int i = 2;
                            while(i<idPessoas.Length)
                            {
                                string id = idPessoas[i];
                                database.insert.Reconhecimento(id);
                                
                                i += 2;
                                MessageBox.Show("Registou mais que um recluso");
                            }
                                }
                        }
                        else
                        {
                            MessageBox.Show("Descricao contem demasiados carateres!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Por Favor Preencha todos os campos!");
                    }
                }
                catch (AccessViolationException ex)
                {
                    System.Windows.Forms.MessageBox.Show("" + ex);
                }
                catch (SqlException ex)
                {
                    System.Windows.Forms.MessageBox.Show("" + ex);
                }

                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("" + ex);
                }
                finally
                {
                    pList.Text = null;
                    description.Text = null;

                }
            
        }

        private void Add_Click(object sender, EventArgs e)
        {
            List<object> var = new List<object>();
            var = database.select.Reclusos();
            //github
            if (var.Count == 0)
            {
                MessageBox.Show("Nao existem reclusos na base de dados.");
                return;
            }
            IncidentsAddForm f1 = new IncidentsAddForm(var, pList);
            f1.Show();
        }

        public void listPrisioners(String[] listP)
        {
            int l = listP.Length;
            if (l == 0)
            {
                pList = new RichTextBox();
                container.Controls.Add(pList);
                pList.Size = new Size(600, (listY));
                pList.Location = new Point(350, 175);
                pList.Text = "Não selecionou ninguém";
                pList.Enabled = false;
            }
            else
            {
                pList = new RichTextBox();
                container.Controls.Add(pList);
                pList.Size = new Size(l * 600, listY);
                pList.Location = new Point(350, 175);
                pList.Text = listP[0] + listP[1] + listP[2] + listP[3] + listP[4];
                pList.Enabled = false;
            }
        }
    }
}
