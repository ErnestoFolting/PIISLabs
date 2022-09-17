using System;
using System.Collections.Generic;

namespace Lab1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = @"D:\Education\PIIS\PIISLabs\Lab1\Lab1\Maze.txt";
            mazeReader reader = new mazeReader(path);
            List<List<int>> Maze = reader.Maze;
            Lee alg = new Lee(Maze);
            cell start = new cell();
            cell final = new cell();
            Console.WriteLine("\nInput start cell i:");
            start.i = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Input start cell j:");
            start.j = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Input final cell i:");
            final.i = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Input final cell j:");
            final.j = Convert.ToInt32(Console.ReadLine());
            alg.findFinalCell(start,final);
        }
    }
}