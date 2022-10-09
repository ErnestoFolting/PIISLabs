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
                //Read the Mazel
                string path = @"D:\Education\PIIS\PIISLabs\Lab2\Lab2\Maze.txt";
                mazeReader reader = new mazeReader(path);
                List<List<int>> Maze = reader.Maze;

                //Read the starting and finish points
                Console.WriteLine("\nInput start cell i:");
                int startI = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Input start cell j:");
                int startJ = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Input final cell i:");
                int finalI = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Input final cell j:");
                int finalJ = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Input enemy start cell i:");
                int enemyStartI = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Input enemy start cell j:");
                int enemyStartJ = Convert.ToInt32(Console.ReadLine());

                //Initialize the game
                Point start = new Point(startI, startJ);
                Point final = new Point(finalI, finalJ);
                Point enemyStart = new Point(enemyStartI, enemyStartJ);
                miniPacman game = new miniPacman(start, final, enemyStart,Maze);

                game.game();
            }
            catch(Exception ex) //Cathing exceptions
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
            
        }
    }
}