using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;
using WebSocketSharp;
using Newtonsoft.Json;

namespace TAISAT
{
    public partial class TAAV : Form
    {
        private SerialPort port;
        private bool isGreen = true;
        private bool isWhite = true;
        private WebSocket ws;
        string rosBridgeUrl = "ws://simple-websocket-server-echo.glitch.me/"; //ROS Server IP'sine göre özelleştirilecek.

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


        #region Methods
        //Form Load and Form Closing:
        private void Form1_Load(object sender, EventArgs e)
        {
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
            CustomButton.SetButtonColors(directionButtons, Color.FromArgb(64, 68, 75), Color.White);
            Button[] batteryButtons = { buttonP1, buttonP2, buttonP3, buttonP4, buttonP5, buttonP6 };
            CustomButton.SetButtonColors(batteryButtons, Color.LimeGreen, Color.White);
            buttonAcKapat.BackColor = Color.FromArgb(200, 16, 46);
            buttonClose.BackColor = Color.FromArgb(255, 96, 92);
            buttonMinimize.BackColor = Color.FromArgb(0, 202, 78);
            buttonMaximize.BackColor = Color.FromArgb(255, 189, 68);

            CustomButton.SetButtonMouseEvents(directionButtons, Color.FromArgb(54, 57, 63), Color.FromArgb(64, 68, 75));
            CustomButton.SetButtonMouseEvents(batteryButtons, Color.LimeGreen, Color.Green);
            buttonAcKapat.FlatAppearance.MouseDownBackColor = Color.Red;
            buttonAcKapat.FlatAppearance.MouseOverBackColor = Color.Red;
            buttonClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 96, 92);
            buttonClose.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 96, 92);
            buttonMaximize.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 189, 68);
            buttonMaximize.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 189, 68);
            buttonMinimize.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 202, 78);
            buttonMinimize.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 202, 78);

            //Header Panel:
            headerPanel.MouseDown += HeaderPanel_MouseDown;
            headerPanel.MouseMove += HeaderPanel_MouseMove;
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


        //Port:
        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string data = port.ReadLine();
            Invoke(new Action(() =>
            {
                richTextBox2.AppendText(data + Environment.NewLine);
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
        private void buttonIleri_Click(object sender, EventArgs e) //İstenilen mesaj ayarlanacak. Bu bir örnek.
        {
            if (ws != null && ws.IsAlive)
            {
                string rosMessage = "{ \"op\": \"publish\", \"topic\": \"/your_topic\", \"msg\": { \"data\": \"Hello, ROS!\" } }";
                ws.Send(rosMessage);
                MessageBox.Show("Sent: " + rosMessage);
            }
            else
            {
                MessageBox.Show("WebSocket connection is not open.");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            WebSocket();
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
            markers.Markers.Add(marker);
            map.Overlays.Add(markers);
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
                    buttonIleri.BackColor = Color.Red;
                    break;
                case Keys.A:
                    buttonSol.BackColor = Color.Red;
                    break;
                case Keys.S:
                    buttonGeri.BackColor = Color.Red;
                    break;
                case Keys.D:
                    buttonSag.BackColor = Color.Red;
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
                    buttonIleri.BackColor = Color.White;
                    break;
                case Keys.A:
                    buttonSol.BackColor = Color.White;
                    break;
                case Keys.S:
                    buttonGeri.BackColor = Color.White;
                    break;
                case Keys.D:
                    buttonSag.BackColor = Color.White;
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
            }
            else
            {
                labelOtonomKontrol.BackColor = Color.ForestGreen;
                labelOtonomKontrol.Text = "AÇIK";
            }
            isGreen = !isGreen;
        }


        //WebSocket:
        private void WebSocket()
        {
            ws = new WebSocket(rosBridgeUrl);

            ws.OnOpen += (sender, e) =>
            {
                MessageBox.Show("ROSBridge sunucusuna bağlantı başarılı.");
            };

            ws.OnError += (sender, e) =>
            {
                MessageBox.Show("Bağlantı hatası: " + e.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };

            ws.OnMessage += (sender, e) =>
            {
                MessageBox.Show("Mesaj alındı: " + e.Data);
            };

            ws.OnClose += (sender, e) =>
            {
                MessageBox.Show("Bağlantı kapatıldı.");
            };

            try
            {
                ws.Connect();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bağlantı hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion


        //Header:
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        private void HeaderPanel_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void HeaderPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            dragging = false;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}