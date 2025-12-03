using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment1
{
    internal class PriorityQueue <T>
    {
        // Use linked list to store priority queue elements
        private readonly LinkedList<T> _list = new LinkedList<T>();
        // Comparison delegate to determine priority
        private readonly Comparison<T> _compare;

        // Constructor
        public PriorityQueue(Comparison<T> comparison)
        {
            _compare = comparison;
        }


        // Add element based on priority
        public void Enqueue(T data)
        {
            _list.PriorityInsert(data, _compare);
        }


        // Remove and return highest priority element
        public T Dequeue()
        {
            if (_list.IsEmpty())
                throw new InvalidOperationException("Priority queue is empty");

            return _list.PopFront();
        }


        // Return highest priority element without removing it
        public T Peek()
        {
            if (_list.IsEmpty())
                throw new InvalidOperationException("Priority queue is empty");

            return _list.PeekFront();
        }


        // Check if the priority queue is empty
        public bool IsEmpty()
        {
            return _list.IsEmpty();
        }

    }
}
