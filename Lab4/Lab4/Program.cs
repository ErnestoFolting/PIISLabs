using System;
using System.Collections.Generic;

namespace Lab4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string pathText = @"D:\Education\PIIS\PIISLabs\Lab4\Lab4\text.txt";
            string pathDijkstra = @"D:\Education\PIIS\PIISLabs\Lab4\Lab4\dijkstra.txt";
            string pathPrima = @"D:\Education\PIIS\PIISLabs\Lab4\Lab4\prima.txt";
            
            FileReader fileReader = new();
            fileReader.getText(pathText);
            fileReader.getDijkstra(pathDijkstra);
            fileReader.getPrima(pathPrima);

            //Karp - Rabin
            Console.WriteLine("What substring you want to find? [From 6 to 8 symbols]");
            string? substr = Console.ReadLine();
            karpRabin karpRabinAlgo = new(fileReader.text, substr);
            karpRabinAlgo.algo();
            Console.WriteLine();

            //Dijkstra
            dijkstra dijkstraAlgo = new(fileReader.dijkstraMatrix);
            dijkstraAlgo.algo();

            //Prima
            prima primaAlgo = new(fileReader.primaMatrix);
            primaAlgo.algo();
        }
    }
}