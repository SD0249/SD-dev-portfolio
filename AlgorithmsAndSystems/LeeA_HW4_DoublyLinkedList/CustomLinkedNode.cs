using System;
using System.Collections.Generic;

/* Amy Lee
 * 03/26/2025
 * Create a DoublyLinkedList whose nodes contain references to
 * both the previous node in the list and the next node in the list. */

namespace LeeA_HW4_DoublyLinkedList
{
    /// <summary>
    ///  Generic CustomLinkedNode class
    ///  Purpose: The basic component of a DoublyLinkedList. Holds a single piece of data
    ///           and also hold references to the previous node and the next node.
    /// </summary>
    /// <typeparam name="T">A Generic Type parameter to set the 
    /// intended data type during runtime.</typeparam>
    internal class CustomLinkedNode<T>
    {
        // --------------------------------------------------------------
        // Fields
        // --------------------------------------------------------------

        // Contains this Node's data
        private T data;

        // Gives the reference to the previous node in the list
        private CustomLinkedNode<T> previous;

        // Gives the reference to the next node in the list
        private CustomLinkedNode<T> next;


        // --------------------------------------------------------------
        // Properties
        // --------------------------------------------------------------
        // !! Since the CustomLinkedList class will be the ONLY class
        //    with access to the CustomLinkedNode objects,
        //    it is perfectly fine to have these properties.

        public T Data
        {
            get { return data; }
            set { data = value; }
        }

        public CustomLinkedNode<T> Previous
        {
            get { return previous; }
            set { previous = value; }
        }

        public CustomLinkedNode<T> Next
        {
            get { return next; }
            set { next = value; }
        }


        // --------------------------------------------------------------
        // Parameterized Constructor
        // --------------------------------------------------------------
        /// <summary>
        /// When constructing a new LinkedNode Object, sets the data of this node
        /// with the passed in value and sets next field to 'null'
        /// (there is no pointer; need to manually manipulate this information.)
        /// </summary>
        /// <param name="data">The data of this node</param>
        public CustomLinkedNode(T data)
        {
            this.data = data;
            this.next = null;
        }


        // --------------------------------------------------------------
        // Class Methods
        // --> No methods needed here as nodes do NOT have behaviors or actions
        //     they take being a container class.
        // --------------------------------------------------------------
    }
}
