using Microsoft.VisualBasic;

namespace INV_CMDMAX
{
    public partial class Form1 : Form
    {
        // Guardar resultados en variables de instancia para evitar borrados accidentales
        private double? resultadoCostoMinimo = null;
        private double? resultadoDemaxmin = null;

        public Form1()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }
        private void Form1_Load(object sender, EventArgs e) { }

        private void BtnCreadorMatriz_Click(object sender, EventArgs e)
        {
            int numOrigenes = (int)NumOrigen.Value;
            int numDestinos = (int)NumDestino.Value;

            // Configuramos el DGV central
            DGVCostos.ColumnCount = numDestinos;
            DGVCostos.RowCount = numOrigenes;

            // Asignamos nombres a los encabezados de las columnas de DGVCostos
            for (int j = 0; j < numDestinos; j++)
            {
                DGVCostos.Columns[j].HeaderText = $"Destino {j + 1}";
            }

            // Asignamos nombres a los encabezados de las filas de DGVCostos
            for (int i = 0; i < numOrigenes; i++)
            {
                DGVCostos.Rows[i].HeaderCell.Value = $"Origen {i + 1}";
            }

            // Configurar el DGV de demanda (fila especial)
            DGVDemanda.ColumnCount = numDestinos;
            DGVDemanda.RowCount = 1; // Solo una fila para la demanda
            DGVDemanda.Rows[0].HeaderCell.Value = "Demanda"; // Encabezado de la fila

            // Configurar el DGV de oferta (columna especial)
            DGVOferta.ColumnCount = 1; // Solo una columna para la oferta
            DGVOferta.RowCount = numOrigenes;
            DGVOferta.Columns[0].HeaderText = "Oferta"; // Encabezado de la columna

            // Ajustar tamaños de las celdas
            int cellWidth = 100; // Ancho fijo para las columnas
            int cellHeight = 40; // Altura fija para las filas
            SetFixedCellSizes(cellWidth, cellHeight);

            // Redimensionar los DGV
            ResizeDataGridView();

            // Limpiar resultados previos al crear una nueva matriz
            resultadoCostoMinimo = null;
            resultadoDemaxmin = null;
            Tbcostominimo.Text = string.Empty;
            TbDemaxmin.Text = string.Empty;
        }

        private void ResizeDataGridView()
        {
            int numOrigenes = (int)NumOrigen.Value;
            int numDestinos = (int)NumDestino.Value;

            int cellWidth = 100; // Ancho fijo para las columnas
            int cellHeight = 40; // Altura fija para las filas

            // Ajustar el tamaño del DGV central
            DGVCostos.Width = (numDestinos * cellWidth) + DGVCostos.RowHeadersWidth + 2;
            DGVCostos.Height = (numOrigenes * cellHeight) + DGVCostos.ColumnHeadersHeight + 2;

            // Ajustar posición y tamaño del DGV de la columna especial (derecha)
            DGVOferta.Left = DGVCostos.Right + 1; // Pegado al lado derecho del DGV central
            DGVOferta.Top = DGVCostos.Top; // Alineado con el DGV central
            DGVOferta.Height = DGVCostos.Height; // Igual altura que el DGV central
            DGVOferta.Width = cellWidth; // Ancho fijo para la columna de oferta

            // Ajustar posición y tamaño del DGV de la fila especial (abajo)
            DGVDemanda.Left = DGVCostos.Left; // Alineado con el DGV central
            DGVDemanda.Top = DGVCostos.Bottom + 1; // Pegado al lado inferior del DGV central
            DGVDemanda.Width = DGVCostos.Width; // Igual ancho que el DGV central
            DGVDemanda.Height = cellHeight; // Altura fija para la fila de demanda

            // Ajustar tamaños de las celdas
            SetFixedCellSizes(cellWidth, cellHeight);
        }

        private void SetFixedCellSizes(int cellWidth, int cellHeight)
        {
            // Configurar DGVCostos
            foreach (DataGridViewColumn column in DGVCostos.Columns)
            {
                column.Width = cellWidth;
            }
            foreach (DataGridViewRow row in DGVCostos.Rows)
            {
                row.Height = cellHeight;
            }

            // Configurar DGVDemanda
            foreach (DataGridViewColumn column in DGVDemanda.Columns)
            {
                column.Width = cellWidth;
            }
            DGVDemanda.Rows[0].Height = cellHeight;

            // Configurar DGVOferta
            DGVOferta.Columns[0].Width = cellWidth;
            foreach (DataGridViewRow row in DGVOferta.Rows)
            {
                row.Height = cellHeight;
            }
        }

        private void DGVOferta_CellContentClick(object sender, DataGridViewCellEventArgs e){}


        private void BtnSolucionar_Click(object sender, EventArgs e)
        {

            //ESTE BOTON SOLO DEBE HACER LO QUE DEBE


            // Fase 1: Extraer vectores de oferta y demanda
            ExtraerVectoresOfertaDemanda(out double[] oferta, out double[] demanda);

            // Fase 2: Mapear la matriz de costos unitarios
            double[,] matrizCostos = MapearMatrizCostos();

            // Fase 3: Validar balanceo
            if (ValidarBalanceo(oferta, demanda))
            {
                // Aquí puedes llamar al motor de resolución (Demaxmin o Costo Mínimo)
                MessageBox.Show("Iniciando el algoritmo de resolución...");
            }


        }
        private bool ValidarBalanceo(double[] oferta, double[] demanda)
        {
            // Sumar los valores de la oferta
            double sumaOferta = 0;
            foreach (var valor in oferta)
            {
                sumaOferta += valor;
            }

            // Sumar los valores de la demanda
            double sumaDemanda = 0;
            foreach (var valor in demanda)
            {
                sumaDemanda += valor;
            }

            // Validar si la matriz está balanceada
            if (sumaOferta == sumaDemanda)
            {
                MessageBox.Show("La matriz está balanceada. Puede proceder con la resolución.");
                return true;
            }
            else
            {
                MessageBox.Show("La matriz no está balanceada. Por favor, ajuste la oferta o la demanda.");
                return false;
            }
        }

        private double[,] MapearMatrizCostos()
        {
            int numOrigenes = (int)NumOrigen.Value;
            int numDestinos = (int)NumDestino.Value;

            // Inicializar la matriz de costos
            double[,] matrizCostos = new double[numOrigenes, numDestinos];

            // Recorrer las celdas interiores del DataGridView
            for (int i = 0; i < numOrigenes; i++)
            {
                for (int j = 0; j < numDestinos; j++)
                {
                    if (DGVCostos.Rows[i].Cells[j].Value != null && double.TryParse(DGVCostos.Rows[i].Cells[j].Value.ToString(), out double costo))
                    {
                        matrizCostos[i, j] = costo;
                    }
                    else
                    {
                        matrizCostos[i, j] = 0; // Valor predeterminado si la celda está vacía o no es válida
                    }
                }
            }

            return matrizCostos;
        }

        private void ExtraerVectoresOfertaDemanda(out double[] oferta, out double[] demanda)
        {
            int numOrigenes = (int)NumOrigen.Value;
            int numDestinos = (int)NumDestino.Value;

            // Inicializar los arreglos
            oferta = new double[numOrigenes];
            demanda = new double[numDestinos];

            // Extraer valores de la oferta (última columna de DGVOferta)
            for (int i = 0; i < numOrigenes; i++)
            {
                if (DGVOferta.Rows[i].Cells[0].Value != null && double.TryParse(DGVOferta.Rows[i].Cells[0].Value.ToString(), out double valorOferta))
                {
                    oferta[i] = valorOferta;
                }
                else
                {
                    oferta[i] = 0; // Valor predeterminado si la celda está vacía o no es válida
                }
            }

            // Extraer valores de la demanda (última fila de DGVDemanda)
            for (int j = 0; j < numDestinos; j++)
            {
                if (DGVDemanda.Rows[0].Cells[j].Value != null && double.TryParse(DGVDemanda.Rows[0].Cells[j].Value.ToString(), out double valorDemanda))
                {
                    demanda[j] = valorDemanda;
                }
                else
                {
                    demanda[j] = 0; // Valor predeterminado si la celda está vacía o no es válida
                }
            }
        }

        private void LblCostoMinimo_Click(object sender, EventArgs e)
        {

        }

        private void DGVOferta_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        

        private void BtnEjecutarCostoMinimo_Click(object sender, EventArgs e)
        {
            // Obtener datos desde la UI
            ExtraerVectoresOfertaDemanda(out double[] oferta, out double[] demanda);
            double[,] costos = MapearMatrizCostos();

            // Ejecutar algoritmo y mostrar en textbox
            var resultado = new CostoMinimo().EjecutarCostoMinimo(oferta, demanda, costos);
            Tbcostominimo.Text = resultado.costoZ.ToString();
        }

        private void BtnEjecutarDemaxmin_Click(object sender, EventArgs e)
        {
            // Obtener datos desde la UI
            ExtraerVectoresOfertaDemanda(out double[] oferta, out double[] demanda);
            double[,] costos = MapearMatrizCostos();

            // Ejecutar algoritmo y mostrar en textbox
            var resultado = new ResolvedorDemaXMin().EjecutarDemaxmin(oferta, demanda, costos);
            TbDemaxmin.Text = resultado.costoZ.ToString();
        }

        private void InitializeCustomComponents()
        {
            // Initialize ComboBox
            CbMetodos.Items.AddRange(new string[] { "Costo Minimo", "Demaxmin" });
            CbMetodos.SelectedIndexChanged += CmbMetodoSeleccion_SelectedIndexChanged;
        }

        private void CmbMetodoSeleccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Extract data
            ExtraerVectoresOfertaDemanda(out double[] oferta, out double[] demanda);
            double[,] costos = MapearMatrizCostos();

            // Execute selected method and show result in corresponding textbox
            if (CbMetodos.SelectedItem == null) return;
            string metodoSeleccionado = CbMetodos.SelectedItem.ToString();
            if (metodoSeleccionado == "Costo Minimo")
            {
                var res = new CostoMinimo().EjecutarCostoMinimo(oferta, demanda, costos);
                resultadoCostoMinimo = res.costoZ;
                Tbcostominimo.Text = resultadoCostoMinimo.ToString();
            }
            else if (metodoSeleccionado == "Demaxmin")
            {
                var res = new ResolvedorDemaXMin().EjecutarDemaxmin(oferta, demanda, costos);
                resultadoDemaxmin = res.costoZ;
                TbDemaxmin.Text = resultadoDemaxmin.ToString();
            }
        }

        private void CbMetodos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void LblDestino_Click(object sender, EventArgs e)
        {

        }
    }
}
