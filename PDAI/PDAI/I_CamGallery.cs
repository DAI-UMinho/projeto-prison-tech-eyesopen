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
        public List<PictureBox> contentor_cams { get; }
        List<Cam> cams;
        PictureBox  cam_contentor;
        Panel content_interface, content;
        Font_Class font;
        Cam cam;
        int fontSize = 8, defaultWidth = 200, defaultHeight = 200, locationX = 50, locationY = 50;

        public I_CamGallery(Panel content_interface)
        {
            this.content_interface = content_interface;

            content = new Panel();
            content.Size = new Size(content_interface.Width, content_interface.Height);
            content.Location = new Point(0,0);

            contentor_cams = new List<PictureBox>();
            cams = new List<Cam>();
            font = new Font_Class();
        }

        public void AddNewCam(int camIndex, string camName)
        {
            Label lcam1;

            //MessageBox.Show(""+ (cams.Count + 1) * locationX);
            //MessageBox.Show("" + (cams.Count + 1) * defaultWidth);
            //MessageBox.Show("" + content_interface.Width);

            if (contentor_cams.Count != 0 && contentor_cams[contentor_cams.Count-1].Location.X + contentor_cams[contentor_cams.Count - 1].Width + contentor_cams[contentor_cams.Count - 1].Width+50 > content.Width)
            {
                locationY += 50 + defaultHeight;
                locationX = 50;
                
                cam_contentor = new PictureBox();
                cam_contentor.Size = new Size(defaultWidth, defaultHeight);
                cam_contentor.Location = new Point(locationX, locationY);
                cam_contentor.SizeMode = PictureBoxSizeMode.StretchImage;
                cam_contentor.BorderStyle = BorderStyle.FixedSingle;
                content.Controls.Add(cam_contentor);

                lcam1 = new Label();
                cam_contentor.Controls.Add(lcam1);
                lcam1.Size = new Size(80, 20);
                lcam1.Location = new Point(0, 0);
                lcam1.Text = camName;
                lcam1.BorderStyle = BorderStyle.FixedSingle;
                font.Size(lcam1, fontSize);
            }
            else
            {
               

                cam_contentor = new PictureBox();
                cam_contentor.Size = new Size(defaultWidth, defaultHeight);
                cam_contentor.Location = new Point(locationX, locationY);
                cam_contentor.SizeMode = PictureBoxSizeMode.StretchImage;
                cam_contentor.BorderStyle = BorderStyle.FixedSingle;
                content.Controls.Add(cam_contentor);

                lcam1 = new Label();
                cam_contentor.Controls.Add(lcam1);
                lcam1.Size = new Size(80, 20);
                lcam1.Location = new Point(0, 0);
                lcam1.Text = camName;
                lcam1.BorderStyle = BorderStyle.FixedSingle;
                font.Size(lcam1, fontSize);

                locationX += 50 + defaultWidth;
            }

         

            contentor_cams.Add(cam_contentor);
            cams.Add(new Cam(0));
            cams[cams.Count-1].BeginWebcam(contentor_cams[cams.Count - 1]);
        }

        public void Start()
        {
            content_interface.Controls.Add(content);


            //for (int i = 0; i < cams.Count; i++)
            //{
            //    cams[i].BeginWebcam(contentor_cams[i]);
            //}

        }

     

    }
}
