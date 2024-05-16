using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using AVL_Tree;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTestAVL
    {
        [TestMethod]
        public void CountIncreaseAfterAdding()
        {
            var tree = new BinaryTree<int, int>();
            int n = 10;
            for (int i = 0; i < n; i++)
            {
                tree.Add(i, i);
            }
            Assert.AreEqual(n, tree.Count);
        }

        [TestMethod]
        public void ItemsExistAfterAdding()
        {
            var tree = new BinaryTree<int, int>();
            var a = new[] { 22, 30, 15, 5, 17, 24, 33, 10, 16, 26 };
            int n = a.Length;
            for (int i = 0; i < n; i++)
            {
                tree.Add(a[i], i);
            }
            Assert.AreEqual(n, tree.Count);
            Array.Sort(a);
            int j = 0;
            foreach (var pair in tree)
            {
                Assert.AreEqual(a[j], pair.Key);
                j++;
            }
        }

        [TestMethod]
        public void ContainsExistingElement()
        {
            var tree = new BinaryTree<int, int>();
            var arrayInts = new[] { 8, 3, 10, 1, 6, 14, 4, 7, 13 };
            foreach (var number in arrayInts)
            {
                tree.Add(number, 0);
            }

            foreach (var number in arrayInts)
            {
                Assert.IsTrue(tree.ContainsKey(number));
            }
        }

        [TestMethod]
        public void ContainsNotExistingElement()
        {
            var tree = new BinaryTree<int, int>();
            var a = new[] { 8, 3, 10, 1, 6, 14, 4, 7, 13 };
            for (int i = 0; i < a.Length; i++)
            {
                tree.Add(a[i], 0);
            }
            Assert.IsFalse(tree.ContainsKey(37));
        }

        [TestMethod]
        public void AddingExistingKeyThrowsException()
        {
            var tree = new BinaryTree<int, int>();
            int n = 10;
            for (int i = 0; i < n; i++)
            {
                tree.Add(i, i);
            }
            Assert.ThrowsException<ArgumentException>(() => tree.Add(n - 1, n - 1));
        }
        [TestMethod]
        public void IfKeyNotFoundGetThrowsKeyNotFoundException()
        {
            var tree = new BinaryTree<int, int>();
            int n = 10;
            for (int i = 0; i < n; i++)
            {
                tree.Add(i, i);
            }
            Assert.ThrowsException<KeyNotFoundException>(() => tree[n + 1]);
        }
        [TestMethod]
        public void IfKeyNotFoundSetAddNewItem()
        {
            var tree = new BinaryTree<int, int>();
            int n = 10;
            for (int i = 0; i < n; i++)
            {
                tree.Add(i, i);
            }
            tree.Add(n, n);
            Assert.IsTrue(tree.ContainsKey(2));
            Assert.IsTrue(tree.ContainsValue(2));
        }
        [TestMethod]
        public void IfKeyAreEqualsNullThrowsArgumentNullException()
        {
            var tree = new BinaryTree<string, int>();
            Assert.ThrowsException<ArgumentNullException>(() => tree[null] = 6);
        }

        [TestMethod]
        public void RetMinElement()
        {
            var tree = new BinaryTree<int, int>();
            int sizeRn = 1000;

            Random rnd = new Random(sizeRn);

            int n = 10;
            int minElm = 0;
            int IdMinelem = 0;

            for (int i = 0; i < n; i++)
            {
                //double val = rnd.NextDouble();
                tree.Add(i, i);
                if (minElm > i)
                {
                    minElm = i;
                    IdMinelem = i;
                }
            }

            Assert.AreEqual(tree.MinElemenTree(), new KeyValuePair<int, int>(IdMinelem, minElm));
        }

        [TestMethod]
        public void DeleteNode()
        {
            var tree = new BinaryTree<int, int>();

            int size = 5;
            for (int i = 0; i < size; i++)
            {
                tree.Add(i, i);
            }

            tree.DeletNode(0);
            Assert.IsTrue(!tree.ContainsKey(0));
        }
    }
}
