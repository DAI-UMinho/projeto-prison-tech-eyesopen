using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Data.SqlClient;
using System.Globalization;
using AForge.Vision.Motion;

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
        RichTextBox motivo;
        string[] t = new string[0];
        Label lDescription,titulo, lMotivo;
        DateTimePicker date;
        DateTimePicker hour;
        Label lDate;
        Label lHour;
        Button register;
        Button add;
        int listX = 200;
        int listY = 35;
        int fontSize = 13;
        Color color = Color.FromArgb(127, 127, 127);
        Panel editPanelBorder, editPanel;




        public Incidents()
        {
            font = new Font_Class();
            database = new Database();
            container = new Panel();
        }

        public Incidents(List<object> aux)
        {
            register.Text = "Editar";
            description.Text = "" + aux.ElementAt(1);
            pList.Text = "" + aux.ElementAt(0) + " - " + aux.ElementAt(3);
            date.Value = DateTime.ParseExact((string) aux.ElementAt(4), "yyyy-MM-dd", CultureInfo.InvariantCulture);
            hour.Value = DateTime.ParseExact((string)aux.ElementAt(4), "HH:mm", CultureInfo.InvariantCulture);
        }

        public void Open()
        {

            editPanelBorder = new Panel();
            container.Controls.Add(editPanelBorder);
            editPanelBorder.Location = new Point((container.Width / 10), (container.Width / 17));
            //editPanelBorder.Size = new Size(1000, 707);
            editPanelBorder.Size = new Size(1000, 700);
            editPanelBorder.BackColor = Color.Black;
            editPanelBorder.SendToBack();

            editPanel = new Panel();
            editPanelBorder.Controls.Add(editPanel);
            editPanel.Location = new Point((container.Width / 20), (container.Width / 14));
            //editPanel.Size = new Size(993, 700);
            editPanel.Size = new Size(1000, 700);
            editPanel.BackColor = Color.FromArgb(242, 242, 242);
            //editPanel.BackColor = Color.FromArgb(205,205,205);
            editPanel.BringToFront();
            editPanel.Dock = DockStyle.Fill;

            description = new RichTextBox();
            editPanel.Controls.Add(description);
            description.Size = new Size(600, 200);
            description.Location = new Point(260, 400);

            titulo = new Label();
            container.Controls.Add(titulo);
            titulo.Size = new Size(700, 70);
            titulo.Location = new Point(450, 0);
            font.Size(titulo, fontSize);
            titulo.Text = "Registar Ocorrência";
            titulo.Font = new Font("Cambria", 30, FontStyle.Bold);
            titulo.ForeColor = Color.DarkBlue;
            titulo.SendToBack();

            lMotivo = new Label();

            editPanel.Controls.Add(lMotivo);
            lMotivo.Size = new Size(100, 35);
            lMotivo.Location = new Point(260, 260);
            lMotivo.Text = "Motivo";
            lMotivo.BorderStyle = BorderStyle.None;
            font.Size(lMotivo, fontSize);
            lMotivo.Font = new Font("SansSerif", 15, FontStyle.Bold);

            motivo = new RichTextBox();
            editPanel.Controls.Add(motivo);
            motivo.Size = new Size(600, 33);
            motivo.Location = new Point(260, 300);

            lDescription = new Label();
            editPanel.Controls.Add(lDescription);
            lDescription.Size = new Size(150, 150);
            lDescription.Location = new Point(260, 350);
            lDescription.Text = "Descrição";
            lDescription.BorderStyle = BorderStyle.None;
            font.Size(lDescription, fontSize);
            lDescription.Font = new Font("SansSerif", 15, FontStyle.Bold);

            date = new DateTimePicker();
            editPanel.Controls.Add(date);
            date.Size = new Size(200, 50);
            date.Location = new Point(260, 205);
            date.Format = DateTimePickerFormat.Short;

            hour = new DateTimePicker();
            editPanel.Controls.Add(hour);
            hour.Size = new Size(200, 50);
            hour.Location = new Point(650, 205);
            hour.Format = DateTimePickerFormat.Custom;
            hour.CustomFormat = "HH:mm tt";
            hour.Value = DateTime.Now.Date;
            hour.ShowUpDown = true;

            lDate = new Label();
            editPanel.Controls.Add(lDate);
            lDate.Size = new Size(100, 50);
            lDate.Location = new Point(260, 160);
            lDate.Text = "Data";
            lDate.BorderStyle = BorderStyle.None;
            font.Size(lDate, fontSize);
            lDate.Font = new Font("SansSerif", 15, FontStyle.Bold);

            lHour = new Label();
            editPanel.Controls.Add(lHour);
            lHour.Size = new Size(100, 50);
            lHour.Location = new Point(650, 160);
            lHour.Text = "Hora";
            lHour.BorderStyle = BorderStyle.None;
            font.Size(lHour, fontSize);
            lHour.Font = new Font("SansSerif", 15, FontStyle.Bold);

            register = new Button();
            register.Size = new Size(150, 60);
            register.Location = new Point(840, 620);
            register.Text = "Registar";
            font.Size(register, fontSize);
            editPanel.Controls.Add(register);
            register.Click += new EventHandler(Register_Click);

            add = new Button();
            add.Size = new Size(150, 50);
            add.Location = new Point(705, 88);
            add.Text = "Adicionar Intervenientes";
            font.Size(add, fontSize);
            editPanel.Controls.Add(add);
            add.BackColor = Color.White;
            add.Click += new EventHandler(Add_Click);
            //add.BackColor = color;

            listPrisioners(t);
        }


        private void Register_Click(object sender, EventArgs e)
        {
            string[] idPessoas = pList.Text.Split('-');
                string idPessoa = idPessoas[0];
                string data;
                data = "" + date.Value.Year + "-" + date.Value.Month + "-" + date.Value.Day + " " + hour.Value.Hour + ":" + hour.Value.Minute +
                    ":" + hour.Value.Second;

            string motivu = motivo.Text;
            string descricao = description.Text;
            int codigoOcorrencia = 0;
            try
            {
                if (idPessoa.Length > 0 && data.Length > 0 && descricao.Length > 0 && motivu.Length >0)
                {
                    if (descricao.Length <= 100)
                    {
                        database.insert.Ocorrencia(idPessoa, data, motivu, descricao, codigoOcorrencia);
                        MessageBox.Show("Registo efetuado");
                        pList.Text = null;
                        description.Text = null;
                        motivo.Text = null;
                        if (idPessoas.Length > 2)
                        {
                            int i = 2;
                            while(i<idPessoas.Length)
                            {
                                string id = idPessoas[i];
                                database.insert.Reconhecimento(id);
                                
                                i += 2;
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
                editPanel.Controls.Add(pList);
                pList.Size = new Size(600, (listY));
                pList.Location = new Point(260, 50);
                pList.Text = "Não selecionou ninguém";
                pList.Enabled = false;
                pList.BackColor = Color.White;
            }
            else
            {
                pList = new RichTextBox();
                editPanel.Controls.Add(pList);
                pList.Size = new Size(l * 600, listY);
                pList.Location = new Point(260, 50);
                pList.BackColor = Color.White;
                pList.Text = listP[0] + listP[1] + listP[2] + listP[3] + listP[4];
                pList.Enabled = false;
            }
        }


    }
}
