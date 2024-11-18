using Microsoft.VisualBasic;
using OpenCvSharp;
using System.Diagnostics;

namespace SplitVideo
{
    public partial class Form1 : Form
    {
        string videoFileName;
        string dirPath = @"C:\SplitVideo\";

        public Form1()
        {
            InitializeComponent();
            SetSplitButtonsDisable();
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath); // 폴더 생성
            }
        }

        private void SetSplitButtonsDisable()
        {
            singleThreadButton.Enabled = false;
            multiThreadButton.Enabled = false;
        }

        private void SetSplitButtonsEnable()
        {
            singleThreadButton.Enabled = true;
            multiThreadButton.Enabled = true;
        }

        private void SetAllButtonsDisable()
        {
            selectButton.Enabled = false;
            singleThreadButton.Enabled = false;
            multiThreadButton.Enabled = false;
        }

        private void SetAllButtonsEnable()
        {
            selectButton.Enabled = true;
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
                    SetSplitButtonsEnable();
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
            VideoWriter recode = new VideoWriter();
            Stopwatch stopwatch = new Stopwatch();

            Mat frame = new Mat();
            int frameCount = 0;
            int frameRate = (int)capture.Get(VideoCaptureProperties.Fps);
            int fullFrameCount = (int)capture.Get(VideoCaptureProperties.FrameCount);
            int framePartial = fullFrameCount / (int)videoPartialCountUpDown.Value;
            int partialIndex = 0;

            progressBar.Minimum = 0;
            progressBar.Maximum = fullFrameCount;
            progressBar.Value = 0;
            progressBar.Step = 1;

            SetAllButtonsDisable();

            stopwatch.Start();
            while (true)
            {
                capture.Read(frame);
                if (frame.Empty())
                {
                    break;
                }

                if (frameCount % framePartial == 0)
                {
                    if (frameCount != 0) // 처음 시작시 개체 해제 안함
                    { 
                        recode.Release();
                        Debug.WriteLine("single thread " + partialIndex + " end write");
                        ++partialIndex;
                    }
                    if (partialIndex < (int)videoPartialCountUpDown.Value) // 마지막 영상을 영상 끝까지 읽음
                    {
                        recode.Open(dirPath + "output" + partialIndex + ".mp4", FourCC.MP4V, frameRate, frame.Size());
                        Debug.WriteLine("single thread " + partialIndex + " start write");
                    }
                }

                recode.Write(frame);

                frameCount++;
                progressBar.PerformStep();
            }
            stopwatch.Stop();
            MessageBox.Show("실행 시간 : " + stopwatch.ElapsedMilliseconds + "ms");

            Debug.WriteLine("single thread split end");

            SetAllButtonsEnable();

            frame.Release();
            recode.Release();
            capture.Release();
        }
    }
}
