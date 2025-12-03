using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment1
{
    internal class Queue <T>
    {
        // Use linked list to store queue elements
        private LinkedList<T> _list;

        // Constructor
        public Queue()
        {
            _list = new LinkedList<T>();
        }


        // Add item to the back of the queue
        public void Enqueue(T data)
        {
            _list.PushBack(data);
        }


        // Add item to the front of the queue
        public void EnqueueFront(T data)
        {
            _list.PushFront(data);
        }


        // Remove and return item from the front of the queue
        public T Dequeue()
        {
            if (_list.IsEmpty())
            {
                throw new InvalidOperationException("Queue is empty");
            }
            return _list.PopFront();
        }


        // Remove and return item from the back of the queue
        public T DequeueBack()
        {
            if (_list.IsEmpty())
            {
                throw new InvalidOperationException("Queue is empty");
            }
            return _list.PopBack();
        }


        // Return item from the front of the queue without removing it
        public T Peek()
        {
            if (_list.IsEmpty())
            {
                throw new InvalidOperationException("Queue is empty");
            }
            return _list.PeekFront();
        }


        // Return item from the back of the queue without removing it
        public T PeekBack()
        {
            if (_list.IsEmpty())
            {
                throw new InvalidOperationException("Queue is empty");
            }
            return _list.PeekBack();
        }


        // Returns true when data is contained in the list
        public bool Contains(T data)
        {
            return _list.Contains(data);
        }


        // Check if the queue is empty
        public bool IsEmpty()
        {
            return _list.IsEmpty();
        }

    }
}
