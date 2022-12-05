using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    public static class ConsoleWriter
    {
        public static void matrixOutput(List<List<double>> matr, List<double> baseVariables)
        {
            Console.WriteLine();
            Console.Write("Y0,B1 ".PadLeft(14));
            for(int i =0;i < matr[0].Count - 1;i++)
            {
                Console.Write(("X" + (i + 1)).PadLeft(8));
            }
            Console.WriteLine();
            for(int i = 0; i < matr.Count; i++)
            {
                if (i == 0)
                {
                    Console.Write("f(x) ");
                }
                else
                {
                    Console.Write("X" + baseVariables[i-1] + "   ");
                }
                for (int j = 0;j< matr[i].Count; j++)
                {
                    Console.Write(Convert.ToString((Math.Round(matr[i][j],2))).PadLeft(8));
                }
                Console.WriteLine();
            }
        }
        public static void resultOutput(List<List<double>> matrix, List<double> baseVariables)
        {
            Console.WriteLine("\nНайменше значення функцiї = {0}",Math.Round(matrix[0][0],2));
            Console.WriteLine("При таких значеннях x:");
            for(int i = 1;i<matrix.Count; i++)
            {
                Console.WriteLine("X{0} = {1}",baseVariables[i-1], Math.Round(matrix[i][0],2));
            }
            for(int i = 1; i < matrix[0].Count; i++)
            {
                if (!baseVariables.Contains(i))
                {
                    Console.WriteLine("X{0} = 0", i);
                }
            }
        }
    }
}
