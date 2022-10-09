using System;
using System.Collections.Generic;

namespace Lab2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string path = @"D:\Education\PIIS\PIISLabs\Lab2\Lab2\Maze.txt";
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
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
            
        }
    }
}