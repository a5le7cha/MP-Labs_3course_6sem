using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AlgorithmsSearchSubstring;

namespace Lab6_AlgSearcheString.Tests
{
    [TestClass]
    public class SubstringSearchTests
    {
        private List<ISubstringSearch> GetAlgorithms(string pattern)
        {
            return new List<ISubstringSearch>()
            {
                new BoyerMoore(pattern),
                new BruteForce(pattern),
                new RabinKarp(pattern),
                new KMP(pattern)
            };
        }

        [TestMethod]
        public void SearchTest()
        {
            string text = "aaaaaaaaaa";
            string pattern = "aa";
            var expected = Enumerable.Range(0, 9).ToList();

            var algms = GetAlgorithms(pattern);

            foreach (var algm in algms)
            {
                var actual = algm.SearchSubstring(text);
                CollectionAssert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void SearchTestWithNonOver()
        {
            string text = "abcabcabc";
            string pattern = "abc";
            var expected = new List<int> { 0, 3, 6 }; //позиции abc

            var algms = GetAlgorithms(pattern);

            foreach (var algm in algms)
            {
                var actual = algm.SearchSubstring(text);
                CollectionAssert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void SearchTestWithNoOccurrences()
        {
            string text = "abcdefgh";
            string pattern = "xyz";
            var expected = new List<int> { };

            var algms = GetAlgorithms(pattern);

            foreach (var algm in algms)
            {
                var actual = algm.SearchSubstring(text);
                CollectionAssert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void SearchBagOfWordsOnAnnaTxt()
        {
            var algms = new List<ISubstringSearch>
            {
                new BoyerMoore(""),
                new RabinKarp(""),
                new KMP("")
            };

            string text;
            using (var sr = new StreamReader("anna.txt", System.Text.Encoding.UTF8))
            {
                text = sr.ReadToEnd().ToLower();
            }

            int number = 180;
            Regex rg = new Regex(@"\w+");
            var bag = new HashSet<string>();
            var matches = rg.Matches(text);
            foreach (Match match in matches)
            {
                if (match.Value.Length > 0) //исключаем пустые паттерны
                {
                    bag.Add(match.Value);
                }
                if (bag.Count > number) break;
            }

            foreach (var pattern in bag)
            {
                Console.WriteLine($"Testing pattern: {pattern}");
                var BF = GetAlgorithms(pattern); //new BruteForce(pattern);
                var expected = BF[1].SearchSubstring(text);
                foreach (var algm in algms)
                {
                    // создаем новый экземпляр алгоритма с текущим паттерном
                    var algorithmInstance = (ISubstringSearch)Activator.CreateInstance(algm.GetType(), pattern);
                    var actual = algorithmInstance.SearchSubstring(text);
                    CollectionAssert.AreEqual(expected, actual);
                }
            }
        }
    }
}