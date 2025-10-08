using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeeA_HW4_DoublyLinkedList
{
    internal class Queue<T> : IQueue<T>
    {
        // ---------------------------------------------------------------
        // Fields
        // ---------------------------------------------------------------

        // A CustomLinkedList field to store the data in the Queue
        private CustomLinkedList<T> queue;


        // ---------------------------------------------------------------
        // Properties
        // ---------------------------------------------------------------

        // Gets the current count of items in the stack
        public int Count
        {
            get { return queue.Count; }
        }

        // Gets whether or not there are items in the stack
        public bool IsEmpty
        {
            get { return queue.Count == 0; }
        }


        // ---------------------------------------------------------------
        // Constructor
        // ---------------------------------------------------------------
        public Queue()
        {
            queue = new CustomLinkedList<T>();
        }


        // ---------------------------------------------------------------
        // Class Methods
        // ---------------------------------------------------------------

        /// <summary>
        /// Returns the front-most data in the queue.
        //  Throws an exception if the queue is empty.
        /// </summary>
        /// <returns>The front-most data in the queue</returns>
        public T Peek()
        {
            if(Count != 0)
            {
                return queue[0];
            }
            else
            {
                throw new Exception("The stack is empty.");
            }
        }


        /// <summary>
        /// Adds new data to the back of the queue.
        /// </summary>
        /// <param name="data">The data to add.</param>
        public void Enqueue(T data)
        {
            queue.Add(data);
        }


        /// <summary>
        /// Removes and returns the front-most data in the queue.
        //  Throws an exception if the queue is empty.
        /// </summary>
        /// <returns>The front-most data in the queue.</returns>
        public T Dequeue()
        {
            if(Count != 0)
            {
                T dataToRemove = queue[0];
                queue.RemoveAt(0);
                return dataToRemove;
            }
            else
            {
                throw new Exception("The stack is empty.");
            }
        }
    }
}
