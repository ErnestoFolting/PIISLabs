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
            cell foundFinal = alg.findFinalCell( start,final);
            alg.buildPath(start,foundFinal);
            /*****************A-STAR ALGO******************/
            Point start2 = new Point();
            Point final2 = new Point();
            start2.i = start.i;
            start2.j = start.j;
            final2.i = final.i;
            final2.j = final.j;
            AStar astar = new AStar(Maze,final2);
            Point foundFinal2 = astar.findFinal(start2);
            astar.buildPath(start2,foundFinal2);
        }
    }
}