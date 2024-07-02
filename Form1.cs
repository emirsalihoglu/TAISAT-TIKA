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
    public partial class Form1 : Form
    {
        private SerialPort port;
        private bool isGreen = true;
        private bool isWhite = true;
        private WebSocket ws;
        string rosBridgeUrl = "ws://simple-websocket-server-echo.glitch.me/"; //ROS Server IP'sine göre özelleştirilecek.

        public Form1()
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

        private void Form1_Load(object sender, EventArgs e)
        {
            timerSaat.Start();
            this.KeyDown += Form1_KeyDown;
            this.KeyPreview = true;

            foreach (string portName in SerialPort.GetPortNames())
            {
                comboBoxPort.Items.Add(portName);
            }

            if (comboBoxPort.Items.Count > 0)
            {
                comboBoxPort.SelectedIndex = 0;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ws != null)
            {
                ws.Close();
            }
        }

        private void timerSaat_Tick(object sender, EventArgs e)
        {
            labelSaat.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string data = port.ReadLine();
            Invoke(new Action(() =>
            {
                richTextBox2.AppendText(data + Environment.NewLine);
            }));
        }

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

        private void buttonHarita_Click(object sender, EventArgs e)
        {
            Harita();
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

        private void buttonZoomIn_Click(object sender, EventArgs e)
        {
            ZoomIn();
        }

        private void buttonZoomOut_Click(object sender, EventArgs e)
        {
            ZoomOut();
        }

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

        private void buttonOtoAcKapat_Click(object sender, EventArgs e)
        {
            OtonomKontrol();
        }

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

        private void buttonIleri_Click(object sender, EventArgs e)
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
    }
}