using System;
using System.Collections.Generic;

/* Amy Lee
 * 04/21/2025
 * Implement Dijkstra's Algorithm, including any changes to 
 * the existing Graph/Vertex classes and any helper methods. */

namespace PE_Dijkstra_sAlgorithm
{
    /// <summary>
    /// Vertex Class
    /// Purpose: A simple class that holds data of this particular node.
    /// </summary>
    internal class Vertex
    {
        //-----------------------------------------------------------------
        // Fields
        //-----------------------------------------------------------------

        // A string field to represent the name of the room
        private string name;

        // A string field to store the description of the room
        private string description;

        //-----------------------------------------------------------------
        // Property
        //-----------------------------------------------------------------
        public string Name
        {
            get { return name; }
        }


        //-----------------------------------------------------------------
        // Constructor
        //-----------------------------------------------------------------
        /// <summary>
        /// Passes in the desired information for this single vertex
        /// </summary>
        /// <param name="name">The name of the data</param>
        /// <param name="description">An explanation of the data</param>
        public Vertex(string name, string description)
        {
            this.name = name;
            this.description = description;
        }


        //-----------------------------------------------------------------
        // Class Method 
        //-----------------------------------------------------------------

        /// <summary>
        /// Returns a string description of a vertex in the desired format
        /// </summary>
        /// <returns>A string description of a vertex in the desired format</returns>
        public override string ToString()
        {
            return $"{name.ToUpper()}: {description}.";
        }
    }
}
