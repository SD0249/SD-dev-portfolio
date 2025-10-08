
/* Amy Lee
 * 02/21/2025
 * Constructing Custom Stacks & Queues to gain a better understanding
 * of how each of these data structures work underhood */

namespace PE_Custom_Stacks___Queues
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // ---------------------------------------------------------------
            // Part 1: GameStack Testing
            // ---------------------------------------------------------------

            Console.WriteLine("---------------------------------" +
                              "------------------------------");
            Console.WriteLine("TESTING THE GAME STACK");
            Console.WriteLine("---------------------------------" +
                              "------------------------------");

            // 1) Create a GameStack object that holds strings
            GameStack<string> castSpells = new GameStack<string>();

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

                while(!castSpells.IsEmpty)
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
            catch(Exception error)
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
            GameQueue<string> playerQueue = new GameQueue<string>();

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
                while(!playerQueue.IsEmpty)
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
            catch(Exception error)
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
        }
    }
}
