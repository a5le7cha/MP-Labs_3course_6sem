using System;
using System.Collections;
using System.Collections.Generic;

namespace BinTreeLib
{
    public class BinaryTree<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>> 
    {
        public int Count { get; private set; }
        private IComparer<TKey> _comparer;
        private Node<TKey, TValue> _root; //корень
        public BinaryTree(): this(null, Comparer<TKey>.Default)
        { }
        public BinaryTree(IComparer<TKey> comparer):this(null, comparer)
        { }
        public BinaryTree(IDictionary<TKey,TValue> dictionary):this(dictionary, Comparer<TKey>.Default)
        { }
        public BinaryTree(IDictionary<TKey,TValue> dictionary, IComparer<TKey> comparer)
        {
            _comparer = comparer;
            Count = 0;
            _root = null;
            if (dictionary != null && dictionary.Count > 0)
            {
                foreach (var pair in dictionary)
                {
                    Add(pair.Key, pair.Value);
                }
            }
        }

        public void Add(TKey key, TValue value)
        {
            var node = new Node<TKey, TValue>(key, value);
            if (_root == null)
            {
                _root = node;
                Count++;
                return;
            }
            var current = _root;
            var parent = _root;
            while (current != null)
            {
                parent = current;
                if (_comparer.Compare( current.Key,node.Key) == 0)
                {
                    throw new ArgumentException("Such key is already added");
                }
                if (_comparer.Compare(current.Key, node.Key) > 0)
                {
                    current.HeightLeft++;
                    current = current.Left;
                }
                else if (_comparer.Compare(current.Key, node.Key) < 0)
                {
                    current.HeightRight++;
                    current = current.Right;
                }
            }
            if (_comparer.Compare(parent.Key, node.Key) > 0)
            {
                parent.Left = node;
                parent.HeightLeft++;
            }
            if (_comparer.Compare(parent.Key, node.Key) < 0)
            {
                parent.Right = node;
                parent.HeightRight++;
            }
            node.Parent = parent;
            Count++;

            Balance();
        }

        public bool ContainsKey(TKey key)
        {
            // Поиск узла осуществляется другим методом.
            return Find(key) != null;
        }
        private Node<TKey, TValue> Find(TKey findKey)
        {
            // Попробуем найти значение в дереве.
            var current = _root;

            // До тех пор, пока не нашли...
            while (current != null)
            {
                int result = _comparer.Compare(current.Key, findKey);
                if (result > 0)
                {
                    // Если искомое значение меньше, идем налево.
                    current = current.Left;
                }
                else if (result < 0)
                {
                    // Если искомое значение больше, идем направо.
                    current = current.Right;
                }
                else
                {
                    // Если равны, то останавливаемся
                    break;
                }
            }
            return current;
        }
        public TValue this[TKey key]
        {
            get 
            {
                if (key == null)
                    throw new ArgumentNullException();
                var node = Find(key);
                return node == null ? throw new KeyNotFoundException() : node.Value;
            }
            set 
            {
                if (key == null)
                    throw new ArgumentNullException();
                var node = Find(key);
                if (node == null)
                    Add(key, value);
                else node.Value = value;
            } 
        }
        public void Clear()
        {
            _root = null;
            Count = 0;
        }

        public bool ContainsValue( TValue value)
        {
            var comparer = EqualityComparer<TValue>.Default;
            foreach( var keyValuePair in Traverse())
            {
                if(comparer.Equals(value, keyValuePair.Value))
                    return true;
            }
            return false;
        }
        IEnumerable<KeyValuePair<TKey, TValue>> Traverse(Node<TKey, TValue> node)
        {
            var nodes = new List<KeyValuePair<TKey, TValue>>();
            if (node != null)
            {
                nodes.AddRange(Traverse(node.Left));
                nodes.Add(new KeyValuePair<TKey, TValue>(node.Key, node.Value));
                nodes.AddRange(Traverse(node.Right));
            }
            return nodes;
        }
        public IEnumerable<KeyValuePair<TKey, TValue>> Traverse()
        {
            return Traverse(_root);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Traverse().GetEnumerator();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return Traverse().GetEnumerator();
        }

        public KeyValuePair<TKey, TValue> MinElemenTree()
        {
            var currRoot = _root;
            while (currRoot.Left != null)
                currRoot = currRoot.Left;

            KeyValuePair<TKey, TValue> TkeyTval = new KeyValuePair<TKey, TValue>(currRoot.Key, currRoot.Value);

            return TkeyTval;
        }

        public void DeletNode(TKey key)
        {
            var node = Find(key);
            var ParentNode = node.Parent;

            if (node.Left == null && node.Right == null)
            {
                if (ParentNode.Left == node)
                    ParentNode.Left = null;
                else ParentNode.Right = null;
            }
            else if (node.Right != null && node.Left == null)
            {
                if (ParentNode.Left == node)
                    ParentNode.Left = node.Right;
                else ParentNode.Right = node.Right;
            }
            else if (node.Right == null && node.Left != null)
            {
                if (ParentNode.Left == node)
                    ParentNode.Left = node.Left;
                else ParentNode.Right = node.Left;
            }
            else if(node.Right != null && node.Left != null)
            {
                var current = node.Right;

                while (current.Left != null)
                    current = current.Left;

                var parent = node.Parent;
                if (parent.Right == node) parent.Right = current;
                else if(parent.Left == node) parent.Left = current;
            }

            Count--;
            Balance();
        }

        void Balance()
        {
            Queue<Node<TKey, TValue>> BFS = new Queue<Node<TKey, TValue>>();
            BFS.Enqueue(_root);

            while (BFS.Count != 0)
            {
                Node<TKey, TValue> current = BFS.Dequeue();

                int heightLeft = current.HeightLeft;
                int heightRight = current.HeightRight;

                if (heightLeft - heightRight == 0 ||
                    Math.Abs(heightLeft - heightRight) == 1)
                {
                    if (current.Left != null) BFS.Enqueue(current.Left);
                    if (current.Right != null) BFS.Enqueue(current.Right);
                }
                else
                {
                    if(heightLeft > heightRight)
                    {
                        Node<TKey, TValue> NodeAbove = current;

                        current = current.Left;
                        int leftH = current.HeightLeft;
                        int rightH = current.HeightRight;

                        if (leftH > rightH)
                        {
                            Node<TKey, TValue> rightChild = null;

                            if (current.Right != null)
                                rightChild = current.Right;
                            current.Parent = NodeAbove.Parent;
                            current.Right = NodeAbove;
                            NodeAbove.Left = rightChild;
                        }
                        else if (rightH > leftH)
                        {
                            Node<TKey, TValue> rightChild = null;
                            Node<TKey, TValue> leftChild = null;

                            rightChild = current.Right.Right;
                            leftChild = current.Right.Left;

                            current.Parent = current.Right;
                            current.Right = leftChild;

                            current.Parent.Right = NodeAbove;
                            NodeAbove.Left = rightChild;
                        }
                    }
                    else if(heightRight > heightLeft)
                    {
                        Node<TKey, TValue> NodeAbove = current;

                        current = current.Right;
                        int leftH = current.HeightLeft;
                        int rightH = current.HeightRight;

                        if (leftH > rightH)
                        {
                            Node<TKey, TValue> rightChild = null;
                            Node<TKey, TValue> leftChild = null;

                            rightChild = current.Right.Right;
                            leftChild = current.Right.Left;

                            current.Parent = current.Left;
                            current.Left = rightChild;

                            current.Parent.Left = NodeAbove;
                            NodeAbove.Right = leftChild;
                        }
                        else if (rightH > leftH)
                        {
                            Node<TKey, TValue> leftChild = null;

                            if (current.Left != null)
                                leftChild = current.Left;
                            current.Parent = NodeAbove.Parent;
                            current.Left = NodeAbove;
                            NodeAbove.Right = leftChild;
                        }
                    }
                }
            }
        }

        //int Height(Node<TKey, TValue> nd)
        //{
        //    if (nd == null) return 0;
        //    else return 1 + Math.Max(Height(nd.Left), Height(nd.Right));
        //}
    }
}