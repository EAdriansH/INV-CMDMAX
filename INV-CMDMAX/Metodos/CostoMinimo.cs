using System;
using System.Collections.Generic;
using System.Text;


public class CostoMinimo
{
    public (double[,] asignaciones, double costoZ) EjecutarCostoMinimo(double[] oferta, double[] demanda, double[,] costos)
    {
        int numFilas = oferta.Length;
        int numCols = demanda.Length;

        double[,] asignaciones = new double[numFilas, numCols];
        double costoTotalZ = 0;

        // Clonamos los arreglos para no alterar los datos de la interfaz visual
        double[] ofertaActiva = (double[])oferta.Clone();
        double[] demandaActiva = (double[])demanda.Clone();

        // El ciclo corre mientras siga existiendo demanda
        while (demandaActiva.Sum() > 0.0001)
        {
            double costoMinimoGlobal = double.MaxValue;
            int bestI = -1; // Coordenada Y (Fila/Origen)
            int bestJ = -1; // Coordenada X (Columna/Destino)

            // --- FASE 1: Exploración Global ---
            // Escaneamos TODA la tabla en busca del costo más bajo absoluto
            // que tenga tanto oferta como demanda disponible.
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

            // Failsafe: Si no encontró nada (ej. si el usuario ingresó una matriz desbalanceada 
            // y se saltó la validación), rompemos el ciclo para evitar un loop infinito.
            if (bestI == -1 || bestJ == -1) break;

            // --- FASE 2: Asignación ---
            // Le damos todo lo que podamos sin pasarnos de lo que piden ni de lo que tenemos
            double cantidadAsignar = Math.Min(ofertaActiva[bestI], demandaActiva[bestJ]);

            asignaciones[bestI, bestJ] = cantidadAsignar;
            costoTotalZ += (cantidadAsignar * costoMinimoGlobal);

            // --- FASE 3: Actualización ---
            // Restamos lo enviado de los inventarios (esto "tacha" virtualmente la fila o columna cuando llega a 0)
            ofertaActiva[bestI] -= cantidadAsignar;
            demandaActiva[bestJ] -= cantidadAsignar;
        }

        return (asignaciones, costoTotalZ);
    }
}