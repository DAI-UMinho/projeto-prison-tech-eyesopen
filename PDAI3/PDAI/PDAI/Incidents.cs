using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace PDAI
{
    class Incidents
    {
        Font_Class font;
        Panel incidents_interface;
        Database db;
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
        int listX = 200;
        int listY = 35;
        int fontSize = 13;


        public Incidents(Panel content_interface, int content_width, int content_height)
        {
            font = new Font_Class();
            db = new Database();

            incidents_interface = new Panel();
            incidents_interface.Size = new Size(content_width, content_height);
            incidents_interface.Location = new Point(0, 0);
            content_interface.Controls.Add(incidents_interface);

            listPrisioners(t);

            description = new RichTextBox();
            incidents_interface.Controls.Add(description);
            description.Size = new Size(600, 300);
            description.Location = new Point(500, 500);

            lDescription = new Label();
            incidents_interface.Controls.Add(lDescription);
            lDescription.Size = new Size(100, 50);
            lDescription.Location = new Point(500, 450);
            lDescription.Text = "Descrição";
            lDescription.BorderStyle = BorderStyle.None;
            font.Size(lDescription, fontSize);

            date = new DateTimePicker();
            incidents_interface.Controls.Add(date);
            date.Size = new Size(200, 50);
            date.Location = new Point(500, 400);
            date.Format = DateTimePickerFormat.Short;

            hour = new DateTimePicker();
            incidents_interface.Controls.Add(hour);
            hour.Size = new Size(200, 50);
            hour.Location = new Point(900, 400);
            hour.Format = DateTimePickerFormat.Custom;
            hour.CustomFormat = "HH:mm tt";
            hour.Value = DateTime.Now.Date;
            hour.ShowUpDown = true;

            lDate = new Label();
            incidents_interface.Controls.Add(lDate);
            lDate.Size = new Size(100, 50);
            lDate.Location = new Point(500, 350);
            lDate.Text = "Data";
            lDate.BorderStyle = BorderStyle.None;
            font.Size(lDate, fontSize);

            lHour = new Label();
            incidents_interface.Controls.Add(lHour);
            lHour.Size = new Size(100, 50);
            lHour.Location = new Point(900, 350);
            lHour.Text = "Hora";
            lHour.BorderStyle = BorderStyle.None;
            font.Size(lHour, fontSize);


            register = new Button();
            register.Size = new Size(150, 60);
            register.Location = new Point(100, 100);
            register.Text = "Registar";
            font.Size(register, fontSize);
            incidents_interface.Controls.Add(register);
            register.Click += new EventHandler(Register_Click);

            add = new Button();
            add.Size = new Size(150, 60);
            add.Location = new Point(500, 200);
            add.Text = "Adicionar Intervenientes";
            font.Size(add, fontSize);
            incidents_interface.Controls.Add(add);
            add.Click += new EventHandler(Add_Click);

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
            int codigoOcorrencia = 0; //ver onde esta isto
            try
            {
                if (idPessoa.Length > 0 && data.Length > 0 && descricao.Length > 0)
                {
                    if (descricao.Length <= 100)
                    {
                        db.insert.Ocorrencia(idPessoa, data, motivo, descricao, codigoOcorrencia);
                        MessageBox.Show("Registo efetuado");
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
            catch (Exception f)
            {

            }
            finally
            {
                pList.Text = null;
                description = null;

            }
        }
    

        private void Add_Click(object sender, EventArgs e)
        {
            List<object> var = new List<object>();
            var = db.select.Reclusos();
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
                incidents_interface.Controls.Add(pList);
                pList.Size = new Size((listX), (listY));
                pList.Location = new Point(500, 300);
                pList.Text = "Não selecionou ninguém";
                pList.Enabled = false;
            }
            else
            {
                pList = new RichTextBox();
                incidents_interface.Controls.Add(pList);
                pList.Size = new Size(l * listX, listY);
                pList.Location = new Point(500, 300);
                pList.Text = listP[0] + listP[1] + listP[2] + listP[3] + listP[4];
                pList.Enabled = false;
            }
        }
    }
}
