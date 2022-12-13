using System;
using System.Collections.Generic;

namespace Lab6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<double> startPoint = new List<double> { 1, 1, 2 };
            NelderMeadAlgo.algo(startPoint);
        }
    }
}