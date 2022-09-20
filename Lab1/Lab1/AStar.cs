using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Point
    {
        public int i;
        public int j;
        public Point previous;
        public int h = 0;
        public int g = 0;
        public void evaluate(List<List<int>> maze, Point final)
        {
            h = (Math.Abs(final.i - i) + Math.Abs(final.j - j))*10;
        }
    }
    internal class AStar
    {
        public List<List<int>> Maze = new List<List<int>>();
        public List<List<bool>> visited = new List<List<bool>>();
        public Point final;
        public AStar(List<List<int>> maze, Point final)
        {
            Maze = maze;
            for (int i = 0; i < maze.Count; i++)
            {
                List<bool> temp = new List<bool>();
                for (int j = 0; j < maze[i].Count; j++)
                {
                    temp.Add(false);
                }
                visited.Add(temp);
            }
            this.final = final;
        }

        public bool isCorrect(Point cellToCheck)
        {
            return (cellToCheck.i >= 0 && cellToCheck.i < Maze.Count && cellToCheck.j >= 0 && cellToCheck.j < Maze[0].Count);
        }

        public List<Point> findNear(Point currentPoint)
        {
            List<int> movesI = new List<int>() { 0, -1, -1, -1, 0, 1, 1, 1 };
            List<int> movesJ = new List<int>() { -1, -1, 0, 1, 1, 1, 0, -1 };
            List<Point> lst = new List<Point>();
            for (int i = 0; i < movesI.Count; i++)
            {
                Point temp = new Point();
                temp.previous = currentPoint;
                temp.i = currentPoint.i + movesI[i];
                temp.j = currentPoint.j + movesJ[i];
                temp.evaluate(Maze,final);
                temp.g = temp.previous.g;
                temp.g += 10;
                if(isCorrect(temp) && Maze[temp.i][temp.j] == 1 && visited[temp.i][temp.j] == false)
                {
                    if(i%2 == 1)
                    {
                        temp.g += 4;
                    }
                    lst.Add(temp);
                }
            }
            return lst;
        }
        public Point findFinal(Point start)
        {
            if (!(isCorrect(start) && isCorrect(final) && Maze[start.i][start.j] == 1 && Maze[final.i][final.j] == 1))
            {
                throw new Exception("Incorrect coodinates!");
            }
            start.evaluate(Maze,final);
            List<Point> opened = new List<Point>();
            opened.Add(start);

            while(opened.Count != 0)
            {
                Point current = opened.First(el => el.h + el.g == opened.Select(el => el.h + el.g).Min());
                if(current.i == final.i && current.j == final.j)
                {
                    Console.WriteLine("\n\nWe have found!!! The cost is:{0}",current.h + current.g);
                    return current;
                }
                opened.Remove(current);
                visited[current.i][current.j] = true;
                List<Point> lst = findNear(current);
                foreach(Point p in lst)
                {
                    bool haveToAdd = true; 
                    foreach(Point pOpened in opened)
                    {
                        if(p.i == pOpened.i && p.j == pOpened.j)
                        {
                            haveToAdd = false;
                            if ((p.h + p.g) < (pOpened.h + pOpened.g))
                            {
                                pOpened.h = p.h;
                                pOpened.g = p.g;
                                pOpened.previous = p.previous;
                            }
                        }
                    }
                    if (haveToAdd)
                    {
                        opened.Add(p);
                    }
                }
            }
            return default;
        }
        public void buildPath(Point start,Point foundFinal)
        {
            Point curr = foundFinal;
            Console.WriteLine("\nThe path A-Star:");
            Console.Write("[{0};{1}]", curr.i, curr.j);
            Maze[final.i][final.j] = 2;
            while (!(curr.i == start.i && curr.j == start.j))
            {
                Console.Write(" - [{0};{1}]", curr.previous.i, curr.previous.j);
                Maze[curr.previous.i][curr.previous.j] = 2;
                curr = curr.previous;
            }
            consoleWriter writer = new consoleWriter();
            writer.printMaze(Maze);
        }
    }
}
