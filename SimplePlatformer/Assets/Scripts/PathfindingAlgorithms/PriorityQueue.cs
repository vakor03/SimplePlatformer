using System;

namespace PathfindingAlgorithms
{
    public class PriorityQueue<T, F> where F : IComparable<F>
    {
        private int _count;
        public int Count => _count;
        private PriorityNode _root;

        public void Enqueue(T element, F priority)
        {
            _count++;
            PriorityNode newNode = new PriorityNode(element, priority);
            if (_root is null)
            {
                _root = newNode;
            }
            else
            {
                _root.AddChild(newNode);
            }
        }

        public T Dequeue()
        {
            if (_count==0)
            {
                throw new IndexOutOfRangeException();
            }

            _count--;
            PriorityNode current = _root;
            while (current.RightChild != null)
            {
                current = current.RightChild;
            }


            if (current.LeftChild != null)
            {
                if (current == _root)
                {
                    _root = _root.LeftChild;
                    _root.Parent = null;
                }
                else
                {
                    current.LeftChild.Parent = current.Parent;
                    current.Parent.RightChild = current.LeftChild;
                }
            }

            return current.Element;
        }

        public void Clear()
        {
            _root = null;
            _count = 0;
        }

        private class PriorityNode
        {
            public PriorityNode LeftChild { get; set; }
            public PriorityNode RightChild { get; set; }
            public PriorityNode Parent { get; set; }
            public F Priority { get; private set; }
            public T Element { get; private set; }

            public PriorityNode(T element, F priority)
            {
                Priority = priority;
                Element = element;
            }

            public void AddChild(PriorityNode priorityNode)
            {
                if (priorityNode.Priority.CompareTo(Priority) >= 0)
                {
                    if (RightChild != null)
                    {
                        RightChild.AddChild(priorityNode);
                    }
                    else
                    {
                        priorityNode.Parent = this;
                        RightChild = priorityNode;
                    }
                }
                else
                {
                    if (LeftChild != null)
                    {
                        LeftChild.AddChild(priorityNode);
                    }
                    else
                    {
                        priorityNode.Parent = this;
                        LeftChild = priorityNode;
                    }
                }
            }
        }
    }
}