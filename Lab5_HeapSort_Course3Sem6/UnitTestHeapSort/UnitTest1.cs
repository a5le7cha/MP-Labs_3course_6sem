using Microsoft.VisualStudio.TestTools.UnitTesting;
using BinaryHeap;
using System.Collections.Generic;

namespace UnitTestHeapSort
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Add()
        {
            List<int> arrNumbers = new List<int>(){ 1, 2, 3, 4, 5 };

            BinaryHeap<int> heap = new BinaryHeap<int>(arrNumbers.Count);

            for (int i = 0; i < arrNumbers.Count; i++)
            {
                heap.Add(arrNumbers[i]);
            }


            int idx = 0;
            foreach(var item in heap)
            {
                Assert.IsTrue(arrNumbers.Contains((int)item));
                idx++;
            }
        }

        [TestMethod]
        public void Contains()
        {
            int[] arrNumbers = { 1, 2, 3, 4, 5 };

            BinaryHeap<int> heap = new BinaryHeap<int>(arrNumbers.Length);

            for (int i = 0; i < arrNumbers.Length; i++)
            {
                heap.Add(arrNumbers[i]);
            }

            Assert.IsTrue(heap.Contains(arrNumbers[arrNumbers.Length - 2]));
        }

        [TestMethod]
        public void Remove()
        {
            int[] arrNumbers = { 1, 2, 3, 4, 5 };

            BinaryHeap<int> heap = new BinaryHeap<int>(arrNumbers.Length);

            for (int i = 0; i < arrNumbers.Length; i++)
            {
                heap.Add(arrNumbers[i]);
            }

            heap.Remove((arrNumbers.Length - 1) / 2);

            Assert.AreEqual(heap._curSize, arrNumbers.Length - 1);
        }
    }
}