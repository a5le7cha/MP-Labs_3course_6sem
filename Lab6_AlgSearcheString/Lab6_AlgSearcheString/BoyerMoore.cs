using System;
using System.Collections.Generic;

namespace Lab6_AlgSearcheString
{
    public class BoyerMoore : ISubstringSearch
    {
        private readonly string pattern;
        public BoyerMoore(string pattern)
        {
            this.pattern = pattern;
        }
        public List<int> SearchSubstring(string text)
        {
            List<int> result = new List<int>();
            int m = pattern.Length;
            int n = text.Length;

            if (m == 0 || n == 0 || m > n)
                return result;

            int[] badCharShift = BuildBadCharShift(pattern);

            int i = 0;
            while (i <= n - m)
            {
                int j = m - 1;
                while (j >= 0 && pattern[j] == text[i + j])
                    j--;

                if (j < 0)
                {
                    result.Add(i);
                    i += m;
                }
                else
                {
                    i += badCharShift[text[i + m - 1]];
                }
            }
            return result;
        }

        private  int[] BuildBadCharShift(string pattern)
        {
            const int unicodeLength = 65536;
            int[] shift = new int[unicodeLength];
            int m = pattern.Length;

            for (int i = 0; i < unicodeLength; i++)
                shift[i] = m;

            for (int i = 0; i < m - 1; i++)
                shift[pattern[i]] = m - 1 - i;

            return shift;
        }
    }
    //public class BoyerMoore : ISubstringSearch
    //{
    //    int[] stopCharsOffsetTable;
    //    int[] goodSuffixOffsetTable;
    //    private readonly string pattern;
    //    public BoyerMoore(string pattern)
    //    {
    //        this.pattern = pattern;
    //    }

    //    public void InitStopCharsOffsetTable(string pattern)
    //    {
    //        int patternLength = pattern.Length;
    //        stopCharsOffsetTable = new int[10000];

    //        //начальная инициализация массива символов,
    //        //символов может быть не более 10000 всего, по кодировке
    //        for (int i = 0; i < 10000; i++)
    //        {
    //            stopCharsOffsetTable[i] = patternLength - 1;
    //        }

    //        //длина строки без последнего символа
    //        int lengthStringWithoutLastChar = patternLength - 1;
    //        for (int i = 0; i < lengthStringWithoutLastChar; i++)
    //        {
    //            stopCharsOffsetTable[pattern[i]] = lengthStringWithoutLastChar - i - 1;
    //        }
    //    }

    //    public void InitGoodSuffixOffsetTable(string pattern)
    //    {
    //        int patternLength = pattern.Length;
    //        goodSuffixOffsetTable = new int[pattern.Length + 1];

    //        //массив хранения границ суффиксов
    //        int[] borderPositions = new int[pattern.Length + 1];

    //        int i = pattern.Length;
    //        int j = pattern.Length + 1;

    //        borderPositions[i] = j;

    //        while (i > 0)
    //        {   
    //            while (j <= patternLength && pattern[i - 1] != pattern[j - 1])
    //            {
    //                if (goodSuffixOffsetTable[j] == 0)
    //                {
    //                    goodSuffixOffsetTable[j] = j - i;
    //                }

    //                j = borderPositions[j];
    //            }

    //            borderPositions[--i] = --j;
    //        }

    //        int prefixBorder = borderPositions[0];

    //        for (int k = 0; k <= patternLength; k++)
    //        {
    //            if (goodSuffixOffsetTable[k] == 0)
    //            {
    //                goodSuffixOffsetTable[k] = prefixBorder;
    //            }

    //            if (k == prefixBorder)
    //            {
    //                prefixBorder = borderPositions[prefixBorder];
    //            }
    //        }
    //    }

    //    List<int> ISubstringSearch.SearchSubstring(string text)
    //    {
    //        List<int> matchesIndexes = new List<int>();

    //        InitStopCharsOffsetTable(pattern);
    //        InitGoodSuffixOffsetTable(pattern);

    //        int textLength = text.Length;
    //        int patternLength = pattern.Length;

    //        int IndexPatternOccurance = 0;
    //        while (IndexPatternOccurance + patternLength - 1 < textLength)
    //        {
    //            //Находим позицию начала совпашего суффикса
    //            int matchesCountOccuranceIndex = patternLength - 1;
    //            while (matchesCountOccuranceIndex >= 0 &&
    //                pattern[matchesCountOccuranceIndex] == text[IndexPatternOccurance + matchesCountOccuranceIndex])
    //            {
    //                matchesCountOccuranceIndex--;
    //            }

    //            //Слово полностью совпало
    //            if (matchesCountOccuranceIndex < 0)
    //            {
    //                matchesIndexes.Add(IndexPatternOccurance);
    //            }

    //            int stopCharOffset = -1;
    //            if (matchesCountOccuranceIndex > 0)
    //                stopCharOffset = stopCharsOffsetTable[text[IndexPatternOccurance + matchesCountOccuranceIndex]];

    //            //Передвигаем курсор опираясь на максимальных сдиг
    //            IndexPatternOccurance += Math.Max(stopCharOffset,
    //                goodSuffixOffsetTable[matchesCountOccuranceIndex + 1]);
    //        }

    //        return matchesIndexes;
    //    }
    //}
}