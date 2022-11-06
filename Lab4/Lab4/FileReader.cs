using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    public class FileReader
    {
        public string text = string.Empty;
        public List<List<int>> dijkstraMatrix = new();
        public List<List<int>> primaMatrix = new();
        public FileReader()
        {
        }
        public void getText(string path)
        {
            string[] lst = File.ReadAllText(path).Split(' ');
            for (int i = 0; i < lst.Length; i++)
            {
                text += lst[i];
                text += '0';
            }
            Console.WriteLine("Text: {0}", text);
        }
        public void getDijkstra(string path) {
            string[] lst = File.ReadAllText(path).Split(' ');
            int counter = 0;
            for (int i = 0; i < 8; i++)
            {
                List<int> temp = new List<int>();
                for (int j = 0; j < 8; j++)
                {

                    temp.Add(Convert.ToInt32(lst[counter++]));

                }
                dijkstraMatrix.Add(temp);
            }
        }
        public void getPrima(string path)
        {
            string[] lst = File.ReadAllText(path).Split(' ');
            int counter = 0;
            for (int i = 0; i < 8; i++)
            {
                List<int> temp = new List<int>();
                for (int j = 0; j < 8; j++)
                {

                    temp.Add(Convert.ToInt32(lst[counter++]));

                }
                primaMatrix.Add(temp);
            }
        }
    }
}
