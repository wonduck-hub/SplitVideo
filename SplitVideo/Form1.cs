using Microsoft.VisualBasic;
using OpenCvSharp;
using System.Diagnostics;
using System.Threading;

namespace SplitVideo
{
    public partial class Form1 : Form
    {
        string videoFileName = string.Empty;
        string dirPath = @"C:\SplitVideo\";

        public Form1()
        {
            InitializeComponent();
            SetSplitButtonsDisable();
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath); // ���� ����
            }
        }

        #region Disable/Enable Buttons
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
        #endregion

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
            Debug.Assert(videoFileName != string.Empty, "videoFileName is empty");

            Thread thead;

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
                    if (frameCount != 0) // ó�� ���۽� ��ü ���� ����
                    {
                        recode.Release();
                        Debug.WriteLine("single thread " + partialIndex + " end write");
                        ++partialIndex;
                    }
                    if (partialIndex < (int)videoPartialCountUpDown.Value) // ������ ������ ���� ������ ����
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
            MessageBox.Show("���� �ð� : " + stopwatch.ElapsedMilliseconds + "ms");

            Debug.WriteLine("single thread split end");

            SetAllButtonsEnable();

            frame.Release();
            recode.Release();
            capture.Release();
        }

        private void multiThreadButton_Click(object sender, EventArgs e)
        {
            Debug.Assert(videoFileName != string.Empty, "videoFileName is empty");
            SetAllButtonsDisable();

            Thread[] threads = new Thread[(int)videoPartialCountUpDown.Value];

            VideoCapture capture = new VideoCapture(videoFileName);
            Stopwatch stopwatch = new Stopwatch();

            Mat frame = new Mat();
            int frameRate = (int)capture.Get(VideoCaptureProperties.Fps);
            int fullFrameCount = (int)capture.Get(VideoCaptureProperties.FrameCount);
            int framePartial = fullFrameCount / (int)videoPartialCountUpDown.Value;

            progressBar.Minimum = 0;
            progressBar.Maximum = (int)videoPartialCountUpDown.Value;
            progressBar.Value = 0;
            progressBar.Step = 1;

            stopwatch.Start();
            for (int i = 0; i < (int)videoPartialCountUpDown.Value; i++)
            {
                int endframe;
                int startFrame = i * framePartial; // ���� ������ ����(�Ʒ� i�� ���� ���� ������ ���� ������ ����!!
                if (i == (int)videoPartialCountUpDown.Value - 1) // ������ ������ ������ ����
                {
                    endframe = fullFrameCount;
                }
                else
                {
                    endframe = (i + 1) * framePartial;
                }

                int threadIndex = i; // i�� �״�� �ѱ�� �ȵ� i ������ ���ٽ� �ȿ��� ĸ�ĵ� �� ���� ���� ĸ���ϱ� �����̴�.
                threads[i] = new Thread(() => VideoRecode(videoFileName, startFrame, endframe, threadIndex));
                threads[i].Start();
            }
            for (int i = 0; i < (int)videoPartialCountUpDown.Value; i++)
            {
                threads[i].Join();
                progressBar.PerformStep();
            }
            stopwatch.Stop();
            MessageBox.Show("���� �ð� : " + stopwatch.ElapsedMilliseconds + "ms");

            SetAllButtonsEnable();

            frame.Release();
            capture.Release();
        }

        private void VideoRecode(string videoPath, int startFrame, int endFrame, int threadNum)
        {
            Debug.Assert(videoPath != string.Empty, "videoFileName is empty");

            VideoCapture capture = new VideoCapture(videoPath);
            VideoWriter recode = new VideoWriter();

            Mat frame = new Mat();
            int frameRate = (int)capture.Get(VideoCaptureProperties.Fps);
            int fullFrameCount = endFrame;
            capture.Set(VideoCaptureProperties.PosFrames, startFrame);
            capture.Read(frame); // ������ ũ�� �ʱ�ȭ

            recode.Open(dirPath + "multi_thread_output" + threadNum + ".mp4", FourCC.MP4V, frameRate, frame.Size());

            Debug.WriteLine("multi thread " + threadNum + " start write");
            for (int frameCount = startFrame; frameCount < endFrame; frameCount++)
            {
                if (frame.Empty())
                {
                    break;
                }

                recode.Write(frame);

                capture.Read(frame);
            }
            Debug.WriteLine("multi thread " + threadNum + " end write");

            frame.Release();
            recode.Release();
            capture.Release();
        }
    }
}
