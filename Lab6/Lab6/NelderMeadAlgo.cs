using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    public static class NelderMeadAlgo
    {
        private const double a = 1;
        private const double yLower = 2;
        private const double yUpper = 3;
        private const double yAVG = (yUpper + yLower) / 2;
        private const double bLower = 0.4;
        private const double bUpper = 0.6;
        private const double bAVG = (bUpper + bLower) / 2;
        private const double distanceBetweenPoints = 1;
        private const double precision = 0.01;
        private const int iterations = 300;
        private const double n = 3;
        public static double startFunction(List<double> xs)
        {
            return -5 * xs[0] * Math.Pow(xs[1], 2) * xs[2] + 2 *
                Math.Pow(xs[0], 2) * xs[1] - 3 * xs[0] * Math.Pow(xs[1], 4) +
                xs[0] * Math.Pow(xs[2], 2);
        }
        public static void algo(List<double> startPoint)
        {
            List<List<double>> tops = new List<List<double>>() { startPoint };
            for (int i = 1; i < n + 1; i++)
            {
                List<double> temp = new();
                for (int j = 0; j < n; j++)
                {
                    if (j == i - 1)
                    {
                        temp.Add(tops[0][j] + D1());
                    }
                    else
                    {
                        temp.Add(tops[0][j] +D2());
                    }
                }
                tops.Add(temp);
            }
            Console.WriteLine("Start points:");
            ConsoleWriter.matrOutput(tops);
            for (int iteration = 0; iteration < iterations; iteration++)
            {
                if (!ConsoleWriter.printAlgoData(tops, iteration))
                {
                    return;
                }
                List<double> funcValues = new();
                for (int i = 0; i < tops.Count; i++)
                {
                    funcValues.Add(startFunction(tops[i]));
                }
                List<double> sorted = new();
                foreach(var el in funcValues)
                {
                    sorted.Add(el);
                }
                sorted.Sort();
                sorted.Reverse();
                double highValue = sorted[0];
                double nextHighValue = sorted[1];
                double lowValue = sorted[Convert.ToInt32(n)];
                int highIndex = funcValues.IndexOf(highValue);
                int nextHighIndex = funcValues.IndexOf(nextHighValue);
                int lowIndex = funcValues.IndexOf(lowValue);

                List<double> centerOfWeight = new() {0,0,0};
                for(int i = 0; i < tops.Count; i++)
                {
                    if(i != highIndex)
                    {
                        centerOfWeight = listsOperation(centerOfWeight, tops[i], "+");
                    }
                }
                    
                for (int i = 0; i < centerOfWeight.Count; i++) centerOfWeight[i] /= n;

                if (Math.Sqrt(funcValues.Select(val => Math.Pow(val - startFunction(centerOfWeight), 2)).Sum() / (n + 1)) <= precision)
                {
                    break;
                }

                List<double> reflectedTop = listsOperation(centerOfWeight, listAndNumberOperation(listsOperation(centerOfWeight, tops[highIndex], "-"), a, "*"),"+");

                if(startFunction(reflectedTop) <= nextHighValue && startFunction(reflectedTop) >= lowValue)
                {
                    tops[highIndex] = reflectedTop;
                    continue;
                }
                if(startFunction(reflectedTop) <= lowValue)
                {
                    var stretchedTop = listsOperation(centerOfWeight, listAndNumberOperation(listsOperation(reflectedTop, centerOfWeight, "-"), yAVG, "*"), "+");
                    if(startFunction(stretchedTop) <= lowValue)
                    {
                        tops[highIndex] = stretchedTop;
                    }
                    else
                    {
                        tops[highIndex] = reflectedTop;
                    }
                    continue;
                }
                if(startFunction(reflectedTop) <= highValue)
                {
                    List<double> contractedTop = listsOperation(centerOfWeight, listAndNumberOperation(listsOperation(reflectedTop, centerOfWeight, "-"), bAVG, "*"), "+");
                    if(startFunction(contractedTop) <= startFunction(reflectedTop))
                    {
                        tops[highIndex] = contractedTop;
                        continue;
                    }
                }
                else
                {
                    List<double> contractedTop = listsOperation(centerOfWeight, listAndNumberOperation(listsOperation(tops[highIndex], centerOfWeight, "-"), bAVG, "*"), "+");
                    if(startFunction(contractedTop) <= highValue)
                    {
                        tops[highIndex] = contractedTop;
                        continue;
                    }
                }
                List<double> bestTop = tops[lowIndex];
                for(int i = 0; i < tops.Count; i++)
                {
                    if(i == lowIndex) continue;

                    tops[i] = listsOperation(bestTop, listAndNumberOperation(listsOperation(tops[i], bestTop, "-"), 0.5, "*"), "+");
                }
            }
        }
        private static double D1()
        {
            return distanceBetweenPoints / (n * Math.Sqrt(2)) * (Math.Sqrt(n + 1) + n - 1);
        }
        private static double D2()
        {
            return distanceBetweenPoints / (n * Math.Sqrt(2)) * (Math.Sqrt(n + 1) - 1);
        }
        private static List<double> listsOperation(List<double> lst1,List<double> lst2,string sign)
        {
            List<double> result = new List<double>();
            for (int i = 0; i < lst1.Count; i++)
            {
                if (sign == "+")
                {
                    result.Add(lst1[i] + lst2[i]); 
                }
                else
                {
                    result.Add(lst1[i] - lst2[i]);
                }
            }
            return result;
        }
        private static List<double> listAndNumberOperation(List<double> lst,double number,string sign)
        {
            List<double> result = new List<double>();
            for (int i =0;i< lst.Count; i++)
            {
                if (sign == "*")
                {
                    result.Add(lst[i] * number);
                }
                else
                {
                    result.Add(lst[i] / number);
                }
            }
            return result;
        }
    }
}
