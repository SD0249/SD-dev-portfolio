using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeeA_HW4_DoublyLinkedList
{
    internal class Stack<T> : IStack<T>
    {
        // ---------------------------------------------------------------
        // Fields
        // ---------------------------------------------------------------

        // A CustomLinkedList Class to contain the data in the stack.
        private CustomLinkedList<T> stack;


        // ---------------------------------------------------------------
        // Properties
        // ---------------------------------------------------------------

        // Gets the current count of items in the stack
        public int Count 
        {
            get { return stack.Count; } 
        }

        // Gets whether or not there are items in the stack
        public bool IsEmpty
        {
            get { return stack.Count == 0; }
        }


        // ---------------------------------------------------------------
        // Constructor
        // ---------------------------------------------------------------
        public Stack()
        {
            stack = new CustomLinkedList<T>();
        }


        // ---------------------------------------------------------------
        // Class Methods
        // ---------------------------------------------------------------

        /// <summary>
        /// Return the top-most element of the stack.
        /// Throws an exception if the stack is empty.
        /// </summary>
        /// <returns>The top-most element of the stack.</returns>
        public T Peek()
        {
            if(Count != 0)
            {
                return stack[Count - 1];
            }
            else
            {
                throw new Exception("The stack is empty.");
            }
        }


        /// <summary>
        /// Adds new data to the top of the stack.
        /// </summary>
        /// <param name="data">
        /// The data to add to the top of the stack</param>
        public void Push(T data)
        {
            stack.Add(data);
        }


        /// <summary>
        /// Removes and returns the top-most element of the stack.
        //  Throws an exception if the stack is empty.
        /// </summary>
        /// <returns>The top-most element of the stack.</returns>
        public T Pop()
        {
            if(Count != 0)
            {
                T dataToReturn = stack[Count - 1];
                stack.RemoveAt(Count - 1);
                return dataToReturn;
            }
            else
            {
                throw new Exception("The stack is empty.");
            }
        }
    }
}
