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
        public int g = 0;
        public void evaluate(List<List<int>> maze, Point final)
        {
            g = (Math.Abs(final.i - i) + Math.Abs(final.j - j))*10;
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
            return (cellToCheck.i >= 0 && cellToCheck.i < Maze[0].Count && cellToCheck.j >= 0 && cellToCheck.j < Maze.Count);
        }

        public List<Point> findNear(Point currentPoint)
        {
            List<int> movesI = new List<int>() { 0, -1, -1, -1, 0, 1, 1, 1 };
            List<int> movesJ = new List<int>() { -1, -1, 0, 1, 1, 1, 0, -1 };
            List<Point> lst = new List<Point>();
            for (int i = 0; i < 8; i++)
            {
                Point temp = new Point();
                temp.previous = currentPoint;
                temp.i = currentPoint.i + movesI[i];
                temp.j = currentPoint.j + movesJ[i];
                temp.evaluate(Maze,final);
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
            start.evaluate(Maze,final);
            List<Point> opened = new List<Point>();
            opened.Add(start);

            while(opened.Count != 0)
            {
                Point current = opened.First(el => el.g == opened.Select(el => el.g).Min());
                if(current.i == final.i && current.j == final.j)
                {
                    Console.WriteLine("\nWe have found!!! The cost is:{0}",current.g);
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
                            if (p.g < pOpened.g)
                            {
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
            Console.WriteLine("The path A-Star:");
            Console.Write("[{0};{1}]", curr.i, curr.j);
            while (!(curr.i == start.i && curr.j == start.j))
            {
                Console.Write(" - [{0};{1}]", curr.previous.i, curr.previous.j);
                curr = curr.previous;
            }
        }
    }
}
