using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
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
        public void printInGameMaze(List<List<int>> maze, Point currentPlayer, Point currentEnemy, Point playerFinal)
        {
            Console.Clear();
            for (int i = 0; i < maze.Count; i++)
            {
                for (int j = 0; j < maze[i].Count; j++)
                {
                    if( currentEnemy.i == i && currentEnemy.j == j)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(" O ");
                    }else if(currentPlayer.i == i && currentPlayer.j == j)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(" O ");
                    }
                    else if (playerFinal.i == i && playerFinal.j == j)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(" O ");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write(" * ");
                    }
                }
                Console.WriteLine();
            }
            Console.ResetColor();
        }
    }
}
