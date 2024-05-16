using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SkipListLib;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CountIncreaseAfterAdding()
        {
            int n = 10;
            var lib = new SkipList<int, int>();

            for (int i = 0; i < n; i++)
            {
                lib.Add(i, i);
            }
            Assert.AreEqual(n, lib.Count);
        }
        [TestMethod]
        public void ItemsExistsAfterAdding()
        {
            var lib = new SkipList<int, int>();
            var nums = new List<int>(new[] { 44, 22, 1, 56, 3, 90, 31, 15, 26 });
            int n = nums.Count;
            for (int i = 0; i < n; i++)
            {
                lib.Add(nums[i], i);
            }
            nums.Sort();
            int j = 0;
            foreach (var pair in lib)
            {
                Assert.AreEqual(nums[j], pair.Key);
                j++;
            }
            Assert.AreEqual(n, lib.Count);
        }
        [TestMethod]
        public void RandomItemsExistsAfterAdding()
        {
            var lib = new SkipList<int, int>();
            var nums = new HashSet<int>();
            var rd = new Random();
            int n = 100;
            while (nums.Count < n)
            {
                nums.Add(rd.Next(1, n * 3));
            }
            foreach (var item in nums)
            {
                lib.Add(item, 1);
            }

            var a = nums.ToList();
            a.Sort();
            int j = 0;
            foreach (var pair in lib)
            {
                Assert.AreEqual(a[j], pair.Key);
                j++;
            }
            Assert.AreEqual(n, lib.Count);
        }

        [TestMethod]
        public void Contains()
        {
            SkipList<int, int> skList = new SkipList<int, int>();
            Random rd = new Random();

            int n = 10;
            List<int> testNum = new List<int>();

            int i = 0, rand;
            while (i < n)
            {
                rand = rd.Next();
                if (!testNum.Contains(rand))
                {
                    testNum.Add(rand);
                    i++;
                }
            }

            for (int j = 0; j < n; j++)
            {
                skList.Add(testNum[j], j);
            }


            for (int j = 0; j < n; j++)
            {
                Assert.IsTrue(skList.Contains(testNum[j]));
            }
        }

        [TestMethod]
        public void Remove()
        {
            SkipList<int, int> skList = new SkipList<int, int>();
            Random rd = new Random();

            int n = 10;
            List<int> testNum = new List<int>();

            int i = 0, rand;
            while (i < n)
            {
                rand = rd.Next();
                if (!testNum.Contains(rand)) 
                {
                    testNum.Add(rand);
                    i++;
                }
            }

            for (int j = 0; j < n; j++)
            {
                skList.Add(testNum[j], j);
            }

            for (int j = 0; j < n; j += 2)
            {
                skList.Remove(testNum[j]);
            }

            for(int j = 1; j<n-1; j+= 2)
            {
                Assert.AreEqual(true, skList.Contains(testNum[j]));
            }

            Assert.AreEqual(n / 2, skList.Count);
        }
    }
}
