using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    public class SimplexAlgo
    {
        private List<List<double>> matrix;
        private List<double> baseVariables;
        public SimplexAlgo()
        {
            matrix = new List<List<double>>{
                new List<double> {
                    -2,0,4,0,0,-2,0
                },
                new List<double> {
                    -1,0,-3,-1,1,2,0
                },
                new List<double> {
                    0,1,0,5,0,-1,0
                },
                new List<double> {
                    2,0,-3,7,0,0,1
                }
            };
            baseVariables = new List<double> {
                4,1,6
            };
        }
        public void method()
        {
            ConsoleWriter.matrixOutput(matrix, baseVariables);
            while (findPivotIndexes().Item1 != -1)
            {
                var indexes = findPivotIndexes();
                matrix[indexes.Item1] = divideTheRow(matrix[indexes.Item1], matrix[indexes.Item1][indexes.Item2]);
                baseVariables[indexes.Item1 - 1] = indexes.Item2;
                ConsoleWriter.matrixOutput(matrix, baseVariables);
                rowOperations(indexes);
                ConsoleWriter.matrixOutput(matrix, baseVariables);
            }
            ConsoleWriter.resultOutput(matrix, baseVariables);
        }
        public Tuple<int,int> findPivotIndexes()
        {
            int pivotColumn = 1;
            double yMax = matrix[0][1]; 
            for(int i = 1; i < matrix[0].Count; i++)
            {
                if(matrix[0][i] > yMax)
                {
                    yMax = matrix[0][i];
                    pivotColumn = i;
                }
            }
            if(yMax <= 0)return Tuple.Create(-1,-1);
            bool infFlag = true;
            List<Tuple<double, int>> dividedValues = new();
            for(int i = 1; i < matrix.Count; i++)
            {
                if(matrix[i][pivotColumn] > 0)
                {
                    infFlag = false;
                    dividedValues.Add(Tuple.Create(Math.Round((matrix[i][0] / matrix[i][pivotColumn]), 2),i));
                }
            }
            if (infFlag) throw new ArgumentException("Функцiя не обмежена знизу.");
            int pivotRow = dividedValues.Find(el => el.Item1 == dividedValues.Min(el => el.Item1)).Item2;
            Tuple<int,int> indexes = Tuple.Create(pivotRow, pivotColumn);
            Console.WriteLine(indexes);
            return indexes;
        }
        public List<double> addRows(List<double> row1, List<double> row2, double coef)
        {
            if(row1.Count != row2.Count)
            {
                throw new ArgumentException("Incorrect operation.");
            }
            for(int i = 0; i < row1.Count; i++)
            {
                row1[i] += coef * row2[i];
            }
            return row1;
        }
        public List<double> divideTheRow(List<double> row1, double coef)
        {
            for(int i = 0; i < row1.Count; i++)
            {
                row1[i] = Math.Round(row1[i] / coef,2);
            }
            return row1;
        }
        public void rowOperations(Tuple<int,int> indexes)
        {
            for(int i = 0; i < matrix.Count; i++)
            {
                Console.WriteLine("check {0}", i);
                if (matrix[i][indexes.Item2] != 0 && i!= indexes.Item1)
                {
                    matrix[i] = addRows(matrix[i], matrix[indexes.Item1], (matrix[i][indexes.Item2]) * (-1));
                }
            }
        }
    }
}
