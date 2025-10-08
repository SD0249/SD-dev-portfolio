using System;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

/* Amy Lee
 * 04/21/2025
 * Implement Dijkstra's Algorithm, including any changes to 
 * the existing Graph/Vertex classes and any helper methods. */

namespace PE_Dijkstra_sAlgorithm
{
    /// <summary>
    /// Graph Class
    /// Purpose: Contains information about the vertices and their relationships.
    ///          Also can perform searches(BFS currently).
    /// </summary>
    internal class Graph
    {
        //-----------------------------------------------------------------
        // Fields
        //-----------------------------------------------------------------

        // A List field that stores ALL the VERTICES in the graph
        // --> Vertex retrieval via an index. Great after we FIND an INDEX
        private List<Vertex> vertices;

        // An ADJACENECY LIST! 
        // --> Great for finding a SPECIFIC vertex
        private Dictionary<string, List<Vertex>> adjacencyList;

        // An ADJACENCY MATRIX!
        // --> QUICK EDGE lookups (relationship between two vertices)
        private int[,] adjMatrix;

        // ** For BFS or DFS (Doesn't matter for Dijkstra's Algorithm;
        //                    we can visit a single vertex multiple times to find the shortest path!)
        // A 1D array of booleans which contains whether this vertex has been visited.
        private bool[] visited;

        // ** Additional Fields for DIJKSTRA'S ALGORITHM
        // --> Having this information in the graph class makes it easier to make SPECIFIC methods.
        //     However, for convienience, having these as properties of each vertices might be easier?
        //     Or maybe not. We need indices no matter how.

        // An array of doubles for shortest distances from the starting vertex to this vertex
        private double[] shortestDistances;

        // An array of vertices for the path neighbor
        private Vertex[] pathNeighbor;

        // An array of booleans to represent permanency(finished or not)
        private bool[] permanent;


        //-----------------------------------------------------------------
        // NO PROPERTIES
        // --> The Graph class should do all the inner work inside here.
        //-----------------------------------------------------------------


        //-----------------------------------------------------------------
        // Constructor
        //-----------------------------------------------------------------
        /// <summary>
        /// The largest constructor ever.
        /// Initializes all the fields of the Graph class.
        /// </summary>
        /// <param name="filePath">The file to read all the vertex and its information 
        /// including its relationship with other vertices.</param>
        public Graph(string filePath)
        {
            // !! Initialize the vertex list & the adjacency list !!
            vertices = new List<Vertex>();
            adjacencyList = new Dictionary<string, List<Vertex>>();

            // Reading data from the file.
            try
            {
                // Open the file to read
                StreamReader reader = new StreamReader(filePath);

                // A local string array and string variable to store information temporarily
                // A local counter variable is added to keep track of how many data was read.
                // When the information for the vertices list is all read, the counter will reach 0.
                // ** The counter should always be modified when adding more vertices to the graph. **
                string line = "";
                string[] splitData;
                int counter = 7;

                while ((line = reader.ReadLine()!) != null)
                {
                    // Ignore the information & format lines
                    if (!line.StartsWith('-'))
                    {
                        splitData = line.Split('|');

                        // I. Populating the Vertex list from the file.
                        if (counter != 0)
                        {
                            // Create and store a new vertex object in the vertex list 
                            // --> The first object represents the name and
                            //     the second object represents the description.
                            vertices.Add(new Vertex(splitData[0], splitData[1]));

                            // Update the control variable(counter)
                            counter--;
                        }
                        // II. Populating the adjacency list from the file
                        // (Actually this might be better to implement after we learn the searching process..)
                        else
                        {
                            // When the counter has reached 0,
                            // it means that now we have reached the point to populate the adjacency list
                            // and the vertices list has been correctly populated.

                            // !! Initialize the adjacency matrix after
                            //    populating the vertex list, ONLY ONCE
                            if (adjMatrix == null)
                            {
                                adjMatrix = new int[vertices.Count, vertices.Count];
                            }

                            // ADDITIONAL STUFF --> WEIGHT INFORMATION INCLUDED FOR DIJKSTRA'S ALGORITHM
                            //                      Populating the adjacency matrix

                            // Split Action 1 to get rooms and every cost information (weight)
                            string[] adjacentRooms = splitData[1].Split(',');

                            // Split Action 2 to separate room and cost information.
                            string[] roomInformation = null;

                            // Create a new List<Vertex> to store all this information
                            // and give reference to the adjacency list by the key
                            List<Vertex> roomRelations = new List<Vertex>();

                            for (int i = 0; i < adjacentRooms.Length; i++)
                            {
                                // Split each room/cost block and save it temporarily
                                roomInformation = adjacentRooms[i].Split("$");

                                // Add the first element of the roomInformation array to the roomRelations list
                                int adjRoomIndex = GetVertexIndex(roomInformation[0]);
                                roomRelations.Add(vertices[adjRoomIndex]);

                                // III. Populate the adjacency matrix with edge data
                                // The second element of the roomInformation array is the cost of the edge
                                adjMatrix[GetVertexIndex(splitData[0]), adjRoomIndex] = int.Parse(roomInformation[1]);
                            }

                            // Now finally, we can populate the adjacency list with the correct data
                            adjacencyList.Add(splitData[0], roomRelations);
                        }
                    }
                }

                // Close the reader
                reader.Close();
            }
            catch (Exception error)
            {
                System.Diagnostics.Debug.WriteLine("There was an error in initializing data.");
                System.Diagnostics.Debug.WriteLine($"For detailed information, check the following: {error.Message}");
            }

            // IV. Initialize the visited boolean array
            visited = new bool[vertices.Count];

            //     Set all the values of the boolean array to false
            //     BEFORE SEARCH (BFS & DFS)
            for (int i = 0; i < vertices.Count; i++)
            {
                visited[i] = false;
            }

            // V. Initialize all the fields that will be used for Dijkstra's Algorithm run
            shortestDistances = new double[vertices.Count];
            pathNeighbor = new Vertex[vertices.Count];
            permanent = new bool[vertices.Count];

            // Resetting the total distance from starting vertex for every vertex
            for (int i = 0; i < vertices.Count; i++)
            {
                // All distances start at "INFINITY"
                shortestDistances[i] = double.MaxValue;
            }

            // Resetting the path neighbor for every vertex
            for (int i = 0; i < vertices.Count; i++)
            {
                // Reset all the path neighbors to null
                pathNeighbor[i] = null;
            }

            // Resetting the permanency for every vertex
            for (int i = 0; i < vertices.Count; i++)
            {
                // Reset all the permanency to false
                permanent[i] = false;
            }
        }


        //-----------------------------------------------------------------
        // Class Methods
        //-----------------------------------------------------------------

        /// <summary>
        /// Iterates through all of the vertices using the list, 
        /// printing each vertex to the console window
        /// </summary>
        public void ListAllVertices()
        {
            foreach (Vertex vertex in vertices)
            {
                Console.WriteLine(vertex);
            }
        }


        /// <summary>
        /// Returns a boolean value of whether this room exists in the map
        /// </summary>
        /// <param name="room">The room object in search for.</param>
        /// <returns>A boolean value of whether this room exists in the map</returns>
        public bool MapContainsRoom(string room)
        {
            // If the room is a key for the adjacency list,
            // then we know that the room is on the map.
            // This is good because it is O(1) time!
            if (adjacencyList.ContainsKey(room))
            {
                return true;
            }

            // If the room is not a key for the adjacency list.
            return false;
        }


        /// <summary>
        /// Returns a boolean variable of 
        /// whether there is a edge between these two vertices
        /// </summary>
        /// <param name="firstRoom">The first vertex object</param>
        /// <param name="secondRoom">The second vertex object to compare</param>
        /// <returns>A boolean variable of 
        /// whether there is a edge between these two vertices</returns>
        public bool AreAdjacent(string firstRoom, string secondRoom)
        {
            if (adjacencyList.ContainsKey(firstRoom))
            {
                // A local vertex list to get reference to the bucket of the first room.
                List<Vertex> bucket = adjacencyList[firstRoom];

                // Search through the list whether it contains the vertex of this other room
                foreach (Vertex vertex in bucket)
                {
                    // There is a relation between the two!
                    if (vertex.Name == secondRoom)
                    {
                        return true;
                    }
                }
            }
            // If the method reaches this point, it MUST return false.
            return false;
        }


        /// <summary>
        /// Returns a list of adjacent vertices to the parameter vertex
        /// If the specified room isn't in the list, return null.
        /// </summary>
        /// <param name="room">The vertex to get its bucket from</param>
        /// <returns>A list of adjacent vertices to the parameter vertex</returns>
        private List<Vertex> GetAdjacentList(string room)
        {
            // If the adjacency list contains this vertex
            if (adjacencyList.ContainsKey(room))
            {
                // Return its bucket
                return adjacencyList[room];
            }
            // If the adjacency list doesn't contain this vertex
            else
            {
                return null;
            }
        }


        /// <summary>
        /// Resets all the fields of the graph class to 
        /// conduct another search or pathfinding algorithm
        /// </summary>
        private void Reset()
        {
            // ** For DFS or BFS
            // Set every value in the boolean array(visited) to false
            for (int i = 0; i < vertices.Count; i++)
            {
                visited[i] = false;
            }

            // ** For Dijkstra's Algorithm

            // Resetting the total distance from starting vertex for every vertex
            for (int i = 0; i < vertices.Count; i++)
            {
                // All distances start at "INFINITY"
                shortestDistances[i] = double.MaxValue;
            }

            // Resetting the path neighbor for every vertex
            for (int i = 0; i < vertices.Count; i++)
            {
                // Reset all the path neighbors to null
                pathNeighbor[i] = null;
            }

            // Resetting the permanency for every vertex
            for (int i = 0; i < vertices.Count; i++)
            {
                // Reset all the permanency to false
                permanent[i] = false;
            }
        }


        /// <summary>
        /// Returns an ADJACENT, UNVISITED neighbor of the specific vertex
        /// </summary>
        /// <param name="roomName">Name of the room to retrieve index</param>
        /// <returns>An ADJACENT, UNVISITED neighbor of the specific vertex</returns>
        private Vertex GetAdjacenctUnvisited(string roomName)
        {
            // 1) Find the index of a Vertex with a specific room name
            //    ** If the target index stays -1, then it means that
            //       this room doesn't exist in this mansion.
            int targetIndex = -1;

            for (int i = 0; i < vertices.Count; i++)
            {
                if (vertices[i].Name == roomName)
                {
                    targetIndex = i;
                    break;
                }
            }

            // !! If after this process, the target index is -1, return null.
            if (targetIndex == -1)
            {
                return null;
            }

            // 2) Use the adjacency matrix to return an adjacent, unvisited neighbor
            //    of this specific vertex if it exists.

            // Using the index found above, find the adjacent rooms and
            // return them if they are NOT VISITED
            for (int c = 0; c < vertices.Count; c++)
            {
                // The neighbor is adjacent to the specific vertex
                if (adjMatrix[targetIndex, c] == 1)
                {
                    // If the neightbor is not visited
                    if (!visited[c])
                    {
                        return vertices[c];
                    }
                }
            }

            // 3) If the method hasn't returned anything until this point,
            //    this vertex doesn't have anymore adjacent and unvisited neighbors.
            return null;
        }


        /// <summary>
        /// Perform a BreathFirstSearch starting from the given vertex.
        /// </summary>
        /// <param name="roomName">Name of the room(Starting Vertex)</param>
        public void BreathFirst(string roomName)
        {
            // 0) Check whether the given room is a valid room
            //    (whether it exists)
            if (!adjacencyList.ContainsKey(roomName))
            {
                // Throw an exception and end the method
                throw new Exception($"The room {roomName} is not a valid room.");
            }

            // 1) Retrieve this vertex from name by the vertices list
            // (Current Index is for the easy access of adjacency matrix)
            Vertex current = null;
            int currentIndex = -1;

            for (int i = 0; i < vertices.Count; i++)
            {
                if (vertices[i].Name == roomName)
                {
                    current = vertices[i];
                    currentIndex = i;
                    break;
                }
            }

            // 2) Reset all vertices (visit records)
            Reset();

            // 3) Create a local Queue<Vertex> to keep track of vertices during the search
            Queue<Vertex> BFS = new Queue<Vertex>();

            // 4) Print the current vertex name, ENQUEUE it, and mark as VISITED
            Console.WriteLine($"Visited {roomName}");
            BFS.Enqueue(current!);
            visited[currentIndex] = true;

            // 5) ITERATIVE PROCESS; as long as there is DATA in the queue..
            while (BFS.Count != 0)
            {
                // Objective: Get an adjacent, unvisited Vertex of
                //            the Vertex in front of the queue.

                // If there is one
                if ((current = GetAdjacenctUnvisited(BFS.Peek().Name)) != null)
                {
                    // !! Don't forget to update the current index too.
                    //   [Better if I would have made a helper method for this eventually.]
                    for (int i = 0; i < vertices.Count; i++)
                    {
                        if (vertices[i].Name == current.Name)
                        {
                            currentIndex = i;
                            break;
                        }
                    }

                    // Print the vertex name, ENQUEUE it, and mark as VISITED
                    Console.WriteLine($"Visited {current.Name}");
                    BFS.Enqueue(current!);
                    visited[currentIndex] = true;
                }
                // If there isn't one
                else
                {
                    // Dequeue THIS vertex
                    // --> All the neighbor of this current vertex has been visited.
                    BFS.Dequeue();

                    // Now the newest current Vertex is at the front of the queue!
                }
            }

            // End of search..
        }


        /// <summary>
        /// Prints the shortest path along with its cost 
        /// by running through Dijkstra's shortest path algorithm.
        /// Finds the shortest path to EVERY vertices from the STARTING vertex.
        /// </summary>
        /// <param name="roomName">Room Name of the starting vertex</param>
        public void ShortestPath(string roomName)
        {
            // 0) Before performing search, RESET all data
            Reset();

            // 1) Retrieve the index of this room vertex
            int startingVertex = GetVertexIndex(roomName);

            // 2) Update the starting vertex's information
            // 2-1) Mark it permanent
            permanent[startingVertex] = true;

            // 2-2) Set its distance to zero
            shortestDistances[startingVertex] = 0;

            // ** An integer variable to store the index of the current working vertex
            //    (Starting vertex becomes the first current working vertex)
            int currentVertex = startingVertex;

            // !! 3) WHILE NON-PERMANENT vertices exist within the graph
            while (!CheckPermanency(permanent))
            {
                // A local list of vertices to store the non-permanent ones
                List<Vertex> nonpermanents = NonpermanentVertices(permanent);

                // Iterate through the non-permanent vertices
                for(int i = 0; i < nonpermanents.Count; i++)
                {
                    // Check whether it is ADJACENT to the currently working vertex
                    if (AreAdjacent(vertices[currentVertex].Name, nonpermanents[i].Name))
                    {
                        // A local integer variable to store the index of this non-permanent vertex
                        int index = GetVertexIndex(nonpermanents[i].Name);

                        // COMPARE the shortest distance stored in this non-permanent vertex to
                        // the distance between this vertex and the current working vertex
                        if (shortestDistances[currentVertex] + adjMatrix[currentVertex, index] < shortestDistances[index])
                        {
                            // If the distance between this vertex and the current working vertex is SMALLER
                            // than the shortest distance stored in this non-permanent vertex, CHANGE the label.
                            // [ Label = Distance from starting vertex & Closest Neighbor ]

                            // Updating the distance should be different when the current working vertex
                            // is the starting vertex or not.
                            if(currentVertex != startingVertex)
                            {
                                // The shortest distance to this vertex will be the shortest distance to the
                                // current vertex plus the cost between the current vertex and this vertex
                                // (bc we need to store the entire shortest distance from the starting vertex)
                                shortestDistances[index] = 
                                    shortestDistances[currentVertex] + adjMatrix[currentVertex, index];
                            }
                            else
                            {
                                shortestDistances[index] = adjMatrix[startingVertex, index];
                            }
                            
                            pathNeighbor[index] = vertices[currentVertex];
                        }
                    }
                }

                // << ALL the ADJACENT & NON-PERMANENT vertices to the CURRENT working vertex has been CHECKED! >>

                // Determine the NEXT CURRENT working vertex
                // by finding a NON-PERMANENT vertex with the SMALLEST distance in the ENTIRE graph!
                Vertex possibleNextVertex;

                // If the helper method has returned a vertex
                if ((possibleNextVertex = GetShortestDistanceVertex(shortestDistances, permanent))!= null)
                {
                    // That becomes the next current working vertex
                    currentVertex = GetVertexIndex(possibleNextVertex.Name);

                    // And mark that as permanent!
                    permanent[currentVertex] = true;
                }
            }
        }


        /// <summary>
        /// Print the shortest path found by Dijkstra's Algorithm to the console window.
        /// </summary>
        /// <param name="destination">Destination room name</param>
        public void PrintShortestPath(string destination)
        {
            // ** BACKWARDS PRINTING (I think I will use a STACK if I were to print it FORWARD order)

            // Inform user about the cost of the path
            Console.WriteLine($"The shortest path (cost of {shortestDistances[GetVertexIndex(destination)]}) is:");

            // A local pointer variable to print out the path
            Vertex pointer = vertices[GetVertexIndex(destination)];

            // Until Path neigbor of this current pointer is null (prints until right before the starting room)
            while (pathNeighbor[GetVertexIndex(pointer.Name)] != null)
            {
                // Print out the room that the pointer is currently pointing
                Console.WriteLine($"  {pointer.Name}");

                // The pointer points to the neighbor of itself.
                pointer = pathNeighbor[GetVertexIndex(pointer.Name)];
            }

            // The starting room should be printed manually
            Console.WriteLine($"  {pointer.Name}");
        }

        //---------------------------------------------------------------
        // Helper Methods
        //---------------------------------------------------------------

        /// <summary>
        /// Returns 1 if it finds a relation between two vertices. 
        /// Returns 0 when found no relation.
        /// Usage for setting adjacency MATRIX for UNWEIGHTED Graphs ONLY.
        /// + HARDCODING(BLEH)
        /// </summary>
        /// <param name="vertex1">The index of a vertex</param>
        /// <param name="vertex2">The index of another vertex</param>
        /// <returns>0 or 1 that represents adjacency</returns>
        private int SetAdjacency(int vertex1, int vertex2)
        {
            // 0) If it is the same vertices, then return 0 early.
            if (vertex1 == vertex2)
            {
                return 0;
            }

            // !! Local variables to store the vertices object obtained by the index
            Vertex vertexOne = vertices[vertex1];
            Vertex vertexTwo = vertices[vertex2];

            // 1) Get the Adjacenecy List of vertex 1
            List<Vertex> bucket = GetAdjacentList(vertexOne.Name);

            // 2) Search through the bucket to determine
            //    whether vertex2 is adjacent to vertex1
            for (int i = 0; i < bucket.Count; i++)
            {
                if (bucket[i].Name == vertexTwo.Name)
                {
                    return 1;
                }
            }

            // 3) If the for loop ended, this implies that
            //    there isn't a relationship between vertex1 and 2.
            return 0;
        }


        /// <summary>
        /// Returns the index of the vertex that contains this room name.
        /// Returns -1(invalid index) if not found.
        /// </summary>
        /// <param name="roomName">Room name</param>
        /// <returns>The index of the vertex that contains this room name</returns>
        private int GetVertexIndex(string roomName)
        {
            for (int i = 0; i < vertices.Count; i++)
            {
                if (roomName == vertices[i].Name)
                {
                    return i;
                }
            }

            return -1;
        }


        /// <summary>
        /// Returns true when ALL vertices are PERMANENT.
        /// Returns false when one or more vertices are NON-PERMANENT.
        /// </summary>
        /// <param name="permanent"></param>
        /// <returns></returns>
        private bool CheckPermanency(bool[] permanent)
        {
            for(int i = 0; i < vertices.Count; i++)
            {
                // Even if one vertex that is NON-PERMANENT is found, return false.
                if (!permanent[i])
                {
                    return false;
                }
            }

            // If the for loop terminated, then it means that ALL vertices are permanent.
            return true;
        }


        /// <summary>
        /// Returns a list of non-permanent vertices to check.
        /// </summary>
        /// <param name="permanent">Whether the shortest distance of a vertex 
        /// from the starting vertex is finalized.</param>
        /// <returns>A list of non-permanenet vertices</returns>
        private List<Vertex> NonpermanentVertices(bool[] permanent)
        {
            // A local list of vertices to store nonpermanent ones
            List<Vertex> nonpermanents = new List<Vertex>();

            // Iterate through the permanent array.
            for (int i = 0; i < vertices.Count; i++)
            {
                // If a nonpermanent one is found, save this vertex into the nonpermanentVertices list.
                if (!permanent[i])
                {
                    nonpermanents.Add(vertices[i]);
                }
            }

            // Return the correctly populated list
            return nonpermanents;
        }


        /// <summary>
        /// From the NON-PERMANENT vertices, 
        /// returns the vertex with the SMALLEST distance in the ENTIRE graph.
        /// </summary>
        /// <param name="shortestDistance">Double array that stores all the 
        /// distance information of each vertex from the starting vertex</param>
        /// <param name="permanent">Whether the shortest distance of a vertex 
        /// from the starting vertex is finalized.</returns>
        private Vertex GetShortestDistanceVertex(double[] shortestDistance, bool[] permanent)
        {
            // A local integer variable to store the index of a NON-PERMANENT vertex that
            // has the shortest distance in the ENTIRE graph.
            int targetVertex = -1;

            // A local double variable to store the current shortest distance/weight
            double currentShortestDistance = double.MaxValue;

            // Iterate through the shortest Distance array
            for(int i = 0; i < vertices.Count; i++)
            {
                // ONLY IF this vertex is NON-PERMANENT
                if (!permanent[i])
                {
                    // Compare the shortest distance of the vertex from the starting vertex
                    // to the most current shortest distance
                    if (shortestDistance[i] < currentShortestDistance)
                    {
                        // Update the current shortest distance
                        currentShortestDistance = shortestDistance[i];

                        // Update the target Vertex (as this one could be the one)
                        targetVertex = i;
                    }
                }
            }

            // After the loop terminates, we have found the vertex
            // with the shortest distance in the entire graph! + NON-PERMANENT
            return vertices[targetVertex];
        }
    }
}
