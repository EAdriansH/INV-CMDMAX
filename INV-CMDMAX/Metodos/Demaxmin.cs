using System;
using System.Collections.Generic;
using System.Text;

namespace INV_CMDMAX.Metodos
{
    internal class Demaxmin
    {
    }
}

public class ResolvedorDemaXMin
{
    // Retorna una tupla con la Matriz de Asignaciones y el Costo Total Z
    public (double[,] asignaciones, double costoZ) EjecutarDemaxmin(double[] oferta, double[] demanda, double[,] costos)
    {
        int numFilas = oferta.Length;
        int numCols = demanda.Length;

        double[,] asignaciones = new double[numFilas, numCols];
        double costoTotalZ = 0;

        // Clonamos los arreglos para no destruir los datos originales de la interfaz
        double[] ofertaActiva = (double[])oferta.Clone();
        double[] demandaActiva = (double[])demanda.Clone();

        // El ciclo corre mientras siga existiendo demanda por satisfacer
        // (Usamos 0.0001 en vez de 0 para evitar bugs de precisión de decimales en double)
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
                    // 1. Escaneamos cuál es el flete más barato disponible en la columna 'j'
                    double minCostoColumnaActual = double.MaxValue;
                    for (int i = 0; i < numFilas; i++)
                    {
                        if (ofertaActiva[i] > 0 && costos[i, j] < minCostoColumnaActual)
                        {
                            minCostoColumnaActual = costos[i, j];
                        }
                    }

                    // 2. Evaluamos si es el nuevo Rey de la Demanda o si hay empate
                    if (demandaActiva[j] > maxDemanda)
                    {
                        maxDemanda = demandaActiva[j];
                        maxJ = j;
                        minCostoMejorColumna = minCostoColumnaActual;
                    }
                    else if (demandaActiva[j] == maxDemanda && maxDemanda > 0)
                    {
                        // ¡EMPATE! Aplicamos regla del paper: El que tenga el flete más barato gana
                        if (minCostoColumnaActual < minCostoMejorColumna)
                        {
                            maxJ = j;
                            minCostoMejorColumna = minCostoColumnaActual;
                        }
                    }
                }
            }

            // Failsafe: Si por alguna razón no encontró columna (ej. matriz mal balanceada), rompemos el ciclo
            if (maxJ == -1) break;

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

            if (bestI == -1) break; // Failsafe por si no hay oferta disponible

            // --- FASE 3: Asignación y Actualización de Inventarios ---
            double cantidadAsignar = Math.Min(ofertaActiva[bestI], demandaActiva[maxJ]);

            asignaciones[bestI, maxJ] = cantidadAsignar;
            costoTotalZ += (cantidadAsignar * costoMinimoEnGanadora);

            // Tachamos / Restamos inventarios
            ofertaActiva[bestI] -= cantidadAsignar;
            demandaActiva[maxJ] -= cantidadAsignar;
        }

        return (asignaciones, costoTotalZ);
    }
}
