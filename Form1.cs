using AForge.Video;
using AForge.Video.DirectShow;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using WebSocketSharp;
namespace TAISAT
{
    public partial class TAAV : Form
    {
        private SerialPort port;
        private bool isGreen = true;
        private bool isWhite = true;
        private bool isStarted = true;
        private bool isMapActive = false;
        private WebSocket ws;
        private bool isWsConnected = false;
        string rosBridgeUrl = "ws://192.168.143.160:9090"; //ROS Server IP'sine göre özelleştirilecek.
        private Image originalImageX;
        private Image originalImageY;
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;

        public TAAV()
        {
            InitializeComponent();
            port = new SerialPort("COM2", 9600);
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

            UpdateButtonColors();//Buradan kaldırılacak port kısmında uygulanacak.

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

            data = data.Trim('{', '}');

            Invoke(new Action(() =>
            {
                string[] dataArray = data.Split(',');

                if (dataArray.Length >= 4)
                {
                    string latitude = dataArray[0];
                    string longitude = dataArray[1];
                    string hiz1 = dataArray[2];
                    string hiz2 = dataArray[3];

                    Lat.Text = latitude;
                    Long.Text = longitude;
                    int hiz1Int = Convert.ToInt32(hiz1);
                    int hiz2Int = Convert.ToInt32(hiz2);
                    labelHiz.Text = Convert.ToString((hiz1Int + hiz2Int) / 2);

                    Harita();
                    Angle();
                    UpdateButtonColors();
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
            isMapActive = !isMapActive;

            if (isMapActive)
            {
                Harita();
            }
            else
            {
                map.Overlays.Clear();
            }
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
        private void buttonIleri_MouseDown(object sender, MouseEventArgs e)
        {
            SendRosMessage("ileri");
        }
        private void buttonIleri_MouseUp(object sender, MouseEventArgs e)
        {
            SendRosMessage("ileriKes");
        }
        private void buttonSol_MouseDown(object sender, MouseEventArgs e)
        {
            SendRosMessage("sol");
        }
        private void buttonSol_MouseUp(object sender, MouseEventArgs e)
        {
            SendRosMessage("solKes");
        }
        private void buttonGeri_MouseDown(object sender, MouseEventArgs e)
        {
            SendRosMessage("geri");
        }
        private void buttonGeri_MouseUp(object sender, MouseEventArgs e)
        {
            SendRosMessage("geriKes");
        }
        private void buttonSag_MouseDown(object sender, MouseEventArgs e)
        {
            SendRosMessage("sag");
        }
        private void buttonSag_MouseUp(object sender, MouseEventArgs e)
        {
            SendRosMessage("sagKes");
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

            /*PointLatLng point = new PointLatLng(xCoordinate, yCoordinate);
            GMapMarker marker = new GMarkerGoogle(point, GMarkerGoogleType.red_pushpin);
            GMapOverlay markersOverlay = new GMapOverlay("markers");

            markersOverlay.Markers.Add(marker);
            map.Overlays.Add(markersOverlay);
            GMapMarker pointer = marker;*/

            //Bitkiler için marker.
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
                    buttonIleri_MouseDown(buttonIleri, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                    break;
                case Keys.A:
                    buttonSol_MouseDown(buttonSol, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                    break;
                case Keys.S:
                    buttonGeri_MouseDown(buttonGeri, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                    break;
                case Keys.D:
                    buttonSag_MouseDown(buttonSag, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                    break;
                case Keys.Space:
                    buttonDur_Click(buttonDur, EventArgs.Empty);
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
                    buttonIleri_MouseUp(buttonIleri, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                    buttonIleri.BackColor = Color.FromArgb(54, 57, 63);
                    break;
                case Keys.A:
                    buttonSol_MouseUp(buttonSol, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                    buttonSol.BackColor = Color.FromArgb(54, 57, 63);
                    break;
                case Keys.S:
                    buttonGeri_MouseUp(buttonGeri, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                    buttonGeri.BackColor = Color.FromArgb(54, 57, 63);
                    break;
                case Keys.D:
                    buttonSag_MouseUp(buttonSag, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
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
        private async void OpenWebSocket()
        {
            ws = new WebSocket(rosBridgeUrl);

            ws.OnOpen += (sender, e) =>
            {
                isWsConnected = true;

                string subscribeMessage = "{ \"op\": \"subscribe\", \"topic\": \"/my_topic\" }";
                ws.Send(subscribeMessage);

                richTextBoxLoglar.AppendText("Subscribed to /my_topic." + Environment.NewLine);

                Task.Delay(500).ContinueWith(t =>
                {
                    if (isWsConnected)
                    {
                        this.Invoke(new Action(() =>
                        {
                            MessageBox.Show("ROSBridge sunucusuna başarıyla bağlanıldı.");
                        }));
                    }
                });
            };

            ws.OnError += (sender, e) =>
            {
                MessageBox.Show($"WebSocket Hatası: {e.Message}");
                Console.WriteLine($"WebSocket Hatası: {e.Message} - {e.Exception}");
                isWsConnected = false;
            };

            ws.OnClose += (sender, e) =>
            {
                this.Invoke(new Action(() =>
                {
                    richTextBoxLoglar.AppendText("WebSocket bağlantısı kapatıldı." + Environment.NewLine);
                    MessageBox.Show("Bağlantı kapatıldı.");
                }));
                isWsConnected = false;
            };

            ws.OnMessage += (sender, e) =>
            {
                try
                {
                    JObject jsonMessage = JObject.Parse(e.Data);
                    string messageData = jsonMessage["msg"]["data"].ToString();

                    this.Invoke(new Action(() =>
                    {
                        richTextBoxLoglar.AppendText("Alındı: " + messageData + Environment.NewLine);
                }));

                    MessageBox.Show("Mesaj alındı: " + messageData);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Alınan mesaj işlenirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                string rosMessage = "{ \"op\": \"publish\", \"topic\": \"/my_topic\", \"msg\": { \"data\": \"" + data + "\" } }";
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


        //Battery Button Color Update:
        private void UpdateButtonColors()
        {
            Button[] buttons = { buttonP1, buttonP2, buttonP3, buttonP4, buttonP5, buttonP6 };

            foreach (var button in buttons)
            {
                if (double.TryParse(button.Text, out double value))
                {
                    if (value <= 3.6)
                    {
                        button.BackColor = Color.Red;
                    }
                    else
                    {
                        button.BackColor = Color.LimeGreen;
                    }
                }
            }
        }
    }
}