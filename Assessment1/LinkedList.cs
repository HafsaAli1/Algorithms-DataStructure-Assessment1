using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Assessment1
{
    // Generic linked list class
    public class LinkedList<T>
    {
        // Only LinkedLists should know about Elements, so it can be a private class
        private class Element<U>
        {
            public U Data { get; }

            public Element<U>? Next { get; set; }

            public Element(U data)
            {
                Data = data;
                Next = null;
            }
        }

        // The head element of the list. Null if empty list
        private Element<T>? _head;

        // Constructor
        public LinkedList()
        {
            _head = null;
        }


        // If list is empty
        public bool IsEmpty()
        {
            return _head == null;
        }


        // Add element to front of list
        public void PushFront(T data)
        {
            Element<T> newElement = new Element<T>(data);
            newElement.Next = _head;
            _head = newElement;
        }


        // Contains - returns true when data is contained in the list
        public bool Contains(T data)
        {
            Element<T>? currentElement = _head;
            // Traverse the list
            while (currentElement != null)
            {
                // Check for equality
                if (EqualityComparer<T>.Default.Equals(currentElement.Data, data))
                {
                    return true;
                }
                // Move to next element
                currentElement = currentElement.Next;
            }

            return false;
        }


        // Add to back of list
        public void PushBack(T data)
        {
            Element<T> newElement = new Element<T>(data);

            // if list is empty, new element becomes head
            if (_head == null)
            {
                _head = newElement;
            }
            else
            {
                // Otherwise, find the last element
                Element<T>? currentElement = _head;
                // Traverse to the last element
                while (currentElement.Next != null)
                {
                    // Move to next element
                    currentElement = currentElement.Next;
                }
                // Link new element at the end
                currentElement.Next = newElement;
            }
        }


        // Remove from front of list 
        public T PopFront()
        {
            if (_head == null)
            {
                throw new InvalidOperationException("List is empty");
            }

            T value = _head.Data;
            _head = _head.Next;
            return value;
        }


        // Remove from back of list 
        public T PopBack()
        {
            if (_head == null)
            {
                throw new InvalidOperationException("List is empty");
            }

            // if only one element, remove it
            if (_head.Next == null)
            {
                T value = _head.Data;
                _head = null;
                return value;
            }

            // traverse to second-last element
            Element<T>? currentElement = _head;
            while (currentElement.Next != null && currentElement.Next.Next != null)
            {
                currentElement = currentElement.Next;
            }

            // currentElement.Next is the last element
            T lastValue = currentElement.Next!.Data;
            currentElement.Next = null;
            return lastValue;
        }


        // Peek at front element
        public T PeekFront()
        {
            if (_head == null)
            {
                throw new InvalidOperationException("List is empty");
            }

            return _head.Data;
        }


        // Peek at back element
        public T PeekBack()
        {
            if (_head == null)
            {
                throw new InvalidOperationException("List is empty");
            }

            Element<T>? currentElement = _head;
            while (currentElement.Next != null)
            {
                currentElement = currentElement.Next;
            }

            return currentElement.Data;
        }


        // Priority Insert 
        public void PriorityInsert(T data, Comparison<T> comparison)
        {
            Element<T> newElement = new Element<T>(data);

            // if list is empty or new element has higher priority, insert at front
            if (_head == null || comparison(data, _head.Data) < 0)
            {
                newElement.Next = _head;
                _head = newElement;
                return;
            }

            // traverse until we find the right spot
            Element<T>? currentElement = _head;
            while (currentElement.Next != null && comparison(data, currentElement.Next.Data) >= 0)
            {
                currentElement = currentElement.Next;
            }

            // insert new element after current
            newElement.Next = currentElement.Next;
            currentElement.Next = newElement;
        }
    }
}


