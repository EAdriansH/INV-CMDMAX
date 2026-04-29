using Microsoft.VisualBasic;

namespace INV_CMDMAX
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void BtnCreadorMatriz_Click(object sender, EventArgs e)
        {
            int origenes = (int)NumOrigen.Value;
            int destinos = (int)NumDestino.Value;
            DGVCostos.ColumnCount = destinos;
            DGVCostos.RowCount = origenes;

        }

        private void DGVOferta_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BtnSolucionar_Click(object sender, EventArgs e)
        {
            bool IgualdadOfertasDemandas = false;

            if ()
                if (CbMetodos.SelectedItem != null && CbMetodos.SelectedItem.ToString().Equals("Costo Minimo"))
                {
                    LblPruebas.Text = "Costo Minimo";

                }
                else if (CbMetodos.SelectedItem != null && CbMetodos.SelectedItem.ToString().Equals("Demaxmin"))
                {
                    LblPruebas.Text = "Demaxmin";
                }
                else
                {
                    MessageBox.Show("Selecciona una opcion valida pai");
                }

        }

        private void LblCostoMinimo_Click(object sender, EventArgs e)
        {

        }
    }
}
