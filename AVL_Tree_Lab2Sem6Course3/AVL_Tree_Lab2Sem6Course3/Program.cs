using System;
using System.Collections.Generic;
using System.Diagnostics;
using AVL_Tree;

namespace AVL_Tree_Lab2Sem6Course3
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryTree<int, int> tree = new BinaryTree<int, int>();
            SortedDictionary<int, int> SortedDic = new SortedDictionary<int, int>();

            int n = 10000;
            int[] array = new int[n];

            Random rd = new Random();

            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < n; i++)
            {
                
                var num = rd.Next(0, n);
                tree.Add(i, num);
                array[i] = num;
            }
            sw.Stop();
            Console.WriteLine($"Metod Add in Tree: {sw.Elapsed.TotalMilliseconds} ms");

            //Queue<Node<int, int>> queue = new Queue<Node<int, int>>();

            //queue.Enqueue(tree._root);
            //for (int i = 0;i <tree.Count; i++)
            //{
            //    var currentNode = queue.Dequeue();
                
            //    if(currentNode.Left != null)
            //        queue.Enqueue(currentNode.Left);
            //    if (currentNode.Right != null)
            //        queue.Enqueue(currentNode.Right);
            //}

            //Console.ReadKey();

            Stopwatch stw = new Stopwatch();
            stw.Start();
            for (int i = 5000; i < 7001; i++)
            {
                tree.DeletNode(i);
            }
            stw.Stop();
            Console.WriteLine($"Metod Delete in Tree: {stw.Elapsed.TotalMilliseconds} ms");

            Stopwatch sW = new Stopwatch();
            sW.Start();
            for (int i = 0; i < n; i++)
            {
                tree.ContainsKey(i);
            }
            sW.Stop();
            Console.WriteLine($"Metod Contains Key in Tree: {sW.Elapsed.TotalMilliseconds} ms");

            Stopwatch Sw = new Stopwatch();
            Sw.Start();
            for (int i = 0; i < n; i++)
            {
                tree.ContainsValue(i);
            }
            Sw.Stop();
            Console.WriteLine($"Metod Contains Value in Tree: {Sw.Elapsed.TotalMilliseconds} ms\n");

            //------------- Sorted Dictionary

            Stopwatch SWDic = new Stopwatch();
            SWDic.Start();
            for (int i = 0; i < n; i++)
            {
                SortedDic.Add(i, array[i]);
            }
            SWDic.Stop();
            Console.WriteLine($"Metod Add in SortDic: {SWDic.Elapsed.TotalMilliseconds} ms");

            Stopwatch SwDic = new Stopwatch();
            SWDic.Start();
            for (int i = 0; i < n; i++)
            {
                SortedDic.ContainsValue(array[i]);
            }
            SWDic.Stop();
            Console.WriteLine($"Metod Contains Value in Sorted Dic: {SWDic.Elapsed.TotalMilliseconds} ms");

            Stopwatch SwDicDel = new Stopwatch();
            SwDicDel.Start();
            for (int i = 5000; i < 7001; i++)
            {
                SortedDic.Remove(i);
            }
            SwDicDel.Stop();
            Console.WriteLine($"Metod Delete in Sorted Dic: {SwDicDel.Elapsed.TotalMilliseconds} ms");


            //Console.WriteLine($"{}");
            //Console.WriteLine($"{}");

            Console.ReadKey();
            
        }
    }
}
