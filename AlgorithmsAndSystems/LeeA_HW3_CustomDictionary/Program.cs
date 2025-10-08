
/* Amy Lee
 * 02/28/2025
 * Constructing a Custom Dictionary;
 * deepening the understanding of Data Structures */

namespace LeeA_HW3_CustomDictionary
{
    /// <summary>
    /// Purpose: To remind myself to use Enum Parsing and also make 
    ///          the comparision inside Switch statement more efficient
    /// </summary>
    enum MenuOption
    {
        // Print the current count of items in the dictionary
        Count,

        // Print the current load factor of the dictionary
        LoadFactor,

        // Ask the user for a key and a value and store them in the dictionary
        // ** Print exceptions if necessary
        Add,

        // Ask the user for a key and attempt to remove it.
        // ** Inform the user if it worked
        Remove,

        // Ask the user for a key. Print the value of the key,
        // or inform that the key is not contained in the dictionary
        Get,

        // Ask the user for a key and a value. Set the entry to that value
        Set,

        // Clear the dictionary
        Clear,

        // Terminate the loop and the program
        Quit
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // ---------------------------------------------------------------
            // Main Method_Testing the Dictionary
            // ---------------------------------------------------------------

            // 1) Initializing a Custom Dictionary Object
            // ** For testing, set the initial size to 5
            CustomDictionary<string, string> foodFlavors 
                = new CustomDictionary<string, string>(5);

            // 2) Seed the Dictionary with 6 initial key/value pairs
            // --> This ensures that the load factor is greater than 1.0.
            // --> At least ONE collision has occurred.
            // ** Hardcoded
            foodFlavors.Add("pizza", "cheese");
            foodFlavors.Add("hamburger", "chicken");
            foodFlavors.Add("taco", "pork");
            foodFlavors["milkshake"] = "cookies and cream";
            foodFlavors["stir-fry"] = "beef";
            foodFlavors["pretzel"] = "spinach and feta";

            // 3) Main Loop for testing!

            // A MenuOption variable to store the user based choice
            MenuOption userChoice;

            // A boolean variable to control whether to exit the loop or not
            bool quit = false;
            bool parsed = false;

            do
            {
                // Display the actions the user can choose
                Console.WriteLine("Custom Dictionary Menu: Count  LoadFactor  Add  " +
                                  "Remove  Get  Set  Clear  Quit");

                // Prompt for userChoice
                Console.Write(">> ");
                parsed = Enum.TryParse(Console.ReadLine()!, out userChoice);

                // If user choice is invalid, reprompt for a correct value
                while(!parsed)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("The given menu option cannot be executed.\n");
                    Console.Write("Enter a valid option displayed above. >> ");
                    Console.ForegroundColor = ConsoleColor.White;

                    parsed = Enum.TryParse(Console.ReadLine()!, out userChoice);
                }

                // A local variable to store the user's input of key and values
                // inside the switch statement (just in case it is needed)
                string key = "";
                string value = "";

                // Execute the menu option of user's choice
                switch(userChoice)
                {
                    case MenuOption.Count:
                        // Inform the user of the current count of items in the dictionary
                        Console.WriteLine
                            ($"The dictionary has {foodFlavors.Count} entries.\n");
                        break;

                    case MenuOption.LoadFactor:
                        // Inform the user of the current load factor of the dictionary
                        #region Notes about LoadFactor
                        // ** A measure of how FULL the dictionary is.
                        // ** Calculation is done by (number of entries) / (number of buckets).
                        #endregion
                        Console.WriteLine
                            ($"The dictionary has a load factor of " +
                             $"{foodFlavors.LoadFactor}.\n");
                        break;

                    case MenuOption.Add:
                        // Ask the user for a key, then a value
                        Console.Write("Type a key: ");
                        key = Console.ReadLine()!;
                        Console.Write("Type a value: ");
                        value = Console.ReadLine()!;

                        // Attempt adding the pair to the dictionary using the Add method
                        // ** Catch exceptions if necessary
                        try
                        {
                            foodFlavors.Add(key, value);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"The key '{key}' was added\n");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        catch(Exception error)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(error.Message + "\n");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        break;

                    case MenuOption.Remove:
                        // Ask user for a key
                        Console.Write("Type a key: ");
                        key = Console.ReadLine()!;

                        // Attempt to remove it from the dictionary
                        // and inform whether it worked
                        if(foodFlavors.Remove(key))
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine($"The key '{key}' was removed.\n");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine
                                ($"The key '{key}' is not in the dictionary.\n");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        break;

                    case MenuOption.Get:
                        // Ask the user for a key
                        Console.Write("Type a key: ");
                        key = Console.ReadLine()!;

                        // Use ContainsKey method to determine whether the key exists
                        if(foodFlavors.ContainsKey(key))
                        {
                            // If the key exists, print the
                            // corresponding value at that key
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"Value is: '{foodFlavors[key]}'\n");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            // If the key does not exist, inform the user
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("The given key does not exist.\n");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        break;

                    case MenuOption.Set:
                        // Ask the user for a key, then a value
                        Console.Write("Type a key: ");
                        key = Console.ReadLine()!;
                        Console.Write("Type a value: ");
                        value = Console.ReadLine()!;

                        // Set the entry at the given key to the specified value.
                        // [ The informing message to the user differs from
                        // whether the key existed in the dictionary or not
                        // before the indexer property does its job ]
                        if(foodFlavors.ContainsKey(key))
                        {
                            foodFlavors[key] = value;
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine
                                ($"The value was changed for the key '{key}'.\n");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            foodFlavors[key] = value;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"The key '{key}' was added.\n");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        break;

                    case MenuOption.Clear:
                        // Clear the dictionary and inform the user
                        foodFlavors.Clear();
                        Console.WriteLine("The dictionary was cleared.\n");
                        break;

                    case MenuOption.Quit:
                        // Update the loop control variable
                        quit = true;

                        // Inform the user
                        Console.WriteLine("Goodbye!");
                        break;
                }

                // Before the loop starts again,
                // reset the parsing boolean value to false
                // for correct input checking
                parsed = false;
            }
            while(!quit);
        }
    }
}
