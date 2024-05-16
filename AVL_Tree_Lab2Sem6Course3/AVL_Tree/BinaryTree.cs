using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace AVL_Tree
{
    public class BinaryTree<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        public int Count { get; private set; }
        private IComparer<TKey> _comparer;
        public Node<TKey, TValue> _root; //корень
        public BinaryTree() : this(null, Comparer<TKey>.Default)
        { }
        public BinaryTree(IComparer<TKey> comparer) : this(null, comparer)
        { }
        public BinaryTree(IDictionary<TKey, TValue> dictionary) : this(dictionary, Comparer<TKey>.Default)
        { }
        public BinaryTree(IDictionary<TKey, TValue> dictionary, IComparer<TKey> comparer)
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
                if (_comparer.Compare(current.Key, node.Key) == 0)
                {
                    throw new ArgumentException("Such key is already added");
                }
                if (_comparer.Compare(current.Key, node.Key) > 0)
                {
                    current = current.Left;
                }
                else if (_comparer.Compare(current.Key, node.Key) < 0)
                {
                    current = current.Right;
                }
            }
            if (_comparer.Compare(parent.Key, node.Key) > 0)
            {
                parent.Left = node;
            }
            if (_comparer.Compare(parent.Key, node.Key) < 0)
            {
                parent.Right = node;
            }
            node.Parent = parent;
            Count++;

            var SearcheNodeDis = SearcheNodeDisbalance(node);

            if (SearcheNodeDis != null)
                Balance(SearcheNodeDis);
        }

        public bool ContainsKey(TKey key)
        {
            // Поиск узла осуществляется другим методом.
            return Find(key) != null;
        }

        private Node<TKey, TValue> Find(TKey fiproxyKey)
        {  
            // Попробуем найти значение в дереве.
            var current = _root;

            // До тех пор, пока не нашли...
            while (current != null)
            {
                int result = _comparer.Compare(current.Key, fiproxyKey);
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

        public bool ContainsValue(TValue value)
        {
            var comparer = EqualityComparer<TValue>.Default;
            foreach (var keyValuePair in Traverse())
            {
                if (comparer.Equals(value, keyValuePair.Value))
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

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return Traverse().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Traverse().GetEnumerator();
        }

        public KeyValuePair<TKey, TValue> MinElemenTree()
        {
            var currRoot = _root;
            var parent = _root;
            while (currRoot != null)
            {
                parent = currRoot;
                currRoot = currRoot.Left;
            }
                

            KeyValuePair<TKey, TValue> TkeyTval = new KeyValuePair<TKey, TValue>(parent.Key, parent.Value);

            return TkeyTval;
        }

        public void DeletNode(TKey key)
        {
            var node = Find(key);
            if (node == null)
                return;
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
            else if (node.Right != null && node.Left != null)
            {
                var current = node.Right;
                var proxy = node;
                while (current != null)
                {
                    proxy = current;
                    current = current.Left;
                }

                var ParentProxy = proxy.Parent;
                if (ParentProxy.Left == proxy)
                    ParentProxy.Left = null;
                else ParentProxy.Right = null;

                var parent = node.Parent;
                if (parent.Right == node)
                {
                    parent.Right = proxy;
                    proxy.Right = node.Right;
                    proxy.Left = node.Left;
                }
                else if (parent.Left == node)
                {
                    parent.Left = proxy;
                    proxy.Right = node.Right;
                    proxy.Left = node.Left;
                }
            }

            Count--;

            var SearcheNodeDis = SearcheNodeDisbalance(node);

            if (SearcheNodeDis != null)
                Balance(SearcheNodeDis);
        }

        Node<TKey, TValue> SearcheNodeDisbalance(Node<TKey, TValue> nodeAdding)
        {
            var childeStart = nodeAdding;
            var current = nodeAdding.Parent;

            while (current != null)
            {
                if (current.Left != null && current.Right != null)
                {
                    if (Math.Abs(current.Left.Height - current.Right.Height) > 1)
                        return current;
                }
                else if (current.Left == null && current.Right != null)
                {
                    if (current.Right.Height > 1)
                        return current;
                }  
                else if(current.Left != null && current.Right == null)
                {
                    if (current.Left.Height > 1)
                        return current;
                }
                    
                current = current.Parent;
            }
            return null;
        }

        void Balance(Node<TKey, TValue> nodeDesbalance)
        {
            Node<TKey, TValue> current = nodeDesbalance;
            var curParent = current.Parent;

            int H_Left, H_Right;

            if (current.Left == null) H_Left = 0; else H_Left = current.Left.Height;
            if (current.Right == null) H_Right = 0; else H_Right = current.Right.Height;

            if (H_Left > H_Right)
            {
                current = current.Left;

                if (current.Left == null) H_Left = 0; else H_Left = current.Left.Height;
                if (current.Right == null) H_Right = 0; else H_Right = current.Right.Height;

                if (H_Left > H_Right)
                {
                    if (current.Right != null)
                    {
                        current.Parent.Left = current.Right;
                        current.Right.Parent = current.Parent;
                    }
                    else current.Parent.Left = null; 
                    
                    current.Right = current.Parent;
                    current.Right.Parent = current;

                    if (curParent == null)
                    {
                        current.Parent = null;
                        _root = current;
                    }
                    else
                    {
                        current.Parent = curParent;
                        if (_comparer.Compare(curParent.Key, current.Key) > 0)
                            curParent.Left = current;
                        if (_comparer.Compare(curParent.Key, current.Key) < 0)
                            curParent.Right = current;
                    }
                }
                else if(H_Left < H_Right)
                {
                    var left = current.Right.Left;
                    var right = current.Right.Right;
                    var parent = current.Parent;

                    current.Parent = current.Right;
                    current.Parent.Left = current;

                    if (left != null)
                    {
                        current.Right = left;
                        left.Parent = current;
                    }
                    else current.Right = null;

                    current.Parent.Right = parent;
                    parent.Parent = current.Parent;

                    parent.Left = right;
                    right.Parent = parent.Left;

                    if (curParent == null)
                    {
                        current.Parent.Parent = null;
                        _root = current.Parent;
                    }
                    else
                    {
                        if (curParent.Left == parent)
                        {
                            curParent.Left = current.Parent;
                            current.Parent.Parent = curParent;
                        }
                        else
                        {
                            curParent.Right = current.Parent;
                            current.Parent.Parent = curParent;
                        }
                    }
                }
            }
            else if(H_Left < H_Right)
            {
                current = current.Right;

                if (current.Left == null) H_Left = 0; else H_Left = current.Left.Height;
                if (current.Right == null) H_Right = 0; else H_Right = current.Right.Height;

                if (H_Left > H_Right)
                {
                    var left = current.Left.Left;
                    var right = current.Left.Right;
                    var parent = current.Parent;

                    current.Parent = current.Left;
                    current.Parent.Right = current;

                    if (right != null)
                    {
                        current.Left = right;
                        right.Parent = current;
                    }
                    else current.Left = null;

                    if(left != null)
                    {
                        parent.Right = left;
                        left.Parent = parent;
                    }
                    else parent.Right = null;

                    current.Parent.Left = parent;
                    parent.Parent = current.Parent;

                    if (curParent == null)
                    {
                        current.Parent = null;
                        _root = current.Parent;
                    }
                    else
                    {
                        if (curParent.Left == parent)
                        {
                            curParent.Left = current.Parent;
                            current.Parent.Parent = curParent;
                        }
                        else
                        {
                            curParent.Right = current.Parent;
                            current.Parent.Parent = curParent;
                        }
                    }
                }
                else if(H_Left < H_Right)
                {
                    var parent = current.Parent;

                    if (current.Left != null)
                    {
                        parent.Right = current.Left;
                        parent.Right.Parent = parent;
                    }
                    else parent.Right = null;
                    
                    current.Left = parent;
                    parent.Parent = current;

                    if (curParent == null)
                    {
                        current.Parent = null;
                        _root = current;
                    }
                    else
                    {
                        current.Parent = curParent;
                        if(_comparer.Compare(curParent.Key,current.Key) > 0)
                            curParent.Left = current;
                        if(_comparer.Compare(curParent.Key, current.Key) < 0)
                            curParent.Right = current;
                    }
                }
            }
        }
    }
}