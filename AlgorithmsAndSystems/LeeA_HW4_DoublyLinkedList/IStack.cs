using System;
using System.Collections.Generic;

namespace LeeA_HW4_DoublyLinkedList
{
    /// <summary>
    /// IStack Interface
    /// Purpose: Holds the common actions that a data structure
    ///          embodying the Stack should have.
    /// </summary>
    internal interface IStack<T>
    {
        // ---------------------------------------------------------------
        // Properties
        // ---------------------------------------------------------------

        // Gets the current count of items in the stack
        int Count { get; }

        // Gets whether or not there are items in the stack
        bool IsEmpty { get; }


        // ---------------------------------------------------------------
        // Class Methods
        // ---------------------------------------------------------------

        // Return the top-most element of the stack.
        // Throws an exception if the stack is empty.
        T Peek();

        // Adds new data to the top of the stack.
        // T: The data to add
        void Push(T item);

        // Removes and returns the top-most element of the stack.
        // Throws an exception if the stack is empty.
        T Pop();
    }
}
