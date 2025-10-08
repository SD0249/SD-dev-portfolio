using System;
using System.Collections.Generic;

/* Amy Lee
 * 03/26/2025
 * Create a DoublyLinkedList whose nodes contain references to
 * both the previous node in the list and the next node in the list. */

namespace LeeA_HW4_DoublyLinkedList
{
    /// <summary>
    /// Generic CustomLinkedList Class
    /// Purpose: A custom data structure which implements a DoublyLinkedList 
    ///          using CustomLinkedNode objects as its components.
    /// </summary>
    /// <typeparam name="T">A Generic Type parameter to set the 
    /// intended data type during runtime.</typeparam>
    internal class CustomLinkedList<T>
    {
        // --------------------------------------------------------------
        // Fields
        // --------------------------------------------------------------

        // The current number of items in the linked list
        private int count;

        // Head; a customlinkednode object which acts as a starting pointer.
        private CustomLinkedNode<T> head;

        // Tail; a customlinkednode object which acts as a ending pointer.
        private CustomLinkedNode<T> tail;


        // --------------------------------------------------------------
        // Properties
        // --------------------------------------------------------------
        public int Count
        {
            get { return count; }
        }

        // ** INDEXER
        /// <summary>
        /// Returns data from any valid spot in the linked list.
        /// </summary>
        /// <param name="index">The index value of that custom node object; 
        /// how many times do you need to jump through the list?</param>
        /// <returns>Data from the node of the given index</returns>
        public T this[int index]
        {
            get
            {
                // The IndexOutOfRange exception is handled in the method.
                return GetData(index);
            }

            set
            {
                CustomLinkedNode<T> targetNode = GetNode(index);

                if(targetNode != null)
                {
                    targetNode.Data = value;
                }
                // If the node at that index is null,
                // it means the index was out of range,
                // from how the GetNode method behaves.
                else
                {
                    throw new IndexOutOfRangeException
                        ($"Error: Cannot set data at invalid index {index}.");
                }
            }
        }


        // --------------------------------------------------------------
        // Default Constructor
        // --------------------------------------------------------------
        /// <summary>
        /// When a new CustomLinkedList is made, 
        /// before you explicitly add nodes to the list, 
        /// the list contains 0 items and the head & tail point nowhere.
        /// </summary>
        public CustomLinkedList()
        {
            count = 0;
            head = null;
            tail = null;
        }


        // --------------------------------------------------------------
        // Class Methods
        // --------------------------------------------------------------

        /// <summary>
        /// Returns a CustomLinkedNode object of the given index.
        /// </summary>
        /// <param name="index">The index value of that custom node object; 
        /// how many times do you need to jump through the list?</param>
        /// <returns>A CustomLinkedNode object of the given index.</returns>
        private CustomLinkedNode<T> GetNode(int index)
        {
            // Case 1:
            // Invalid Index
            if (index < 0 || index >= count)
            {
                return null;
            }
            // Case 2:
            // When Index is 0, no need to jump through the list.
            else if (index == 0)
            {
                return head;
            }
            // Case 3:
            // When Index is count - 1, no need to jump through the list.
            else if (index == count -1)
            {
                return tail;
            }
            // Case 4:
            // Valid Index bigger than 0 and smaller than count - 1
            else
            {
                // The current pointer object to iterate through the list
                CustomLinkedNode<T> current;

                // Determine whether the target index is closer to
                // the head or the tail pointer

                // Case A: Target index is closer to the head pointer
                if(index <= count/2)
                {
                    // Start from the head of the list
                    current = head;

                    // Iterate through the list necessary amount of times
                    for (int i = 0; i < index; i++)
                    {
                        current = current.Next;
                    }
                }
                // Case B: Target index is closer to the tail pointer
                else
                {
                    // Start from the tail of the list
                    current = tail;

                    // Iterate through the list necessary amount of times
                    for(int i = 0; i < count - index; i++)
                    {
                        current = current.Previous;
                    }
                }

                // Return the final value of current.
                return current;
            }
        }


        /// <summary>
        /// Returns the data from the node of the given index.
        /// </summary>
        /// <param name="index">The index value of that custom node object</param>
        /// <returns>The data from the node of the given index.</returns>
        /// <exception cref="IndexOutOfRangeException">Exception thrown 
        /// when an invalid index is given</exception>
        private T GetData(int index)
        {
            // Get the Node of that index and store it in a local variable
            CustomLinkedNode<T> targetNode = GetNode(index);

            // If the targetnode has data, return it.
            // If not, throw an exception.
            if (targetNode != null)
            {
                return targetNode.Data;
            }
            else
            {
                throw new IndexOutOfRangeException
                    ($"Error: Cannot get data from invalid index {index}.");
            }
        }


        /// <summary>
        /// Adds data to a new CustomLinkedNode at the end of the list.
        /// </summary>
        /// <param name="data">
        /// The data that a new CustomLinkedNode will hold</param>
        public void Add(T data)
        {
            // Create a new node object to add at the end of the list
            CustomLinkedNode<T> newNode = new CustomLinkedNode<T>(data);

            // Case 1:
            // If the list is not empty
            if (count != 0)
            {
                // Save the reference of the previous tail
                // before we set the tail to the newly added Node.
                CustomLinkedNode<T> current = tail;

                // Point the tail node to the newly added node
                tail = newNode;

                // The previous tail points to the current tail.
                current.Next = tail;

                // The current tail points to the previous tail.
                tail.Previous = current;
            }
            // Case 2:
            // If the list is empty
            else
            {
                // Since the list is empty, the head points to the newNode.
                // Also the tail points to the newNode too!
                head = newNode;
                tail = newNode;
            }

            // Either case, increment the count field
            count++;
        }


        /// <summary>
        /// Returns the data of the node that was removed.
        /// </summary>
        /// <param name="index">The index value of that custom node object</param>
        /// <returns>The data of the node that was removed.</returns>
        /// <exception cref="Exception">An exception thrown when the list is empty
        /// or if the given index is invalid.</exception>
        public T RemoveAt(int index)
        {
            // A local variable to temporarily
            // have the reference of the node to be removed.
            CustomLinkedNode<T> nodeToRemove;

            // Case 1:
            // The list is empty
            if (count == 0)
            {
                throw new Exception($"Error: Cannot remove invalid index {index}.");
            }
            // Case 2: 
            // Invalid Index.
            else if (index < 0 || index >= count)
            {
                throw new Exception($"Error: Cannot remove invalid index {index}.");
            }
            // Case 3: 
            // Removing the ONLY element in the list
            else if (count == 1)
            {
                // Update the local variable to store the node to remove
                nodeToRemove = head;

                // Set both head & tail to null.
                head = null;
                tail = null;
            }
            // Case 4:
            // Removing the HEAD node
            else if (index == 0)
            {
                // Update the local variable to store the node to remove
                nodeToRemove = head;

                // Set the head to point the next node
                head = head.Next;

                // This new head should not point to the previous head anymore
                head.Previous = null;
            }
            // Case 5:
            // Removing the TAIL node
            else if (index == count - 1)
            {
                // Update the local variable to store the node to remove
                nodeToRemove = tail;

                // Set the tail to point the node before the previous tail
                tail = tail.Previous;

                // This new tail should not point to the previous tail anymore
                tail.Next = null;
            }
            // Case 6:
            // Removing a node somewhere in the middle
            else
            {
                // Update the local variable to store the node to remove
                nodeToRemove = GetNode(index);

                // A local node variable to store the previous node and the consecutive node
                CustomLinkedNode<T> previousNode = GetNode(index - 1);
                CustomLinkedNode<T> nextNode = GetNode(index + 1);

                // Change the pointer of the node previous
                // to the nodeToRemove to point the next node
                previousNode.Next = nextNode;

                // Change the pointer of the node subsequent
                // to the nodeToRemove to point to the previous node
                nextNode.Previous = previousNode;
            }

            // Decrement the count field
            count--;

            // Return the data of the removed node.
            return nodeToRemove.Data;
        }


        /// <summary>
        /// Implements the action of clearing the linked list
        /// </summary>
        public void Clear()
        {
            // Simply, set the head & the tail pointer of the list to be null.
            // Then we lose ALL the access we had to each nodes in the list
            // (This is the action of allowing C#'s garbage collector
            //  to automatically reclaim the memory used by the nodes)
            head = null;
            tail = null;

            // Also don't forget to RESET the count field of the LinkedList!!
            count = 0;
        }


        /// <summary>
        /// Insert a new CustomLinkedNode object with a specified data 
        /// into the list at the specific index.
        /// </summary>
        /// <param name="data">
        /// The data a new CustomLinkedNode object will hold</param>
        /// <param name="index">The index 
        /// where the new CustomLinkedNode object will be placed</param>
        public void Insert(T data, int index)
        {
            // I. Check whether the index is valid for insertion
            if(index < 0 || index > count)
            {
                throw new IndexOutOfRangeException
                    ($"Error: Cannot insert into invalid index {index}.");
            }

            // II. If the list is EMPTY (count == 0)
            if(count == 0)
            {
                // It's same to adding the node to the list
                Add(data);
                return;
            }

            // Create a local variable to temporarily
            // store reference of the node to be added
            CustomLinkedNode<T> nodeToInsert = new CustomLinkedNode<T>(data);

            // III. Valid Index - Handle insertion by cases.
            // III-1) Insert at FRONT (Index 0)
            if(index == 0)
            {
                // The current head points to the inserted node
                head.Previous = nodeToInsert;

                // The inserted node points to the current head
                nodeToInsert.Next = head;

                // The head now points to the inserted node
                head = nodeToInsert;
            }
            // III-2) Insert at END (Index count)
            else if(index == count)
            {
                // The current tail points to the inserted node
                tail.Next = nodeToInsert;

                // The inserted node points to the current tail
                nodeToInsert.Previous = tail;

                // The tail now points to the inserted node
                tail = nodeToInsert;
            }
            // III-3) Inserting somewhere in the middle
            else
            {
                // The inserted node points to the node
                // at the index previous to its index
                nodeToInsert.Previous = GetNode(index - 1);

                // The inserted node points to the node
                // at the index subsequent to its index
                nodeToInsert.Next = GetNode(index + 1);

                // The node at the index previous to the given index
                // points to this inserted node
                GetNode(index - 1).Next = nodeToInsert;

                // The node at the index subsequent to the given index
                // points to this inserted node
                GetNode(index + 1).Previous = nodeToInsert;
            }

            // Increment the count field
            count++;
        }


        /// <summary>
        /// Starting from the head, print all of the list's data in order
        /// </summary>
        public void PrintForward()
        {
            // If the list is empty, inform the user about it
            if(count == 0)
            {
                Console.WriteLine("There are no items in the list.");
                return;
            }

            // A local variable to get reference to the nodes in the list
            CustomLinkedNode<T> current = head;

            for (int i = 0; i < count; i++)
            {
                // Access the correct node in the list
                if(i != 0)
                {
                    current = current.Next;
                }

                // Print the data of the accessed node
                Console.WriteLine(current.Data);
            }
        }


        /// <summary>
        /// Starting from the tail, print all of the list's data in reverse order
        /// </summary>
        public void PrintBackward()
        {
            // If the list is empty, inform the user about it
            if (count == 0)
            {
                Console.WriteLine("There are no items in the list.");
                return;
            }

            // A local variable to get reference to the nodes in the list
            CustomLinkedNode<T> current = tail;

            for (int i = 0; i < count; i++)
            {
                // Access the correct node in the list
                if (i != 0)
                {
                    current = current.Previous;
                }

                // Print the data of the accessed node
                Console.WriteLine(current.Data);
            }
        }
    }
}

