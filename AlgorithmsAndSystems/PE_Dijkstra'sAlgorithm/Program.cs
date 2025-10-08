
/* Amy Lee
 * 04/21/2025
 * Implement Dijkstra's Algorithm, including any changes to 
 * the existing Graph/Vertex classes and any helper methods. */

namespace PE_Dijkstra_sAlgorithm
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region PE_Graphs
            // Create the Graph object and a string variable to store the current room
            Graph map = new Graph("../../../rooms.txt");
            // string currentRoom = "hall";
            // string previousRoom = "";

            // A boolean variable to reprompt the user for a correct input when necessary
            // bool exists = false;
            // bool isAdjacent = false;

            // Print all rooms in the map
            // map.ListAllVertices();
            // Console.WriteLine();

            // Until the user reaches the exit, they are not going out this mansion.
            /* while (currentRoom != "exit")
            {
                // Inform the user the current room they are in.
                Console.WriteLine($"\nYou are currently in the {currentRoom}.");

                // Get the current room's adjacency list
                List<Vertex> currentAdjacencyList = map.GetAdjacentList(currentRoom);

                // Iterate through the adjacency list
                // to inform the user of the choices they have
                Console.Write("Nearby are the  ");

                for (int i = 0; i < currentAdjacencyList.Count; i++)
                {
                    Console.Write($" - {currentAdjacencyList[i].Name}    ");
                }

                // Prompt the user to make a choice of a room
                Console.Write("\nWhere would you like to go? ");
                previousRoom = currentRoom;
                currentRoom = Console.ReadLine()!.ToLower();

                // Check whether this room is in the map
                exists = map.MapContainsRoom(currentRoom);

                // Check whether this room is adjacent to the previous input
                isAdjacent = map.AreAdjacent(previousRoom, currentRoom);

                // Ensure that the user given room EXISTS && is ADJACENT.
                while (!exists || !isAdjacent)
                {
                    // When the room does not exist, keep reprompting the user for a correct input
                    while (!exists)
                    {
                        Console.WriteLine($"\nSorry, '{currentRoom}' is not a room in the map.");
                        Console.Write("Where would you like to go? ");
                        currentRoom = Console.ReadLine()!.ToLower();
                        exists = map.MapContainsRoom(currentRoom);
                    }

                    // !! Even though the room is in the map, it might NOT be ADJACENT to this room !!
                    while (!isAdjacent)
                    {
                        Console.WriteLine($"\nSorry, '{currentRoom}' is not adjacent to the {previousRoom}.");
                        Console.Write("Where would you like to go? ");
                        currentRoom = Console.ReadLine()!.ToLower();
                        exists = map.MapContainsRoom(currentRoom);
                        isAdjacent = map.AreAdjacent(previousRoom, currentRoom);

                        // To print the correct message,
                        // exit this loop if the room doesn't exist (of course it is not adjacent either.)
                        // (Get the error message priority correct)
                        if (!exists)
                        {
                            break;
                        }
                    }
                }
            }

            // When the while loop ends, it means that we have successfully exited the mansion!
            Console.WriteLine("\nYou have successfully left the mansion."); */
            #endregion

            #region PE_GraphSearching_BFS
            // Continued from the previous PE

            // 1) Call the BreathFirst() Method using the mainhall as the first room
            // map.BreathFirst("hall");
            // Console.WriteLine();

            // 2) Call BreathFirst() again to test the reset method
            // map.BreathFirst("hall");
            #endregion

            #region PE_Dijkstra's Algorithm
            // A local string field to keep track of user input
            string userInput = "";
            
            // First search
            Console.WriteLine("-----------------------------------------------------------------");
            Console.WriteLine("Search 1: Starting in the hall.");
            Console.Write("Which room are you trying to get to? > ");
            userInput = Console.ReadLine()!;

            // Validate user input
            while(!map.MapContainsRoom(userInput))
            {
                Console.WriteLine("\nSorry, that is not a room.");
                Console.Write("Which room are you trying to get to? > ");
                userInput = Console.ReadLine()!;
            }
            Console.WriteLine();

            // Execute shortest path finding
            map.ShortestPath("hall");

            // Print out the shortest path
            map.PrintShortestPath(userInput);
            Console.WriteLine();


            // Second search
            Console.WriteLine("-----------------------------------------------------------------");
            Console.WriteLine("Search 2: Starting in the conservatory.");
            Console.Write("Which room are you trying to get to? > ");
            userInput = Console.ReadLine()!;

            // Validate user input
            while (!map.MapContainsRoom(userInput))
            {
                Console.WriteLine("\nSorry, that is not a room.");
                Console.Write("Which room are you trying to get to? > ");
                userInput = Console.ReadLine()!;
            }
            Console.WriteLine();

            // Execute shortest path finding
            map.ShortestPath("conservatory");

            // Print out the shortest path
            map.PrintShortestPath(userInput);
            Console.WriteLine();
            #endregion
        }
    }
}
