using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment1
{
    internal class Stack <T>
    {
        // a Stack is a Linked List accessed form the front end
        private readonly LinkedList<T> _list = new LinkedList<T>();

        
        // Add item to the top of the stack
        public void Push(T data)
        {
            _list.PushFront(data);
        }


        // Add item to bottom of the stack
        public void PushToBottom(T data)
        {
            _list.PushBack(data);
        }


        // Remove and return item from the top of the stack
        public T Pop()
        {
            if (_list.IsEmpty())
                throw new InvalidOperationException("Stack is empty");

            return _list.PopFront();
        }


        // Remove and return item from the bottom of the stack
        public T PopFromBottom()
        {
            if (_list.IsEmpty())
                throw new InvalidOperationException("Stack is empty");
            return _list.PopBack();
        }


        // Return item from the top of the stack without removing it
        public T Peek()
        {
            if (_list.IsEmpty())
                throw new InvalidOperationException("Stack is empty");

            return _list.PeekFront();
        }


        // Return item from the bottom of the stack without removing it
        public T PeekBottom()
        {
            if (_list.IsEmpty())
                throw new InvalidOperationException("Stack is empty");
            return _list.PeekBack();
        }


        // Returns true when data is contained in the list
        public bool Contains(T data)
        {
            return _list.Contains(data);
        }


        // Check if the stack is empty
        public bool IsEmpty()
        {
            return _list.IsEmpty();
        }
    }
}
