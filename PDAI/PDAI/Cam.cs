using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Face;
using Emgu.CV.Structure;

namespace PDAI
{
    class Cam
    {


        public VideoCapture Webcam { get; set; }
        public EigenFaceRecognizer FaceRecognition { get; set; }
        public CascadeClassifier FaceDetection { get; set; }
        public CascadeClassifier EyesDetection { get; set; }

        public Mat Frame { get; set; }

        public List<Image<Gray, byte>> Faces { get; set; }
        public List<int> IDs { get; set; }

        public int ProcessedImageWidth = 128;
        public int ProcessedImageHeight = 150;


        public int TimerCounter { set; get; } = 0;
        public int TimerLimit { set; get; } = 20;
        public int ScanCounter { set; get; } = 0;

        public string YMLPath { set; get; } = Application.StartupPath + "/TrainingData/trainingData.xml";

        public System.Windows.Forms.Timer Timer { set; get; }

        public bool FaceSquare { set; get; } = false;
        public bool EyeSquare { set; get; } = false;
        public bool FaceDetected { set; get; } = false;
        public List<int> FacesName;

        PictureBox WebcamBox;
        int indexCam;

        public Cam(int indexCam)
        {


            //FaceRecognition = new EigenFaceRecognizer(80, double.PositiveInfinity);
            //FaceDetection = new CascadeClassifier("haarcascade_frontalface_default.xml");
            //EyesDetection = new CascadeClassifier("haarcascade_eye.xml");
            Frame = new Mat();
            Faces = new List<Image<Gray, byte>>();
            IDs = new List<int>();

            FacesName = new List<int>();
            this.indexCam = indexCam;


        }

        public void BeginWebcam(PictureBox WebcamBox)
        {
            this.WebcamBox = WebcamBox;

            if (Webcam == null)
                Webcam = new VideoCapture(indexCam);

            Webcam.ImageGrabbed += Webcam_ImageGrabbad;
            Webcam.Start();


        }

        public void StopWebcam()
        {
            Webcam.Stop();

        }

        private void Webcam_ImageGrabbad(object sender, EventArgs e)
        {
            Webcam.Retrieve(Frame);
            var imageFrame = Frame.ToImage<Bgr, byte>();
            var bitmap = imageFrame.Bitmap;

            if (imageFrame != null)
            {
                var grayFrame = imageFrame.Convert<Gray, byte>();
                //var faces = FaceDetection.DetectMultiScale(grayFrame, 1.3, 2);
                //var eyes = EyesDetection.DetectMultiScale(grayFrame, 1.1, 5);

                //if (FaceSquare)
                //{
                //    //foreach (var face in faces)
                //{
                //imageFrame.Draw(face, new Bgr(Color.BurlyWood), 3);

                //if (FaceDetected)
                //    using (Graphics gr = Graphics.FromImage(bitmap))
                //    {
                //        System.Drawing.Font font = new System.Drawing.Font("Times New Roman", 24, FontStyle.Bold, GraphicsUnit.Pixel);
                //        gr.DrawString(FacesName[0].ToString(), font, Brushes.LightGreen, face.Location.X, face.Location.Y - font.Height);
                //    }
                //  }



                //  }


                //if (EyeSquare)
                //    foreach (var eye in eyes)
                //        imageFrame.Draw(eye, new Bgr(Color.Yellow), 3);

                WebcamBox.Image = bitmap;
            }

        }





        private void Train_Click(object sender, EventArgs e)
        {

            //if (IDBox.Text != string.Empty)
            //{
            //    IDBox.Enabled = !IDBox.Enabled;


            //    Timer = new System.Windows.Forms.Timer();
            //    Timer.Interval = 500;
            //    Timer.Tick += Timer_Tick;
            //    Timer.Start();
            //    Train.Enabled = !Train.Enabled;


            //}


        }


        //Main Logic of the program
        private void Timer_Tick(object sender, EventArgs e)
        {



            //var imageFrame = Frame.ToImage<Gray, byte>();


            //if (TimerCounter < TimerLimit)
            //{
            //    TimerCounter++;


            //    if (imageFrame != null)
            //    {
            //        var faces = FaceDetection.DetectMultiScale(imageFrame, 1.3, 2);

            //        if (faces.Count() > 0)
            //        {
            //            var processImage = imageFrame.Copy(faces[0]).Resize(ProcessedImageWidth, ProcessedImageHeight, Emgu.CV.CvEnum.Inter.Cubic);
            //            Faces.Add(processImage);



            //            IDs.Add(Convert.ToInt32(IDBox.Text));
            //            ScanCounter++;

            //            OutputBox.AppendText($"{ScanCounter} Sucesseful Scans Taken...{Environment.NewLine}");
            //            OutputBox.ScrollToCaret();


            //        }
            //    }
            //}
            //else
            //{

            //    Mat[] arrayFacesConverted = new Mat[Faces.Count()];

            //    for (int i = 0; i < Faces.Count; i++)
            //    {
            //        arrayFacesConverted[i] = Faces[i].Mat;
            //    }

            //    FaceRecognition.Train(arrayFacesConverted, IDs.ToArray());
            //    FaceRecognition.Write(YMLPath);
            //    Timer.Stop();
            //    TimerCounter = 0;
            //    Train.Enabled = !Train.Enabled;
            //    IDBox.Enabled = !IDBox.Enabled;
            //    OutputBox.AppendText($"Training Complete... {Environment.NewLine}");
            //    MessageBox.Show("Training Complete!");

            //}




        }




        private void PredictButton_Click(object sender, EventArgs e)
        {
            //Webcam.Retrieve(Frame);
            var imageFrame = Frame.ToImage<Gray, byte>();

            if (imageFrame != null)
            {

                var faces = FaceDetection.DetectMultiScale(imageFrame, 1.3, 2);

                if (faces.Count() != 0)
                {
                    var processImage = imageFrame.Copy(faces[0]).Resize(ProcessedImageWidth, ProcessedImageHeight, Emgu.CV.CvEnum.Inter.Cubic);
                    var result = FaceRecognition.Predict(processImage);

                    FaceDetected = true;
                    FacesName.Add(result.Label);



                    //if (result.Label.ToString() == "1")  MessageBox.Show(yourName);
                    //else MessageBox.Show(noName);

                }
                else
                {
                    MessageBox.Show("Face was not found - Try again!");

                }

            }


        }

        private void FaceButton_Click(object sender, EventArgs e)
        {
            //if (FaceSquare)
            //    FaceButton.Text = "Face On";
            //else
            //    FaceButton.Text = "Face Off";

            //FaceSquare = !FaceSquare;
        }

        private void EyesButton_Click(object sender, EventArgs e)
        {
            //if (EyeSquare)
            //    EyesButton.Text = "Eyes On";
            //else
            //    EyesButton.Text = "Eyes Off";

            //EyeSquare = !EyeSquare;
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            // this.Close();
        }

        private void WebcamBox_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }


    }
}
