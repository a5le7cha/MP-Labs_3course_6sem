using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using AlgorithmsSearchSubstring;

namespace Lab6_AlgSearcheString
{
    class Program
    {
        static void Main(string[] args)
        {
            string pattern;
            using(StreamReader sr = new StreamReader("input.txt")) 
            {
                pattern = sr.ReadToEnd();
            }

            List<ISubstringSearch> listAlgorithms = new List<ISubstringSearch>()
            {
                new BruteForce(pattern),
                new RabinKarp(pattern),
                new BoyerMoore(pattern),
                new KMP(pattern)
            };

            string text = null;
            using(var rd = new StreamReader("anna.txt"))
            {
                text = rd.ReadToEnd();
            }

            List<int> resulet = new List<int>();

            #region BruteForce
            Stopwatch SW_BruteForce = new Stopwatch();
            SW_BruteForce.Start();
            resulet = listAlgorithms[0].SearchSubstring(text);
            SW_BruteForce.Stop();

            Console.WriteLine($"\nBruteForce: {SW_BruteForce.Elapsed.TotalMilliseconds} ms");
            foreach(var item in resulet)
            {
                int i = 0;
                while(i < pattern.Length)
                {
                    Console.Write($"{text[item + i]}");
                    i++;
                }
                Console.WriteLine($"\n{resulet.Count}");
                break;
            }
            Console.WriteLine();
            #endregion

            #region RabinKarp
            Stopwatch SW_RabinKarp = new Stopwatch();
            SW_RabinKarp.Start();
            resulet = listAlgorithms[1].SearchSubstring(text);
            SW_RabinKarp.Stop();

            Console.WriteLine($"RabinKarp: {SW_RabinKarp.Elapsed.TotalMilliseconds} ms");
            foreach (var item in resulet)
            {
                int i = 0;
                while (i < pattern.Length)
                {
                    Console.Write($"{text[item + i]}");
                    i++;
                }
                Console.WriteLine($"\n{resulet.Count}");
                break;
            }
            Console.WriteLine();
            #endregion

            #region BoyerMoore
            Stopwatch SW_BoyerMoore = new Stopwatch();
            SW_BoyerMoore.Start();
            resulet = listAlgorithms[2].SearchSubstring(text);
            SW_BoyerMoore.Stop();
            Console.WriteLine($"BoyerMoore: {SW_BoyerMoore.Elapsed.TotalMilliseconds} ms");

            foreach (var item in resulet)
            {
                int i = 0;
                while (i < pattern.Length)
                {
                    Console.Write($"{text[item + i]}");
                    i++;
                }
                Console.WriteLine($"\n{resulet.Count}");
                break;
            }
            Console.WriteLine();
            #endregion
            

            #region KMP
            Stopwatch SW_KMP = new Stopwatch();
            SW_KMP.Start();
            resulet = listAlgorithms[3].SearchSubstring(text);
            SW_KMP.Stop();

            Console.WriteLine($"KMP: {SW_KMP.Elapsed.TotalMilliseconds} ms");

            foreach (var item in resulet)
            {
                int i = 0;
                while (i < pattern.Length)
                {
                    Console.Write($"{text[item + i]}");
                    i++;
                }
                Console.WriteLine($"\n{resulet.Count}");
                break;
            }
            Console.WriteLine();
            #endregion

            Console.ReadKey();
        }
    }
}