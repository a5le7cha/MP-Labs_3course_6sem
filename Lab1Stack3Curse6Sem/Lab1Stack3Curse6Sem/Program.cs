using System;
using System.Collections.Generic;
using System.Diagnostics;
using StackLib;

namespace Lab1Stack3Curse6Sem
{
    public class Program
    {
        static void Main(string[] args)
        {
            MyStack<int> stack1 = new MyStack<int>();
            Stack<int> st1 = new Stack<int>();

            
            for (int i = 0; i < 600; i++)
            {
                stack1.Push(i);
                st1.Push(i);
            }


            MyStack<int> stack = new MyStack<int>();
            Stack<int> st = new Stack<int>();

            int n = 100000;

            Stopwatch stWatch = new Stopwatch();
            Stopwatch FullWork = new Stopwatch();

            stWatch.Start();
            for (int i = 0; i < n; i++)
            {
                stack.Push(i);
            }
            stWatch.Stop();
            Console.WriteLine($"My Stack Metod Push: {stWatch.Elapsed.TotalMilliseconds} ms");

            Stopwatch Watch = Stopwatch.StartNew();
            for (int i = 0; i < n/2; i++)
            {
                stack.Pop();
            }
            Watch.Stop();
            Console.WriteLine($"My Stack Metod Pop: {Watch.Elapsed.TotalMilliseconds} ms");


            Stopwatch Watch2 = new Stopwatch();
            Watch2.Start();
            for (int i = 50000; i < n; i++)
            {
                stack.Contains(i);
            }
            Watch2.Stop();
            Console.WriteLine($"My Stack Metod Contains: {Watch2.Elapsed.TotalMilliseconds} ms");

            stack.Push(1);
            Stopwatch WatchPeek = Stopwatch.StartNew();
            stack.Peek();
            WatchPeek.Stop();
            Console.WriteLine($"My Stack Metod Peek: {WatchPeek.Elapsed.TotalMilliseconds} ms");



            //------------------------------- Base Stack
            Stopwatch Watch3 = Stopwatch.StartNew();
            for (int i = 0; i < n; i++)
            {
                st.Push(i);
            }
            Watch3.Stop();

            Console.WriteLine($"Stack Metod Push: {Watch3.Elapsed.TotalMilliseconds} ms");

            Stopwatch Watch4 = Stopwatch.StartNew();
            for (int i = 0; i < n/2; i++)
            {
                st.Pop();
            }
            Watch4.Stop();
            Console.WriteLine($"Stack Metod Pop: {Watch4.Elapsed.TotalMilliseconds} ms");

            Stopwatch Watch5 = Stopwatch.StartNew();
            for (int i = 50000; i < n; i++)
            {
                st.Contains(i);
            }
            Watch5.Stop();
            Console.WriteLine($"Stack Metod Contains: {Watch5.Elapsed.TotalMilliseconds} ms");

            st.Push(1);
            Stopwatch Watch6 = Stopwatch.StartNew();
            st.Peek();
            Watch6.Stop();
            Console.WriteLine($"Stack Metod Peek: {Watch6.Elapsed.TotalMilliseconds} ms");

            Console.ReadKey();
        }
    }
}