using System;
using System.Collections.Generic;

/* Amy Lee
 * 02/21/2025
 * Constructing Custom Stacks & Queues to gain a better understanding
 * of how each of these data structures work underhood */

namespace PE_Custom_Stacks___Queues
{
    /// <summary>
    /// IStack Interface
    /// Purpose: Simply put in words, IStack interface stores the rules(foundation) 
    ///          for the items that want to implement the attributes of the Stack.
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
        // Methods
        // ---------------------------------------------------------------

        // Return the top-most element of the stack.
        // Throws an exception if the stack is empty.
        T Peek();

        // Adds new data to the top of the Stack.
        // T: The data to add.
        void Push(T item);

        // Removes and returns the top-most element of the stack.
        // Throws an exception if the stack is empty.
        T Pop();
    }
}
