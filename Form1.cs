using AForge.Video;
using AForge.Video.DirectShow;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using WebSocketSharp;
namespace TAISAT
{
    public partial class TAAV : Form
    {
        private bool isGreen = true;
        private bool isWhite = true;
        private bool isStarted = true;
        private bool isMapActive = false;
        private WebSocket ws;
        private bool isWsConnected = false;
        string rosBridgeUrl = "ws://192.168.246.24:9090"; //ROS Server IP'sine göre özelleştirilecek.
        private Image originalImageX;
        private Image originalImageY;
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;
        private StringBuilder incomingDataBuffer = new StringBuilder();

        public TAAV()
        {
            InitializeComponent();
            InitializeSerialPort();
        }


        //Port:
        private void InitializeSerialPort()
        {
            serialPort = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);
            serialPort.DataReceived += SerialPort_DataReceived;
            serialPort.Encoding = Encoding.ASCII; // This can be changed depending on the data format
        }

        private List<byte> dataBuffer = new List<byte>(); // To accumulate data

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                // Read the available data into a byte array
                byte[] buffer = new byte[serialPort.BytesToRead];
                serialPort.Read(buffer, 0, buffer.Length);

                // Add new data to the buffer list
                dataBuffer.AddRange(buffer);

                // Process data if we have enough bytes
                ProcessData();
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur during read
                MessageBox.Show($"Error reading from serial port: {ex.Message}");
            }
        }

        private void ProcessData()
        {
            // Check if we have exactly 10 bytes
            if (dataBuffer.Count < 10)
            {
                return; // Not enough data yet
            }

            // Extract the first 10 bytes for processing
            byte[] buffer = dataBuffer.Take(10).ToArray();

            // Remove processed data from the buffer
            dataBuffer.RemoveRange(0, 10);

            // Extract and convert data
            float enlem = BitConverter.ToSingle(buffer, 0);
            float boylam = BitConverter.ToSingle(buffer, 4);
            uint hiz1 = buffer[8];
            uint hiz2 = buffer[9];

            // Calculate average speed
            float averageSpeed = (hiz1 + hiz2) / 2f;

            // Update UI controls with the extracted data
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    Lat.Text = enlem.ToString("F5"); // Format to 2 decimal places
                    Long.Text = boylam.ToString("F5"); // Format to 2 decimal places
                    labelHiz.Text = averageSpeed.ToString("F2") + " m/s"; // Format to 2 decimal places

                    Harita();
                }));
            }
            else
            {
                Lat.Text = enlem.ToString("F2");
                Long.Text = boylam.ToString("F2");
                labelHiz.Text = averageSpeed.ToString("F2");
            }
        }


        //Form Load and Form Closing:
        private void Form1_Load(object sender, EventArgs e)
        {
            //Timer:
            timerSaat.Start();
            this.KeyDown += Form1_KeyDown;
            this.KeyPreview = true;

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
        }


        //Timer:
        private void timerSaat_Tick(object sender, EventArgs e)
        {
            labelSaat.Text = DateTime.Now.ToString("HH:mm:ss");
        }


        //Button Click:
        private void buttonBaslat_Click(object sender, EventArgs e)
        {
            serialPort.Open();

            string yeniMetin = "COM3 port taraması başlatıldı.";
            string mevcutMetin = richTextBoxPort.Text;
            richTextBoxPort.Text = mevcutMetin + Environment.NewLine + yeniMetin;
        }
        private void buttonDurdur_Click(object sender, EventArgs e)
        {
            serialPort.Close();

            string yeniMetin = "COM3 port taraması durduruldu.";
            string mevcutMetin = richTextBoxPort.Text;
            richTextBoxPort.Text = mevcutMetin + Environment.NewLine + yeniMetin;
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

                // Subscribe to the raw_image_publisher
                string subscribeMessageCamera = "{ \"op\": \"subscribe\", \"topic\": \"/camera/raw_image\" }";
                ws.Send(subscribeMessageCamera);
                richTextBoxLoglar.AppendText("Subscribed to /camera/raw_image." + Environment.NewLine);

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
                    string topic = jsonMessage["topic"].ToString();

                    if (topic == "/camera/raw_image")
                    {
                        // Handle sensor_msgs/Image data
                        int height = jsonMessage["msg"]["height"].ToObject<int>();
                        int width = jsonMessage["msg"]["width"].ToObject<int>();
                        string encoding = jsonMessage["msg"]["encoding"].ToString();
                        string dataBase64 = jsonMessage["msg"]["data"].ToString();
                        byte[] imageData = Convert.FromBase64String(dataBase64); // Correctly parse the base64 string

                        Image image = ConvertRosImageToBitmap(imageData, width, height, encoding);

                        this.Invoke(new Action(() =>
                        {
                            pictureBoxCamera.Image = image;
                        }));
                    }
                    else
                    {
                        // Handle other topics
                        string messageData = jsonMessage["msg"]["data"].ToString();
                        this.Invoke(new Action(() =>
                        {
                            richTextBoxLoglar.AppendText($"Topic: {topic}, Mesaj: {messageData}" + Environment.NewLine);
                        }));
                    }
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

        private Image ConvertRosImageToBitmap(byte[] imageData, int width, int height, string encoding)
        {
            try
            {
                Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, bmp.PixelFormat);

                IntPtr ptr = bmpData.Scan0;

                if (encoding == "bgr8")
                {
                    // Convert BGR to RGB
                    byte[] rgbValues = new byte[imageData.Length];
                    for (int i = 0; i < imageData.Length; i += 3)
                    {
                        rgbValues[i] = imageData[i + 2]; // R
                        rgbValues[i + 1] = imageData[i + 1]; // G
                        rgbValues[i + 2] = imageData[i]; // B
                    }
                    System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, rgbValues.Length);
                }
                else if (encoding == "rgb8")
                {
                    // Directly copy the RGB data
                    System.Runtime.InteropServices.Marshal.Copy(imageData, 0, ptr, imageData.Length);
                }
                else if (encoding == "mono8")
                {
                    // Convert grayscale to RGB
                    byte[] rgbValues = new byte[width * height * 3];
                    for (int i = 0; i < width * height; i++)
                    {
                        rgbValues[i * 3] = imageData[i]; // R
                        rgbValues[i * 3 + 1] = imageData[i]; // G
                        rgbValues[i * 3 + 2] = imageData[i]; // B
                    }
                    System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, rgbValues.Length);
                }
                else
                {
                    MessageBox.Show($"Unsupported encoding: {encoding}");
                }

                bmp.UnlockBits(bmpData);
                return bmp;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Image conversion failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
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
                //MessageBox.Show("Sent: " + rosMessage);
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