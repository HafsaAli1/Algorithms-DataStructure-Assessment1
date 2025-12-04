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


        // Check if the priority queue contains a specific element
        public bool Contains(T data)
        {
            return _list.Contains(data);
        }


        public T GetExisting(T data)
        {
            // temporary list holds elements whilst searching,
            var tempList = new LinkedList<T>();
            // Store matching item if found
            T found = default(T);
            // when target is found
            bool exists = false;

            while (!_list.IsEmpty())
            {
                // Remove front element
                T item = _list.PopFront();

                // Check if this item matches the one we are looking for
                if (!exists && EqualityComparer<T>.Default.Equals(item, data))
                {
                    // existing neighbour is in open list
                    found = item;
                    exists = true;
                }

                // Store every item in tempList to be restored
                tempList.PushBack(item);
            }

            // Restore
            while (!tempList.IsEmpty())
                _list.PushBack(tempList.PopFront());

            return found;
        }


        // Check if the priority queue is empty
        public bool IsEmpty()
        {
            return _list.IsEmpty();
        }

    }
}
