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
using Emgu.CV;
using Emgu.CV.Face;
using Emgu.CV.Structure;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Threading;


namespace PDAI
{
    class viewCamNRec
    {
        public Panel container { get; }
        public int locationX { set { container.Location = new System.Drawing.Point(value, container.Location.Y); } get { return container.Location.X; } }
        public int locationY { set { container.Location = new System.Drawing.Point(container.Location.X, value); } get { return container.Location.Y; } }
        public int width { set { container.Size = new Size(value, container.Height); } get { return container.Width; } }
        public int height { set { container.Size = new Size(container.Width, value); } get { return container.Height; } }

        Database db;

        public List<System.Windows.Forms.PictureBox> contentor_cams { get; }
        List<Cam> cams;
        System.Windows.Forms.PictureBox cam_contentor;
        Panel content_interface, content, save, camPanel;
        Font_Class font;
        Cam cam;
        int fontSize = 8;
        //defaultWidth = 200, defaultHeight = 200, locationX = 50, locationY = 50, width, height;
        private VideoCaptureDevice videoSource;
        double var;

        Button pause, start, apagar;
        AForge.Controls.VideoSourcePlayer pb;
        private FilterInfoCollection videoDevices;
        AForge.Controls.PictureBox pauseImg, pic;
        public EigenFaceRecognizer FaceRecognition { get; set; }
        public CascadeClassifier FaceDetection { get; set; }
        public CascadeClassifier EyesDetection { get; set; }

        public Mat Frame { get; set; }

        public List<Image<Gray, byte>> Faces { get; set; }
        public List<int> IDs { get; set; }

        public int ProcessedImageWidth = 100;
        public int ProcessedImageHeight = 100;


        public int TimerCounter { set; get; } = 0;
        public int TimerLimit { set; get; } = 20;
        public int ScanCounter { set; get; } = 0;

        public string YMLPath { set; get; } = Application.StartupPath + "/TrainingData/trainingData.xml";

        public System.Windows.Forms.Timer Timer { set; get; }

        public bool FaceSquare { set; get; } = false;
        public bool EyeSquare { set; get; } = false;
        public bool FaceDetected { set; get; } = false;
        public List<int> FacesName;

        Image<Bgr, byte> frameImg;

        Image<Gray, byte> grayImg;

        Image<Gray, byte> trainImg;

        Image<Gray, byte> trainImg_II;

        Rectangle[] rectangles;

        Bitmap bitmap;


        public viewCamNRec()
        {
            container = new Panel();
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


        private void pb_MouseDoubleClick(Object sender, MouseEventArgs e)
        {
            var = Char.GetNumericValue((sender as AForge.Controls.VideoSourcePlayer).Name.ToString(), 17);
            container.Controls.Clear();

            Frame = new Mat();
            Faces = new List<Image<Gray, byte>>();
            IDs = new List<int>();

            FacesName = new List<int>();

            font = new Font_Class();
            db = new Database();



            camPanel = new Panel();
            container.Controls.Add(camPanel);
            camPanel.Location = new System.Drawing.Point((container.Width / 5), (container.Height / 17));
            camPanel.Size = new Size(993, 800);
            camPanel.BackColor = Color.Transparent;

            pb = new VideoSourcePlayer();
            camPanel.Controls.Add(pb);
            pb.Dock = DockStyle.Fill;
            //pb.Visible = false;

            pic = new AForge.Controls.PictureBox();
            camPanel.Controls.Add(pic);
            pic.Location = new System.Drawing.Point(pb.Location.X, pb.Location.Y);
            pic.Size = new Size(pb.Size.Width, pb.Size.Height);
            pic.SizeMode = PictureBoxSizeMode.StretchImage;

            pauseImg = new AForge.Controls.PictureBox();
            camPanel.Controls.Add(pauseImg);
            pauseImg.Location = new System.Drawing.Point(pb.Location.X, pb.Location.Y);
            pauseImg.Size = new Size(pb.Size.Width, pb.Size.Height);
            pauseImg.Visible = false;
            pauseImg.SizeMode = PictureBoxSizeMode.StretchImage;

            pause = new Button();
            container.Controls.Add(pause);
            pause.Size = new Size(60, 60);
            pause.Location = new System.Drawing.Point((camPanel.Location.X + (camPanel.Size.Width / 2)), (camPanel.Location.Y + camPanel.Size.Height));
            pause.BackgroundImage = Properties.Resources.Pause_Button_PNG_File;
            pause.FlatStyle = FlatStyle.Flat;
            pause.BackgroundImageLayout = ImageLayout.Stretch;
            pause.Cursor = Cursors.Hand;
            pause.Click += new EventHandler(Pause_Click);


            start = new Button();
            container.Controls.Add(start);
            start.Size = new Size(60, 60);
            start.Location = new System.Drawing.Point((camPanel.Location.X + (camPanel.Size.Width / 2)), (camPanel.Location.Y + camPanel.Size.Height));
            start.BackgroundImage = Properties.Resources.iconfinder_ic_play_arrow_48px_352072;
            start.FlatStyle = FlatStyle.Flat;
            start.Cursor = Cursors.Hand;
            start.BackgroundImageLayout = ImageLayout.Stretch;
            start.Click += new EventHandler(Start_Click);


            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            VideoCaptureDevice videoSource1 = new VideoCaptureDevice(videoDevices[Convert.ToInt32(var) - 1].MonikerString);
            videoSource1.DesiredFrameRate = 10;

            videoSource1.NewFrame += Device_NewFrame;
            pb.VideoSource = videoSource1;
            pb.Start();
            videoSource1.NewFrame += new NewFrameEventHandler(video_NewFrame);

        }

        public void Open()
        {
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
                container.Controls.Add(p);
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

        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pauseImg.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void Device_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            bitmap = (Bitmap)eventArgs.Frame.Clone();
            frameImg = new Image<Bgr, byte>(bitmap);

        }
    }
}
