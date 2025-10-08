using System;
using System.Collections.Generic;

namespace LeeA_HW4_DoublyLinkedList
{
    /// <summary>
    /// IQueue Interface
    /// Purpose: Holds the common actions of a Queue 
    ///          for a defined Custom Data structure to embody.
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
