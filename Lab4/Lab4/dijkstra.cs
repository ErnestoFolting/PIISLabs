using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    public class Point
    {
        public int number;
        public int distanceToStart;
    }
    public class dijkstra
    {
        private List<List<int>> _matrix = new();
        public dijkstra(List<List<int>> matrix)
        {
            _matrix = matrix;
        }
        public void algo()
        {
            int startPointNumber = startPointNumberFind();
            List<Point> generatedStartPoints = generateStartPoints(startPointNumber);
            while (generatedStartPoints.Count > 0)
            {
                Point currentPoint = generatedStartPoints.MinBy(el => el.distanceToStart);
                generatedStartPoints.Remove(currentPoint);
                if (currentPoint.distanceToStart == int.MaxValue || currentPoint.distanceToStart <0)
                {
                    Console.WriteLine("From {0} to {1} there is no path", startPointNumber, currentPoint.number);
                }
                else
                {
                    Console.WriteLine("From {0} to {1} distance is {2}.", startPointNumber, currentPoint.number, currentPoint.distanceToStart);
                }
                List<Point> nearPoints = findNear(currentPoint, generatedStartPoints);
                foreach(var el in nearPoints)
                {
                    if(el.distanceToStart > currentPoint.distanceToStart + _matrix[currentPoint.number][el.number])
                    {
                        el.distanceToStart = currentPoint.distanceToStart + _matrix[currentPoint.number][el.number];
                    }
                }
            }

        }
        private List<Point> findNear(Point current,List<Point> openPoints)
        {
            List<Point> near = new();
            for(int i = 0; i < 8; i++)
            {
                if(_matrix[current.number][i]!=0 && openPoints.FindIndex(p => p.number == i) != -1)
                {
                    near.Add(openPoints.FirstOrDefault(p => p.number == i));
                }
            }
            return near;
        }
        private List<Point> generateStartPoints(int startNumber)
        {
            List<Point> startPoints = new();
            for(int i = 0; i < 8; i++)
            {
                Point currPoint = new();
                currPoint.number = i;
                if(i == startNumber)
                {
                    currPoint.distanceToStart = 0;
                }
                else
                {
                    currPoint.distanceToStart = int.MaxValue;
                }
                startPoints.Add(currPoint);
            }
            return startPoints;
        } 
        private int startPointNumberFind() {
            int posMax = 0;
            int counterMax = 0;
            for(int i = 0; i < _matrix.Count; i++)
            {
                int currentCounter = 0;
                for(int j = 0;j< _matrix[i].Count; j++)
                {
                    if(_matrix[i][j] != 0)
                    {
                        currentCounter++;
                    }
                }
                if (currentCounter > counterMax)
                {
                    counterMax = currentCounter;
                    posMax = i;
                }
            }
            return posMax;
        }
    }
}
