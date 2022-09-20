using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    internal class consoleWriter
    {
        public void printMaze(List<List<int>> maze)
        {
            Console.WriteLine("\n\nOur Maze:");
            for (int i = 0; i < maze.Count; i++)
            {
                for (int j = 0; j < maze[i].Count; j++)
                {
                    if (maze[i][j] == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }else if(maze[i][j] == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    Console.Write(" {0}", maze[i][j]);
                }
                Console.WriteLine();
            }
            Console.ResetColor();
        }
    }
}
