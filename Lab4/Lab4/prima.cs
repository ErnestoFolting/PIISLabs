using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    public class prima
    {
        private List<List<int>> _matrix = new();
        private int distance = 0;
        private string tree = string.Empty;
        public prima(List<List<int>> matrix)
        {
            _matrix = matrix;
        }
        public void algo()
        {
            List<int> tops = new() { 0 };
            while(tops.Count != _matrix.Count)
            {
                var near = findNear(tops);
                if (near.Count == 0) {
                    Console.WriteLine("\nUnfortunately, we can not build a tree.\n");
                    return;
                }
                var nearestTop = near.MinBy(el => _matrix[el.Item1][el.Item2]);
                tree += Convert.ToString(nearestTop.Item1) + " - " + Convert.ToString(nearestTop.Item2) + ";\n";
                distance += _matrix[nearestTop.Item1][nearestTop.Item2];
                tops.Add(nearestTop.Item2);
            }
            Console.WriteLine("\nTree is \n{0}",tree);
            Console.WriteLine("Distance is {0}",distance);
        }
        public List<Tuple<int,int>> findNear(List<int> tops)
        {
            List<Tuple<int,int>> result = new();
            for(int i = 0; i < tops.Count; i++)
            {
                for (int j = 0; j < _matrix.Count; j++)
                {
                    if (_matrix[tops[i]][j] != 0 && !tops.Contains(j))
                    {
                        result.Add(Tuple.Create(tops[i], j));
                    }
                }
            }
            return result;
        }
    }
}
