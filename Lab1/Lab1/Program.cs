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

            Console.WriteLine("\nInput start cell i:");
            int startI = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Input start cell j:");
            int startJ = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Input final cell i:");
            int finalI = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Input final cell j:");
            int finalJ = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Choose the algo: Lee(0) A-Star(1)");
            int choose = Convert.ToInt32((Console.ReadLine()));
            if(choose == 0)
            {
                Lee alg = new Lee(Maze);
                cell start = new cell();
                cell final = new cell();
                start.i = startI;
                start.j = startJ;
                final.i = finalI;
                final.j = finalJ;
                cell foundFinal = alg.findFinalCell(start, final);
                alg.buildPath(start, foundFinal);
            }
            else
            {
                Point start2 = new Point();
                Point final2 = new Point();
                start2.i = startI;
                start2.j = startJ;
                final2.i = finalI;
                final2.j = finalJ;
                AStar astar = new AStar(Maze, final2);
                Point foundFinal2 = astar.findFinal(start2);
                astar.buildPath(start2, foundFinal2);
            } 
        }
    }
}