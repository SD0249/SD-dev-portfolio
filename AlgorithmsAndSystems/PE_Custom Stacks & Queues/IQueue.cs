using System;
using System.Collections.Generic;

namespace PE_Custom_Stacks___Queues
{
    /// <summary>
    /// IQueue Interface
    /// Purpose: Simply put in words, IQueue interface stores the rules(foundation) 
    ///          for the items that want to implement the attributes of the Queue.
    /// </summary>
    internal interface IQueue<T>
    {
        // ---------------------------------------------------------------
        // Properties
        // ---------------------------------------------------------------

        // Gets the current count of items in the queue
        int Count { get; }

        // Gets whether or not there are items in the queue
        bool IsEmpty { get; }


        // ---------------------------------------------------------------
        // Class Methods
        // ---------------------------------------------------------------

        // Returns the front-most data in the queue.
        // Throws an exception if the queue is empty.
        T Peek();

        // Adds new data to the back of the queue.
        // T: The data to add
        void Enqueue(T item);

        // Removes and returns the front-most data in the queue.
        // Throws an exception if the queue is empty.
        T Dequeue();
    }
}
