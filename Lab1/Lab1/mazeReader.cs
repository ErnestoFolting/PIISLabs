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
            Console.Write("The size of Maze: {0}x{1} ", rows, columns);
            consoleWriter writer = new consoleWriter();
            writer.printMaze(Maze);
        }
    }
}
