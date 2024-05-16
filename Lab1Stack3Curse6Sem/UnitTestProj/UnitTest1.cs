using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using StackLib;
using System.CodeDom;

namespace UnitTestProj
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void PushTest()
        {
            MyStack<int> stack = new MyStack<int>();
            int size = 5;
            for (int i = 0; i < size; i++)
            {
                stack.Push(i);
            }
            Assert.AreEqual(size, stack.Count);
        }

        [TestMethod]
        public void CreatNewStackWithConstructionNull()
        {
            MyStack<int> stack = new MyStack<int>();
            Assert.AreEqual(stack.Count, 0);
            Assert.AreEqual(stack._capacity, 4);
        }

        [TestMethod]
        public void CreatNewStackCapacity()
        {
            int expectedCapacity = 10;
            MyStack<int> stack = new MyStack<int>(expectedCapacity);
            Assert.AreEqual(0, stack.Count);
            Assert.AreEqual(expectedCapacity, stack._capacity);
        }

        [TestMethod]
        public void PopTest()
        {
            MyStack<int> stack = new MyStack<int>();
            int ExpectedItem = 1;
            stack.Push(ExpectedItem);
            int number = stack.Pop();
            Assert.AreEqual(ExpectedItem, number);
            Assert.AreEqual(0, stack.Count);
        }

        [TestMethod]
        public void PeekTest()
        {
            MyStack<int> stack = new MyStack<int>();
            int number;
            stack.Push(1);
            for (int i = 0; i < 5; i++)
            {
                number = stack.Peek();
                Assert.AreEqual(1, number);
            }
        }

        [TestMethod]
        public void ContainsTest()
        {
            MyStack<int> stack = new MyStack<int>();
            stack.Push(1);
            stack.Push(2);
            Assert.AreEqual(true, stack.Contains(1));
        }

        [TestMethod]
        public void IncreaseArray()
        {
            MyStack<int> st = new MyStack<int>();

            st.Push(1);
            st.Push(2);
            st.Push(3);
            st.Push(4);
            st.Push(5);
            Assert.AreEqual(8, st._capacity);
        }
    }
}