using AForge.Video;
using AForge.Video.DirectShow;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Runtime.Remoting.Contexts;
using System.Windows.Forms;
using WebSocketSharp;
namespace TAISAT
{
    public partial class TAAV : Form
    {
        private SerialPort port;
        private bool isGreen = true;
        private bool isWhite = true;
        private bool isStarted = true;
        private WebSocket ws;
        private bool isWsConnected = false;
        string rosBridgeUrl = "ws://simple-websocket-server-echo.glitch.me/"; //ROS Server IP'sine göre özelleştirilecek.
        private Image originalImageX;
        private Image originalImageY;
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;


        public TAAV()
        {
            InitializeComponent();
            port = new SerialPort("COM3", 9600); 
            port.DataReceived += Port_DataReceived;

            try
            {
                port.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Port açılamadı: " + ex.Message);
            }
        }


        //Camera:
        private void ListCameras()
        {
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            foreach (FilterInfo device in videoDevices)
            {
                comboBox1.Items.Add(device.Name);
            }

            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Kamera bulunamadı!");
            }
        }


        //Form Load and Form Closing:
        private void Form1_Load(object sender, EventArgs e)
        {
            ListCameras();


            //Timer:
            timerSaat.Start();
            this.KeyDown += Form1_KeyDown;
            this.KeyPreview = true;

            //Port:
            foreach (string portName in SerialPort.GetPortNames())
            {
                comboBoxPort.Items.Add(portName);
            }

            if (comboBoxPort.Items.Count > 0)
            {
                comboBoxPort.SelectedIndex = 0;
            }

            //Button Customization:
            CustomButton.SetButton(this.Controls);

            Button[] directionButtons = { buttonIleri, buttonGeri, buttonSag, buttonSol, buttonDur };
            CustomButton.SetButtonColors(directionButtons, Color.FromArgb(47, 49, 54), Color.White);
            Button[] batteryButtons = { buttonP1, buttonP2, buttonP3, buttonP4, buttonP5, buttonP6 };
            CustomButton.SetButtonColors(batteryButtons, Color.LimeGreen, Color.White);
            buttonAcKapat.BackColor = Color.FromArgb(200, 16, 46);

            CustomButton.SetButtonMouseEvents(directionButtons, Color.FromArgb(54, 57, 63), Color.FromArgb(64, 68, 75));
            CustomButton.SetButtonMouseEvents(batteryButtons, Color.LimeGreen, Color.Green);
            buttonAcKapat.FlatAppearance.MouseDownBackColor = Color.Red;
            buttonAcKapat.FlatAppearance.MouseOverBackColor = Color.Red;

            //Image Angle Reset:
            if (pictureBoxEgimX.Image != null)
            {
                originalImageX = new Bitmap(pictureBoxEgimX.Image);
            }

            if (pictureBoxEgimY.Image != null)
            {
                originalImageY = new Bitmap(pictureBoxEgimY.Image);
            }

            Angle();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ws != null)
            {
                ws.Close();
            }
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
                videoSource.WaitForStop();
            }
        }


        //Timer:
        private void timerSaat_Tick(object sender, EventArgs e)
        {
            labelSaat.Text = DateTime.Now.ToString("HH:mm:ss");
        }


        //Port:
        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string data = port.ReadLine();
            Invoke(new Action(() =>
            {
                string[] dataArray = data.Split(',');
                if (dataArray.Length >= 7) //veri sayısına göre değiştirilecek.
                {
                    /*
                    latitude = dataArray[0];
                    Lat.Text = dataArray[0];
                    longitude = dataArray[1];
                    Long.Text = dataArray[1];
                    labelHiz.Text = dataArray[2];
                    labelMesafe.Text = dataArray[3];
                    buttonP1.Text = dataArray[4];
                    buttonP2.Text = dataArray[5];
                    buttonP3.Text = dataArray[6];
                    buttonP4.Text = dataArray[7];
                    buttonP5.Text = dataArray[8];
                    buttonP6.Text = dataArray[9];
                    labelBatarya.Text = dataArray[10];
                    labelEgimX.Text = dataArray[11];
                    labelEgimY.Text = dataArray[12];
                    
                    Harita();
                    Angle();
                    */
                }
                else
                {
                    MessageBox.Show("Yeterli veri alınamadı.");
                }
            }));
        }


        //Button Click:
        private void buttonBaslat_Click(object sender, EventArgs e)
        {
            if (!port.IsOpen)
            {
                try
                {
                    port.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Port açılamadı: " + ex.Message);
                }
            }
        }
        private void buttonDurdur_Click(object sender, EventArgs e)
        {
            port.Close();
        }
        private void buttonHarita_Click(object sender, EventArgs e)
        {
            Harita();
        }
        private void buttonZoomIn_Click(object sender, EventArgs e)
        {
            ZoomIn();
        }
        private void buttonZoomOut_Click(object sender, EventArgs e)
        {
            ZoomOut();
        }
        private void buttonOtoAcKapat_Click(object sender, EventArgs e)
        {
            OtonomKontrol();
        }
        private void buttonAcKapat_Click(object sender, EventArgs e)
        {
            AracAcKapat();
        }
        private void buttonIleri_Click(object sender, EventArgs e)
        {
            SendRosMessage("ileri");
        }
        private void buttonSol_Click(object sender, EventArgs e)
        {
            SendRosMessage("sol");
        }
        private void buttonGeri_Click(object sender, EventArgs e)
        {
            SendRosMessage("geri");
        }
        private void buttonSag_Click(object sender, EventArgs e)
        {
            SendRosMessage("sag");
        }
        private void buttonDur_Click(object sender, EventArgs e)
        {
            SendRosMessage("dur");
        }
        private void buttonCapaIndır_Click(object sender, EventArgs e)
        {
            SendRosMessage("capa indir");
        }
        private void buttonCapaKaldir_Click(object sender, EventArgs e)
        {
            SendRosMessage("capa kaldir");
        }

        private void buttonWebSocket_Click(object sender, EventArgs e)
        {
            if (isWsConnected)
            {
                CloseWebSocket();
            }
            else
            {
                OpenWebSocket();
            }
        }


        //Map:
        private void Harita()
        {
            map.MapProvider = GMapProviders.GoogleMap;
            double lat = Convert.ToDouble(Lat.Text);
            double lon = Convert.ToDouble(Long.Text);
            map.Position = new GMap.NET.PointLatLng(lat, lon);
            map.Zoom = 19;
            map.MinZoom = 5;
            map.MaxZoom = 100;
            GMap.NET.PointLatLng point = new GMap.NET.PointLatLng(lat, lon);
            GMapMarker marker = new GMarkerGoogle(point, GMarkerGoogleType.red_pushpin);
            GMapOverlay markers = new GMapOverlay("markers");
        }

        private void ZoomIn()
        {
            if (map.Zoom < map.MaxZoom)
            {
                map.Zoom += 1;
            }
        }
        private void ZoomOut()
        {
            if (map.Zoom > map.MinZoom)
            {
                map.Zoom -= 1;
            }
        }


        //Keyboard Control:
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.O:
                    OtonomKontrol();
                    break;
                case Keys.H:
                    Harita();
                    break;
                case Keys.Z:
                    ZoomIn();
                    break;
                case Keys.X:
                    ZoomOut();
                    break;
                case Keys.W:
                    buttonIleri.PerformClick();
                    buttonIleri.BackColor = Color.White;
                    break;
                case Keys.A:
                    buttonSol.PerformClick();
                    buttonSol.BackColor = Color.White;
                    break;
                case Keys.S:
                    buttonGeri.PerformClick();
                    buttonGeri.BackColor = Color.White;
                    break;
                case Keys.D:
                    buttonSag.PerformClick();
                    buttonSag.BackColor = Color.White;
                    break;
                case Keys.Space:
                    buttonDur.PerformClick();
                    break;
            }
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.O:
                    OtonomKontrol();
                    break;
                case Keys.W:
                    buttonIleri.BackColor = Color.FromArgb(54, 57, 63);
                    break;
                case Keys.A:
                    buttonSol.BackColor = Color.FromArgb(54, 57, 63);
                    break;
                case Keys.S:
                    buttonGeri.BackColor = Color.FromArgb(54, 57, 63);
                    break;
                case Keys.D:
                    buttonSag.BackColor = Color.FromArgb(54, 57, 63);
                    break;
            }
        }


        //Autonomous:
        private void OtonomKontrol()
        {
            if (isGreen)
            {
                labelOtonomKontrol.BackColor = Color.Red;
                labelOtonomKontrol.Text = "KAPALI";
                SendRosMessage("otonom kapat");

            }
            else
            {
                labelOtonomKontrol.BackColor = Color.ForestGreen;
                labelOtonomKontrol.Text = "AÇIK";
                SendRosMessage("otonom ac");
            }
            isGreen = !isGreen;
        }


        //Start-Stop:
        private void AracAcKapat()
        {
            if (isStarted)
            {
                SendRosMessage("arac kapat");
            }
            else
            {
                SendRosMessage("arac ac");
            }
        }


        //Open WebSocket:
        private void OpenWebSocket()
        {
            ws = new WebSocket(rosBridgeUrl);

            ws.OnOpen += (sender, e) =>
            {
                MessageBox.Show("ROSBridge sunucusuna bağlantı başarılı.");
                isWsConnected = true;
            };

            ws.OnError += (sender, e) =>
            {
                MessageBox.Show("Bağlantı hatası: " + e.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isWsConnected = false;
            };

            ws.OnMessage += (sender, e) =>
            {
                MessageBox.Show("Mesaj alındı: " + e.Data);
            };

            ws.OnClose += (sender, e) =>
            {
                MessageBox.Show("Bağlantı kapatıldı.");
                isWsConnected = false;
            };

            try
            {
                ws.Connect();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bağlantı hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isWsConnected = false;
            }
        }


        //Close WebSocket:
        private void CloseWebSocket()
        {
            if (ws != null && isWsConnected)
            {
                ws.Close();
                isWsConnected = false;
            }
        }


        //Ros Message:
        private void SendRosMessage(string data)
        {
            if (ws != null && ws.IsAlive)
            {
                string rosMessage = "{ \"op\": \"publish\", \"topic\": \"/your_topic\", \"msg\": { \"data\": \"" + data + "\" } }";
                ws.Send(rosMessage);
                MessageBox.Show("Sent: " + rosMessage);
            }
            else
            {
                MessageBox.Show("WebSocket connection is not open.");
            }
        }


        //Angle:
        private void RotateImage(PictureBox pictureBox, Image originalImage, float angle)
        {
            if (originalImage == null)
                return;

            pictureBox.Image = new Bitmap(originalImage);

            Bitmap newImage = new Bitmap(originalImage.Width, originalImage.Height);

            using (Graphics g = Graphics.FromImage(newImage))
            {
                g.TranslateTransform((float)originalImage.Width / 2, (float)originalImage.Height / 2);

                g.RotateTransform(angle);

                g.TranslateTransform(-(float)originalImage.Width / 2, -(float)originalImage.Height / 2);
                g.DrawImage(originalImage, new Point(0, 0));
            }

            pictureBox.Image = newImage;
        }
        private void Angle()
        {
            if (double.TryParse(labelEgimX.Text, out double xAngle) &&
                double.TryParse(labelEgimY.Text, out double yAngle))
            {
                RotateImage(pictureBoxEgimX, originalImageX, (float)xAngle);
                RotateImage(pictureBoxEgimY, originalImageY, (float)yAngle);
            }
            else
            {
                MessageBox.Show("Lütfen geçerli sayısal bir değer girin.");
            }
        }


        //Camera:
        private void videoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap frame = (Bitmap)eventArgs.Frame.Clone();
            pictureBox1.Image = frame;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.Stop();
                pictureBox1.Image = null;
            }

            videoSource = new VideoCaptureDevice(videoDevices[comboBox1.SelectedIndex].MonikerString);

            VideoCapabilities[] videoCapabilities = videoSource.VideoCapabilities;

            VideoCapabilities highestResolution = videoCapabilities[0];
            foreach (VideoCapabilities cap in videoCapabilities)
            {
                if (cap.FrameSize.Width * cap.FrameSize.Height > highestResolution.FrameSize.Width * highestResolution.FrameSize.Height)
                {
                    highestResolution = cap;
                }
            }

            videoSource.VideoResolution = highestResolution;

            videoSource.NewFrame += new NewFrameEventHandler(videoSource_NewFrame);
            videoSource.Start();
        }
    }
}