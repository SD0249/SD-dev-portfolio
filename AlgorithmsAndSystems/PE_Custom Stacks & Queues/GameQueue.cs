using System;
using System.Collections.Generic;

namespace PE_Custom_Stacks___Queues
{
    /// <summary>
    /// GameQueue Class
    /// Purpose: Implements the IQueue Interface in the game, 
    ///          which embodies the attributes of a Queue data structure
    /// </summary>
    /// <typeparam name="T">Generic Type Parameter; 
    /// The data type of the class is determined during runtime</typeparam>
    internal class GameQueue<T> : IQueue<T>
    {
        // ---------------------------------------------------------------
        // Fields
        // ---------------------------------------------------------------

        // A generic list field to hold the data in each class
        private List<T> data;


        // ---------------------------------------------------------------
        // Properties (Required to implement IQueue<T> interface)
        // ---------------------------------------------------------------

        // Gets the current count of items in the queue
        public int Count
        {
            get { return data.Count; }
        }

        // Gets whether or not there are items in the queue
        public bool IsEmpty
        {
            get { return data.Count == 0; }
        }


        // ---------------------------------------------------------------
        // Default Constructor 
        // ---------------------------------------------------------------

        /// <summary>
        /// Initializes/Sets the necessary fields 
        /// of the GameQueue for it to function
        /// </summary>
        public GameQueue()
        {
            // Initializes the data list field
            data = new List<T>();
        }


        // ---------------------------------------------------------------
        // Class Methods (Required to implement IQueue<T> interface)
        // ---------------------------------------------------------------

        // ** Peek()

        /// <summary>
        /// Returns the front-most data in the queue.
        /// </summary>
        /// <returns>The front-most data in the queue.</returns>
        /// <exception cref="Exception">
        /// Throws an exception if the queue is empty.</exception>
        public T Peek()
        {
            // If the data list is empty, throw an exception
            if(IsEmpty)
            {
                throw new Exception("Cannot peek at an empty queue.");
            }
            // If the data list is not empty,
            // return the front-most data in the queue
            else
            {
                return data[0];
            }
        }


        // ** Enqueue()

        /// <summary>
        /// Adds new data to the back of the queue.
        /// </summary>
        /// <param name="item">The data to add.</param>
        public void Enqueue(T item)
        {
            data.Add(item);
        }


        // ** Dequeue()

        /// <summary>
        /// Removes and returns the front-most data in the queue.
        /// </summary>
        /// <returns>The front-most data in the queue.</returns>
        /// <exception cref="Exception">
        /// Throws an exception if the queue is empty.</exception>
        public T Dequeue()
        {
            // If the data list is empty, throw an exception
            if(IsEmpty)
            {
                throw new Exception("Cannot dequeue from an empty queue.");
            }
            // If the data list is not empty,
            // return the front-most data in the queue.
            else
            {
                T itemToRemove = data[0];

                data.RemoveAt(0);

                return itemToRemove;
            }
        }
    }
}
