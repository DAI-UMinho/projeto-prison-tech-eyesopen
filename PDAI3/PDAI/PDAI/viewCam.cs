using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using PDAI;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Controls;

namespace PDAI
{
    class viewCam
    {
        Font_Class font;
        Panel viewCam_interface, save, editPanel, editPanelBorder, camPanel;
        Database db;
        Button pause, start;
        int fontSize = 13, saveWidth, saveHeight;
        double var;
        AForge.Controls.VideoSourcePlayer pb;
        private FilterInfoCollection videoDevices;
        AForge.Controls.PictureBox pauseImg;

        public viewCam(Panel content_interface, int content_width, int content_height, double dbl)
        {
            var = dbl;
            save = content_interface;
            saveWidth = content_width;
            saveHeight = content_height;

            font = new Font_Class();
            db = new Database();

            viewCam_interface = new Panel();
            viewCam_interface.Size = new Size(content_width, content_height);
            viewCam_interface.Location = new System.Drawing.Point(0, 0);
            content_interface.Controls.Add(viewCam_interface);
            viewCam_interface.BackColor = Color.FromArgb(196, 196, 196);

            camPanel = new Panel();
            viewCam_interface.Controls.Add(camPanel);
            camPanel.Location = new System.Drawing.Point((viewCam_interface.Width / 5), (viewCam_interface.Height / 17));
            camPanel.Size = new Size(993, 800);
            camPanel.BackColor = Color.Transparent;

            pb = new VideoSourcePlayer();
            camPanel.Controls.Add(pb);
            pb.Dock = DockStyle.Fill;

            pauseImg = new AForge.Controls.PictureBox();
            camPanel.Controls.Add(pauseImg);
            pauseImg.Location = new System.Drawing.Point(pb.Location.X,pb.Location.Y);
            pauseImg.Size = new Size(pb.Size.Width,pb.Size.Height);
            pauseImg.Visible = false;
            pauseImg.SizeMode = PictureBoxSizeMode.StretchImage;

            pause = new Button();
            viewCam_interface.Controls.Add(pause);
            pause.Size = new Size(60,60);
            pause.Location = new System.Drawing.Point((camPanel.Location.X + (camPanel.Size.Width/2)), (camPanel.Location.Y + camPanel.Size.Height));
            pause.BackgroundImage = Properties.Resources.Pause_Button_PNG_File;
            pause.FlatStyle = FlatStyle.Flat;
            pause.BackgroundImageLayout = ImageLayout.Stretch;
            pause.Cursor = Cursors.Hand;
            pause.Click += new EventHandler(Pause_Click);
            

            start = new Button();
            viewCam_interface.Controls.Add(start);
            start.Size = new Size(60, 60);
            start.Location = new System.Drawing.Point((camPanel.Location.X + (camPanel.Size.Width / 2)), (camPanel.Location.Y + camPanel.Size.Height));
            start.BackgroundImage = Properties.Resources.iconfinder_ic_play_arrow_48px_352072;
            start.FlatStyle = FlatStyle.Flat;
            start.Cursor = Cursors.Hand;
            start.BackgroundImageLayout = ImageLayout.Stretch;
            start.Click += new EventHandler(Start_Click);

            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            VideoCaptureDevice videoSource1 = new VideoCaptureDevice(videoDevices[Convert.ToInt32(var)-1].MonikerString);
            videoSource1.DesiredFrameRate = 10;

            pb.VideoSource = videoSource1;
            pb.Start();
            videoSource1.NewFrame += new NewFrameEventHandler(video_NewFrame);

        }

        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pauseImg.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void Pause_Click(object sender, EventArgs e)
        {
            pauseImg.Visible = true;
            pb.Visible = false;
            pause.Visible = false;
            start.Visible = true;
            pb.SignalToStop();

        }

        private void Start_Click(object sender, EventArgs e)
        {
            pb.Start();
            pauseImg.Visible = false;
            pb.Visible = true;
            pause.Visible = true;
            start.Visible = false;
        }
    }
}
