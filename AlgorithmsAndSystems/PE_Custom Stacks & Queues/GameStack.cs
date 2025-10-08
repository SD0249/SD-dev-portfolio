using System;
using System.Collections.Generic;

/* Amy Lee
 * 02/21/2025
 * Constructing Custom Stacks & Queues to gain a better understanding
 * of how each of these data structures work underhood */

namespace PE_Custom_Stacks___Queues
{
    /// <summary>
    /// GameStack class
    /// Purpose: Implements the IStack interface in the Game, 
    ///          which embodies the attributes of a stack data structure
    /// </summary>
    /// <typeparam name="T">Generic Type Parameter; 
    /// The data type of the class is determined during runtime</typeparam>
    internal class GameStack<T> : IStack<T>
    {
        // ---------------------------------------------------------------
        // Fields
        // ---------------------------------------------------------------

        // A generic list field to hold the data in each class
        private List<T> data;


        // ---------------------------------------------------------------
        // Properties (Required to implement IStack<T> interface)
        // ---------------------------------------------------------------

        // Gets the current count of items in the stack
        public int Count
        {
            get { return data.Count;  }
        }

        // Gets whether or not there are items in the stack
        public bool IsEmpty
        {
            get { return data.Count == 0; }
        }


        // ---------------------------------------------------------------
        // Default Constructor
        // ---------------------------------------------------------------

        /// <summary>
        /// Initialize/Set the necessary fields for the GameStack class to function
        /// </summary>
        public GameStack()
        {
            // Initialize the data list field
            data = new List<T>();
        }


        // ---------------------------------------------------------------
        // Class Methods (Required to implement IStack<T> interface)
        // ---------------------------------------------------------------

        // ** Peek()

        /// <summary>
        /// Return the top-most element of the stack.
        /// </summary>
        /// <returns>The top-most element of the stack.</returns>
        /// <exception cref="Exception">
        /// Throws an exception if the stack is empty.</exception>
        public T Peek()
        {
            // If the data list field is empty, throw an exception
            if(IsEmpty)
            {
                throw new Exception("Cannot peek from an empty stack.");
            }
            // If the data list field is not empty,
            // return the last element of the data list field
            else
            {
                return data[Count - 1];
            }
        }


        // ** Push()

        /// <summary>
        /// Adds new data to the top of the Stack.
        /// </summary>
        /// <param name="item">The data to add to the stack</param>
        public void Push(T item)
        {
            data.Add(item);
        }


        // ** Pop()

        /// <summary>
        /// Removes and returns the top-most element of the stack.
        /// </summary>
        /// <returns>The top-most element of the stack.</returns>
        /// <exception cref="Exception">
        /// Throws an exception if the stack is empty.</exception>
        public T Pop()
        {
            // If the data list field is empty, throw an exception
            if (IsEmpty)
            {
                throw new Exception("Cannot pop from an empty stack.");
            }
            // If the data list field is not empty,
            // return the top-most element of the stack
            else
            {
                // **************************************************************
                // Q - What if the generic type T is storing a class object?
                //     Would the pointer of this local variable point towards
                //     the memory of this class object, and when removed from
                //     the list field of Game Stack class, would we be able to
                //     return the item?
                //     Is this a method that only works for value types?
                // **************************************************************
                T itemToReturn = data[Count - 1];

                data.RemoveAt(Count - 1);

                return itemToReturn;
            }
        }
    }
}
