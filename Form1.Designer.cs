namespace TAISAT
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelC1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelC1_L1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1C1_L1_L1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxKontrolSistemi = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelKontrolSistemi = new System.Windows.Forms.TableLayoutPanel();
            this.buttonIleri = new System.Windows.Forms.Button();
            this.buttonSol = new System.Windows.Forms.Button();
            this.buttonSag = new System.Windows.Forms.Button();
            this.buttonGeri = new System.Windows.Forms.Button();
            this.buttonDur = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxOtonomKontrolSistemi = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelOtonomKontrolSistemi = new System.Windows.Forms.TableLayoutPanel();
            this.labelOtonomKontrol = new System.Windows.Forms.Label();
            this.buttonOtoAcKapat = new System.Windows.Forms.Button();
            this.groupBoxAcKapat = new System.Windows.Forms.GroupBox();
            this.buttonAcKapat = new System.Windows.Forms.Button();
            this.tableLayoutPanelC1_L2 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxLoglar = new System.Windows.Forms.GroupBox();
            this.groupBoxHataKodlari = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelC2 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxAracBilgileri = new System.Windows.Forms.GroupBox();
            this.groupBoxPort = new System.Windows.Forms.GroupBox();
            this.groupBoxGorevListesi = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelC3 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxKamera = new System.Windows.Forms.GroupBox();
            this.groupBoxBitkiBilgisi = new System.Windows.Forms.GroupBox();
            this.groupBoxHarita = new System.Windows.Forms.GroupBox();
            this.map = new GMap.NET.WindowsForms.GMapControl();
            this.tableLayoutPanelMain.SuspendLayout();
            this.tableLayoutPanelC1.SuspendLayout();
            this.tableLayoutPanelC1_L1.SuspendLayout();
            this.tableLayoutPanel1C1_L1_L1.SuspendLayout();
            this.groupBoxKontrolSistemi.SuspendLayout();
            this.tableLayoutPanelKontrolSistemi.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBoxOtonomKontrolSistemi.SuspendLayout();
            this.tableLayoutPanelOtonomKontrolSistemi.SuspendLayout();
            this.groupBoxAcKapat.SuspendLayout();
            this.tableLayoutPanelC1_L2.SuspendLayout();
            this.tableLayoutPanelC2.SuspendLayout();
            this.tableLayoutPanelC3.SuspendLayout();
            this.groupBoxHarita.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.tableLayoutPanelMain.ColumnCount = 3;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanelC1, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanelC2, 1, 0);
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanelC3, 2, 0);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 1;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(1904, 1041);
            this.tableLayoutPanelMain.TabIndex = 0;
            // 
            // tableLayoutPanelC1
            // 
            this.tableLayoutPanelC1.ColumnCount = 1;
            this.tableLayoutPanelC1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelC1.Controls.Add(this.tableLayoutPanelC1_L1, 0, 0);
            this.tableLayoutPanelC1.Controls.Add(this.tableLayoutPanelC1_L2, 0, 1);
            this.tableLayoutPanelC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelC1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelC1.Name = "tableLayoutPanelC1";
            this.tableLayoutPanelC1.RowCount = 3;
            this.tableLayoutPanelC1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 47.5F));
            this.tableLayoutPanelC1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanelC1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42.5F));
            this.tableLayoutPanelC1.Size = new System.Drawing.Size(755, 1035);
            this.tableLayoutPanelC1.TabIndex = 0;
            // 
            // tableLayoutPanelC1_L1
            // 
            this.tableLayoutPanelC1_L1.ColumnCount = 1;
            this.tableLayoutPanelC1_L1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelC1_L1.Controls.Add(this.tableLayoutPanel1C1_L1_L1, 0, 0);
            this.tableLayoutPanelC1_L1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelC1_L1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelC1_L1.Name = "tableLayoutPanelC1_L1";
            this.tableLayoutPanelC1_L1.RowCount = 2;
            this.tableLayoutPanelC1_L1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanelC1_L1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelC1_L1.Size = new System.Drawing.Size(749, 485);
            this.tableLayoutPanelC1_L1.TabIndex = 0;
            // 
            // tableLayoutPanel1C1_L1_L1
            // 
            this.tableLayoutPanel1C1_L1_L1.ColumnCount = 2;
            this.tableLayoutPanel1C1_L1_L1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel1C1_L1_L1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1C1_L1_L1.Controls.Add(this.groupBoxKontrolSistemi, 0, 0);
            this.tableLayoutPanel1C1_L1_L1.Controls.Add(this.tableLayoutPanel1, 1, 0);
            this.tableLayoutPanel1C1_L1_L1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1C1_L1_L1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1C1_L1_L1.Name = "tableLayoutPanel1C1_L1_L1";
            this.tableLayoutPanel1C1_L1_L1.RowCount = 1;
            this.tableLayoutPanel1C1_L1_L1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1C1_L1_L1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 382F));
            this.tableLayoutPanel1C1_L1_L1.Size = new System.Drawing.Size(743, 382);
            this.tableLayoutPanel1C1_L1_L1.TabIndex = 0;
            // 
            // groupBoxKontrolSistemi
            // 
            this.groupBoxKontrolSistemi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(95)))), ((int)(((byte)(95)))));
            this.groupBoxKontrolSistemi.Controls.Add(this.tableLayoutPanelKontrolSistemi);
            this.groupBoxKontrolSistemi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxKontrolSistemi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBoxKontrolSistemi.Font = new System.Drawing.Font("Bahnschrift SemiBold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBoxKontrolSistemi.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBoxKontrolSistemi.Location = new System.Drawing.Point(3, 3);
            this.groupBoxKontrolSistemi.Name = "groupBoxKontrolSistemi";
            this.groupBoxKontrolSistemi.Size = new System.Drawing.Size(551, 376);
            this.groupBoxKontrolSistemi.TabIndex = 0;
            this.groupBoxKontrolSistemi.TabStop = false;
            this.groupBoxKontrolSistemi.Text = "KONTROL SİSTEMİ";
            this.groupBoxKontrolSistemi.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBoxKontrolSistemi_Paint);
            // 
            // tableLayoutPanelKontrolSistemi
            // 
            this.tableLayoutPanelKontrolSistemi.ColumnCount = 3;
            this.tableLayoutPanelKontrolSistemi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelKontrolSistemi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanelKontrolSistemi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanelKontrolSistemi.Controls.Add(this.buttonIleri, 1, 0);
            this.tableLayoutPanelKontrolSistemi.Controls.Add(this.buttonSol, 0, 1);
            this.tableLayoutPanelKontrolSistemi.Controls.Add(this.buttonSag, 2, 1);
            this.tableLayoutPanelKontrolSistemi.Controls.Add(this.buttonGeri, 1, 2);
            this.tableLayoutPanelKontrolSistemi.Controls.Add(this.buttonDur, 1, 1);
            this.tableLayoutPanelKontrolSistemi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelKontrolSistemi.Location = new System.Drawing.Point(3, 32);
            this.tableLayoutPanelKontrolSistemi.Name = "tableLayoutPanelKontrolSistemi";
            this.tableLayoutPanelKontrolSistemi.RowCount = 3;
            this.tableLayoutPanelKontrolSistemi.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelKontrolSistemi.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelKontrolSistemi.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelKontrolSistemi.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelKontrolSistemi.Size = new System.Drawing.Size(545, 341);
            this.tableLayoutPanelKontrolSistemi.TabIndex = 0;
            // 
            // buttonIleri
            // 
            this.buttonIleri.BackColor = System.Drawing.Color.Transparent;
            this.buttonIleri.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonIleri.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(95)))), ((int)(((byte)(95)))));
            this.buttonIleri.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray;
            this.buttonIleri.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonIleri.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonIleri.Image = ((System.Drawing.Image)(resources.GetObject("buttonIleri.Image")));
            this.buttonIleri.Location = new System.Drawing.Point(184, 3);
            this.buttonIleri.Name = "buttonIleri";
            this.buttonIleri.Size = new System.Drawing.Size(175, 107);
            this.buttonIleri.TabIndex = 0;
            this.buttonIleri.UseVisualStyleBackColor = false;
            // 
            // buttonSol
            // 
            this.buttonSol.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(95)))), ((int)(((byte)(95)))));
            this.buttonSol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonSol.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(95)))), ((int)(((byte)(95)))));
            this.buttonSol.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray;
            this.buttonSol.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonSol.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSol.Image = ((System.Drawing.Image)(resources.GetObject("buttonSol.Image")));
            this.buttonSol.Location = new System.Drawing.Point(3, 116);
            this.buttonSol.Name = "buttonSol";
            this.buttonSol.Size = new System.Drawing.Size(175, 107);
            this.buttonSol.TabIndex = 1;
            this.buttonSol.UseVisualStyleBackColor = false;
            // 
            // buttonSag
            // 
            this.buttonSag.BackColor = System.Drawing.Color.Transparent;
            this.buttonSag.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonSag.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(95)))), ((int)(((byte)(95)))));
            this.buttonSag.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray;
            this.buttonSag.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonSag.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSag.Image = ((System.Drawing.Image)(resources.GetObject("buttonSag.Image")));
            this.buttonSag.Location = new System.Drawing.Point(365, 116);
            this.buttonSag.Name = "buttonSag";
            this.buttonSag.Size = new System.Drawing.Size(177, 107);
            this.buttonSag.TabIndex = 2;
            this.buttonSag.UseVisualStyleBackColor = false;
            // 
            // buttonGeri
            // 
            this.buttonGeri.BackColor = System.Drawing.Color.Transparent;
            this.buttonGeri.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonGeri.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(95)))), ((int)(((byte)(95)))));
            this.buttonGeri.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray;
            this.buttonGeri.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonGeri.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonGeri.Image = ((System.Drawing.Image)(resources.GetObject("buttonGeri.Image")));
            this.buttonGeri.Location = new System.Drawing.Point(184, 229);
            this.buttonGeri.Name = "buttonGeri";
            this.buttonGeri.Size = new System.Drawing.Size(175, 109);
            this.buttonGeri.TabIndex = 3;
            this.buttonGeri.UseVisualStyleBackColor = false;
            // 
            // buttonDur
            // 
            this.buttonDur.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.buttonDur.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonDur.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.buttonDur.FlatAppearance.BorderSize = 10;
            this.buttonDur.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray;
            this.buttonDur.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkGray;
            this.buttonDur.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDur.Font = new System.Drawing.Font("Bahnschrift", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonDur.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.buttonDur.Location = new System.Drawing.Point(184, 116);
            this.buttonDur.Name = "buttonDur";
            this.buttonDur.Size = new System.Drawing.Size(175, 107);
            this.buttonDur.TabIndex = 4;
            this.buttonDur.Text = "DUR";
            this.buttonDur.UseVisualStyleBackColor = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.groupBoxOtonomKontrolSistemi, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxAcKapat, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(560, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(180, 376);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // groupBoxOtonomKontrolSistemi
            // 
            this.groupBoxOtonomKontrolSistemi.Controls.Add(this.tableLayoutPanelOtonomKontrolSistemi);
            this.groupBoxOtonomKontrolSistemi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxOtonomKontrolSistemi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBoxOtonomKontrolSistemi.Font = new System.Drawing.Font("Bahnschrift SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBoxOtonomKontrolSistemi.Location = new System.Drawing.Point(3, 97);
            this.groupBoxOtonomKontrolSistemi.Name = "groupBoxOtonomKontrolSistemi";
            this.groupBoxOtonomKontrolSistemi.Size = new System.Drawing.Size(174, 276);
            this.groupBoxOtonomKontrolSistemi.TabIndex = 4;
            this.groupBoxOtonomKontrolSistemi.TabStop = false;
            this.groupBoxOtonomKontrolSistemi.Text = "OTONOM KONTROL SİSTEMİ";
            // 
            // tableLayoutPanelOtonomKontrolSistemi
            // 
            this.tableLayoutPanelOtonomKontrolSistemi.ColumnCount = 1;
            this.tableLayoutPanelOtonomKontrolSistemi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelOtonomKontrolSistemi.Controls.Add(this.labelOtonomKontrol, 0, 0);
            this.tableLayoutPanelOtonomKontrolSistemi.Controls.Add(this.buttonOtoAcKapat, 0, 1);
            this.tableLayoutPanelOtonomKontrolSistemi.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanelOtonomKontrolSistemi.Location = new System.Drawing.Point(3, 51);
            this.tableLayoutPanelOtonomKontrolSistemi.Name = "tableLayoutPanelOtonomKontrolSistemi";
            this.tableLayoutPanelOtonomKontrolSistemi.RowCount = 2;
            this.tableLayoutPanelOtonomKontrolSistemi.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelOtonomKontrolSistemi.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelOtonomKontrolSistemi.Size = new System.Drawing.Size(168, 222);
            this.tableLayoutPanelOtonomKontrolSistemi.TabIndex = 0;
            // 
            // labelOtonomKontrol
            // 
            this.labelOtonomKontrol.AutoSize = true;
            this.labelOtonomKontrol.BackColor = System.Drawing.Color.ForestGreen;
            this.labelOtonomKontrol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelOtonomKontrol.Font = new System.Drawing.Font("Bahnschrift", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelOtonomKontrol.Location = new System.Drawing.Point(3, 0);
            this.labelOtonomKontrol.Name = "labelOtonomKontrol";
            this.labelOtonomKontrol.Size = new System.Drawing.Size(162, 111);
            this.labelOtonomKontrol.TabIndex = 0;
            this.labelOtonomKontrol.Text = "AÇIK";
            this.labelOtonomKontrol.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonOtoAcKapat
            // 
            this.buttonOtoAcKapat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOtoAcKapat.Location = new System.Drawing.Point(3, 114);
            this.buttonOtoAcKapat.Name = "buttonOtoAcKapat";
            this.buttonOtoAcKapat.Size = new System.Drawing.Size(162, 105);
            this.buttonOtoAcKapat.TabIndex = 1;
            this.buttonOtoAcKapat.UseVisualStyleBackColor = true;
            // 
            // groupBoxAcKapat
            // 
            this.groupBoxAcKapat.Controls.Add(this.buttonAcKapat);
            this.groupBoxAcKapat.Font = new System.Drawing.Font("Bahnschrift SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBoxAcKapat.Location = new System.Drawing.Point(3, 3);
            this.groupBoxAcKapat.Name = "groupBoxAcKapat";
            this.groupBoxAcKapat.Size = new System.Drawing.Size(174, 82);
            this.groupBoxAcKapat.TabIndex = 5;
            this.groupBoxAcKapat.TabStop = false;
            this.groupBoxAcKapat.Text = "ARAÇ AÇ / KAPAT";
            // 
            // buttonAcKapat
            // 
            this.buttonAcKapat.BackColor = System.Drawing.SystemColors.Control;
            this.buttonAcKapat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonAcKapat.Font = new System.Drawing.Font("Bahnschrift", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonAcKapat.Location = new System.Drawing.Point(3, 19);
            this.buttonAcKapat.Name = "buttonAcKapat";
            this.buttonAcKapat.Size = new System.Drawing.Size(168, 60);
            this.buttonAcKapat.TabIndex = 6;
            this.buttonAcKapat.UseVisualStyleBackColor = false;
            // 
            // tableLayoutPanelC1_L2
            // 
            this.tableLayoutPanelC1_L2.ColumnCount = 2;
            this.tableLayoutPanelC1_L2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelC1_L2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelC1_L2.Controls.Add(this.groupBoxLoglar, 1, 0);
            this.tableLayoutPanelC1_L2.Controls.Add(this.groupBoxHataKodlari, 0, 0);
            this.tableLayoutPanelC1_L2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelC1_L2.Location = new System.Drawing.Point(3, 494);
            this.tableLayoutPanelC1_L2.Name = "tableLayoutPanelC1_L2";
            this.tableLayoutPanelC1_L2.RowCount = 1;
            this.tableLayoutPanelC1_L2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelC1_L2.Size = new System.Drawing.Size(749, 97);
            this.tableLayoutPanelC1_L2.TabIndex = 1;
            // 
            // groupBoxLoglar
            // 
            this.groupBoxLoglar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxLoglar.Font = new System.Drawing.Font("Bahnschrift SemiBold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBoxLoglar.Location = new System.Drawing.Point(377, 3);
            this.groupBoxLoglar.Name = "groupBoxLoglar";
            this.groupBoxLoglar.Size = new System.Drawing.Size(369, 91);
            this.groupBoxLoglar.TabIndex = 1;
            this.groupBoxLoglar.TabStop = false;
            this.groupBoxLoglar.Text = "LOGLAR";
            // 
            // groupBoxHataKodlari
            // 
            this.groupBoxHataKodlari.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxHataKodlari.Font = new System.Drawing.Font("Bahnschrift SemiBold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBoxHataKodlari.Location = new System.Drawing.Point(3, 3);
            this.groupBoxHataKodlari.Name = "groupBoxHataKodlari";
            this.groupBoxHataKodlari.Size = new System.Drawing.Size(368, 91);
            this.groupBoxHataKodlari.TabIndex = 0;
            this.groupBoxHataKodlari.TabStop = false;
            this.groupBoxHataKodlari.Text = "HATA KODLARI";
            // 
            // tableLayoutPanelC2
            // 
            this.tableLayoutPanelC2.ColumnCount = 1;
            this.tableLayoutPanelC2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelC2.Controls.Add(this.groupBoxAracBilgileri, 0, 1);
            this.tableLayoutPanelC2.Controls.Add(this.groupBoxPort, 0, 3);
            this.tableLayoutPanelC2.Controls.Add(this.groupBoxGorevListesi, 0, 2);
            this.tableLayoutPanelC2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelC2.Location = new System.Drawing.Point(764, 3);
            this.tableLayoutPanelC2.Name = "tableLayoutPanelC2";
            this.tableLayoutPanelC2.RowCount = 4;
            this.tableLayoutPanelC2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19F));
            this.tableLayoutPanelC2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27F));
            this.tableLayoutPanelC2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27F));
            this.tableLayoutPanelC2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27F));
            this.tableLayoutPanelC2.Size = new System.Drawing.Size(374, 1035);
            this.tableLayoutPanelC2.TabIndex = 1;
            // 
            // groupBoxAracBilgileri
            // 
            this.groupBoxAracBilgileri.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxAracBilgileri.Font = new System.Drawing.Font("Bahnschrift SemiBold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBoxAracBilgileri.Location = new System.Drawing.Point(3, 199);
            this.groupBoxAracBilgileri.Name = "groupBoxAracBilgileri";
            this.groupBoxAracBilgileri.Size = new System.Drawing.Size(368, 273);
            this.groupBoxAracBilgileri.TabIndex = 5;
            this.groupBoxAracBilgileri.TabStop = false;
            this.groupBoxAracBilgileri.Text = "ARAÇ BİLGİLERİ";
            // 
            // groupBoxPort
            // 
            this.groupBoxPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxPort.Font = new System.Drawing.Font("Bahnschrift SemiBold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBoxPort.Location = new System.Drawing.Point(3, 757);
            this.groupBoxPort.Name = "groupBoxPort";
            this.groupBoxPort.Size = new System.Drawing.Size(368, 275);
            this.groupBoxPort.TabIndex = 3;
            this.groupBoxPort.TabStop = false;
            this.groupBoxPort.Text = "PORTTAN GELEN BİLGİLER";
            // 
            // groupBoxGorevListesi
            // 
            this.groupBoxGorevListesi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxGorevListesi.Font = new System.Drawing.Font("Bahnschrift SemiBold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBoxGorevListesi.Location = new System.Drawing.Point(3, 478);
            this.groupBoxGorevListesi.Name = "groupBoxGorevListesi";
            this.groupBoxGorevListesi.Size = new System.Drawing.Size(368, 273);
            this.groupBoxGorevListesi.TabIndex = 0;
            this.groupBoxGorevListesi.TabStop = false;
            this.groupBoxGorevListesi.Text = "GÖREV LİSTESİ";
            // 
            // tableLayoutPanelC3
            // 
            this.tableLayoutPanelC3.ColumnCount = 1;
            this.tableLayoutPanelC3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelC3.Controls.Add(this.groupBoxKamera, 0, 0);
            this.tableLayoutPanelC3.Controls.Add(this.groupBoxBitkiBilgisi, 0, 1);
            this.tableLayoutPanelC3.Controls.Add(this.groupBoxHarita, 0, 2);
            this.tableLayoutPanelC3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelC3.Location = new System.Drawing.Point(1144, 3);
            this.tableLayoutPanelC3.Name = "tableLayoutPanelC3";
            this.tableLayoutPanelC3.RowCount = 3;
            this.tableLayoutPanelC3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanelC3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelC3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanelC3.Size = new System.Drawing.Size(757, 1035);
            this.tableLayoutPanelC3.TabIndex = 2;
            // 
            // groupBoxKamera
            // 
            this.groupBoxKamera.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxKamera.Font = new System.Drawing.Font("Bahnschrift SemiBold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBoxKamera.Location = new System.Drawing.Point(3, 3);
            this.groupBoxKamera.Name = "groupBoxKamera";
            this.groupBoxKamera.Size = new System.Drawing.Size(751, 356);
            this.groupBoxKamera.TabIndex = 0;
            this.groupBoxKamera.TabStop = false;
            this.groupBoxKamera.Text = "KAMERA";
            // 
            // groupBoxBitkiBilgisi
            // 
            this.groupBoxBitkiBilgisi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxBitkiBilgisi.Font = new System.Drawing.Font("Bahnschrift SemiBold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBoxBitkiBilgisi.Location = new System.Drawing.Point(3, 365);
            this.groupBoxBitkiBilgisi.Name = "groupBoxBitkiBilgisi";
            this.groupBoxBitkiBilgisi.Size = new System.Drawing.Size(751, 252);
            this.groupBoxBitkiBilgisi.TabIndex = 1;
            this.groupBoxBitkiBilgisi.TabStop = false;
            this.groupBoxBitkiBilgisi.Text = "BİTKİ BİLGİSİ";
            // 
            // groupBoxHarita
            // 
            this.groupBoxHarita.Controls.Add(this.map);
            this.groupBoxHarita.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxHarita.Font = new System.Drawing.Font("Bahnschrift SemiBold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBoxHarita.Location = new System.Drawing.Point(3, 623);
            this.groupBoxHarita.Name = "groupBoxHarita";
            this.groupBoxHarita.Size = new System.Drawing.Size(751, 409);
            this.groupBoxHarita.TabIndex = 2;
            this.groupBoxHarita.TabStop = false;
            this.groupBoxHarita.Text = "HARİTA";
            // 
            // map
            // 
            this.map.Bearing = 0F;
            this.map.CanDragMap = true;
            this.map.Dock = System.Windows.Forms.DockStyle.Fill;
            this.map.EmptyTileColor = System.Drawing.Color.Navy;
            this.map.GrayScaleMode = false;
            this.map.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.map.LevelsKeepInMemmory = 5;
            this.map.Location = new System.Drawing.Point(3, 32);
            this.map.MarkersEnabled = true;
            this.map.MaxZoom = 2;
            this.map.MinZoom = 2;
            this.map.MouseWheelZoomEnabled = true;
            this.map.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.map.Name = "map";
            this.map.NegativeMode = false;
            this.map.PolygonsEnabled = true;
            this.map.RetryLoadTile = 0;
            this.map.RoutesEnabled = true;
            this.map.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.map.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.map.ShowTileGridLines = false;
            this.map.Size = new System.Drawing.Size(745, 374);
            this.map.TabIndex = 0;
            this.map.Zoom = 0D;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelC1.ResumeLayout(false);
            this.tableLayoutPanelC1_L1.ResumeLayout(false);
            this.tableLayoutPanel1C1_L1_L1.ResumeLayout(false);
            this.groupBoxKontrolSistemi.ResumeLayout(false);
            this.tableLayoutPanelKontrolSistemi.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBoxOtonomKontrolSistemi.ResumeLayout(false);
            this.tableLayoutPanelOtonomKontrolSistemi.ResumeLayout(false);
            this.tableLayoutPanelOtonomKontrolSistemi.PerformLayout();
            this.groupBoxAcKapat.ResumeLayout(false);
            this.tableLayoutPanelC1_L2.ResumeLayout(false);
            this.tableLayoutPanelC2.ResumeLayout(false);
            this.tableLayoutPanelC3.ResumeLayout(false);
            this.groupBoxHarita.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelC1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelC2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelC3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelC1_L2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelC1_L1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1C1_L1_L1;
        private System.Windows.Forms.GroupBox groupBoxKontrolSistemi;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBoxOtonomKontrolSistemi;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelOtonomKontrolSistemi;
        private System.Windows.Forms.GroupBox groupBoxLoglar;
        private System.Windows.Forms.GroupBox groupBoxHataKodlari;
        private System.Windows.Forms.GroupBox groupBoxKamera;
        private System.Windows.Forms.GroupBox groupBoxBitkiBilgisi;
        private System.Windows.Forms.GroupBox groupBoxHarita;
        private System.Windows.Forms.GroupBox groupBoxGorevListesi;
        private System.Windows.Forms.GroupBox groupBoxPort;
        private System.Windows.Forms.GroupBox groupBoxAracBilgileri;
        private GMap.NET.WindowsForms.GMapControl map;
        private System.Windows.Forms.Label labelOtonomKontrol;
        private System.Windows.Forms.Button buttonOtoAcKapat;
        private System.Windows.Forms.GroupBox groupBoxAcKapat;
        private System.Windows.Forms.Button buttonAcKapat;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelKontrolSistemi;
        private System.Windows.Forms.Button buttonIleri;
        private System.Windows.Forms.Button buttonSol;
        private System.Windows.Forms.Button buttonSag;
        private System.Windows.Forms.Button buttonGeri;
        private System.Windows.Forms.Button buttonDur;
    }
}

