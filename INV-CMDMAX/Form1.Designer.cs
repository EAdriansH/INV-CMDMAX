namespace INV_CMDMAX
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            LblOrigen = new Label();
            DGVCostos = new DataGridView();
            BtnCreadorMatriz = new Button();
            CbMetodos = new ComboBox();
            NumOrigen = new NumericUpDown();
            NumDestino = new NumericUpDown();
            LblDestino = new Label();
            LblMetodos = new Label();
            BtnSolucionar = new Button();
            LblCostoMinimo = new Label();
            Tbcostominimo = new TextBox();
            menuStrip1 = new MenuStrip();
            OPCIONESToolStripMenuItem = new ToolStripMenuItem();
            modoDeVistaToolStripMenuItem = new ToolStripMenuItem();
            resolucionFlashToolStripMenuItem = new ToolStripMenuItem();
            resolucionPasoPorPasoToolStripMenuItem = new ToolStripMenuItem();
            DGVDemanda = new DataGridView();
            DGVOferta = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)DGVCostos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NumOrigen).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NumDestino).BeginInit();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DGVDemanda).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DGVOferta).BeginInit();
            SuspendLayout();
            // 
            // LblOrigen
            // 
            LblOrigen.AutoSize = true;
            LblOrigen.BackColor = Color.Transparent;
            LblOrigen.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            LblOrigen.ForeColor = SystemColors.HighlightText;
            LblOrigen.Location = new Point(22, 49);
            LblOrigen.Name = "LblOrigen";
            LblOrigen.Size = new Size(71, 20);
            LblOrigen.TabIndex = 0;
            LblOrigen.Text = "Origenes";
            // 
            // DGVCostos
            // 
            DGVCostos.AllowUserToAddRows = false;
            DGVCostos.AllowUserToDeleteRows = false;
            DGVCostos.AllowUserToResizeColumns = false;
            DGVCostos.AllowUserToResizeRows = false;
            DGVCostos.BackgroundColor = SystemColors.ButtonFace;
            DGVCostos.BorderStyle = BorderStyle.None;
            DGVCostos.ColumnHeadersHeight = 25;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            DGVCostos.DefaultCellStyle = dataGridViewCellStyle1;
            DGVCostos.Location = new Point(202, 54);
            DGVCostos.Margin = new Padding(0);
            DGVCostos.MultiSelect = false;
            DGVCostos.Name = "DGVCostos";
            DGVCostos.RowHeadersWidth = 120;
            DGVCostos.ScrollBars = ScrollBars.None;
            DGVCostos.SelectionMode = DataGridViewSelectionMode.CellSelect;
            DGVCostos.Size = new Size(648, 251);
            DGVCostos.TabIndex = 1;
            DGVCostos.CellContentClick += DGVOferta_CellContentClick;
            // 
            // BtnCreadorMatriz
            // 
            BtnCreadorMatriz.Location = new Point(21, 184);
            BtnCreadorMatriz.Name = "BtnCreadorMatriz";
            BtnCreadorMatriz.Size = new Size(125, 29);
            BtnCreadorMatriz.TabIndex = 2;
            BtnCreadorMatriz.Text = "Crear Matriz";
            BtnCreadorMatriz.UseVisualStyleBackColor = true;
            BtnCreadorMatriz.Click += BtnCreadorMatriz_Click;
            // 
            // CbMetodos
            // 
            CbMetodos.FormattingEnabled = true;
            CbMetodos.ImeMode = ImeMode.AlphaFull;
            CbMetodos.Location = new Point(21, 252);
            CbMetodos.Name = "CbMetodos";
            CbMetodos.Size = new Size(125, 28);
            CbMetodos.TabIndex = 3;
            CbMetodos.SelectedIndexChanged += CbMetodos_SelectedIndexChanged;
            // 
            // NumOrigen
            // 
            NumOrigen.BorderStyle = BorderStyle.FixedSingle;
            NumOrigen.Location = new Point(22, 72);
            NumOrigen.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            NumOrigen.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            NumOrigen.Name = "NumOrigen";
            NumOrigen.Size = new Size(124, 27);
            NumOrigen.TabIndex = 4;
            NumOrigen.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // NumDestino
            // 
            NumDestino.Location = new Point(22, 144);
            NumDestino.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            NumDestino.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            NumDestino.Name = "NumDestino";
            NumDestino.Size = new Size(124, 27);
            NumDestino.TabIndex = 5;
            NumDestino.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // LblDestino
            // 
            LblDestino.AutoSize = true;
            LblDestino.BackColor = Color.Transparent;
            LblDestino.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            LblDestino.ForeColor = SystemColors.Control;
            LblDestino.Location = new Point(23, 121);
            LblDestino.Name = "LblDestino";
            LblDestino.Size = new Size(70, 20);
            LblDestino.TabIndex = 6;
            LblDestino.Text = "Destinos";
            LblDestino.Click += LblDestino_Click;
            // 
            // LblMetodos
            // 
            LblMetodos.AutoSize = true;
            LblMetodos.BackColor = Color.Transparent;
            LblMetodos.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            LblMetodos.ForeColor = SystemColors.ButtonHighlight;
            LblMetodos.Location = new Point(13, 229);
            LblMetodos.Name = "LblMetodos";
            LblMetodos.Size = new Size(148, 20);
            LblMetodos.TabIndex = 7;
            LblMetodos.Text = "Metodo de Solucion";
            // 
            // BtnSolucionar
            // 
            BtnSolucionar.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BtnSolucionar.Location = new Point(38, 286);
            BtnSolucionar.Name = "BtnSolucionar";
            BtnSolucionar.Size = new Size(94, 28);
            BtnSolucionar.TabIndex = 8;
            BtnSolucionar.Text = "Solucionar";
            BtnSolucionar.UseVisualStyleBackColor = true;
            BtnSolucionar.Click += BtnSolucionar_Click;
            // 
            // LblCostoMinimo
            // 
            LblCostoMinimo.AutoSize = true;
            LblCostoMinimo.BackColor = Color.Transparent;
            LblCostoMinimo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            LblCostoMinimo.ForeColor = SystemColors.ButtonHighlight;
            LblCostoMinimo.Location = new Point(21, 360);
            LblCostoMinimo.Name = "LblCostoMinimo";
            LblCostoMinimo.Size = new Size(111, 20);
            LblCostoMinimo.TabIndex = 10;
            LblCostoMinimo.Text = "Costo Minimo:";
            LblCostoMinimo.Click += LblCostoMinimo_Click;
            // 
            // Tbcostominimo
            // 
            Tbcostominimo.Location = new Point(22, 383);
            Tbcostominimo.Name = "Tbcostominimo";
            Tbcostominimo.Size = new Size(160, 27);
            Tbcostominimo.TabIndex = 11;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { OPCIONESToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1466, 28);
            menuStrip1.TabIndex = 12;
            menuStrip1.Text = "menuStrip1";
            // 
            // OPCIONESToolStripMenuItem
            // 
            OPCIONESToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { modoDeVistaToolStripMenuItem });
            OPCIONESToolStripMenuItem.Name = "OPCIONESToolStripMenuItem";
            OPCIONESToolStripMenuItem.Size = new Size(93, 24);
            OPCIONESToolStripMenuItem.Text = "OPCIONES";
            // 
            // modoDeVistaToolStripMenuItem
            // 
            modoDeVistaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { resolucionFlashToolStripMenuItem, resolucionPasoPorPasoToolStripMenuItem });
            modoDeVistaToolStripMenuItem.Name = "modoDeVistaToolStripMenuItem";
            modoDeVistaToolStripMenuItem.Size = new Size(187, 26);
            modoDeVistaToolStripMenuItem.Text = "Modo de vista";
            // 
            // resolucionFlashToolStripMenuItem
            // 
            resolucionFlashToolStripMenuItem.Name = "resolucionFlashToolStripMenuItem";
            resolucionFlashToolStripMenuItem.Size = new Size(263, 26);
            resolucionFlashToolStripMenuItem.Text = "Resolucion flash";
            // 
            // resolucionPasoPorPasoToolStripMenuItem
            // 
            resolucionPasoPorPasoToolStripMenuItem.Name = "resolucionPasoPorPasoToolStripMenuItem";
            resolucionPasoPorPasoToolStripMenuItem.Size = new Size(263, 26);
            resolucionPasoPorPasoToolStripMenuItem.Text = "Resolucion paso por paso";
            // 
            // DGVDemanda
            // 
            DGVDemanda.AllowUserToAddRows = false;
            DGVDemanda.AllowUserToDeleteRows = false;
            DGVDemanda.AllowUserToResizeColumns = false;
            DGVDemanda.AllowUserToResizeRows = false;
            DGVDemanda.BackgroundColor = SystemColors.ButtonFace;
            DGVDemanda.BorderStyle = BorderStyle.None;
            DGVDemanda.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DGVDemanda.ColumnHeadersVisible = false;
            DGVDemanda.Location = new Point(202, 317);
            DGVDemanda.MultiSelect = false;
            DGVDemanda.Name = "DGVDemanda";
            DGVDemanda.RowHeadersWidth = 120;
            DGVDemanda.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            DGVDemanda.ScrollBars = ScrollBars.None;
            DGVDemanda.SelectionMode = DataGridViewSelectionMode.CellSelect;
            DGVDemanda.Size = new Size(648, 49);
            DGVDemanda.TabIndex = 13;
            // 
            // DGVOferta
            // 
            DGVOferta.AllowUserToAddRows = false;
            DGVOferta.AllowUserToDeleteRows = false;
            DGVOferta.AllowUserToResizeColumns = false;
            DGVOferta.AllowUserToResizeRows = false;
            DGVOferta.BackgroundColor = SystemColors.ButtonFace;
            DGVOferta.BorderStyle = BorderStyle.None;
            DGVOferta.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DGVOferta.Location = new Point(864, 54);
            DGVOferta.MultiSelect = false;
            DGVOferta.Name = "DGVOferta";
            DGVOferta.RowHeadersVisible = false;
            DGVOferta.RowHeadersWidth = 51;
            DGVOferta.ScrollBars = ScrollBars.None;
            DGVOferta.SelectionMode = DataGridViewSelectionMode.CellSelect;
            DGVOferta.Size = new Size(57, 251);
            DGVOferta.TabIndex = 14;
            DGVOferta.CellContentClick += DGVOferta_CellContentClick_1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DimGray;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1466, 586);
            Controls.Add(DGVOferta);
            Controls.Add(DGVDemanda);
            Controls.Add(Tbcostominimo);
            Controls.Add(LblCostoMinimo);
            Controls.Add(BtnSolucionar);
            Controls.Add(LblMetodos);
            Controls.Add(LblDestino);
            Controls.Add(NumDestino);
            Controls.Add(NumOrigen);
            Controls.Add(CbMetodos);
            Controls.Add(BtnCreadorMatriz);
            Controls.Add(DGVCostos);
            Controls.Add(LblOrigen);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Resolucionador";
            TransparencyKey = Color.BurlyWood;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)DGVCostos).EndInit();
            ((System.ComponentModel.ISupportInitialize)NumOrigen).EndInit();
            ((System.ComponentModel.ISupportInitialize)NumDestino).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)DGVDemanda).EndInit();
            ((System.ComponentModel.ISupportInitialize)DGVOferta).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label LblOrigen;
        private DataGridView DGVCostos;
        private Button BtnCreadorMatriz;
        private ComboBox CbMetodos;
        private NumericUpDown NumOrigen;
        private NumericUpDown NumDestino;
        private Label LblDestino;
        private Label LblMetodos;
        private Button BtnSolucionar;
        private Label LblCostoMinimo;
        private TextBox Tbcostominimo;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem OPCIONESToolStripMenuItem;
        private ToolStripMenuItem modoDeVistaToolStripMenuItem;
        private ToolStripMenuItem resolucionFlashToolStripMenuItem;
        private ToolStripMenuItem resolucionPasoPorPasoToolStripMenuItem;
        private DataGridView DGVDemanda;
        private DataGridView DGVOferta;
    }
}
