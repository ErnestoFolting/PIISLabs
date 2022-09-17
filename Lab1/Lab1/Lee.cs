using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
struct cell
{
    public int i;
    public int j;
    public int distance = 0;
}
namespace Lab1
{

    internal class Lee
    {
        public List<List<int>> Maze = new List<List<int>>(); 
        public List<List<bool>> visited = new List<List<bool>>();
        public Lee(List<List<int>> maze)
        {
            Maze = maze;
            for(int i = 0;i< maze.Count; i++)
            {
                List<bool> temp = new List<bool>();
                for (int j = 0; j < maze[i].Count; j++)
                {
                    temp.Add(false);
                }
                visited.Add(temp);
            }
        }
        public bool isCorrect(cell cellToCheck)
        {
            return (cellToCheck.i >= 0 && cellToCheck.i < Maze[0].Count && cellToCheck.j >= 0 && cellToCheck.j < Maze.Count);
        }
        public List<cell> nearFind(cell currentCell)
        {
            cell temp = new cell();
            temp.distance = ++currentCell.distance;
            temp.i = currentCell.i + 1;
            temp.j = currentCell.j;
            List<cell> list = new List<cell>();
            if (isCorrect(temp) && Maze[temp.i][temp.j] == 1 && visited[temp.i][temp.j] == false)
            {
                list.Add(temp);
            }
            temp.i = currentCell.i - 1;
            temp.j = currentCell.j;
            if (isCorrect(temp) && Maze[temp.i][temp.j] == 1 && visited[temp.i][temp.j] == false)
            {
                list.Add(temp);
            }
            temp.i = currentCell.i;
            temp.j = currentCell.j + 1;
            if (isCorrect(temp) && Maze[temp.i][temp.j] == 1 && visited[temp.i][temp.j] == false)
            {
                list.Add(temp);
            }
            temp.i = currentCell.i;
            temp.j = currentCell.j - 1;
            if (isCorrect(temp) && Maze[temp.i][temp.j] == 1 && visited[temp.i][temp.j] == false)
            {
                list.Add(temp);
            }
            return list;
        }
        public cell findFinalCell(cell start, cell final)
        {

            if (!(isCorrect(start) && isCorrect(final) && Maze[start.i][start.j] == 1 && Maze[final.i][final.j] == 1))
            {
                throw new Exception("Incorrect coodinates!");
            }
            visited[start.i][start.j] = true;
            Queue<cell> q = new Queue<cell>();
            q.Enqueue(start);
            while (q.Count != 0)
            {
                cell cur = q.Dequeue();
                if(cur.i == final.i && cur.j == final.j)
                {
                    Console.WriteLine("We found the final cell! Coords is: [{0},{1}]. Distance is: {2}",cur.i,cur.j,cur.distance);
                    return cur;
                }

                List<cell> list = nearFind(cur);
                foreach (cell el in list)
                {
                    q.Enqueue(el);
                    visited[el.i][el.j] = true;
                }                
            }
            Console.WriteLine("NOT FOUND");
            return default;
        }
    }
}
