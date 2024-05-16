using System;
using System.Collections.Generic;
using SkipListLib;
using System.Diagnostics;

namespace Lab4_SkipList
{
    class Program
    { 
        //нужно запускать в Debug, библеотека там собрана 
        static void Main(string[] args)
        {
            int n = 10000;
            SkipList<int, int> Slist = new SkipList<int, int>();
            SortedList<int, int> SortedList = new SortedList<int, int>();

            List<int> array = new List<int>();
            Random rd = new Random();
            int rand, j = 0;

            while (j < n)
            {
                rand = rd.Next(0, 3*n);
                if (!array.Contains(rand))
                {
                    array.Add(rand);
                    j++;
                }

                //array.Add(j);
                //j++;
            }

            Stopwatch swAddSList = new Stopwatch();
            swAddSList.Start();
            for (int i = 0; i < n; i++)
            {
                Slist.Add(array[i], i);
            }
            swAddSList.Stop();

            Console.WriteLine($"Skip List Add: {swAddSList.Elapsed.TotalMilliseconds}");

            Stopwatch swAddSortedList = new Stopwatch();
            swAddSortedList.Start();
            for (int i = 0; i < n; i++)
            {
                SortedList.Add(array[i], i);
            }
            swAddSortedList.Stop();

            Console.WriteLine($"Sorted List Add: {swAddSortedList.Elapsed.TotalMilliseconds}");

            Stopwatch swRemoveSList = new Stopwatch();
            swRemoveSList.Start();
            for (int i = 5000; i <= 7000; i++)
            {
                Slist.Remove(array[i]);
            }
            swRemoveSList.Stop();

            Console.WriteLine($"Skip List Remove: {swRemoveSList.Elapsed.TotalMilliseconds}");

            Stopwatch swRemoveSortedList = new Stopwatch();
            swRemoveSortedList.Start();
            for (int i = 5000; i <= 7000; i++)
            {
                SortedList.Remove(array[i]);
            }
            swRemoveSortedList.Stop();

            Console.WriteLine($"Sorted List Remove: {swRemoveSortedList.Elapsed.TotalMilliseconds}");

            Stopwatch swContainsSList = new Stopwatch();
            swContainsSList.Start();
            for (int i = 5000; i <= 7000; i++)
            {
                Slist.Contains(array[i]);
            }
            swContainsSList.Stop();

            Console.WriteLine($"Skip List Contains: {swContainsSList.Elapsed.TotalMilliseconds}");

            Stopwatch swContainsSortedList = new Stopwatch();
            swContainsSortedList.Start();
            for (int i = 5000; i <= 7000; i++)
            {
                SortedList.ContainsKey(array[i]);
            }
            swContainsSortedList.Stop();

            Console.WriteLine($"Sorted List ContainsKey: {swContainsSortedList.Elapsed.TotalMilliseconds}");

            Console.ReadKey();
        }
    }
}