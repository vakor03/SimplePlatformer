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
                return;
            }
            
            //Find place
            PriorityNode current = _root;
            while (current.Next!=null && current.Next.Priority.CompareTo(priority)<=0)
            {
                current = current.Next;
            }

            newNode.Next = current.Next;
            current.Next = newNode;
        }

        public T Dequeue()
        {
            if (_count==0)
            {
                throw new ArgumentOutOfRangeException();
            }

            _count--;
            T result = _root.Element;
            _root = _root.Next;
            return result;
        }

        public void Clear()
        {
            _root = null;
            _count = 0;
        }

        private class PriorityNode
        {
            //public PriorityNode Previous { get; set; }
            public PriorityNode Next { get; set; }
            public F Priority { get; private set; }
            public T Element { get; private set; }

            public PriorityNode(T element, F priority)
            {
                Priority = priority;
                Element = element;
            }
        }
    }
}