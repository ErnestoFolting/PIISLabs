using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    
    public class karpRabin
    {
        private readonly string _text;
        private readonly string _substr;
        public karpRabin(string text, string substr)
        {
            _text = text;
            _substr = substr;
        }
        public void algo()
        {
            int hashToFind = getHash(_substr);
            Console.WriteLine("Hash to find: {0}",hashToFind);
            int index = 0;
            List<int> hashes = new();
            while (index <= _text.Length - _substr.Length)
            {
                string substrToCheck = _text.Substring(index, _substr.Length);
                hashes.Add(getHash(substrToCheck));
                index++;
            }
            Console.WriteLine("\nHashes");
            foreach (var el in hashes)
            {  
                Console.Write(el);
                Console.Write(" ");
            }
            bool found = false;
            for(int i =0;i< hashes.Count; i++)
            {
                if(hashes[i] == hashToFind)
                {
                    if(_text.Substring(i, _substr.Length) == _substr)
                    {
                        Console.WriteLine("\nFound the substring at position {0}",i);
                        found = true;
                    }
                }
            }
            if (!found) {
                Console.WriteLine("\nUnfortunately, we can not find the substring.");
            }
        }
        public int getHash(string substrToEvaluate)
        {
            return (Convert.ToInt32(substrToEvaluate) % 13);
        }
    }
}
