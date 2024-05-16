using System;
using BinaryHeap;

namespace Lab5_HeapSort_Course3Sem6
{
    class Program
    {
        //Дан список из n элементов.
        //Надо найти второй по величине элемент меньше, чем за 2n-3 сравнений.
        static void Main(string[] args)
        {
            Console.WriteLine("Введите максимальный размер массива: ");
            int size = int.Parse(Console.ReadLine());

            BinaryHeap<int> heap = new BinaryHeap<int>(size);

            Console.WriteLine("Введите элементы массива через пробел: ");
            string[] input = Console.ReadLine().Split(' ');

            for (int i = 0; i < size; i++)
            {
                heap.Add(Convert.ToInt32(input[i]));
            }

            int root = heap.heap[0];
            heap.Remove(0);
            int max2 = heap.heap[0];
            heap.Remove(0);

            while (root == max2)
            {
                max2 = heap.heap[0];
                heap.Remove(0);
            }

            Console.WriteLine($"\nВторой по величине элемент: {max2}");

            heap = new BinaryHeap<int>(size);
            for (int i = 0; i < size; i++)
            {
                heap.Add(Convert.ToInt32(input[i]));
            }

            heap.Sorted();
            Array.Reverse(heap.heap);//сортировка по убыванию 

            Console.WriteLine();
            foreach(var item in heap)
            {
                Console.Write($"{item} ");
            }

            Console.ReadKey();
        }
    }
}