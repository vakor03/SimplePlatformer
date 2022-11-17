using System;

namespace PathfindingAlgorithms
{
    public class PriorityQueue<TElement, TPriority> where TPriority : IComparable<TPriority>
    {
        private int _count;
        public int Count => _count;
        private PriorityNode _root;

        public void Enqueue(TElement element, TPriority priority)
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

        public TElement Dequeue()
        {
            if (_count==0)
            {
                throw new ArgumentOutOfRangeException();
            }

            _count--;
            TElement result = _root.Element;
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
            public PriorityNode Next { get; set; }
            public TPriority Priority { get; private set; }
            public TElement Element { get; private set; }

            public PriorityNode(TElement element, TPriority priority)
            {
                Priority = priority;
                Element = element;
            }
        }
    }
}