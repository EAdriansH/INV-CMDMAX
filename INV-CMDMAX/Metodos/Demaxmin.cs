using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INV_CMDMAX.Metodos
{
    public class ResolvedorDemaXMin
    {
        // 1. Actualizamos la firma para retornar la lista de pasos
        public (double[,] asignaciones, double costoZ, List<PasoAlgoritmo> pasosGenerados) EjecutarDemaxmin(double[] oferta, double[] demanda, double[,] costos)
        {
            int numFilas = oferta.Length;
            int numCols = demanda.Length;

            double[,] asignaciones = new double[numFilas, numCols];
            double costoTotalZ = 0;

            // Variables para la "Máquina del Tiempo"
            List<PasoAlgoritmo> listaDeFotos = new List<PasoAlgoritmo>();
            List<int> filasTachadas = new List<int>();
            List<int> columnasTachadas = new List<int>();

            // Clonamos los arreglos para no destruir los datos originales
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

            // El ciclo corre mientras siga existiendo demanda por satisfacer
            while (demandaActiva.Sum() > 0.0001)
            {
                double maxDemanda = -1;
                int maxJ = -1; // Índice de la columna ganadora
                double minCostoMejorColumna = double.MaxValue;

                // --- FASE 1: Buscar la Demanda Máxima y aplicar regla de desempate ---
                for (int j = 0; j < numCols; j++)
                {
                    if (demandaActiva[j] > 0)
                    {
                        double minCostoColumnaActual = double.MaxValue;
                        for (int i = 0; i < numFilas; i++)
                        {
                            if (ofertaActiva[i] > 0 && costos[i, j] < minCostoColumnaActual)
                            {
                                minCostoColumnaActual = costos[i, j];
                            }
                        }

                        if (demandaActiva[j] > maxDemanda)
                        {
                            maxDemanda = demandaActiva[j];
                            maxJ = j;
                            minCostoMejorColumna = minCostoColumnaActual;
                        }
                        else if (demandaActiva[j] == maxDemanda && maxDemanda > 0)
                        {
                            // ¡EMPATE! Aplicamos regla del paper: El flete más barato gana
                            if (minCostoColumnaActual < minCostoMejorColumna)
                            {
                                maxJ = j;
                                minCostoMejorColumna = minCostoColumnaActual;
                            }
                        }
                    }
                }

                if (maxJ == -1) break; // Failsafe

                // --- FASE 2: Buscar la planta (fila) más barata dentro de la columna ganadora ---
                int bestI = -1;
                double costoMinimoEnGanadora = double.MaxValue;

                for (int i = 0; i < numFilas; i++)
                {
                    if (ofertaActiva[i] > 0 && costos[i, maxJ] < costoMinimoEnGanadora)
                    {
                        costoMinimoEnGanadora = costos[i, maxJ];
                        bestI = i;
                    }
                }

                if (bestI == -1) break; // Failsafe

                // --- FASE 3: Asignación y Actualización de Inventarios ---
                double cantidadAsignar = Math.Min(ofertaActiva[bestI], demandaActiva[maxJ]);

                asignaciones[bestI, maxJ] = cantidadAsignar;
                costoTotalZ += (cantidadAsignar * costoMinimoEnGanadora);

                ofertaActiva[bestI] -= cantidadAsignar;
                demandaActiva[maxJ] -= cantidadAsignar;

                // --- DETECCIÓN DE TACHADOS (Para la interfaz visual) ---
                if (ofertaActiva[bestI] < 0.0001 && !filasTachadas.Contains(bestI))
                {
                    filasTachadas.Add(bestI);
                }
                if (demandaActiva[maxJ] < 0.0001 && !columnasTachadas.Contains(maxJ))
                {
                    columnasTachadas.Add(maxJ);
                }

                // --- INICIO DE LA TOMA FOTOGRÁFICA ---
                PasoAlgoritmo foto = new PasoAlgoritmo();
               
                foto.Explicacion = $"Demanda Máx en Destino {maxJ + 1} ({maxDemanda} u). Se asignaron {cantidadAsignar} unidades desde Origen {bestI + 1} a costo ${costos[bestI, maxJ]}.";

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

            return (asignaciones, costoTotalZ, listaDeFotos);
        }
    }
}