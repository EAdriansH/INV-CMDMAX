using System;
using System.Collections.Generic;
using System.Text;

namespace INV_CMDMAX.Metodos
{
    public class PasoAlgoritmo
    {
        public string Explicacion;

        // Las tres "fotografías" de los datos
        public string[,] MatrizPrincipal; 
        public double[] OfertaRestante;
        public double[] DemandaRestante;

        // Los registros de qué se ha ido anulando
        public List<int> FilasTachadas;
        public List<int> ColumnasTachadas;
    }
}