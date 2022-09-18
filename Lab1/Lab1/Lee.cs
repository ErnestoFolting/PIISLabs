using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
class cell
{
    public int i;
    public int j;
    public int distance = 0;
    public cell previous;
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
            int i = currentCell.i;
            int j = currentCell.j;
            int distance = currentCell.distance + 1;
            cell temp1 = new cell();
            cell temp2 = new cell();
            cell temp3 = new cell();
            cell temp4 = new cell();
            temp1.distance = distance;
            temp2.distance = distance;
            temp3.distance = distance;
            temp4.distance = distance;
            temp1.previous = currentCell;
            temp2.previous = currentCell;
            temp3.previous = currentCell;
            temp4.previous = currentCell;
            temp1.i = i + 1;
            temp1.j = j;
            List<cell> list = new List<cell>();
            if (isCorrect(temp1) && Maze[temp1.i][temp1.j] == 1 && visited[temp1.i][temp1.j] == false)
            {
                
                list.Add(temp1);
            }
            temp2.i = i - 1;
            temp2.j = j;
            if (isCorrect(temp2) && Maze[temp2.i][temp2.j] == 1 && visited[temp2.i][temp2.j] == false)
            {
                list.Add(temp2);
            }
            temp3.i = i;
            temp3.j = j + 1;
            if (isCorrect(temp3) && Maze[temp3.i][temp3.j] == 1 && visited[temp3.i][temp3.j] == false)
            {
                list.Add(temp3);
            }
            temp4.i = i;
            temp4.j = j - 1;
            if (isCorrect(temp4) && Maze[temp4.i][temp4.j] == 1 && visited[temp4.i][temp4.j] == false)
            {
                list.Add(temp4);
            }
            return list;
        }
        public cell findFinalCell( cell start, cell final)
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
            Console.WriteLine("THE FINAL CELL IS NOT FOUND");
            return default;
        }
        public void buildPath(cell start, cell final)
        {
            cell curr = final;
            Console.WriteLine("The path:");
            Console.Write("[{0};{1}]",final.i,final.j);
            while(!(curr.i == start.i && curr.j == start.j)){
                Console.Write(" - [{0};{1}]", curr.previous.i, curr.previous.j);
                curr = curr.previous;
            }
        }
    }
}
