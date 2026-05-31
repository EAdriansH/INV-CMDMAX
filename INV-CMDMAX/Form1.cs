using System;
using System.Drawing;
using INV_CMDMAX.Metodos;
using Microsoft.VisualBasic;

namespace INV_CMDMAX
{

    public partial class Form1 : Form
    {
        List<PasoAlgoritmo> historialPasos = new List<PasoAlgoritmo>();
        int pasoActual = 0;
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

        private void DGVOferta_CellContentClick(object sender, DataGridViewCellEventArgs e) { }


        private void BtnSolucionar_Click(object sender, EventArgs e)
        {
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
                    if (DGVCostos.Rows[i].Cells[j].Value != null)
                    {
                        string textoCelda = DGVCostos.Rows[i].Cells[j].Value.ToString();

                        // LIMPIEZA VISUAL 
                        // Si la celda tiene formato visual (ej. "$5" o "40 ($5)"), extraemos solo el costo
                        if (textoCelda.Contains("$"))
                        {
                            int indiceDolar = textoCelda.IndexOf('$');
                            // Cortamos todo lo que esté antes del $, y le quitamos el paréntesis final si lo tiene
                            textoCelda = textoCelda.Substring(indiceDolar + 1).Replace(")", "").Trim();
                        }

                        if (double.TryParse(textoCelda, out double costo))
                        {
                            matrizCostos[i, j] = costo;
                        }
                        else
                        {
                            matrizCostos[i, j] = 0; // Valor predeterminado si falla
                        }
                    }
                    else
                    {
                        matrizCostos[i, j] = 0; // Valor predeterminado si la celda está vacía
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
            // 1. Extraer datos limpios de la interfaz
            ExtraerVectoresOfertaDemanda(out double[] oferta, out double[] demanda);
            double[,] costos = MapearMatrizCostos();

            if (CbMetodos.SelectedItem == null) return;
            string metodoSeleccionado = CbMetodos.SelectedItem.ToString();

            // 2. BORRADO TÁCTICO: Limpiar el historial visual actual
            historialPasos.Clear();
            pasoActual = 0;
            lblExplicacion.Text = "Calculando..."; 

            // 3. Ejecutar y guardar fotos según el método
            if (metodoSeleccionado == "Costo Minimo")
            {
               
                //Ahora el metodo devolvera el z y la lista de pasos :D
                var res = new CostoMinimo().EjecutarCostoMinimo(oferta, demanda, costos);
                resultadoCostoMinimo = res.costoZ;
                Tbcostominimo.Text = resultadoCostoMinimo.ToString();

                historialPasos = res.pasosGenerados; // <--- Enganchamos la máquina del tiempo
            }
            else if (metodoSeleccionado == "Demaxmin")
            {
                var res = new ResolvedorDemaXMin().EjecutarDemaxmin(oferta, demanda, costos);
                resultadoDemaxmin = res.costoZ;
                TbDemaxmin.Text = resultadoDemaxmin.ToString();

                historialPasos = res.pasosGenerados; // <--- Enganchamos la máquina del tiempo
            }

            // 4. Proyectar el primer paso automáticamente si todo salió bien
            if (historialPasos.Count > 0)
            {
                PintarPaso(0);
            }
        }

        private void CbMetodos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void LblDestino_Click(object sender, EventArgs e)
        {

        }



        private void BtnBalancear_Click_1(object sender, EventArgs e)
        {
            try
            {
               
                int numFilas = DGVCostos.RowCount;
                int numCols = DGVCostos.ColumnCount;

                double totalOferta = 0;
                double totalDemanda = 0;

                // Sumar oferta desde DGVOferta (columna única)
                for (int i = 0; i < DGVOferta.RowCount; i++)
                {
                    totalOferta += Convert.ToDouble(DGVOferta.Rows[i].Cells[0].Value ?? 0);
                }

                // Sumar demanda desde DGVDemanda (fila única)
                for (int j = 0; j < DGVDemanda.ColumnCount; j++)
                {
                    totalDemanda += Convert.ToDouble(DGVDemanda.Rows[0].Cells[j].Value ?? 0);
                }

                if (Math.Abs(totalOferta - totalDemanda) < 0.0001)
                {
                    MessageBox.Show("La matriz ya está balanceada.", "Balanceo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (totalOferta < totalDemanda)
                {
                    // Falta oferta: agregar un origen ficticio (fila) con costos 0 y oferta = diferencia
                    double diferencia = totalDemanda - totalOferta;

                    // Agregar fila en DGVCostos al final
                    DGVCostos.RowCount = DGVCostos.RowCount + 1;
                    int nuevaFila = DGVCostos.RowCount - 1;
                    DGVCostos.Rows[nuevaFila].HeaderCell.Value = "Origen Ficticio";

                    // Poner costos a 0 y marcar color
                    for (int j = 0; j < DGVCostos.ColumnCount; j++)
                    {
                        DGVCostos.Rows[nuevaFila].Cells[j].Value = 0;
                        DGVCostos.Rows[nuevaFila].Cells[j].Style.BackColor = Color.LightGray;
                    }

                    // Agregar la oferta en DGVOferta (añadir nueva fila)
                    DGVOferta.RowCount = DGVOferta.RowCount + 1;
                    DGVOferta.Rows[nuevaFila].Cells[0].Value = diferencia;
                    // Actualizar nombre en encabezado si están visibles
                    try { DGVOferta.Rows[nuevaFila].HeaderCell.Value = "O. Ficticio"; } catch { }

                    // Actualizar contador de orígenes y redimensionar vistas
                    if (NumOrigen.Value == NumOrigen.Maximum) NumOrigen.Maximum = NumOrigen.Maximum + 1;
                    NumOrigen.Value = NumOrigen.Value + 1;
                    ResizeDataGridView();

                    MessageBox.Show($"Se agregó un Origen Ficticio con oferta {diferencia}.", "Balanceo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else // totalOferta > totalDemanda
                {
                    // Falta demanda: agregar un destino ficticio (columna) con costos 0 y demanda = diferencia
                    double diferencia = totalOferta - totalDemanda;

                    // Agregar columna en DGVCostos
                    int nuevaCol = DGVCostos.ColumnCount;
                    DGVCostos.ColumnCount = DGVCostos.ColumnCount + 1;
                    DGVCostos.Columns[nuevaCol].HeaderText = "D. Ficticio";

                    // Poner costos a 0 y marcar color
                    for (int i = 0; i < DGVCostos.RowCount; i++)
                    {
                        DGVCostos.Rows[i].Cells[nuevaCol].Value = 0;
                        DGVCostos.Rows[i].Cells[nuevaCol].Style.BackColor = Color.LightGray;
                    }

                    // Agregar la demanda en DGVDemanda (añadir columna)
                    DGVDemanda.ColumnCount = DGVDemanda.ColumnCount + 1;
                    DGVDemanda.Rows[0].Cells[nuevaCol].Value = diferencia;

                    // Actualizar contador de destinos y redimensionar vistas
                    if (NumDestino.Value == NumDestino.Maximum) NumDestino.Maximum = NumDestino.Maximum + 1;
                    NumDestino.Value = NumDestino.Value + 1;
                    ResizeDataGridView();

                    MessageBox.Show($"Se agregó un Destino Ficticio con demanda {diferencia}.", "Balanceo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al balancear: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSiguiente_Click(object sender, EventArgs e)
        {
            if (pasoActual < historialPasos.Count - 1)
            {
                pasoActual++;
                PintarPaso(pasoActual);
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (pasoActual > 0)
            {
                pasoActual--;
                PintarPaso(pasoActual);
            }
        }



        private void PintarPaso(int indicePaso)
        {
            if (historialPasos == null || historialPasos.Count == 0) return;

            PasoAlgoritmo paso = historialPasos[indicePaso];
            lblExplicacion.Text = paso.Explicacion;

            int numFilas = paso.MatrizPrincipal.GetLength(0);
            int numColumnas = paso.MatrizPrincipal.GetLength(1);

            // 1. LIMPIAR COLORES Y LLENAR DGVCostos
            for (int i = 0; i < numFilas; i++)
            {
                for (int j = 0; j < numColumnas; j++)
                {
                    DGVCostos.Rows[i].Cells[j].Style.BackColor = Color.White;
                    DGVCostos.Rows[i].Cells[j].Style.ForeColor = Color.Black;
                    DGVCostos.Rows[i].Cells[j].Value = paso.MatrizPrincipal[i, j];
                }
            }

            // 2. LIMPIAR COLORES Y LLENAR DGVOferta (Asumiendo que es de 1 sola columna)
            for (int i = 0; i < numFilas; i++)
            {
                DGVOferta.Rows[i].Cells[0].Style.BackColor = Color.LightBlue; 
                DGVOferta.Rows[i].Cells[0].Style.ForeColor = Color.Black;
                DGVOferta.Rows[i].Cells[0].Value = paso.OfertaRestante[i];
            }

            // 3. LIMPIAR COLORES Y LLENAR DGVDemanda (Asumiendo que es de 1 sola fila)
            for (int j = 0; j < numColumnas; j++)
            {
                DGVDemanda.Rows[0].Cells[j].Style.BackColor = Color.LightGreen; 
                DGVDemanda.Rows[0].Cells[j].Style.ForeColor = Color.Black;
                DGVDemanda.Rows[0].Cells[j].Value = paso.DemandaRestante[j];
            }

            // 4. EFECTO VISUAL: Tachar Filas (En DGVCostos y DGVOferta)
            if (paso.FilasTachadas != null)
            {
                foreach (int f in paso.FilasTachadas)
                {
                    // Tachar la fila entera en la matriz central
                    for (int j = 0; j < numColumnas; j++)
                    {
                        DGVCostos.Rows[f].Cells[j].Style.BackColor = Color.LightGray;
                        DGVCostos.Rows[f].Cells[j].Style.ForeColor = Color.DarkGray;
                    }
                    // Tachar la celda de oferta de ese Origen
                    DGVOferta.Rows[f].Cells[0].Style.BackColor = Color.LightGray;
                    DGVOferta.Rows[f].Cells[0].Style.ForeColor = Color.DarkGray;
                }
            }

            // 5. EFECTO VISUAL: Tachar Columnas (En DGVCostos y DGVDemanda)
            if (paso.ColumnasTachadas != null)
            {
                foreach (int c in paso.ColumnasTachadas)
                {
                    // Tachar la columna entera en la matriz central
                    for (int i = 0; i < numFilas; i++)
                    {
                        DGVCostos.Rows[i].Cells[c].Style.BackColor = Color.LightGray;
                        DGVCostos.Rows[i].Cells[c].Style.ForeColor = Color.DarkGray;
                    }
                    // Tachar la celda de demanda de ese Destino
                    DGVDemanda.Rows[0].Cells[c].Style.BackColor = Color.LightGray;
                    DGVDemanda.Rows[0].Cells[c].Style.ForeColor = Color.DarkGray;
                }
            }
        }
    }
}
