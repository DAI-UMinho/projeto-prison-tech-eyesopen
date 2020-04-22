using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace PDAI
{
    class I_CamGallery
    {
        public List<PictureBox> container_cams { get; }
        List<Cam> cams;
        PictureBox  cam_container;
        Panel content_interface, content;
        Font_Class font;
        Cam cam;
        int fontSize = 8, defaultWidth = 200, defaultHeight = 200, locationX = 50, locationY = 50, width, height;
        int lastLocationX, lastLocationY;

        public I_CamGallery(Panel content_interface)
        {
            this.content_interface = content_interface;
            width = content_interface.Width;
            height = content_interface.Height;

            content = new Panel();
            content.Size = new Size(content_interface.Width, content_interface.Height);
            content.Location = new Point(0,0);

            container_cams = new List<PictureBox>();
            cams = new List<Cam>();
            font = new Font_Class();
        }

        public void AddNewCam(int camIndex, string camName)
        {
            Label lcam1;

            //MessageBox.Show(""+ (cams.Count + 1) * locationX);
            //MessageBox.Show("" + (cams.Count + 1) * defaultWidth);
            //MessageBox.Show("" + content_interface.Width);

            if (container_cams.Count != 0 && container_cams[container_cams.Count-1].Location.X + container_cams[container_cams.Count - 1].Width + container_cams[container_cams.Count - 1].Width+50 > content.Width)
            {
                locationY += 50 + defaultHeight;
                locationX = 50;
                
                cam_container = new PictureBox();
                cam_container.Click += new EventHandler(Cam_Click);
                cam_container.Size = new Size(defaultWidth, defaultHeight);
                cam_container.Location = new Point(locationX, locationY);
                cam_container.SizeMode = PictureBoxSizeMode.StretchImage;
                cam_container.BorderStyle = BorderStyle.FixedSingle;
                content.Controls.Add(cam_container);

                lcam1 = new Label();
                cam_container.Controls.Add(lcam1);
                lcam1.Size = new Size(80, 20);
                lcam1.Location = new Point(0, 0);
                lcam1.Text = camName;
                lcam1.BorderStyle = BorderStyle.FixedSingle;
                font.Size(lcam1, fontSize);
            }
            else
            {
               

                cam_container = new PictureBox();
                cam_container.Click += new EventHandler(Cam_Click);
                cam_container.Size = new Size(defaultWidth, defaultHeight);
                cam_container.Location = new Point(locationX, locationY);
                cam_container.SizeMode = PictureBoxSizeMode.StretchImage;
                cam_container.BorderStyle = BorderStyle.FixedSingle;
                content.Controls.Add(cam_container);

                lcam1 = new Label();
                cam_container.Controls.Add(lcam1);
                lcam1.Size = new Size(80, 20);
                lcam1.Location = new Point(0, 0);
                lcam1.Text = camName;
                lcam1.BorderStyle = BorderStyle.FixedSingle;
                font.Size(lcam1, fontSize);

                locationX += 50 + defaultWidth;
            }

         

            container_cams.Add(cam_container);
            cams.Add(new Cam(0));
            cams[cams.Count-1].BeginWebcam(container_cams[cams.Count - 1]);
        }

        public void Start()
        {
            content_interface.Controls.Add(content);


            //for (int i = 0; i < cams.Count; i++)
            //{
            //    cams[i].BeginWebcam(container_cams[i]);
            //}

        }

     

        public void Cam_Click(object sender, EventArgs e)
        {
            int x=50, y=50,thisWidth= width - 100, thisHeight= height - 100;

            ((PictureBox)sender).BringToFront();
            if (defaultWidth != ((PictureBox)sender).Width)
            {
                thisWidth = defaultWidth;
                thisHeight = defaultHeight;
                x = lastLocationX;
                y = lastLocationY;
            }
            lastLocationX = ((PictureBox)sender).Location.X;
            lastLocationY = ((PictureBox)sender).Location.Y;
            
            ((PictureBox)sender).Location = new Point(x, y);
            ((PictureBox)sender).Size = new Size(thisWidth, thisHeight);
        }

    }
}
