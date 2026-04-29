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
            LblOrigen = new Label();
            DGVCostos = new DataGridView();
            BtnCreadorMatriz = new Button();
            CbMetodos = new ComboBox();
            NumOrigen = new NumericUpDown();
            NumDestino = new NumericUpDown();
            LblDestino = new Label();
            LblMetodos = new Label();
            BtnSolucionar = new Button();
            LblPruebas = new Label();
            LblCostoMinimo = new Label();
            Tbcostominimo = new TextBox();
            ((System.ComponentModel.ISupportInitialize)DGVCostos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NumOrigen).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NumDestino).BeginInit();
            SuspendLayout();
            // 
            // LblOrigen
            // 
            LblOrigen.AutoSize = true;
            LblOrigen.Location = new Point(22, 49);
            LblOrigen.Name = "LblOrigen";
            LblOrigen.Size = new Size(68, 20);
            LblOrigen.TabIndex = 0;
            LblOrigen.Text = "Origenes";
            // 
            // DGVCostos
            // 
            DGVCostos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DGVCostos.Location = new Point(265, 108);
            DGVCostos.Name = "DGVCostos";
            DGVCostos.RowHeadersWidth = 51;
            DGVCostos.Size = new Size(733, 336);
            DGVCostos.TabIndex = 1;
            DGVCostos.CellContentClick += DGVOferta_CellContentClick;
            // 
            // BtnCreadorMatriz
            // 
            BtnCreadorMatriz.Location = new Point(33, 183);
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
            CbMetodos.Items.AddRange(new object[] { "Costo Minimo", "Demaxmin" });
            CbMetodos.Location = new Point(265, 52);
            CbMetodos.Name = "CbMetodos";
            CbMetodos.Size = new Size(151, 28);
            CbMetodos.TabIndex = 3;
            // 
            // NumOrigen
            // 
            NumOrigen.Location = new Point(22, 72);
            NumOrigen.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            NumOrigen.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            NumOrigen.Name = "NumOrigen";
            NumOrigen.Size = new Size(150, 27);
            NumOrigen.TabIndex = 4;
            NumOrigen.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // NumDestino
            // 
            NumDestino.Location = new Point(22, 127);
            NumDestino.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            NumDestino.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            NumDestino.Name = "NumDestino";
            NumDestino.Size = new Size(150, 27);
            NumDestino.TabIndex = 5;
            NumDestino.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // LblDestino
            // 
            LblDestino.AutoSize = true;
            LblDestino.Location = new Point(22, 103);
            LblDestino.Name = "LblDestino";
            LblDestino.Size = new Size(66, 20);
            LblDestino.TabIndex = 6;
            LblDestino.Text = "Destinos";
            // 
            // LblMetodos
            // 
            LblMetodos.AutoSize = true;
            LblMetodos.Location = new Point(265, 29);
            LblMetodos.Name = "LblMetodos";
            LblMetodos.Size = new Size(144, 20);
            LblMetodos.TabIndex = 7;
            LblMetodos.Text = "Metodo de Solucion";
            // 
            // BtnSolucionar
            // 
            BtnSolucionar.Location = new Point(450, 52);
            BtnSolucionar.Name = "BtnSolucionar";
            BtnSolucionar.Size = new Size(94, 28);
            BtnSolucionar.TabIndex = 8;
            BtnSolucionar.Text = "Solucionar";
            BtnSolucionar.UseVisualStyleBackColor = true;
            BtnSolucionar.Click += BtnSolucionar_Click;
            // 
            // LblPruebas
            // 
            LblPruebas.AutoSize = true;
            LblPruebas.Location = new Point(608, 56);
            LblPruebas.Name = "LblPruebas";
            LblPruebas.Size = new Size(71, 20);
            LblPruebas.TabIndex = 9;
            LblPruebas.Text = "PRUEBAS";
            // 
            // LblCostoMinimo
            // 
            LblCostoMinimo.AutoSize = true;
            LblCostoMinimo.Location = new Point(12, 285);
            LblCostoMinimo.Name = "LblCostoMinimo";
            LblCostoMinimo.Size = new Size(163, 20);
            LblCostoMinimo.TabIndex = 10;
            LblCostoMinimo.Text = "Solucion Costo Minimo";
            LblCostoMinimo.Click += LblCostoMinimo_Click;
            // 
            // Tbcostominimo
            // 
            Tbcostominimo.Location = new Point(12, 308);
            Tbcostominimo.Name = "Tbcostominimo";
            Tbcostominimo.Size = new Size(146, 27);
            Tbcostominimo.TabIndex = 11;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1010, 553);
            Controls.Add(Tbcostominimo);
            Controls.Add(LblCostoMinimo);
            Controls.Add(LblPruebas);
            Controls.Add(BtnSolucionar);
            Controls.Add(LblMetodos);
            Controls.Add(LblDestino);
            Controls.Add(NumDestino);
            Controls.Add(NumOrigen);
            Controls.Add(CbMetodos);
            Controls.Add(BtnCreadorMatriz);
            Controls.Add(DGVCostos);
            Controls.Add(LblOrigen);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Resolucionador";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)DGVCostos).EndInit();
            ((System.ComponentModel.ISupportInitialize)NumOrigen).EndInit();
            ((System.ComponentModel.ISupportInitialize)NumDestino).EndInit();
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
        private Label LblPruebas;
        private Label LblCostoMinimo;
        private TextBox Tbcostominimo;
    }
}
