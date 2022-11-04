using System;
using System.Collections.Generic;

namespace Lab4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = @"D:\Education\PIIS\PIISLabs\Lab4\Lab4\text.txt";
            FileReader fileReader = new FileReader(path);
            Console.WriteLine("What substring you want to find? [From 6 to 8 symbols]");
            string? substr = Console.ReadLine();
            karpRabin karpRabinAlgo = new(fileReader.text,substr);
            karpRabinAlgo.algo();
        }
    }
}