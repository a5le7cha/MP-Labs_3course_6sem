﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace UnitTestProj
{
    [TestClass]
    public class UnitTest1
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
            Assert.Equal(n, tree.Count);
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
            Assert.Equal(n, tree.Count);
            Array.Sort(a);
            int j = 0;
            foreach (var pair in tree)
            {
                Assert.Equal(a[j], pair.Key);
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
                Assert.True(tree.ContainsKey(number));
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
            Assert.False(tree.ContainsKey(37));
        }

        [TestMethod]
        public void ConstructorFromOtherDictionaryWorks()
        {
            Dictionary<string, string> openWith =
            new Dictionary<string, string>
                (StringComparer.CurrentCultureIgnoreCase)
            {
                // Add some elements to the dictionary.
                { "txt", "notepad.exe" },
                { "Bmp", "paint.exe" },
                { "DIB", "paint.exe" },
                { "rtf", "wordpad.exe" }
            };
            BinaryTree<string, string> copy =
                    new BinaryTree<string, string>(openWith,
                        StringComparer.CurrentCultureIgnoreCase);
            Assert.True(openWith.Count == copy.Count);
            foreach (var pair in openWith)
            {
                Assert.True(copy.ContainsKey(pair.Key));
                Assert.True(copy.ContainsValue(pair.Value));
            }
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
            Assert.Throws<ArgumentException>(() => tree.Add(n - 1, n - 1));
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
            Assert.Throws<KeyNotFoundException>(() => tree[n + 1]);
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
            tree[n] = n;
            Assert.True(tree.ContainsKey(n));
            Assert.True(tree.ContainsValue(n));
        }
        [TestMethod]
        public void IfKeyEqualsNullThrowsArgumentNullException()
        {
            var tree = new BinaryTree<string, int>();
            Assert.Throws<ArgumentNullException>(() => tree[null] = 6);
        }

        [TestMethod]
        public void RetMinElement()
        {
            var tree = new BinaryTree<int, double>();
            int sizeRn = 1000;

            Random rnd = new Random(sizeRn);

            int n = 10;
            double minElm = 0;
            int IdMinelem = 0;

            for (int i = 0; i < n; i++)
            {
                double val = rnd.NextDouble();
                tree.Add(i, val);
                if (minElm > val)
                {
                    minElm = val;
                    IdMinelem = i;
                }
            }

            Assert.Equals(tree.MinElemenTree(), new KeyValuePair<int, double>(IdMinelem, minElm));
        }
    }
}