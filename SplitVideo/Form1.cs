using OpenCvSharp;
using System.Diagnostics;

namespace SplitVideo
{
    public partial class Form1 : Form
    {
        string videoFileName;
        string dirPath = @"C:\SplitVideo";

        public Form1()
        {
            InitializeComponent();
            SetButtonsDisable();
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath); // 폴더 생성
            }
        }

        private void SetButtonsDisable()
        {
            singleThreadButton.Enabled = false;
            multiThreadButton.Enabled = false;
        }

        private void SetButtonsEnable()
        {
            singleThreadButton.Enabled = true;
            multiThreadButton.Enabled = true;
        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            try
            {

                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Video Files|*.mp4;*.avi;*.mov;*.flv";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    videoFileName = openFileDialog.FileName;
                    SetButtonsEnable();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void singleThreadButton_Click(object sender, EventArgs e)
        {
            VideoCapture capture = new VideoCapture(videoFileName);

            Mat frame = new Mat();
            int frameCount = 0;
            int frameRate = (int)capture.Get(VideoCaptureProperties.Fps);
            int fullFrameCount = (int)capture.Get(VideoCaptureProperties.FrameCount);
            int framePartial = fullFrameCount / (int)videoPartialCountUpDown.Value;
            int partialIndex = 0;

            VideoWriter recode = new VideoWriter();

            while (true)
            {
                capture.Read(frame);
                if (frame.Empty())
                {
                    break;
                }

                if (frameCount % framePartial == 0)
                {
                    if (frameCount != 0) 
                    { 
                        recode.Release();
                        Debug.WriteLine(partialIndex + " end write");
                        ++partialIndex;
                    }
                    if (partialIndex < (int)videoPartialCountUpDown.Value)
                    {
                        recode.Open("C:\\SplitVideo\\" + "output" + partialIndex + ".mp4", FourCC.MP4V, frameRate, frame.Size());
                        Debug.WriteLine(partialIndex + " start write");
                    }
                }

                recode.Write(frame);

                frameCount++;
            }

            recode.Release();
            capture.Release();
        }
    }
}
