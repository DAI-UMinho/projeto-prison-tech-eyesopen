using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Controls;

namespace PDAI
{
    class I_CamGallery
    {
        public Panel container { get; }
        //public int locationX { set { container.Location = new Point(value, container.Location.Y); } get { return container.Location.X; } }
        //public int locationY { set { container.Location = new Point(container.Location.X, value); } get { return container.Location.Y; } }
        //public int width { set { container.Size = new Size(value, container.Height); } get { return container.Width; } }
        //public int height { set { container.Size = new Size(container.Width, value); } get { return container.Height; } }

        public List<System.Windows.Forms.PictureBox> contentor_cams { get; }
        List<Cam> cams;
        System.Windows.Forms.PictureBox cam_contentor;
        Panel content_interface, content, save;
        Font_Class font;
        Cam cam;
        int fontSize = 8, defaultWidth = 200, defaultHeight = 200, locationX = 50, locationY = 50, width, height;
        int lastLocationX, lastLocationY, saveWidth, saveHeight;
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;
        double var;


        public I_CamGallery(Panel content_interface, int content_width, int content_height)
        {
            container = new Panel();
            save = content_interface;
            saveWidth = content_width;
            saveHeight = content_height;

            content = new Panel();
            content.Size = new Size(content_width, content_height);
            content.Location = new System.Drawing.Point(0, 0);
            content_interface.Controls.Add(content);
            content.BackColor = Color.FromArgb(196, 196, 196);

            // show device list

            // enumerate video devices
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            if (videoDevices.Count == 0)
            {
                throw new Exception();
            }

            for (int i = 1, n = videoDevices.Count; i <= n; i++)
            {
                string cameraName = i + " : " + videoDevices[i - 1].Name;
                Panel p = new Panel();
                content.Controls.Add(p);
                p.Dock = DockStyle.Left;
                p.Size = new Size(250, 300);
                p.Padding = new Padding(5, 5, 5, 5);

                Label l = new Label();
                p.Controls.Add(l);
                l.Text = cameraName;
                l.Dock = DockStyle.Top;
                l.Size = new Size(245, 30);

                AForge.Controls.VideoSourcePlayer pb = new AForge.Controls.VideoSourcePlayer();
                p.Controls.Add(pb);
                pb.Dock = DockStyle.Top;
                pb.Size = new Size(245, 260);
                pb.MouseDoubleClick += new MouseEventHandler(pb_MouseDoubleClick);
                pb.Cursor = Cursors.Hand;

                VideoCaptureDevice videoSource1 = new VideoCaptureDevice(videoDevices[i - 1].MonikerString);
                videoSource1.DesiredFrameRate = 10;

                pb.VideoSource = videoSource1;
                pb.Start();

                p.Name = "Panel" + cameraName;
                l.Name = "Label" + cameraName;
                pb.Name = "VideoSourcePlayer" + cameraName;

            }
        }



        public void StopCameras()
        {
            for (int i = 1, n = videoDevices.Count; i <= n; i++)
            {
                string videoSource = "VideoSourcePlayer" + i + " : " + videoDevices[i - 1].Name;

                (content.Controls.Find(videoSource, true).FirstOrDefault() as VideoSourcePlayer).SignalToStop();
                (content.Controls.Find(videoSource, true).FirstOrDefault() as VideoSourcePlayer).WaitForStop();
            }

        }

        public I_CamGallery()
        {
        }

        private void pb_MouseDoubleClick(Object sender, MouseEventArgs e)
        {
            var = Char.GetNumericValue((sender as AForge.Controls.VideoSourcePlayer).Name.ToString(), 17);
            save.Controls.Clear();
            viewCam vc = new viewCam(save, saveWidth, saveHeight, var);
        }

    }
}

