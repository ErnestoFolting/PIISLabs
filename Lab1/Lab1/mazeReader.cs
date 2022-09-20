using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    internal class mazeReader
    {
        public List<List<int>> Maze = new List<List<int>>();
        public mazeReader(string path)
        {
            string[] lst = File.ReadAllText(path).Split(' ');
            int counter = 2;
            int rows = Convert.ToInt32(lst[0]);
            int columns = Convert.ToInt32(lst[1]);
            for (int i = 0; i < rows; i++)
            {
                List<int> temp = new List<int>();
                for (int j = 0; j < columns; j++)
                {
                    
                    temp.Add(Convert.ToInt32(lst[counter++]));
                    
                }
                Maze.Add(temp);
            }
            Console.WriteLine("The size of Maze: {0}x{1} \n", rows, columns);
            Console.WriteLine("Our Maze:");
            for(int i = 0; i < 10; i++)
            {
                for(int j = 0; j < 10; j++)
                {
                    if(Maze[i][j] == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                    }
                    Console.Write(" {0}",Maze[i][j]);
                }
                Console.WriteLine() ;
            }
            Console.ResetColor();
        }
    }
}
