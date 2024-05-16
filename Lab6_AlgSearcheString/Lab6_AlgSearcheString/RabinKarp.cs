using System;
using System.Collections.Generic;
using System.Xml.Schema;

namespace Lab6_AlgSearcheString
{

    public class RabinKarp : ISubstringSearch
    {
        private readonly string pattern;
        private readonly int d; // Размер алфавита


        public RabinKarp(string pattern)
        {
            this.pattern = pattern;
            d = 256; // Размер алфавита (например, ASCII символы)
            
        }

        public List<int> SearchSubstring(string text)
        {
            List<int> result = new List<int>();
            int m = pattern.Length;
            int n = text.Length;
            if (m > n) return result;

            uint p = 0; // хэш образца
            uint t = 0; // хэш текущего окна текста
            uint h = 1;

            for (int i = 0; i < m - 1; i++)
            {
                h = ((uint)(h << 8)); //коэффициент для удаления старшей цифры h = pow(d, m-1) % q
            }

            // Вычисляем хэш образца и первого окна текста
            // hash=(((pattern[0]⋅d+pattern[1])⋅d+pattern[2])⋅d+...)⋅d+pattern[m−1]
            for (int i = 0; i < m; i++)
            {
                p = ((uint)(p + pattern[i]) << 8);
                t = ((uint)(t + text[i]) << 8);
            }

            for (int i = 0; i <= n - m; i++)
            {
                if (p == t)
                {
                    bool match = true;
                    for (int j = 0; j < m; j++)
                    {
                        if (text[i + j] != pattern[j])
                        {
                            match = false;
                            break;
                        }
                    }
                    if (match)
                    {
                        result.Add(i);
                    }
                }

                // Вычисляем хэш следующего окна текста
                if (i < n - m)
                {
                    t = (uint)(((t - text[i] * h) + text[i + m]) << 8);
                }
            }

            return result;
        }
    }
}