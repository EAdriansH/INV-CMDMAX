using System;
using System.Drawing;
using System.Windows.Forms;

public class FormResultados : Form
{
    private DataGridView gridAsignaciones;
    private Label lblCostoZ;

    // El constructor recibe la matriz ya resuelta y el costo final
    public FormResultados(double[,] matriz, double costoZ, string metodoUsado)
    {
        // 1. Configurar la ventana principal
        this.Text = $"Resultados - {metodoUsado}";
        this.Size = new Size(600, 400);
        this.StartPosition = FormStartPosition.CenterParent; // Se centra sobre la ventana principal
        this.ShowIcon = false;

        // 2. Crear y configurar el título del Costo Total
        lblCostoZ = new Label();
        lblCostoZ.Text = $"Costo Total Óptimo (Z) = ${costoZ}";
        lblCostoZ.Font = new Font("Segoe UI", 16, FontStyle.Bold);
        lblCostoZ.BackColor = Color.FromArgb(31, 59, 115); // Azul oscuro (estilo TecNM)
        lblCostoZ.ForeColor = Color.White;
        lblCostoZ.Dock = DockStyle.Top;
        lblCostoZ.Height = 50;
        lblCostoZ.TextAlign = ContentAlignment.MiddleCenter;
        this.Controls.Add(lblCostoZ);

        // 3. Crear y configurar el DataGridView de resultados
        gridAsignaciones = new DataGridView();
        gridAsignaciones.Dock = DockStyle.Fill;
        gridAsignaciones.AllowUserToAddRows = false;
        gridAsignaciones.AllowUserToDeleteRows = false;
        gridAsignaciones.ReadOnly = true; // Solo lectura, el usuario no debe editar el resultado
        gridAsignaciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        gridAsignaciones.AllowUserToResizeRows = false;
        gridAsignaciones.RowHeadersWidth = 80;
        this.Controls.Add(gridAsignaciones);
        gridAsignaciones.BringToFront(); // Para que respete el espacio del Label

        // 4. Llenar la tabla con los datos de la matriz
        int filas = matriz.GetLength(0);
        int columnas = matriz.GetLength(1);

        // Generar Columnas (Destinos)
        for (int j = 0; j < columnas; j++)
        {
            gridAsignaciones.Columns.Add($"Destino{j + 1}", $"Destino {j + 1}");
        }

        // Generar Filas (Orígenes) e inyectar las asignaciones
        for (int i = 0; i < filas; i++)
        {
            gridAsignaciones.Rows.Add();
            gridAsignaciones.Rows[i].HeaderCell.Value = $"Origen {i + 1}";

            for (int j = 0; j < columnas; j++)
            {
                if (matriz[i, j] > 0)
                {
                    // Si hubo un envío, mostramos el número y pintamos la celda de verde
                    gridAsignaciones.Rows[i].Cells[j].Value = matriz[i, j];
                    gridAsignaciones.Rows[i].Cells[j].Style.BackColor = Color.LightGreen;
                    gridAsignaciones.Rows[i].Cells[j].Style.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                }
                else
                {
                    // Si no hubo envío, ponemos un guion para que se vea más limpio
                    gridAsignaciones.Rows[i].Cells[j].Value = "-";
                    gridAsignaciones.Rows[i].Cells[j].Style.ForeColor = Color.LightGray;
                }
            }
        }
    }
}