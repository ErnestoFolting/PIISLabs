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
        public FileReader(string path)
        {
            string[] lst = File.ReadAllText(path).Split(' ');
            for(int i =0; i < lst.Length; i++)
            {
                text += lst[i];
                text += '0';
            }
            Console.WriteLine("Text: {0}",text);
        }
    }
}
