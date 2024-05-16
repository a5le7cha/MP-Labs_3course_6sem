using System;
using System.Collections.Generic;

namespace Lab6_AlgSearcheString
{
    public class KMP : ISubstringSearch
    {
        private readonly string pattern;
        private readonly int[] pi;

        public KMP(string pattern)
        {
            if (pattern == null)
                throw new ArgumentNullException(nameof(pattern));
            this.pattern = pattern;
            pi = new int[pattern.Length];
            ComputePrefixFunction();
        }

        private void ComputePrefixFunction()
        {
            int m = pattern.Length;
            if (m == 0) return; // нет необходимости в вычислении для пустой строки
            pi[0] = 0;
            int k = 0;

            for (int i = 1; i < m; i++)
            {
                while (k > 0 && pattern[k] != pattern[i])
                {
                    k = pi[k - 1];
                }

                if (pattern[k] == pattern[i])
                {
                    k++;
                }

                pi[i] = k;
            }
        }

        public List<int> SearchSubstring(string text)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));

            List<int> result = new List<int>();
            int n = text.Length;
            int m = pattern.Length;
            if (m == 0 || n == 0 || m > n) return result;

            int k = 0;

            for (int i = 0; i < n; i++)
            {
                while (k > 0 && pattern[k] != text[i])
                {
                    k = pi[k - 1];
                }

                if (pattern[k] == text[i])
                {
                    k++;
                }

                if (k == m)
                {
                    result.Add(i - m + 1);
                    k = pi[k - 1];
                }
            }

            return result;
        }
    }
}