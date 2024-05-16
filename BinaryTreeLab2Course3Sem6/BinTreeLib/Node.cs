using System;

namespace BinTreeLib
{
    internal class Node<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
        public Node<TKey, TValue> Left, Right, Parent;
        public int HeightLeft { get; set; } = 0;
        public int HeightRight { get; set; } = 0;
        public Node(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
}