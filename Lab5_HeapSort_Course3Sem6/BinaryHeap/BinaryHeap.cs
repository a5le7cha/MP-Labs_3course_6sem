using System;
using System.Collections;

namespace BinaryHeap
{
    public class BinaryHeap<T>: IEnumerable where T: IComparable
    {
        public T[] heap;
        private int _maxSize = 0;
        public int _curSize = 0;

        public BinaryHeap()
        {
            _maxSize = 10; //значение по умолчанию
            heap = new T[_maxSize];
        }

        public BinaryHeap(int sizeArray)
        {
            _maxSize = sizeArray;
            heap = new T[_maxSize];
        }

        public void Add(T element) //добавление элемента 
        {
            if (_curSize != _maxSize)
            {
                heap[_curSize] = element;
                LiftUp(_curSize);
                _curSize++;
            }
            else throw new StackOverflowException("Достигнут максимальный размер массива!");
        }

        public bool Remove(int index) //удаление элемента по индексу
        {
            if (index >= 0 && _curSize > index)
            {
                T root = heap[index];
                heap[index] = heap[--_curSize];
                LowerDown(index);
                return true;
            }

            return false;
        }

        public bool Contains(T item) //проверка наличие элемента
        {
           for(int i =  0; i < heap.Length; i++)
           {
                if (heap[i].CompareTo(item) == 0) return true;
           }

           return false;
        }

        #region My Methods Heapify 
        private void LiftUp(int index)
        {
            int parentIndex = (index - 1) / 2;//индекс родителя
            T curNode = heap[index];
            while(index > 0 &&  heap[parentIndex].CompareTo(curNode) < 0)
            {
                heap[index] = heap[parentIndex];
                index = parentIndex;
                parentIndex = (parentIndex - 1) / 2;
            }

            heap[index] = curNode;
        }

        private void LowerDown(int index)
        {
            int largetChild;
            T top = heap[index];

            while (index < _curSize / 2)
            {
                int leftChild = 2 * index + 1;
                int rightChild = leftChild + 1;

                if (rightChild < _curSize && heap[leftChild].CompareTo(heap[rightChild]) < 0)
                    largetChild = rightChild;
                else largetChild = leftChild;

                if (top.CompareTo(heap[largetChild]) >= 0) break;

                heap[index] = heap[largetChild];
                index = largetChild;
            }

            heap[index] = top;
        }
        #endregion

        public T Sorted()
        {
            int size = _curSize;
            T twoElement = heap[0];
            bool flag = false;
            var root = heap[0];

            for (int i = 0; i < heap.Length; i++)
            {
                var temp = heap[_curSize-1];
                heap[_curSize - 1] = heap[0];
                heap[0] = temp;

                if (flag == false)
                {
                    if(root.CompareTo(heap[0]) != 0)
                        twoElement = heap[0];

                    flag = true;
                }

                _curSize--;
                Heapify(0);
            }

            _curSize = size;

            return twoElement;
        }

        //восстанавливает свойство кучи 
        private void Heapify(int index) 
        {
            var left = 2 * index + 1;
            var right = 2 * index + 2;
            var largest = index;
            if (left < _curSize && heap[left].CompareTo(heap[index]) > 0)
            { largest = left; }
            if (right < _curSize && heap[right].CompareTo(heap[largest]) > 0)
            { largest = right; }

            if (largest == index) return;
            var temp = heap[largest];
            heap[largest] = heap[index];
            heap[index] = temp;
            Heapify(largest);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return heap.GetEnumerator();    
        }
    }
}