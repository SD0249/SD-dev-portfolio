using System;
using System.Collections.Generic;

/* Amy Lee
 * 03/26/2025
 * Create a DoublyLinkedList whose nodes contain references to
 * both the previous node in the list and the next node in the list. */

namespace LeeA_HW4_DoublyLinkedList
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Introduction to the assignment
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("*** Doubly Linked List Tests ***");
            Console.WriteLine("This Main method will test all operations of your doubly linked list.");
            Console.WriteLine("That includes testing every thrown exception for invalid operations.");
            Console.WriteLine();
            Console.WriteLine("All console output should match the Console Compare extension.");
            Console.WriteLine("(See myCourses for Console Compare instructions.)");
            Console.WriteLine();
            Console.WriteLine("All of Main (text your instructor wrote) should already match Console Compare.");
            Console.WriteLine("What do YOU need to match?");
            Console.WriteLine("  - Check your exception messages");
            Console.WriteLine("  - Check your output from the two print methods");
            Console.ForegroundColor = ConsoleColor.Gray;

            // Press a key to test the next section
            MoveToNextSection("testing the list with addition");

            // Instantiate a new Linked List of strings.
            CustomLinkedList<string> myList = new CustomLinkedList<string>();

            #region Adding Data
            // Test adding data, then print the list's contents for confirmation.
            PrintHeader("Add Data");
            Console.WriteLine("Adding 4 items to the list.");
            myList.Add("Arrows");
            myList.Add("Biscuits");
            myList.Add("Crowbar");
            myList.Add("Day of rations");
            PrintListContents(myList);

            // Enter gets to the next section
            MoveToNextSection("testing the removal operations");
            #endregion

            #region Removing Data
            // Test removing an invalid index of -1
            try
            {
                RemoveAnItem(myList, "invalid", -1);
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }

            // Test removing a too-large index
            try
            {
                RemoveAnItem(myList, "invalid", myList.Count);
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }

            // Test removing an internal node
            RemoveAnItem(myList, "second", 1);
            PrintListContents(myList);

            // Test removing the head
            RemoveAnItem(myList, "head", 0);
            PrintListContents(myList);

            // Test removing the tail
            RemoveAnItem(myList, "tail", myList.Count - 1);
            PrintListContents(myList);

            // Test removing the only item in the list
            RemoveAnItem(myList, "only", myList.Count - 1);
            PrintListContents(myList);

            // Test removing any index from an empty list
            try
            {
                RemoveAnItem(myList, "invalid", 0);
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }

            // Enter gets to the next section
            MoveToNextSection("testing the insertion operations");
            #endregion

            #region Inserting Data
            // Test Insertion into an empty list
            InsertItem(myList, "Emblem", 0);
            PrintListContents(myList);

            // Add more data to the list
            PrintHeader("Add Data");
            Console.WriteLine("Add 'Flask' and 'Grappling hook' into the list.");
            myList.Add("Flask");
            myList.Add("Grappling hook");
            PrintListContents(myList);

            // Test Insertion into the head
            InsertItem(myList, "Hammer", 0);
            PrintListContents(myList);

            // Test Insertion at the tail
            InsertItem(myList, "Incense", myList.Count);
            PrintListContents(myList);

            // Test Insertion into the middle
            InsertItem(myList, "Jug of water", 2);
            PrintListContents(myList);

            // Test inserting an invalid index
            try
            {
                InsertItem(myList, "K", -1);
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }

            // Test inserting a too-large index
            try
            {
                InsertItem(myList, "K", myList.Count + 1);
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }

            // Enter gets to the next section
            MoveToNextSection("testing the get indexer");
            #endregion

            #region Get Data
            // Test data retrieval at specific indices.
            GetData(myList, 0);
            GetData(myList, 2);
            GetData(myList, myList.Count - 1);

            // Test getting a negative index
            try
            {
                GetData(myList, -1);
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }

            // Test getting a too-large index
            try
            {
                GetData(myList, myList.Count);
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }

            // Enter gets to the next section
            MoveToNextSection("testing the set indexer");
            #endregion

            #region Set Data
            // Test data setting at specific indices.
            SetData(myList, 0, "Lantern");
            SetData(myList, 2, "Mirror");
            SetData(myList, myList.Count - 1, "Net");
            PrintListContents(myList);

            // Test setting a negative index
            try
            {
                SetData(myList, -1, "O");
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }

            // Test setting a too-large index
            try
            {
                SetData(myList, myList.Count, "O");
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }

            // Enter gets to the next section
            MoveToNextSection("testing clearing of the list");
            #endregion

            #region Clear
            // Test clearing the list
            PrintHeader("Clear the list");
            myList.Clear();
            PrintListContents(myList);

            // Enter gets to the next section
            MoveToNextSection("adding more data");
            #endregion

            #region Last Addition After Clear
            // Test adding data again, then print the list's contents for confirmation.
            PrintHeader("Add Data");
            Console.WriteLine("Adding 2 items to the list.");
            myList.Add("Pitons");
            myList.Add("Rope");
            PrintListContents(myList);
            #endregion

            #region Stack & Queue Implementation with DoublyLinkedLists
            // ---------------------------------------------------------------
            // Part 1: GameStack Testing
            // ---------------------------------------------------------------

            Console.WriteLine("---------------------------------" +
                              "------------------------------");
            Console.WriteLine("TESTING THE GAME STACK");
            Console.WriteLine("---------------------------------" +
                              "------------------------------");

            // 1) Create a GameStack object that holds strings
            Stack<string> castSpells = new Stack<string>();

            try
            {
                // 2) Add 4 spells to the stack(Push) and print it using Peek
                Console.WriteLine
                    ("The following spells are being put on the stack:");

                castSpells.Push("Shock");
                Console.WriteLine("- " + castSpells.Peek());
                castSpells.Push("Fork");
                Console.WriteLine("- " + castSpells.Peek());
                castSpells.Push("Counterspell");
                Console.WriteLine("- " + castSpells.Peek());
                castSpells.Push("Force of Will");
                Console.WriteLine("- " + castSpells.Peek() + " \n");

                // 3) Print the Count of the stack
                Console.WriteLine($"There are {castSpells.Count} " +
                                  $"spells in the stack.\n");

                // 4) Remove all spells from the stack until the stack is empty
                Console.WriteLine("Spells resolving in reverse order:");

                while (!castSpells.IsEmpty)
                {
                    Console.WriteLine("- " + castSpells.Pop());
                }
                Console.WriteLine();

                // 5) Print the Count of the stack again
                Console.WriteLine($"There are {castSpells.Count} " +
                                  $"spells in the stack.\n");

                // 6) Pop the EMPTY stack in a try/catch block,
                //    printing the exception message
                castSpells.Pop();
            }
            catch (Exception error)
            {
                Console.WriteLine
                    ("Error occurred in Main: " + error.Message);
            }

            try
            {
                // 7) Peek the EMPTY stack in a try/catch block,
                //    printing the exception message
                castSpells.Peek();
            }
            catch (Exception error)
            {
                Console.WriteLine
                    ("Error occurred in Main: " + error.Message);
            }

            // White space before the next activity
            Console.WriteLine();


            // ---------------------------------------------------------------
            // Part 2: GameQueue Testing
            // ---------------------------------------------------------------

            Console.WriteLine("---------------------------------" +
                              "------------------------------");
            Console.WriteLine("TESTING THE GAME QUEUE");
            Console.WriteLine("---------------------------------" +
                              "------------------------------");

            // 1) Create a GameQueue object that holds strings
            Queue<string> playerQueue = new Queue<string>();

            try
            {
                // 2) Add 4 player names to the queue
                //    and print the name returned from Peek
                Console.WriteLine
                    ("The following players are joining the queue:");

                playerQueue.Enqueue("GandalfThePurple");
                Console.WriteLine("- " + playerQueue.Peek());
                playerQueue.Enqueue("SporkNinja");
                Console.WriteLine("- " + playerQueue.Peek());
                playerQueue.Enqueue("TacticalTurtle");
                Console.WriteLine("- " + playerQueue.Peek());
                playerQueue.Enqueue("LaggyMcLagz");
                Console.WriteLine("- " + playerQueue.Peek() + "\n");

                // 3) Print the Count of the queue
                Console.WriteLine
                    ($"There are {playerQueue.Count} players in the queue.\n");

                // 4) Remove all players from the queue and
                //    print the Count of players left in the queue.
                while (!playerQueue.IsEmpty)
                {
                    Console.WriteLine
                        ($"\"{playerQueue.Dequeue()}\" has joined the server: "
                        + $"{playerQueue.Count} player(s) left in queue");
                }
                Console.WriteLine();

                // 5) Dequeue the EMPTY queue in a try/catch block,
                //    printing the exception message 
                playerQueue.Dequeue();
            }
            catch (Exception error)
            {
                Console.WriteLine
                    ("Error occurred in Main: " + error.Message);
            }

            try
            {
                // 6) Peek the EMPTY queue in a try/catch block,
                //    printing the exception message
                playerQueue.Peek();
            }
            catch (Exception error)
            {
                Console.WriteLine
                    ("Error occurred in Main: " + error.Message);
            }

            #endregion
        }

        /// <summary>
        /// Tests item removal from the doubly linked list.
        /// </summary>
        /// <param name="myList">Doubly linked list of type CustomLinkedList<T></param>
        /// <param name="nodeToRemove">Description of the order the removed node is, like "first" or "invalid"</param>
        /// <param name="index">INdex number of node to remove.</param>
        public static void RemoveAnItem(CustomLinkedList<string> myList, string nodeToRemove, int index)
        {
            PrintHeader("Remove Data");
            Console.WriteLine($"Remove the {nodeToRemove} node at index {index}.");
            Console.WriteLine($"\"{myList.RemoveAt(index)}\" was removed from the list.");
        }


        /// <summary>
        /// Tests item addition in a doubly linked list.
        /// </summary>
        /// <param name="myList">Doubly linked list of type CustomLinkedList<T></param>
        /// <param name="itemToInsert">String data to be inserted into the list.</param>
        /// <param name="index">Zero-based index, position at which to insert.</param>
        public static void InsertItem(CustomLinkedList<string> myList, string itemToInsert, int index)
        {
            PrintHeader("Insert Data");
            Console.WriteLine($"Inserting {itemToInsert} at index {index}.");
            myList.Insert(itemToInsert, index);
        }


        /// <summary>
        /// Tests data retrieval with an indexer.
        /// </summary>
        /// <param name="myList">Doubly linked list of type CustomLinkedList<T></param>
        /// <param name="index">Zero-based index of data to retrieve.</param>
        public static void GetData(CustomLinkedList<string> myList, int index)
        {
            PrintHeader("Get data");
            Console.WriteLine($"The data at index {index} is '{myList[index]}'.");
        }


        /// <summary>
        /// Tests data setting with an indexer.
        /// </summary>
        /// <param name="myList">Doubly linked list of type CustomLinkedList<T></param>
        /// <param name="index">Zero-based index of data to set.</param>
        /// <param name="itemToSet">String, the value to enter into the specified index</param>
        public static void SetData(CustomLinkedList<string> myList, int index, string itemToSet)
        {
            PrintHeader("Set data");
            Console.WriteLine($"Changing the data at index {index} to be '{itemToSet}'.");
            myList[index] = itemToSet;
        }


        /// <summary>
        /// Prints all of the data in a doubly linked list in forward and backward manner.
        /// </summary>
        /// <param name="myList">Doubly linked list of type CustomLinkedList<T></param>
        public static void PrintListContents(CustomLinkedList<string> myList)
        {
            PrintHeader("Check the list contents");
            Console.WriteLine($"The list contains {myList.Count} item(s).");

            Console.WriteLine("\n--> Here are the items (in forward order):");
            myList.PrintForward();

            Console.WriteLine("\n--> Here are the items (in backward order):");
            myList.PrintBackward();
        }


        /// <summary>
        /// Draws an instruction header of asterisks.
        /// </summary>
        /// <param name="description"></param>
        public static void PrintHeader(string description)
        {
            // Set the header color
            Console.ForegroundColor = ConsoleColor.White;

            // FIRST LINE:
            // Print first line of the header
            string headerLine = "************************************************";
            Console.WriteLine("\n\n" + headerLine);

            // NEXT LINE:
            // Determine the second line's trailing asterisks
            // The total line length - (5 stars in the beginning, 2 spaces, and the description)
            int numberOfStars = headerLine.Length - (5 + 2 + description.Length);

            // Print the leading asterisks
            Console.Write("*****");

            // Print the description in a different color
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write($" {description.ToUpper()} ");
            Console.ForegroundColor = ConsoleColor.White;

            // Print the remaining number of asterisks
            Console.WriteLine(new string('*', numberOfStars));

            // LAST LINE:
            // Full line of asterisks
            Console.WriteLine(headerLine);

            // Reset back to non-header color
            Console.ForegroundColor = ConsoleColor.Gray;
        }


        /// <summary>
        /// Prints red text with a prompt for user to see the next series of testing code.
        /// </summary>
        /// <param name="prompt">Description of the testing code.</param>
        public static void MoveToNextSection(string prompt)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"\n--> Ready for {prompt.ToUpper()}? Press any key. <--");
            Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
    
}
