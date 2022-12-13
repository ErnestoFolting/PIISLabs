using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    public static class ConsoleWriter
    {
        public static void matrOutput(List<List<double>> matr)
        {
            for(int i = 0; i < matr.Count; i++)
            {
                for(int j = 0; j < matr[i].Count; j++)
                {
                    Console.Write(matr[i][j] + " ");
                }
                Console.WriteLine();
            }
        }
        public static bool printAlgoData(List<List<double>> tops,int iteration)
        {
            Console.WriteLine("\nIteration {0}",iteration);
            List<double> funcValues = new List<double>();
            Console.WriteLine("Tops:");
            for(int i =0;i< tops.Count; i++)
            {
                funcValues.Add(NelderMeadAlgo.startFunction(tops[i]));
                Console.Write("( ");
                for(int j =0;j < tops[i].Count; j++)
                {
                    Console.Write("{0} ;",tops[i][j]);
                }
                Console.Write(" )\n");
            }
            double maxFuncValue = funcValues.Max();
            double minFuncValue = funcValues.Min();
            var maxIndex = funcValues.IndexOf(maxFuncValue);
            var minIndex = funcValues.IndexOf(minFuncValue);
            if (maxFuncValue == double.PositiveInfinity || minFuncValue == double.NegativeInfinity)
            {
                Console.WriteLine("Infinity.\n");
                return false;
            }
            else
            {
                Console.WriteLine("F max: {0} pointIndex: {1}",maxFuncValue,maxIndex);
                Console.WriteLine("F min: {0} pointIndex: {1}",minFuncValue,minIndex);
                return true;
            }
        }
    }
}
