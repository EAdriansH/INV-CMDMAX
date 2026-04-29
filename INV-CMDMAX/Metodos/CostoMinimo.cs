using System;
using System.Collections.Generic;
using System.Text;

namespace INV_CMDMAX.Metodos
{
    internal class CostoMinimo
    {
    }
}
public class CostoMinimo
{
    public static double Calcular(double[,] matriz)
    {
        int filas = matriz.GetLength(0);
        int columnas = matriz.GetLength(1); 

        double minimo = double.MaxValue;

        for (int i = 0; i < filas; i++)
        {
            for (int j = 0; j < columnas; j++)
            {
                if (matriz[i, j] < minimo)
                {
                    minimo = matriz[i, j];
                }
            }
        }
        return minimo;
    }
}