using System;
using System.Collections.Generic;

namespace Lab6_AlgSearcheString
{
    public class BruteForce : ISubstringSearch
    {
        private readonly string pattern;

        public BruteForce(string pattern)
        {
            this.pattern = pattern;
        }

        public List<int> SearchSubstring(string text)
        {
            var results = new List<int>(); //индексы с совпадениями

            for (int i = 0; i <= text.Length - pattern.Length; i++)
            {
                for (int j = 0; j < pattern.Length; j++)
                {
                    if (text[i + j] != pattern[j])
                        break;

                    if (j == pattern.Length - 1)
                        results.Add(i);
                }
            }
            return results;

        }
    }
}