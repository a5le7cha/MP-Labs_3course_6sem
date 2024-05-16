using System;

namespace AVL_Tree
{
    public class Node<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
        public Node<TKey, TValue> Left, Right, Parent;
        public int Height 
        { 
            get 
            {
                if(Left != null && Right != null)
                    return Math.Max(Left.Height, Right.Height) + 1;
                if(Left == null)
                {
                    if(Right != null)
                        return Right.Height + 1;
                    else return 1;
                }
                else return Left.Height + 1;
            }
        } 
        public Node(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
}
