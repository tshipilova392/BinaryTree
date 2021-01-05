using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTrees
{
    public class BinaryTree<T> : IEnumerable<T>
        where T:IComparable
    {
        private TreeNode<T> root;

        public void Add(T element)
        {
            if (root == null)
            {
                root = new TreeNode<T>(element, null, null, 1);
                return;
            }
            
            TreeNode<T> newNode = new TreeNode<T>(element, null, null, 1);

            TreeNode<T> tmpNode = null;

            TreeNode<T> currentNode = root;
            while(currentNode!=null)
            {
                tmpNode = currentNode;
                tmpNode.Size++;
                if (currentNode.Value.CompareTo(element)<=0)
                {
                    currentNode = currentNode.Right;
                }
                else
                {
                    currentNode = currentNode.Left;
                }
            }

            if (tmpNode.Value.CompareTo(element) <= 0)
                tmpNode.Right = newNode;
            else
                tmpNode.Left = newNode;
        }
        public bool Contains(T element)
        {
            TreeNode<T> tmpNode = root;

            while (tmpNode!=null)
            {
                if (tmpNode.Value.CompareTo(element) == 0) return true;
                else
                {
                    if (tmpNode.Value.CompareTo(element)<0)
                    {
                        tmpNode = tmpNode.Right;
                    }
                    else
                    {
                        tmpNode = tmpNode.Left;
                    }
                }
            }
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Stack<TreeNode<T>> s = new Stack<TreeNode<T>>();
            TreeNode<T> current = root;
            while (!(s.Count==0)||(current != null))
            {
                if (current != null)
                {
                    s.Push(current);
                    current = current.Left;                    
                }
                else
                {
                    current = s.Pop();
                    yield return current.Value;
                    current = current.Right;
                }
            }
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public T this[int index]
        {
            get
            {
                int i = index + 1;
                if (i < 1 || i > root.Size) throw new IndexOutOfRangeException();
                return Find(i).Value;
            }
            set
            {
                int i = index + 1;
                if (i < 1 || i > root.Size) throw new IndexOutOfRangeException();
                TreeNode<T> element = Find(i);
                element.Value = value;
            }
        }

        private TreeNode<T> Find(int i)
        {
            var currentNode = root;
           
            while (currentNode!=null)
            {
                //int currentSize = currentNode.Size;
                int leftSize = 0;
                if (currentNode.Left!=null)
                {
                    leftSize = currentNode.Left.Size;
                }
                if (i <= leftSize)
                {
                    currentNode = currentNode.Left;
                }
                else if (i==leftSize+1)
                {
                    return currentNode;
                }
                else
                {
                    int k = currentNode.Size - currentNode.Right.Size;
                    currentNode = currentNode.Right;                   
                    i=i-k;
                }               
            }
            return null;
        }
    }

    public class TreeNode<T>
        where T : IComparable
    {
        public T Value { get; set; }
        public int Size { get; set; }
        public TreeNode<T> Left {get;set;}
        public TreeNode<T> Right { get; set; }

        public TreeNode(T v, TreeNode<T> left, TreeNode<T> right, int size)
        {
            Value = v;
            Left = left;
            Right = right;
            Size = size;
        }
        public int Compare(T element)
        {
            return Value.CompareTo(element);
        }
    }
}
