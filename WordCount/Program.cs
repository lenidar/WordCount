using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordCount
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, int> breakdown = new Dictionary<string, int>();
            Dictionary<string, int> sortedBD = new Dictionary<string, int>();
            int wordCount = 0;
            List<string> lines = new List<string>();
            string[] words = new string[] { };

            int leastCount = 0;
            string sortKey = "";

            char[] letters = { };

            //string fileName = "Jupiter.txt";
            string fileName = "Odin.txt";

            string outputFile = "";

            // create output file name
            outputFile = fileName.Split('.')[0] + "_WordCount.txt";

            Console.Write("Begin Reading...");

            string line = "";
            using (StreamReader sr = new StreamReader(fileName))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    if(line.Length > 0)
                        lines.Add(line);
                }
            }

            Console.WriteLine("Done!");
            Console.Write("Begin Counting...");

            foreach (string l in lines)
            {
                words = l.Split(' ');
                for(int x = 0; x < words.Length; x++)
                //foreach(string word in words)
                {
                    if (words[x].Length > 0)
                    {
                        // special character filter
                        if (words[x].Length > 1)
                        {
                            letters = words[x].ToCharArray();
                            if ((int)letters[letters.Length - 1] == 33 // !
                                || (int)letters[letters.Length - 1] == 44 // ,
                                || (int)letters[letters.Length - 1] == 46 // .
                                || (int)letters[letters.Length - 1] == 63 // ?
                                )
                            {
                                words[x] = "";
                                for (int y = 0; y < letters.Length - 1; y++)
                                {
                                    words[x] += letters[y];
                                }
                            }
                        }


                        if (breakdown.ContainsKey(words[x].ToLower()))
                            breakdown[words[x].ToLower()] += 1;
                        else
                            breakdown[words[x].ToLower()] = 1;

                        wordCount++;
                    }
                }
            }

            Console.WriteLine("Done!");
            Console.Write("Begin Sorting...");

            while (breakdown.Count > 0)
            {
                leastCount = 0;

                foreach(KeyValuePair<string, int> kvp in breakdown)
                {
                    if(leastCount < kvp.Value)
                    {
                        leastCount = kvp.Value;
                        sortKey = kvp.Key;
                    }
                }

                sortedBD[sortKey] = leastCount;
                breakdown.Remove(sortKey);
            }
            Console.WriteLine("Done!");
            Console.Write("Begin Writing...");

            using (StreamWriter sw = new StreamWriter(outputFile))
            {
                sw.WriteLine("Word count of {0}.", fileName);
                sw.WriteLine("Total Wordcount is {0}.", wordCount);
                foreach(KeyValuePair<string,int> kvp in sortedBD) 
                {
                    sw.WriteLine("{0}-{1}", kvp.Key, kvp.Value);
                }
            }
            Console.WriteLine("Done!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
