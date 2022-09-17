using System;
using System.Collections.Generic;

namespace Lab1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = @"D:\Education\PIIS\PIISLabs\Lab1\Lab1\Maze.txt";
            mazeReader check = new mazeReader(path);
        }
    }
}