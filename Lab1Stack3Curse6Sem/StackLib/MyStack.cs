using System;
using System.Collections.Generic;

namespace StackLib
{
    public class MyStack<T>
    {
        T[] array;
        public int _capacity = 4;
        
        public int Count { get; private set; }

        public MyStack() {
            array = new T[_capacity];
            Count = 0;
        }
        public MyStack(int cnt) {
            _capacity = cnt;
            array = new T[_capacity];
            Count = 0;
        }
        public void Push(T item)
        {
            if (Count == _capacity)
                IncreaseArray();
            array[Count] = item;
            Count++;
        }

        public T Pop()
        {
            if (Count == 0) throw new InvalidOperationException("Empty Stack");
            Count--;
            return array[Count];
        }

        public T Peek()
        {
            if (Count == 0) throw new InvalidOperationException("Empty Stack");
            else return array[Count - 1];
        }

        public bool Contains(T item)
        {
            EqualityComparer<T> comparer = EqualityComparer<T>.Default;
            for (int i = Count-1; i >= 0; i--)
                if (comparer.Equals( array[i],item))
                    return true;
            return false;
        }

        private void IncreaseArray()
        {
            T[] CopyArray = new T[array.Length];
            for (int i = 0; i < array.Length; i++)
                CopyArray[i] = array[i];

            array = new T[_capacity * 2];
            _capacity *= 2;
        }
    }
}