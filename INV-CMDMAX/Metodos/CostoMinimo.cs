using System;
using System.Collections.Generic;
using System.Linq; // Necesario para usar .Sum()
using System.Text;


namespace INV_CMDMAX.Metodos
{
    public class CostoMinimo
    {
        // 1. Actualizamos la firma para que devuelva la lista de pasos (pasosGenerados)
        public (double[,] asignaciones, double costoZ, List<PasoAlgoritmo> pasosGenerados) EjecutarCostoMinimo(double[] oferta, double[] demanda, double[,] costos)
        {
            int numFilas = oferta.Length;
            int numCols = demanda.Length;

            double[,] asignaciones = new double[numFilas, numCols];
            double costoTotalZ = 0;

            // Variables para la "Máquina del Tiempo"
            List<PasoAlgoritmo> listaDeFotos = new List<PasoAlgoritmo>();
            List<int> filasTachadas = new List<int>();
            List<int> columnasTachadas = new List<int>();

            // Clonamos los arreglos para no alterar los datos crudos
            double[] ofertaActiva = (double[])oferta.Clone();
            double[] demandaActiva = (double[])demanda.Clone();
            //--- FOTO INICIAL (Paso 0) ---
            PasoAlgoritmo fotoInicial = new PasoAlgoritmo();
            fotoInicial.Explicacion = "Estado inicial de la matriz. Presione 'Siguiente' para comenzar las asignaciones.";
            string[,] matrizInicial = new string[numFilas, numCols];
            for (int i = 0; i < numFilas; i++)
            {
                for (int j = 0; j < numCols; j++)
                {
                    matrizInicial[i, j] = $"${costos[i, j]}"; // Mostramos solo el costo original
                }
            }
            fotoInicial.MatrizPrincipal = matrizInicial;
            fotoInicial.OfertaRestante = (double[])ofertaActiva.Clone();
            fotoInicial.DemandaRestante = (double[])demandaActiva.Clone();
            fotoInicial.FilasTachadas = new List<int>();
            fotoInicial.ColumnasTachadas = new List<int>();
            listaDeFotos.Add(fotoInicial);
            // --------------------------------

            // El ciclo corre mientras siga existiendo demanda
            while (demandaActiva.Sum() > 0.0001)
            {
                double costoMinimoGlobal = double.MaxValue;
                int bestI = -1; // Coordenada Y (Fila/Origen)
                int bestJ = -1; // Coordenada X (Columna/Destino)

                // --- FASE 1: Exploración Global ---
                for (int i = 0; i < numFilas; i++)
                {
                    for (int j = 0; j < numCols; j++)
                    {
                        if (ofertaActiva[i] > 0 && demandaActiva[j] > 0)
                        {
                            if (costos[i, j] < costoMinimoGlobal)
                            {
                                costoMinimoGlobal = costos[i, j];
                                bestI = i;
                                bestJ = j;
                            }
                        }
                    }
                }

                // Failsafe: Evitar loops infinitos
                if (bestI == -1 || bestJ == -1) break;

                // --- FASE 2: Asignación ---
                double cantidadAsignar = Math.Min(ofertaActiva[bestI], demandaActiva[bestJ]);

                asignaciones[bestI, bestJ] = cantidadAsignar;
                costoTotalZ += (cantidadAsignar * costoMinimoGlobal);

                // --- FASE 3: Actualización ---
                ofertaActiva[bestI] -= cantidadAsignar;
                demandaActiva[bestJ] -= cantidadAsignar;

                // --- DETECCIÓN DE TACHADOS (Para la interfaz visual) ---
                if (ofertaActiva[bestI] < 0.0001 && !filasTachadas.Contains(bestI))
                {
                    filasTachadas.Add(bestI);
                }
                if (demandaActiva[bestJ] < 0.0001 && !columnasTachadas.Contains(bestJ))
                {
                    columnasTachadas.Add(bestJ);
                }

                // --- INICIO DE LA TOMA FOTOGRÁFICA ---
                PasoAlgoritmo foto = new PasoAlgoritmo();
                
                foto.Explicacion = $"Se asignaron {cantidadAsignar} unidades a costo ${costos[bestI, bestJ]}.";

                // CREAMOS LA MATRIZ VISUAL COMBINANDO ASIGNACIONES Y COSTOS ORIGINALES
                string[,] matrizVisual = new string[numFilas, numCols];
                for (int fila = 0; fila < numFilas; fila++)
                {
                    for (int col = 0; col < numCols; col++)
                    {
                        if (asignaciones[fila, col] > 0)
                        {
                            // Si hay mercancía asignada, mostramos formato Excel: "40 ($5)"
                            matrizVisual[fila, col] = $"{asignaciones[fila, col]} (${costos[fila, col]})";
                        }
                        else
                        {
                            // Si no hay nada asignado, mantenemos el costo original visible: "$5"
                            matrizVisual[fila, col] = $"${costos[fila, col]}";
                        }
                    }
                }

                foto.MatrizPrincipal = matrizVisual;
                foto.OfertaRestante = (double[])ofertaActiva.Clone();
                foto.DemandaRestante = (double[])demandaActiva.Clone();
                foto.FilasTachadas = new List<int>(filasTachadas);
                foto.ColumnasTachadas = new List<int>(columnasTachadas);

                listaDeFotos.Add(foto);
                // --- FIN DE LA TOMA FOTOGRÁFICA ---
            }

            // Retornamos todo al Form1
            return (asignaciones, costoTotalZ, listaDeFotos);
        }
    }
}