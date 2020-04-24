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
    class viewCam
    {
        Font_Class font;
        Panel viewCam_interface, save, editPanel, editPanelBorder, camPanel;
        Database db;
        Button pause, start, apagar;
        int fontSize = 13, saveWidth, saveHeight;
        double var;
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

        Image<Gray, byte> trainImg_II, trainImg_III, trainImg_IV, trainImg_V, trainImg_VI;

        Rectangle[] rectangles;

        Bitmap bitmap;

        System.Windows.Forms.PictureBox teste;

        FaceRecognizer recognizer = new FisherFaceRecognizer(0, 3500);


        //private Capture _capture = null; //Camera
        //private bool _captureInProgress = false; //Variable to track camera state
        //int CameraDevice = 0; //Variable to track camera device selected
        //Video_Device[] WebCams; //List containing all the camera available

        public viewCam(Panel content_interface, int content_width, int content_height, double dbl)
        {
            var = dbl;
            save = content_interface;
            saveWidth = content_width;
            saveHeight = content_height;
            db = new Database();

            Faces = new List<Image<Gray, byte>>();
            IDs = new List<int>();

            FaceRecognition = new EigenFaceRecognizer(80, double.PositiveInfinity);

            for (int i = 0; i < db.select.trainImages().Count; i = (i + 2))
            {
                System.Diagnostics.Debug.WriteLine(db.select.trainImages()[i + 1].ToString());

                string[] filePaths = Directory.GetFiles(db.select.trainImages()[i + 1].ToString());
                trainImg_II = new Image<Gray, byte>(filePaths[0]);
                trainImg_II = trainImg_II.Resize(ProcessedImageWidth, ProcessedImageHeight, Emgu.CV.CvEnum.Inter.Cubic);
                Faces.Add(trainImg_II);
                IDs.Add(Int32.Parse(db.select.trainImages()[i].ToString()));

            }

            var faceImages = new Image<Gray, byte>[Faces.Count];
            var faceLabels = new int[IDs.Count];


            for (int i = 0; i < IDs.Count; i++)
            {
                faceLabels[i] = IDs[i];
            }

            Mat[] arrayFacesConverted = new Mat[Faces.Count()];

            for (int i = 0; i < Faces.Count; i++)
            {
                arrayFacesConverted[i] = Faces[i].Mat;
            }

            FaceRecognition.Train(arrayFacesConverted, faceLabels);

            Frame = new Mat();


            FacesName = new List<int>();

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
            pb.Visible = false;

            pic = new AForge.Controls.PictureBox();
            camPanel.Controls.Add(pic);
            pic.Location = new System.Drawing.Point(pb.Location.X, pb.Location.Y);
            pic.Size = new Size(pb.Size.Width, pb.Size.Height);
            pic.Dock = DockStyle.Fill;
            pic.SizeMode = PictureBoxSizeMode.StretchImage;

            pauseImg = new AForge.Controls.PictureBox();
            camPanel.Controls.Add(pauseImg);
            pauseImg.Location = new System.Drawing.Point(pic.Location.X, pic.Location.Y);
            pauseImg.Size = new Size(pic.Size.Width, pic.Size.Height);
            pauseImg.Visible = false;
            pauseImg.SizeMode = PictureBoxSizeMode.StretchImage;

            pause = new Button();
            viewCam_interface.Controls.Add(pause);
            pause.Size = new Size(60, 60);
            pause.Location = new System.Drawing.Point((camPanel.Location.X + (camPanel.Size.Width / 2)), (camPanel.Location.Y + camPanel.Size.Height));
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
            VideoCaptureDevice videoSource1 = new VideoCaptureDevice(videoDevices[Convert.ToInt32(var) - 1].MonikerString);
            //videoSource1.DesiredFrameRate = 30;
            videoSource1.Start();
            videoSource1.NewFrame += new NewFrameEventHandler(video_NewFrame);
            videoSource1.NewFrame += Device_NewFrame;
            pb.VideoSource = videoSource1;
            pb.Start();






        }

        static readonly CascadeClassifier cascadeClassifier = new CascadeClassifier("haarcascade_frontalface_default.xml");

        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            //pauseImg.Image = (Bitmap)eventArgs.Frame.Clone();

        }

        private void Device_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {

            //grayImg = new Image<Gray, byte>(bitmap);

            FacesName = new List<int>();
            bitmap = (Bitmap)eventArgs.Frame.Clone();
            frameImg = new Image<Bgr, byte>(bitmap);
            predict();

            /*foreach (Rectangle rectangle in rectangles)
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    using (Pen pen = new Pen(Color.Red, 1))
                    {
                        graphics.DrawRectangle(pen, rectangle);
                        Font font = new Font("Times New Roman", 24, FontStyle.Bold, GraphicsUnit.Pixel);
                        graphics.DrawString("Unknown".ToString(), font, Brushes.LightGreen, rectangle.Location.X, rectangle.Location.Y - font.Height);
                    }
                }
            }*/


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
            pb.Visible = false;
            pause.Visible = true;
            start.Visible = false;
        }

        private void Apagar_Click(object sender, EventArgs e)
        {

        }

        public void train()
        {


            if (frameImg != null)
            {
                //Rectangle[] faces = cascadeClassifier.DetectMultiScale(frameImg, 1.3, 2);

                /*if (faces.Count() > 0)
                {
                    var processImage = imageFrame.Copy(faces[0]).Resize(ProcessedImageWidth, ProcessedImageHeight, Emgu.CV.CvEnum.Inter.Cubic);
                    Faces.Add(processImage);



                    IDs.Add(Int32.Parse(IDBox.Text));
                    ScanCounter++;



                }*/


                var faceImages = new Image<Gray, byte>[Faces.Count];
                var faceLabels = new int[IDs.Count];

                for (int i = 0; i < IDs.Count; i++)
                {
                    faceLabels[i] = IDs[i];
                }

                Mat[] arrayFacesConverted = new Mat[Faces.Count()];

                for (int i = 0; i < Faces.Count; i++)
                {
                    arrayFacesConverted[i] = Faces[i].Mat;
                }

                recognizer.Train(arrayFacesConverted, faceLabels);
                //FaceRecognition.Write(YMLPath);

                System.Diagnostics.Debug.WriteLine("Train!!!!");

            }



            //Timer.Stop();
            //TimerCounter = 0;



            //Timer = new System.Windows.Forms.Timer();
            //Timer.Interval = 500;
            //Timer.Tick += Timer_Tick;
            //Timer.Start();
        }

        /*private void Timer_Tick(object sender, EventArgs e)
        {

            //  Webcam.Retrieve(Frame);

            //var imageFrame = Frame.ToImage<Gray, byte>();


            if (TimerCounter < TimerLimit)
            {
                TimerCounter++;


                if (grayImage != null)
                {
                    Rectangle[] faces = cascadeClassifier.DetectMultiScale(grayImage, 1.3, 2);

                    /*if (faces.Count() > 0)
                    {
                        var processImage = imageFrame.Copy(faces[0]).Resize(ProcessedImageWidth, ProcessedImageHeight, Emgu.CV.CvEnum.Inter.Cubic);
                        Faces.Add(processImage);



                        IDs.Add(Int32.Parse(IDBox.Text));
                        ScanCounter++;



                    }*/

        /* trainImg = new Image<Gray, byte>(Properties.Resources.Donald_Trump_official_portrait);
         trainImg = trainImg.Resize(ProcessedImageWidth, ProcessedImageHeight, Emgu.CV.CvEnum.Inter.Cubic);
         Faces.Add(trainImg);

         IDs.Add(Int32.Parse("1"));

     }
 }
 else
 {

     Mat[] arrayFacesConverted = new Mat[Faces.Count()];

     for (int i = 0; i < Faces.Count; i++)
     {
         arrayFacesConverted[i] = Faces[i].Mat;
     }

     FaceRecognition.Train(arrayFacesConverted, IDs.ToArray());
     FaceRecognition.Write(YMLPath);
     Timer.Stop();
     TimerCounter = 0;

 }




}*/

        public void predict()
        {
            //grayImg = frameImg.Convert<Gray, byte>();


            if (frameImg != null)
            {

                //Rectangle[] faces = cascadeClassifier.DetectMultiScale(frameImg, 1.3, 2);

                rectangles = cascadeClassifier.DetectMultiScale(frameImg, 1.2, 10, new Size(50, 50), Size.Empty);

                if (rectangles.Count() != 0)
                {
                    foreach (Rectangle rectangle in rectangles)
                    {

                        var processImage = frameImg.Copy(rectangle).Convert<Gray, byte>().Resize(ProcessedImageWidth, ProcessedImageHeight, Emgu.CV.CvEnum.Inter.Cubic);
                        var result = FaceRecognition.Predict(processImage);

                        if (Int32.Parse(result.Label.ToString()) != -1)
                        {
                            FaceDetected = true;
                            FacesName.Add(result.Label);

                            using (Graphics graphics = Graphics.FromImage(bitmap))
                            {
                                using (Pen pen = new Pen(Color.Red, 1))
                                {
                                    graphics.DrawRectangle(pen, rectangle);
                                    Font font = new Font("Times New Roman", 24, FontStyle.Bold, GraphicsUnit.Pixel);
                                    graphics.DrawString(result.Label.ToString(), font, Brushes.Black, rectangle.Location.X, rectangle.Location.Y - font.Height);
                                }
                            }


                            pic.Image = bitmap;
                        }
                        else
                        {
                            using (Graphics graphics = Graphics.FromImage(bitmap))
                            {
                                using (Pen pen = new Pen(Color.Red, 1))
                                {
                                    graphics.DrawRectangle(pen, rectangle);
                                    Font font = new Font("Times New Roman", 24, FontStyle.Bold, GraphicsUnit.Pixel);
                                    graphics.DrawString("Desconhecido", font, Brushes.Black, rectangle.Location.X, rectangle.Location.Y - font.Height);
                                }
                            }
                        }



                        System.Diagnostics.Debug.WriteLine("Aqui!!!! - ");
                    }

                }

                else
                {
                    pic.Image = bitmap;
                }

            }
        }
    }
}
